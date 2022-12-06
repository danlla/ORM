using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    [Table("transport")]
    public class Transport
    {
        [Key]
        [Column("id_transport")]
        public int IdTransport { get; set; }
        [Column("government_number")]
        public string? GovernmentNumber { get; set; }
        [Column("id_type_transport")]
        public int TransportTypeId { get; set; }
        public TypeTransport? TypeTransport { get; set; }
        [Column("id_model")]
        public int ModelId { get; set; }
        public Model? Model { get; set; }
        [Column("year_of_manufacture")]
        public DateTime? YearOFManufacture { get; set; }

    }
}
