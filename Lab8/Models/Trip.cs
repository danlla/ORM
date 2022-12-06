using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    [Table("trip")]
    public class Trip
    {
        [Key]
        [Column("id_trip")]
        public int IdTrip { get; set; }
        [Column("id_driver")]
        public int DriverId { get; set; }
        public Driver? Driver { get; set; }
        [Column("route")]
        public int Route { get; set; }
        [Column("id_transport")]
        public int TransportId { get; set; }
        public Transport? Transport { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
    }
}
