using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FreeShare.Models
{
    // Models represent any table we want in the database

    public class Data
    {
        // This will create an Id value automatically, we do not need to pass a value
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // This means Name cannot be null
        [Required]
        public string Name { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public virtual Category CId { get; set; }
    }

    public class Category
    {
        [Key]
        public string NameToken { get; set; }
        public string Description { get; set; }
        public ICollection<Data> Data { get; set; }
        public virtual CategoryType TypeId { get; set; }
    }

    public class CategoryType
    {


        public ICollection<Category> Type { get; set; }
        [Key]
        public string Name { get; set; }

    }

}