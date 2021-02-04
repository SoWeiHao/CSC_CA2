using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class TalentImage
    {
        public string ImageName { set; get; }
        public string ImageExt { set; get; }
        public IFormFile MyImage { set; get; }
    }
}
