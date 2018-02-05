using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class MailGraph
    {
        public string Arrows;
        public int From;
        public int To;

        public MailGraph(int from, int to)
        {
            Arrows = "To";
            From = from;
            To = to;
        }
        //...
    }
}