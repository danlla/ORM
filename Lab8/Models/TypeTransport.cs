using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    [Table("type_transport")]
    public class TypeTransport
    {
        [Key]
        [Column("id_type_transport")]
        public int IdTypeTransport { get; set; }
        [Column("name")]
        public string? Name { get; set; }
    }
}
