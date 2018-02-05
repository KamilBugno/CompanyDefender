using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class PersonMailGraphViewModel
    {
        public List<MailGraph> Mails;
        public List<PersonMailGraph> PersonsMails;

        public PersonMailGraphViewModel(List<MailGraph> mails, List<PersonMailGraph> personsMails)
        {
            Mails = mails;
            PersonsMails = personsMails;
        }
    }
}