using CompanyDefender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CompanyDefender
{
    public class PersonMailGraphVMCreator
    {
        public PersonMailFullViewModel CreateFromMailRecords(List<MailRecord> mailsFromRest, string query = null)
        {
            var mailsGraph = new List<MailGraphViewModel>();
            var personsGraph = new List<PersonGraphViewModel>();
            var mailsTable = new List<MailTableViewModel>();
            foreach (MailRecord mailRecord in mailsFromRest)
            {
                var idFrom = GetId(mailRecord.from);
                var idTo = GetId(mailRecord.to);

                mailsGraph.Add(new MailGraphViewModel(idFrom, idTo));

                mailsTable.Add(new MailTableViewModel(mailRecord.full_name_from, mailRecord.full_name_to,
                    mailRecord.topic, mailRecord.body));

                if (!personsGraph.Exists(person => person.Id == idFrom))
                {
                    personsGraph.Add(new PersonGraphViewModel(idFrom, mailRecord.full_name_from));
                }
                if (!personsGraph.Exists(person => person.Id == idTo))
                {
                    personsGraph.Add(new PersonGraphViewModel(idTo, mailRecord.full_name_to));
                }
            }

            return new PersonMailFullViewModel(mailsGraph, personsGraph, mailsTable, query);
        }

        private int GetId(string key)
        {
            return Int32.Parse(Regex.Replace(key, "HRSystem/", ""));
        }

    }
}