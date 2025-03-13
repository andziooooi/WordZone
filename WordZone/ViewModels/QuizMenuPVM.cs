using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class QuizMenuPVM : BaseVM
    {
        private Dictionary<string, string> _dictionary;
        private DataService _dataService;
        private MainWVM _mainWVM;
        private string _packetName;
        private List<string> _packetNamesList;
        private bool _polEngCB;

        public ICommand StartQuizCommand {get;}
        public string PacketName
        {
            get { return _packetName; }
            set
            {
                _packetName = value;
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
        public List<string> PacketNamesList
        {
            get { return _packetNamesList; }
            set
            {
                _packetNamesList = value;
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
            }
        }

        public QuizMenuPVM(string packetname,DataService ds,MainWVM mainWVM)
        {
            _dictionary = new Dictionary<string, string>();
            _dataService = ds;
            _mainWVM = mainWVM;
            _polEngCB = false;
            _packetNamesList = _dataService.GetPacketsNames();
            if (packetname == "") 
            {
                if (_packetNamesList.Count >0)
                {
                    _packetName = _packetNamesList.First();
                }
            }
            else
            {
                _packetName = packetname;
            }
                StartQuizCommand = new RelayCommand(StartQuiz);

        } 

        private void StartQuiz(object obj)
        {
            if (PacketName ==null)
            {
                MessageBox.Show("Najpierw trzeba wybrać zbiór");
                return;
            }
            var check = _dataService.GetTranslations(PacketName);
            if(check.Count ==0) 
            {
                MessageBox.Show("Brak tłumaczeń w zbiorze");
                return;
            }

            Dictionary = _dataService.CreateDictionary(PacketName, PolEngCB);
            _mainWVM.CurrentViewModel = new QuizPVM(PacketName, Dictionary, _dataService, _mainWVM);
        }
    }

}
