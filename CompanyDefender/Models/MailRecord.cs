﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class MailRecord
    {
        public int mail_key { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string full_name_from { get; set; }
        public string full_name_to { get; set; }
        public string from_mail_address { get; set; }
        public string to_mail_address { get; set; }
        public string topic { get; set; }
        public string body { get; set; }
    }
}