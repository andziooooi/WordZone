using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Services;

namespace WordZone.ViewModels
{
    class MainWVM : INotifyPropertyChanged
    {
        public ICommand StartQuizCommand { get;}
        public ICommand MakePacketCommand { get;}
        private string _tableName;
        private Dictionary<string, string> _dictionary;
        private DataService _dataService;
        public string TableName
        {
            get { return _tableName; }
            set
            {
                _tableName = value;
                OnPropertyChanged();
            }
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

        public MainWVM(DataService dataService)
        {
            _dataService = dataService;
            StartQuizCommand = new RelayCommand(StartQuiz);
            MakePacketCommand = new RelayCommand(MakePacket);

            _dictionary = new Dictionary<string, string>();
        }

        private void MakePacket(object obj)
        {
            if (!string.IsNullOrEmpty(TableName))
            {
                _dataService.CreateTable(TableName);
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
