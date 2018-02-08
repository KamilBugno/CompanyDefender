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
                var idFrom = GetPersonId(mailRecord.from);
                var idTo = GetPersonId(mailRecord.to);

                mailsGraph.Add(new MailGraphViewModel(GetMailId(mailRecord.mail_key), idFrom, idTo));

                mailsTable.Add(new MailTableViewModel(GetMailId(mailRecord.mail_key), mailRecord.full_name_from, mailRecord.full_name_to,
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

        private int GetPersonId(string key)
        {
            return Int32.Parse(Regex.Replace(key, "HRSystem/", ""));
        }

        private string GetMailId(string key)
        {
            return Regex.Replace(key, "Mails/", "");
        }

    }
}