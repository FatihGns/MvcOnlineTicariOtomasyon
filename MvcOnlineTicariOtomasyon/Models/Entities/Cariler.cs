using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class Cariler
    {
        [Key]
        public int CariID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        [Required(ErrorMessage = "Cari adı en fazla 30 karakter olabilir.")]
        public string CariName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(40)]
        [Required(ErrorMessage = "Cari Soyadı en fazla 40 karakter olabilir.")]

        public string CariSurname { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        [Required(ErrorMessage = "Cari Şehir en fazla 13 karakter olabilir.")]
        public string CariSehir { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        [Required(ErrorMessage = "Cari Mail en fazla 50 karakter olabilir..")]
        public string CariMail { get; set; }
        public bool Status { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string CariSifre { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }

    }
}