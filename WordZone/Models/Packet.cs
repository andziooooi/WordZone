using System.ComponentModel.DataAnnotations;

namespace WordZone.Models
{
    public class Packet
    {
        [Key]
        public int ID { get; set; }
        public string PacketName { get; set; }
        public virtual List<Translation> Translations { get; set; } = new();
    }
}
