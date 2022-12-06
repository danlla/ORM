using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    [Table("driver")]
    public class Driver
    {
        [Key]
        [Column("id_driver")]
        public int IdDriver { get; set; }
        [Column("full_name")]
        public string? FullName { get; set; }
        [Column("passport")]
        public string? Passport { get; set; }
        [Column("license")]
        public string? License { get; set; }
        [Column("id_address")]
        public int AddressID { get; set; }
        public Address? Address { get; set; }
        [Column("phone_number")]
        public string? PhoneNumber { get; set; }
    }
}
