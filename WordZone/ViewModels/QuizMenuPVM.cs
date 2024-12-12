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
        private string _tableName;
        private List<string> _tableNamesList;

        public ICommand StartQuizCommand {get;}
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
        public List<string> TableNamesList
        {
            get { return _tableNamesList; }
            set
            {
                _tableNamesList = value;
                OnPropertyChanged();
            }
        }

        public QuizMenuPVM(string tablename,DataService ds,MainWVM mainWVM)
        {
            _dictionary = new Dictionary<string, string>();
            _dataService = ds;
            _mainWVM = mainWVM;
            _tableNamesList = _dataService.GetTablesName();
            if (tablename == "") 
            {
                _tableName="Wybierz zbiór";
            }
            else
            {
                _tableName = tablename;
            }
                StartQuizCommand = new RelayCommand(StartQuiz);

        } 

        private void StartQuiz(object obj)
        {
            if (TableName != "Wybierz zbiór")
            {
                Dictionary = _dataService.CreateDictionary(TableName);
                _mainWVM.CurrentViewModel = new QuizPVM(TableName,Dictionary,_dataService, _mainWVM);
            }
        }
    }

}
