using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Models;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class EditPacketVM : BaseVM
    {
        private DataService _dataService;
        private List<string> _tableNamesList;
        private string _tableName;
        private List<Translation> _translations;
        private Visibility _updateVis;
        private int _initialValue;
        public string TableNameEdit { get; set; }
        public ObservableCollection<Translation> TextRows {  get; set; }
        public Visibility UpdateVis
        {
            get { return _updateVis; }
            set
            {
                _updateVis = value;
                OnPropertyChanged();
            }
        }
        public List<Translation> Translations
        {
            get { return _translations; }
            set 
            {
                _translations = value;
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

        public List<string> TableNamesList
        {
            get { return _tableNamesList; }
            set
            {
                _tableNamesList = value;
                OnPropertyChanged();
            }
        }
        public ICommand StartEditCommand { get;}
        public ICommand UpdateItemsCommand { get;}
        public ICommand AddRowCommand { get;}
        public ICommand DeleteRowCommand { get;}
        public ICommand DeletePacketCommand { get;}

        public EditPacketVM(DataService ds)
        {
            _dataService = ds;

            _tableNamesList = _dataService.GetTablesName();
            _tableName = "Wybierz zbiór";
            _translations = new List<Translation>();
            TextRows = new ObservableCollection<Translation>();
            _updateVis = Visibility.Hidden;
            _initialValue = 0;
            TableNameEdit = TableName;

            StartEditCommand = new RelayCommand(StartEdit);
            UpdateItemsCommand = new RelayCommand(UpdateItems);
            AddRowCommand = new RelayCommand(AddRow);
            DeleteRowCommand = new RelayCommand(DeleteRow);
            DeletePacketCommand = new RelayCommand(DeletePacket);
        }

        private void StartEdit(object obj)
        {
            if(_tableName != "Wybierz zbiór" && _tableName !=null)
            {
                Translations = _dataService.GetTranslations(TableName);
                _initialValue = Translations.Count;
                TextRows.Clear();
                foreach (Translation translation in Translations)
                {
                    TextRows.Add(translation);
                }
                TableNameEdit = TableName;
                OnPropertyChanged(nameof(TableNameEdit));
                UpdateVis = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Najpierw należy wybrać zbiór!");
            }

        }
        private void UpdateItems(object obj)
        {
            _dataService.UpdateTable(TextRows, TableName,_initialValue);
            TextRows.Clear();
            UpdateVis = Visibility.Hidden;
        }
        private void AddRow(object obj)
        {
            TextRows.Add(new Translation("",""));
        }
        private void DeleteRow(object obj)
        {
            string Eng = obj.ToString();
            _dataService.UpdateTable(TextRows,TableName,_initialValue);
            _dataService.DeleteRow(Eng, TableName);
            StartEdit(null);
        }
        private void DeletePacket(object obj)
        {
            _dataService.DropTable(TableName);
            TextRows.Clear();
            UpdateVis = Visibility.Hidden;
            TableNamesList = _dataService.GetTablesName();
            TableName = "Wybierz zbiór";

        }

    }
}
