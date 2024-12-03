using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class QuizPVM : INotifyPropertyChanged
    {
        private string _tableName;

        public ICommand StartQuizCommand { get; }
        public string TableName
        {
            get { return _tableName; }
            set
            {
                _tableName = value;
                OnPropertyChanged();
            }
        }
        private Dictionary<string, string> _dictionary;
        private DataService _dataService;

        public QuizPVM(DataService ds)
        {
            _dataService = ds;
            StartQuizCommand = new RelayCommand(StartQuiz);
            _dictionary = new Dictionary<string, string>();
            _tableName = "siema";
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
            Dictionary = _dataService.CreateDictionary(TableName);
        }

 
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
