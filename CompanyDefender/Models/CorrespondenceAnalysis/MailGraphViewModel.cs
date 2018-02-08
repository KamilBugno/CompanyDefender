﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class MailGraphViewModel
    {
        public string Arrows;
        public int From;
        public int To;
        public string Label;

        public MailGraphViewModel(string label, int from, int to)
        {
            Arrows = "To";
            From = from;
            To = to;
            Label = label;
        }
        //...
    }
}