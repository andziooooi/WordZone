using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
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
        private List<int> ListOfBad;
        private int _currentIndex;
        private int _currentIndexDisp;
        private string _keyVal;
        private string _dicVal;
        private string _userVal;
        private string _qbutton;
        private int _numberofelements;
        private Dictionary<string, string> _dictionary;
        private Dictionary<string, string> _dictionarybad;
        private DataService _dataService;
        private Visibility _qVis;
        private Visibility _qmVis;
        private Visibility _qresVis;
        private Visibility _qendbutvis;
        private int _points;
        private Dictionary<string, string> _dictionarygood;


        public ICommand StartQuizCommand { get; }
        public ICommand BackToMenuCommand { get; }
        public ICommand NextQuestionCommand { get; }
        public ICommand EndQuizCommand { get; }
        public ICommand ReDoCommand { get; }
        public ICommand NewPackCommand {  get; }
        public Visibility QVis
        {
            get { return _qVis; }
            set
            {
                _qVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility QResVis
        {
            get { return _qresVis; }
            set
            {
                _qresVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility QMVis
        {
            get { return _qmVis; }
            set
            {
                _qmVis = value;
                OnPropertyChanged();
            }
        }
        public Visibility QEndButVis
        {
            get { return _qendbutvis; }
            set
            {
                _qendbutvis = value;
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
        public string QButton
        {
            get { return _qbutton; }
            set
            {
                _qbutton = value;
                OnPropertyChanged();
            }
        }
        public int CurrentIndexDisp
        {
            get { return _currentIndexDisp; }
            set
            {
                _currentIndexDisp = value;
                OnPropertyChanged();
            }
        }
        public int NumberOfElements
        {
            get { return _numberofelements; }
            set
            {
                _numberofelements = value;
                OnPropertyChanged();
            }
        }

        public string KeyVal
        {
            get { return _keyVal; }
            set
            {
                _keyVal = value;
                OnPropertyChanged();
            }
        }
        public string DicVal
        {
            get { return _dicVal; }
            set
            {
                _dicVal = value;
                OnPropertyChanged();
            }
        }
        public string UserVal
        {
            get { return _userVal; }
            set
            {
                _userVal = value;
                OnPropertyChanged();
            }
        }
        public int Points
        {
            get { return _points; }
            set
            {
                _points = value;
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
        public Dictionary<string, string> Dictionary
        {
            get { return _dictionary; }
            set
            {
                _dictionary = value;
                OnPropertyChanged();
            }
        }
        public Dictionary<string, string> DictionaryBad
        {
            get { return _dictionarybad; }
            set
            {
                _dictionarybad = value;
                OnPropertyChanged();
            }
        }
        public Dictionary<string, string> DictionaryGood
        {
            get { return _dictionarygood; }
            set
            {
                _dictionarygood = value;
                OnPropertyChanged();
            }
        }


        public QuizPVM(DataService ds, MainWVM mainWVM)
        {
            _mainViewModel = mainWVM;
            _dataService = ds;
            _qmVis = Visibility.Visible;
            _qVis = Visibility.Hidden;
            _qresVis = Visibility.Hidden;
            _qendbutvis = Visibility.Visible;
            _keyVal = "";
            _dicVal = "";
            _userVal = "";
            _qbutton = "Następne pytanie";
            _currentIndex = 0;
            _currentIndexDisp = 1;
            _numberofelements = 0;
            _points = 0;
            ListOfBad = new List<int>();


            _tableNamesList = _dataService.GetTablesName();
            _dictionary = new Dictionary<string, string>();
            _dictionarybad = new Dictionary<string, string>();
            _dictionarygood= new Dictionary<string, string>();
            _tableName = "Wybierz zbiór";

            //commands
            StartQuizCommand = new RelayCommand(StartQuiz);
            BackToMenuCommand = new RelayCommand(BackToMenu);
            NextQuestionCommand = new RelayCommand(NextQuestion);
            EndQuizCommand = new RelayCommand(EndQuiz);
            ReDoCommand = new RelayCommand(ReDo);
            NewPackCommand = new RelayCommand(NewPack);
        }
        private void BackToMenu(object obj)
        {
            _mainViewModel.CurrentViewModel = null;
        }

        private void StartQuiz(object obj)
        {
            if (TableName != "Wybierz zbiór")
            {
                Dictionary = _dataService.CreateDictionary(TableName);
                NumberOfElements = Dictionary.Count;
                QVis = Visibility.Visible;
                QMVis = Visibility.Hidden;
                KeyVal = Dictionary.ElementAt(_currentIndex).Key;
                DicVal = Dictionary.ElementAt(_currentIndex).Value;
            }
        }
        private void NextQuestion(object obj)
        {
            if (DicVal.ToLower() != UserVal.ToLower())
            {
                ListOfBad.Add(_currentIndex);
            }
            else
            {
                Points++;
            }
            UserVal = "";
            _currentIndex++;
            CurrentIndexDisp++;
            if (_currentIndex < _numberofelements -1)
            {
                KeyVal = Dictionary.ElementAt(_currentIndex).Key;
                DicVal = Dictionary.ElementAt(_currentIndex).Value;
            }
            else if (_currentIndex == _numberofelements - 1)
            {
                KeyVal = Dictionary.ElementAt(_currentIndex).Key;
                DicVal = Dictionary.ElementAt(_currentIndex).Value;
                QEndButVis = Visibility.Hidden;
                QButton = "Zakończ Quiz";
            }
            else
            {
                QVis = Visibility.Hidden;
                QMVis= Visibility.Hidden;
                Results();
                QResVis = Visibility.Visible;

            }
        }

        private void EndQuiz(object obj)
        {
            if (DicVal.ToLower() == UserVal.ToLower())
            {
                Points++;
                _currentIndex++;
            }
            while (_currentIndex <= _numberofelements-1)
            {
                ListOfBad.Add( _currentIndex);
                _currentIndex++;
            }

            QVis = Visibility.Hidden;
            QMVis= Visibility.Hidden;
            Results();
            QResVis = Visibility.Visible;
        }

        private void ReDo(object obj) 
        {
            var tnhelp = TableName;
            ReInitialize();
            _tableName = tnhelp;
            StartQuiz(null);
        }
        private void NewPack(object obj)
        {
            ReInitialize();
        }
        private void ReInitialize()
        {
            QMVis = Visibility.Visible;
            QVis = Visibility.Hidden;
            QResVis = Visibility.Hidden;
            QEndButVis = Visibility.Visible;
            KeyVal = "";
            DicVal = "";
            UserVal = "";
            QButton = "Następne pytanie";
            _currentIndex = 0;
            CurrentIndexDisp = 1;
            NumberOfElements = 0;
            Points = 0;
            ListOfBad = new List<int>();


            TableNamesList = _dataService.GetTablesName();
            Dictionary = new Dictionary<string, string>();
            DictionaryBad = new Dictionary<string, string>();
            DictionaryGood= new Dictionary<string, string>();
            TableName = "Wybierz zbiór";

        }

        private void Results()
        {
            Dictionary<string, string> helper = new Dictionary<string, string>();
            foreach (var item in ListOfBad)
            {
                var KeyToAdd = Dictionary.Keys.ElementAt(item);
                var ValToAdd = Dictionary[KeyToAdd];
                helper.Add(KeyToAdd, ValToAdd);
            }
            DictionaryBad = helper;

            foreach (KeyValuePair<string, string> h in DictionaryBad)
            {
                if (Dictionary.ContainsKey(h.Key))
                {
                    Dictionary.Remove(h.Key);
                    OnPropertyChanged(nameof(Dictionary));
                }
            }
            DictionaryGood = Dictionary;

        }


        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
