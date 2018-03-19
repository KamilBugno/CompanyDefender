using CompanyDefender.Constant;
using CompanyDefender.Models;
using CompanyDefender.Models.CorrespondenceAnalysis;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.HTTP
{
    public class ElasticSearchClient 
    {
        private ElasticClient elasticClient;
        private RESTfulClient restClient;
        private CorrespondenceAnalysisVMCreator personMailGraphVMCreator;

        public ElasticSearchClient(CorrespondenceAnalysisVMCreator personMailGraphVMCreator, RESTfulClient restClient)
        {
            var settings = new ConnectionSettings(new Uri(ApplicationConstant.urlElasticSearchService))
            .DefaultIndex(ApplicationConstant.elastcSearchIndex);

            elasticClient = new ElasticClient(settings);
            this.restClient = restClient;
            this.personMailGraphVMCreator = personMailGraphVMCreator;
        }

        public PersonMailFullViewModel Search(string query, string startDate, string endDate)
        {
            var searchResponse = elasticClient.Search<_doc>(s => s
                .Query(q => q
                     .Bool(b => b
                        .Must(a => a
                            .MultiMatch(m => m
                                .Query(query)
                                .Fields(Infer.Field<_doc>(p => p.body)
                                    .And(Infer.Field<_doc>(p => p.topic))
                                    .And(Infer.Field<_doc>(p => p.attachment)))
                                .Fuzziness(Fuzziness.EditDistance(2))))
                        .Filter(f => f
                            .DateRange(ra => ra
                                .Field(p => p.date)
                                .Format("yyyy-MM-dd")
                                .GreaterThanOrEquals(startDate)
                                .LessThanOrEquals(endDate)
                                ))
                    )   
                )
                .MinScore(0.5)
            );

            var mails = new List<MailRecord>();

            foreach (_doc searchedMail in searchResponse.Documents)
            {
                var jsonResponse = restClient.GetMailByKey(searchedMail.key);
                var mailRecord = JsonConvert.DeserializeObject<List<MailRecord>>(jsonResponse)[0];
                mails.Add(mailRecord);
            }

            return personMailGraphVMCreator.CreateFromMailRecords(mails);
        }
    }
}