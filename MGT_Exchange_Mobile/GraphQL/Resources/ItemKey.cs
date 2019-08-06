using System;
using System.Collections.Generic;
using System.Text;

namespace MGT_Exchange_Client.GraphQL.Resources
{
    public class itemKey
    {
        public string tag { get; set; }
        public string value { get; set; }
        public itemKey(string _tag, string _value)
        {
            tag = _tag;
            value = _value;
        }
    }
}
