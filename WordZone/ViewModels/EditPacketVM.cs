using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Models;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class EditPacketVM : INotifyPropertyChanged
    {
        private DataService _dataService;
        private List<string> _tableNamesList;
        private string _tableName;
        private List<Translation> _translations;
        private Visibility _updateVis;
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

        public EditPacketVM(DataService ds)
        {
            _dataService = ds;

            _tableNamesList = _dataService.GetTablesName();
            _tableName = "Wybierz zbiór";
            _translations = new List<Translation>();
            TextRows = new ObservableCollection<Translation>();
            _updateVis = Visibility.Hidden;

            StartEditCommand = new RelayCommand(StartEdit);
            UpdateItemsCommand = new RelayCommand(UpdateItems);
            AddRowCommand = new RelayCommand(AddRow);
        }

        private void StartEdit(object obj)
        {
            Translations = _dataService.GetTranslations(TableName);
            TextRows.Clear();
            foreach (Translation translation in Translations) 
            {
                TextRows.Add(translation);
            }
            UpdateVis = Visibility.Visible;
        }
        private void UpdateItems(object obj)
        {
            _dataService.UpdateTable(TextRows, TableName);
            TextRows.Clear();
            UpdateVis = Visibility.Hidden;
        }
        //private void AddRow(object obj)
        //{
        //    TextRows.Add(new Translation());
        //}

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
