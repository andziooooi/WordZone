using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class FlashCardsPVM : BaseVM
    {
        private string _packetName;
        private List<string> _packetNamesList;
        private DataService _dataService;
        private Dictionary<string, string> _words;
        private string _fcValue;
        private int _index;
        private int _numberofwords;
        private bool _polEngCB;
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
        public List<string> PacketNamesList
        {
            get { return _packetNamesList; }
            set
            {
                _packetNamesList = value;
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
        public string PacketName
        {
            get { return _packetName; }
            set
            {
                _packetName = value;
                OnPropertyChanged();
                ResetView();
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
        public bool PolEngCB
        {
            get { return _polEngCB; }
            set
            {
                _polEngCB = value;
                OnPropertyChanged();
                ResetView();
            }
        }
        public FlashCardsPVM(DataService ds)
        {
            _dataService = ds;
            _packetNamesList = _dataService.GetPacketsNames();
            _words = new Dictionary<string, string>();
            _polEngCB = false;
            _packetName = "Wybierz zbiór";
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
            if (_packetName != null && _packetName != "Wybierz zbiór")
            {

                _index = 0;
                Words = _dataService.CreateDictionary(PacketName,PolEngCB);
                FCValue = Words.ElementAt(0).Key;
                _numberofwords = Words.Count;
                Debug.WriteLine(_numberofwords);
                if (_numberofwords != 1) 
                {
                    NextVis = Visibility.Visible;
                }
                else
                {
                    NextVis = Visibility.Hidden;
                }
                
            }
            else
            {
                MessageBox.Show("Najpierw należy wybrać zbiór!");
            }

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
        private void ResetView()
        {
            FCValue = "";
            NextVis = Visibility.Hidden;
            PrevVis = Visibility.Hidden;
        }

    }
}

