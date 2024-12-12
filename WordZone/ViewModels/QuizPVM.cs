using System.Windows;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class QuizPVM : BaseVM
    {
        private MainWVM _mainViewModel;
        private string _tableName;
        private List<int> ListOfBad;
        private int _currentIndex;
        private int _currentIndexDisp;
        private string _keyVal;
        private string _dicVal;
        private string _userVal;
        private string _qbutton;
        private int _numberofelements;
        private Dictionary<string, string> _dictionary;
        private DataService _dataService;
        private int _points;
        private Visibility _qEndButVis;


        public ICommand NextQuestionCommand { get; }
        public ICommand EndQuizCommand { get; }

        public Visibility QEndButVis
        {
            get { return _qEndButVis; }
            set
            {
                _qEndButVis = value;
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
        public string UserVal
        {
            get { return _userVal; }
            set
            {
                _userVal = value;
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


        public QuizPVM(string tableName,Dictionary<string,string> dictionary,DataService ds, MainWVM mainWVM)
        {
            _mainViewModel = mainWVM;
            _dataService = ds;
            _dictionary = dictionary;
            _numberofelements = Dictionary.Count;
            _currentIndex = 0;
            _keyVal = Dictionary.ElementAt(0).Key;
            _dicVal = Dictionary.ElementAt(0).Value;
            _userVal = "";
            _qbutton = "Następne pytanie";
            _currentIndexDisp = 1;
            _points = 0;
            ListOfBad = new List<int>();
            _qEndButVis = Visibility.Visible;
            _tableName = tableName;

            //commands
            NextQuestionCommand = new RelayCommand(NextQuestion);
            EndQuizCommand = new RelayCommand(EndQuiz);

        }
        private void NextQuestion(object obj)
        {
            if (_dicVal.ToLower() != UserVal.ToLower())
            {
                ListOfBad.Add(_currentIndex);
            }
            else
            {
                _points++;
            }
            UserVal = "";
            _currentIndex++;
            CurrentIndexDisp++;
            if (_currentIndex < _numberofelements -1)
            {
                KeyVal = Dictionary.ElementAt(_currentIndex).Key;
                _dicVal = Dictionary.ElementAt(_currentIndex).Value;
            }
            else if (_currentIndex == _numberofelements - 1)
            {
                KeyVal = Dictionary.ElementAt(_currentIndex).Key;
                _dicVal = Dictionary.ElementAt(_currentIndex).Value;
                QButton = "Zakończ Quiz";
                QEndButVis = Visibility.Hidden;
            }
            else
            {
                _mainViewModel.CurrentViewModel = new QuizResultPVM(_tableName,ListOfBad, Dictionary, _mainViewModel,_points);
            }
        }

        private void EndQuiz(object obj)
        {
            if (_dicVal.ToLower() == UserVal.ToLower())
            {
                _points++;
                _currentIndex++;
            }
            while (_currentIndex <= _numberofelements-1)
            {
                ListOfBad.Add( _currentIndex);
                _currentIndex++;
            }

            _mainViewModel.CurrentViewModel = new QuizResultPVM(_tableName,ListOfBad, Dictionary, _mainViewModel,_points);
        }

    }
}
