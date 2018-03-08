using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class PersonMailFullViewModel
    {
        public List<MailGraphViewModel> MailsGraph { get; set; }
        public List<PersonGraphViewModel> PersonsGraph { get; set; }
        public List<MailTableViewModel> MailsTable { get; set; }
        public string Query { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public PersonMailFullViewModel()
        {
           
        }

        public PersonMailFullViewModel(List<MailGraphViewModel> mails, List<PersonGraphViewModel> personsMails,
            List<MailTableViewModel> mailsTable, string query)
        {
            MailsGraph = mails;
            PersonsGraph = personsMails;
            MailsTable = (from mail in mailsTable
                         orderby Int32.Parse(mail.Id)
                         select mail).ToList();
            Query = query;
        }
    }
}