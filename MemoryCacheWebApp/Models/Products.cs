using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MemoryCacheWebApp.Models
{

    [Table("Products")]
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Required Field")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required Field")]
        public string ProductNumber { get; set; }
    }
}
