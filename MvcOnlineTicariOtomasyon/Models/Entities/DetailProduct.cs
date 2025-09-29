using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class DetailProduct
    {
        public IEnumerable<Product> Deger1 { get; set;}
        public IEnumerable<Detail> Deger2{ get; set;}
    }

}