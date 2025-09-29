using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class SatisHareket
    {
        [Key]
        public int SatisID { get; set; }
        //ÜRÜN
        public int ProductID { get; set; }
        public virtual Product Products { get; set; }

        //CARİ
        public int CariID { get; set; }
        public virtual Cariler Carilers { get; set; }
        //PERSONEL
        public int EmployeeID { get; set; }
        public virtual Employee Employees { get; set; }
        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamTutar { get; set; }
    }
}