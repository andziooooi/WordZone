
using System.ComponentModel.DataAnnotations;

namespace WordZone.Models
{
    public class Translation
    {
        [Key]
        public int ID { get; set; }
        public string EnglishWord { get; set; }
        public string PolishTranslation { get; set; }
        public int PacketID { get; set; }
        public Packet Packet { get; set; }  // Nawigacja do Packet
    }
}
