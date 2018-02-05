using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class PersonMailFullViewModel
    {
        public List<MailGraphViewModel> MailsGraph;
        public List<PersonGraphViewModel> PersonsGraph;
        public List<MailTableViewModel> MailsTable;

        public PersonMailFullViewModel(List<MailGraphViewModel> mails, List<PersonGraphViewModel> personsMails,
            List<MailTableViewModel> personViewModel)
        {
            MailsGraph = mails;
            PersonsGraph = personsMails;
            MailsTable = personViewModel;
        }
    }
}