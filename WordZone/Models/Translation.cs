
namespace WordZone.Models
{
    public class Translation
    {
        public int Id { get; set; }
        public string EnglishWord { get; set; }
        public string PolishTranslation { get; set; }
        public Translation(string englishWord, string polishTranslation)
        {
            EnglishWord = englishWord;
            PolishTranslation = polishTranslation;
        }
    }
}
