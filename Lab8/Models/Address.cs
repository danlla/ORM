using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    [Table("address")]
    public class Address
    {
        [Key]
        [Column("id_address")]
        public int IdAddress { get; set; }
        [Column("city")]
        public string? City { get; set; }
        [Column("street")]
        public string? Street { get; set; }
        [Column("house")]
        public string? House { get; set; }
    }
}
