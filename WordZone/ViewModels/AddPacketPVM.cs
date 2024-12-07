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
    public class AddPacketPVM : INotifyPropertyChanged
    {
        private string _tableName;
        private int _numberofrows;
        private MainWVM _mainViewModel;
        private Visibility _addDelButtons;

        public ICommand MakePacketCommand { get; }
        public ICommand GenerateRowsCommand { get; }
        public ICommand AddRowCommand { get; }
        public ICommand DeleteRowCommand { get; }
        private DataService _dataService;


        public ObservableCollection<Translation> TextRows { get; set; }

        public Visibility AddDelButtons
        {
            get { return _addDelButtons; }
            set
            {
                _addDelButtons = value;
                OnPropertyChanged();
            }
        }
        public int NumberOfRows
        {
            get { return _numberofrows; }
            set
            {
                _numberofrows = value;
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
        public AddPacketPVM(DataService ds,MainWVM mainWVM)
        {
            _mainViewModel = mainWVM;
            _dataService = ds;

            _tableName = "";
            _addDelButtons = Visibility.Hidden;
            _numberofrows =0;
            TextRows = new ObservableCollection<Translation>();

            MakePacketCommand = new RelayCommand(MakePacket);
            GenerateRowsCommand = new RelayCommand(GenerateRows);
            AddRowCommand = new RelayCommand(AddRow);
            DeleteRowCommand = new RelayCommand(DeleteRow);
        }

        private void GenerateRows(object obj)
        {
            TextRows.Clear();

            for (int i = 0; i < NumberOfRows; i++)
            {
                TextRows.Add(new Translation());
            }
            AddDelButtons = Visibility.Visible;
        }
        private void AddRow(object obj)
        {
            TextRows.Add(new Translation());
        }
        private void DeleteRow(object obj)
        {
            if (TextRows != null && TextRows.Count > 0)
            {
                TextRows.RemoveAt(TextRows.Count - 1);
            }
        }
        private void MakePacket(object obj)
        {
            if (!string.IsNullOrEmpty(TableName))
            {
                _dataService.CreateTable(TableName,TextRows);
                TextRows.Clear() ;
                TableName = "";
                NumberOfRows = 0;
                AddDelButtons = Visibility.Hidden;
            }
        }
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }

}
