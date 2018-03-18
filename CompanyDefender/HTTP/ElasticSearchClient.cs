using CompanyDefender.Constant;
using CompanyDefender.Models.CorrespondenceAnalysis;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.HTTP
{
    public class ElasticSearchClient 
    {
        private ElasticClient client;

        public ElasticSearchClient()
        {
            var settings = new ConnectionSettings(new Uri(ApplicationConstant.urlElasticSearchService))
            .DefaultIndex(ApplicationConstant.elastcSearchIndex);

            client = new ElasticClient(settings);
        }

        public void Query(string query)
        {
            var searchResponse = client.Search<_doc>(s => s
                .Query(q => q
                     .MultiMatch(m => m
                        .Query(query)
                        .Fields(Infer.Field<_doc>(p => p.body)
                            .And(Infer.Field<_doc>(p => p.topic))
                            .And(Infer.Field<_doc>(p => p.attachment)))
                        .Fuzziness(Fuzziness.EditDistance(2))
                    )   
                )
                .MinScore(0.5)
            );

            var people = searchResponse.Documents;
            var a = 0;
        }
    }
}