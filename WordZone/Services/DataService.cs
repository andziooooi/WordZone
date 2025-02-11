
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using WordZone.Models;

namespace WordZone.Services
{
    public class DataService
    {
        private TranslationContext _context;
        public DataService(TranslationContext dbContext)
        {
            _context = dbContext;
        }
        public void AddTranslations(string packetName, IEnumerable<Translation> translations)
        {
            if (string.IsNullOrEmpty(packetName) || translations == null)
            {
                return;
            }

            var packet = _context.Packets.Include(p => p.Translations)
                          .FirstOrDefault(p => p.PacketName == packetName);

            if (packet == null)
            {
                packet = new Packet { PacketName = packetName, Translations = new List<Translation>() };
                _context.Packets.Add(packet);
            }

            var validTranslations = translations
                .Where(t => !string.IsNullOrEmpty(t.PolishTranslation) && !string.IsNullOrEmpty(t.EnglishWord))
                .ToList();

            packet.Translations.AddRange(validTranslations);
            _context.SaveChanges();
        }
        public List<Translation> GetTranslations(string packetName)
        {
            if (!string.IsNullOrEmpty(packetName))
            {
                var packet =  _context.Packets.Where(packet =>  packet.PacketName == packetName).FirstOrDefault();
                if (packet == null) { return null!; }
                var translations = _context.Translations.Where(item => item.PacketID == packet.ID).ToList();
                return translations;
            }
            else
            {
                List<Translation> er = new List<Translation>();
                return er;
            }
        }
        public Dictionary<string,string> CreateDictionary(string packetName,bool PolEngCB)
        {

            if (!string.IsNullOrEmpty(packetName))
            {
                if (PolEngCB) 
                {
                    var translations = GetTranslations(packetName);
                    Dictionary<string, string> Dictionary = translations.ToDictionary(t => t.PolishTranslation, t => t.EnglishWord);
                    return Dictionary;
                }
                else
                {
                    var translations = GetTranslations(packetName);
                    Dictionary<string, string> Dictionary = translations.ToDictionary(t => t.EnglishWord, t => t.PolishTranslation);
                    return Dictionary;
                }

            }
            else
            {
                Dictionary<string,string> error = new Dictionary<string,string>();
                error.Add("Error", "wystąpił error");
                return error;
            }
            
        }
        public List<string> GetPacketsNames()
        {
            var packets = _context.Packets.ToList();
            var res = new List<string>();
            foreach(var packet in packets)
            {
                res.Add(packet.PacketName);
            }            
            return res;
        }

        public void UpdatePacket(ObservableCollection<Translation> translations, string packetName)
        {
            if (!string.IsNullOrEmpty(packetName))
            {
                var packet = _context.Packets.FirstOrDefault(item => item.PacketName == packetName);
                if (packet is null) { return; }

                var translationsToRemove = _context.Translations.Where(item => item.PacketID == packet.ID);
                if (!translationsToRemove.Any()) { return; }

                _context.Translations.RemoveRange(translationsToRemove);

                var toInsert = translations.Select(item => new Translation
                {
                    EnglishWord = item.EnglishWord,
                    PolishTranslation = item.PolishTranslation,
                    PacketID = packet.ID
                }).Where(item => item.EnglishWord != null && item.PolishTranslation !=null);
                if (!toInsert.Any()) { return; }

                _context.Translations.AddRange(toInsert);
                _context.SaveChanges();
            }
        }
        public void DeleteRow(int translationID)
        {
            var ToRemove = _context.Translations.FirstOrDefault(item => item.ID == translationID);
            if (ToRemove is null) {  return; }

            _context.Translations.Remove(ToRemove);
        }
        public void DeletePacket(string packetName)
        {
            var packet = _context.Packets.FirstOrDefault(item => item.PacketName == packetName);
            if (packet is null) { return; }

            var translationsToDelete = _context.Translations.Where(item => item.PacketID == packet.ID);
            if (!translationsToDelete.Any()) { return; }

            _context.Translations.RemoveRange(translationsToDelete);
            _context.Packets.Remove(packet);
            _context.SaveChanges();
        }
    }
}
