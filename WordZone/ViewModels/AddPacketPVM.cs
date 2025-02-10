using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Models;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class AddPacketPVM : BaseVM
    {
        private string _packetName;
        private MainWVM _mainViewModel;
        private Visibility _addDelButtons;
        private Visibility _generateRowsVis;

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
        public Visibility GenerateRowsVis
        {
            get { return _generateRowsVis; }
            set
            {
                _generateRowsVis = value;
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
            }
        }
        public AddPacketPVM(DataService ds,MainWVM mainWVM)
        {
            _mainViewModel = mainWVM;
            _dataService = ds;

            _packetName = "";
            _addDelButtons = Visibility.Hidden;
            _generateRowsVis = Visibility.Visible;
            TextRows = new ObservableCollection<Translation>();

            MakePacketCommand = new RelayCommand(MakePacket);
            GenerateRowsCommand = new RelayCommand(GenerateRows);
            AddRowCommand = new RelayCommand(AddRow);
            DeleteRowCommand = new RelayCommand(DeleteRow);
        }

        private void GenerateRows(object obj)
        {
            TextRows.Clear();
            TextRows.Add(new Translation());
            AddDelButtons = Visibility.Visible;
            GenerateRowsVis = Visibility.Hidden;
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
            if (!string.IsNullOrEmpty(PacketName))
            {
                _dataService.AddTranslations(PacketName,TextRows);
                TextRows.Clear() ;
                PacketName = "";
                AddDelButtons = Visibility.Hidden;
                GenerateRowsVis = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Wprowadź nazwę zbioru");
            }
        }

    }

}
