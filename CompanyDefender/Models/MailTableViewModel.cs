﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class MailTableViewModel
    {
        public string Id;
        public string FullNameFrom;
        public string FullNameTo;
        public string Topic;
        public string Body;

        public MailTableViewModel(string id, string fullNameFrom, string fullNameTo, string topic, string body)
        {
            Id = id;
            FullNameFrom = fullNameFrom;
            FullNameTo = fullNameTo;
            Topic = topic;
            Body = body;
        }


    }
}