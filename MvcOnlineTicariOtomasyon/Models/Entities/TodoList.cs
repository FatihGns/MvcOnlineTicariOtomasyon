using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Entities
{
    public class TodoList
    {
        [Key]
        public int TodoListID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Title { get; set; }
        [Column(TypeName = "Bit")]
        public bool Status { get; set; }
    }
}