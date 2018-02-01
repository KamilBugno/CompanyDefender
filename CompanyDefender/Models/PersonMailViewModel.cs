using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class PersonMailViewModel
    {
        public List<Mail> Mails;
        public List<PersonMail> PersonsMails;

        public PersonMailViewModel(List<Mail> mails, List<PersonMail> personsMails)
        {
            Mails = mails;
            PersonsMails = personsMails;
        }
    }
}