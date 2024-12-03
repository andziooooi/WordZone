﻿
using Microsoft.EntityFrameworkCore;
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
        public void CreateTable(string tableName,List<Translation> translations)
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
        public Dictionary<string,string> CreateDictionary(string tableName)
        {

            if (!string.IsNullOrEmpty(tableName))
            {
                var translations = _context.Translations.FromSqlRaw($"SELECT * FROM {tableName}").AsNoTracking().ToList();
                Dictionary<string, string> Dictionary = translations.ToDictionary(t => t.EnglishWord, t => t.PolishTranslation);
                return Dictionary;
            }
            else
            {
                Dictionary<string,string> error = new Dictionary<string,string>();
                error.Add("Error", "wystąpił error");
                return error;
            }
            
        }
    }
}
