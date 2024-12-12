using System.Windows;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class FlashCardsPVM : BaseVM
    {
        private string _tableName;
        private List<string> _tableNamesList;
        private DataService _dataService;
        private Dictionary<string, string> _words;
        private string _fcValue;
        private int _index;
        private int _numberofwords;
        private Visibility _prevVis;
        private Visibility _nextVis;

        public ICommand StartLearningCommand { get; }
        public ICommand FCChangeCommand { get; }
        public ICommand NextFCCommand { get; }
        public ICommand PreviousFCCommand { get; }

        public Dictionary<string, string> Words
        {
            get { return _words; }
            set
            {
                _words = value;
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
        public string FCValue
        {
            get { return _fcValue; }
            set
            {
                _fcValue = value;
                OnPropertyChanged();
            }
        }
        public string TableName
        {
            get { return _tableName; }
            set
            {
                _tableName = value;
                OnPropertyChanged();
            }
        }
        public Visibility PrevVis
        {
            get { return _prevVis; }
            set
            {
                _prevVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility NextVis
        {
            get { return _nextVis; }
            set
            {
                _nextVis = value;
                OnPropertyChanged();
            }
        }
        public FlashCardsPVM(DataService ds)
        {
            _dataService = ds;
            _tableNamesList = _dataService.GetTablesName();
            _words = new Dictionary<string, string>();
            _tableName = "Wybierz zbiór";
            _fcValue = "";
            _nextVis = Visibility.Hidden;
            _prevVis = Visibility.Hidden;
            _numberofwords = 0;

            StartLearningCommand = new RelayCommand(StartLearning);
            FCChangeCommand = new RelayCommand(FCChange);
            NextFCCommand = new RelayCommand(NextFC);
            PreviousFCCommand = new RelayCommand(PreviousFC);

        }

        private void StartLearning(object obj)
        {
            _index = 0;
            Words = _dataService.CreateDictionary(TableName);
            FCValue = Words.ElementAt(0).Key;
            _numberofwords = Words.Count;
            NextVis = Visibility.Visible;
        }
        private void FCChange(object obj)
        {
            if (FCValue == Words.ElementAt(_index).Key) 
            {
                FCValue = Words.ElementAt(_index).Value;
            }
            else
            {
                FCValue = Words.ElementAt(_index).Key;
            }
        }
        private void NextFC(object obj)
        {
            _index++;
            PrevVis = Visibility.Visible;
            if(_numberofwords-1 <= _index)
            {
                NextVis = Visibility.Hidden;
            }
            FCValue = Words.ElementAt(_index).Key;
        }
        private void PreviousFC(object obj) 
        {
            _index--;
            NextVis = Visibility.Visible;
            if (_index == 0)
            {
                PrevVis = Visibility.Hidden;
            }
            FCValue = Words.ElementAt(_index).Key;
        }

    }
}

