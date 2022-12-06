using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab8.Models
{
    [Table("model")]
    public class Model
    {
        [Key]
        [Column("id_model")]
        public int IdModel { get; set; }
        [Column("title")]
        public string? Title { get; set; }
        [Column("low_floor")]
        public bool LowFloor { get; set; }
        [Column("max_capacity")]
        public int MaxCapacity { get; set; }
    }
}
