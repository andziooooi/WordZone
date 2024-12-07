
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WordZone.Models
{
    [Keyless]
    public class Translation
    {
        public string EnglishWord { get; set; }
        public string PolishTranslation { get; set; }
        public Translation() { }
        public Translation(string englishWord, string polishTranslation)
        {
            EnglishWord = englishWord;
            PolishTranslation = polishTranslation;
        }
    }
}
