using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReadHTML
{
    public class Markup
    {
        public string NodeType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public List<Markup> Children { get; set; }

        public Dictionary<string, string> Attributes { get; set; }

    }
}
