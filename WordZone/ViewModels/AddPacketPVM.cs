using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
        public ICommand MakePacketCommand { get; }
        public ICommand BackToMenuCommand { get; }
        public ICommand GenerateRowsCommand { get; }
        private DataService _dataService;

        public ObservableCollection<Translation> TextRows { get; set; }


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
            TextRows = new ObservableCollection<Translation>();

            MakePacketCommand = new RelayCommand(MakePacket);
            GenerateRowsCommand = new RelayCommand(GenerateRows);
        }

        private void GenerateRows(object obj)
        {
            TextRows.Clear();

            for (int i = 0; i < NumberOfRows; i++)
            {
                TextRows.Add(new Translation());
            }
        }
        private void MakePacket(object obj)
        {
            if (!string.IsNullOrEmpty(TableName))
            {
                _dataService.CreateTable(TableName,TextRows);
            }
        }
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }

}
