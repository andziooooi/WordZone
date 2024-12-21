
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
        public void CreateTable(string tableName,ObservableCollection<Translation> translations)
        {
            string sql = $@"
            CREATE TABLE IF NOT EXISTS [{tableName}] (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                EnglishWord TEXT NOT NULL,
                PolishTranslation TEXT NOT NULL
            )";

            _context.Database.ExecuteSqlRaw(sql);
            foreach (var translation in translations)
            {
                string sqlcommand = $@"INSERT INTO [{tableName}] (EnglishWord, PolishTranslation) VALUES ('{translation.EnglishWord}','{translation.PolishTranslation}')";
                _context.Database.ExecuteSqlRaw(sqlcommand);
            }
        }
        public List<Translation> GetTranslations(string tableName)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                var translations = _context.Translations.FromSqlRaw($"SELECT * FROM {tableName}").AsNoTracking().ToList();
                return translations;
            }
            else
            {
                List<Translation> er = new List<Translation>();
                er.Add(new Translation("error", "error"));
                return er;
            }
        }
        public Dictionary<string,string> CreateDictionary(string tableName,bool PolEngCB)
        {

            if (!string.IsNullOrEmpty(tableName))
            {
                if (PolEngCB) 
                {
                    var translations = GetTranslations(tableName);
                    Dictionary<string, string> Dictionary = translations.ToDictionary(t => t.PolishTranslation, t => t.EnglishWord);
                    return Dictionary;
                }
                else
                {
                    var translations = GetTranslations(tableName);
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
        public List<string> GetTablesName()
        {
            string sql = @"SELECT name from sqlite_master where type ='table'";
            var tables = _context.TableNames.FromSqlRaw(sql).AsNoTracking().ToList();
            tables.RemoveAt(0);
            List<string> tablesName = new List<string>();
            tablesName.Add("Wybierz zbiór");
            foreach (var table in tables) 
            {
                tablesName.Add(table.Name);
            }
            
            return tablesName;
        }

        public void UpdateTable(ObservableCollection<Translation> translation, string tableName,int initialvalue)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                string sql = "";
                for (int i = 0; i < translation.Count; i++)
                {
                    if(i<initialvalue)
                    {
                        sql = $@"UPDATE {tableName} SET EnglishWord = '{translation[i].EnglishWord}', PolishTranslation = '{translation[i].PolishTranslation}' WHERE Id = {translation[i].Id};";
                        _context.Database.ExecuteSqlRaw(sql);
                    }
                    else if (!string.IsNullOrEmpty(translation[i].EnglishWord))
                    {
                         sql = $@"INSERT INTO [{tableName}] (EnglishWord, PolishTranslation) VALUES ('{translation[i].EnglishWord}','{translation[i].PolishTranslation}')";
                        _context.Database.ExecuteSqlRaw(sql);
                    }
                }
            }
        }
        public void DeleteRow(string eng, string tableName)
        {
            string sql = $@"DELETE FROM {tableName} where EnglishWord = '{eng}'";
            _context.Database.ExecuteSqlRaw(sql);
        }
        public void DropTable(string tableName)
        {
            string sql = $@"DROP TABLE {tableName}";
            _context.Database.ExecuteSqlRaw(sql);
        }
        public int CountRows(string tableName)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                string sql = $@"select count(*) from {tableName}";
                int result = Convert.ToInt32(_context.Database.ExecuteSqlRaw(sql));
                return result;
            }
            else
            {
                return 0;
            }
        }
    }
}
