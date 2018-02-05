using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class MailTableViewModel
    {
        public string FullNameFrom;
        public string FullNameTo;
        public string Topic;
        public string Body;

        public MailTableViewModel(string fullNameFrom, string fullNameTo, string topic, string body)
        {
            FullNameFrom = fullNameFrom;
            FullNameTo = fullNameTo;
            Topic = topic;
            Body = body;
        }


    }
}