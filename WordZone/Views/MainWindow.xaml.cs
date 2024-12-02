using Microsoft.EntityFrameworkCore;
using System.Windows;
using WordZone.Models;
using WordZone.Services;
using WordZone.ViewModels;

namespace WordZone
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            TranslationContext dbContext = new TranslationContext(new DbContextOptionsBuilder<TranslationContext>().UseSqlite("Data Source=translations.db").Options);
            var dataService = new DataService(dbContext);
            InitializeComponent();
            DataContext = new MainWVM(dataService);
        }
    }
}
