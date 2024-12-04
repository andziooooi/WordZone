using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class QuizPVM : INotifyPropertyChanged
    {
        private MainWVM _mainViewModel;
        private string _tableName;
        private List<string> _tableNamesList;

        public ICommand StartQuizCommand { get; }
        public ICommand BackToMenuCommand { get; }
        public string TableName
        {
            get { return _tableName; }
            set
            {
                _tableName = value;
                OnPropertyChanged();
            }
        }
        public List<string> TableNamesList
        {
            get { return _tableNamesList; }
            set
            {
                _tableNamesList = value;
                OnPropertyChanged();
            }
        }
        private Dictionary<string, string> _dictionary;
        private DataService _dataService;

        public QuizPVM(DataService ds, MainWVM mainWVM)
        {
            _mainViewModel = mainWVM;
            _dataService = ds;

            TableNamesList = _dataService.GetTablesName();
            _dictionary = new Dictionary<string, string>();
            TableName = "Wybierz zbiór";

            StartQuizCommand = new RelayCommand(StartQuiz);
            BackToMenuCommand = new RelayCommand(BackToMenu);
        }
        private void BackToMenu(object obj)
        {
            _mainViewModel.Menu = Visibility.Visible;
            _mainViewModel.CurrentViewModel = null;
        }
        public Dictionary<string, string> Dictionary
        {
            get { return _dictionary; }
            set
            {
                _dictionary = value;
                OnPropertyChanged();
            }
        }
        private void StartQuiz(object obj)
        {
            if(TableName != "Wybierz zbiór")
            {
                Dictionary = _dataService.CreateDictionary(TableName);
            }
            
        }

 
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
