using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class TestObject
    {
        public TestObject(string p, string b, string v)
        {
            platform = p;
            browserName = b;
            version = v;
        }
        public string platform { get; set; }
        public string browserName { get; set; }
        public string version { get; set; }
    }
}
