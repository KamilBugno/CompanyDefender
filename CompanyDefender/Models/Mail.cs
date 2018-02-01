using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CompanyDefender.Models
{
    public class Mail
    {
        public string Arrows;
        public int From;
        public int To;

        public Mail(int from, int to)
        {
            Arrows = "To";
            From = from;
            To = to;
        }
        //...
    }
}