using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Dtos
{
    public class MesajDto
    {
        public string Name { get; set; }
        public string Mesaj { get; set; }
        public DateTime Tarih { get; set; }
    }
}