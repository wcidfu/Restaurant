using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Entities
{
    [Table("product")]
    public class ProductEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("category")]
        public string Category { get; set; } 

        [Column("price")]
        public decimal Price { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }
    }
}
