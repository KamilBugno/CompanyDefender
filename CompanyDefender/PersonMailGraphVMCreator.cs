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
        public PersonMailGraphViewModel CreateFromMailRecords(List<MailRecord> mailsFromRest)
        {
            var mails = new List<MailGraph>();
            var persons = new List<PersonMailGraph>();
            foreach (MailRecord mailRecord in mailsFromRest)
            {
                var idFrom = GetId(mailRecord.from);
                var idTo = GetId(mailRecord.to);
                mails.Add(new MailGraph(idFrom, idTo));

                if (!persons.Exists(person => person.Id == idFrom))
                {
                    persons.Add(new PersonMailGraph(idFrom, mailRecord.full_name_from));
                }
                if (!persons.Exists(person => person.Id == idTo))
                {
                    persons.Add(new PersonMailGraph(idTo, mailRecord.full_name_to));
                }
            }

            return new PersonMailGraphViewModel(mails, persons);
        }

        private int GetId(string key)
        {
            return Int32.Parse(Regex.Replace(key, "HRSystem/", ""));
        }

    }
}