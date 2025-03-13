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
        private List<string> _packetNamesList;
        private string _packetName;
        private List<Translation> _translations;
        private Visibility _updateVis;
        private int _initialValue;
        public string PacketNameEdit { get; set; }
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
        public string PacketName
        {
            get { return _packetName; }
            set
            {
                _packetName = value;
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
        public ICommand StartEditCommand { get;}
        public ICommand UpdateItemsCommand { get;}
        public ICommand AddRowCommand { get;}
        public ICommand DeleteRowCommand { get;}
        public ICommand DeletePacketCommand { get;}

        public EditPacketVM(DataService ds)
        {
            _dataService = ds;

            _packetNamesList = _dataService.GetPacketsNames();
            _packetName = _packetNamesList[0];
            _translations = new List<Translation>();
            TextRows = new ObservableCollection<Translation>();
            _updateVis = Visibility.Hidden;
            _initialValue = 0;
            PacketNameEdit = PacketName;

            StartEditCommand = new RelayCommand(StartEdit);
            UpdateItemsCommand = new RelayCommand(UpdateItems);
            AddRowCommand = new RelayCommand(AddRow);
            DeleteRowCommand = new RelayCommand(DeleteRow);
            DeletePacketCommand = new RelayCommand(DeletePacket);
        }

        private void StartEdit(object obj)
        {
            if(_packetName !=null)
            {
                Translations = _dataService.GetTranslations(PacketName);
                _initialValue = Translations.Count;
                TextRows.Clear();
                foreach (Translation translation in Translations)
                {
                    TextRows.Add(translation);
                }
                PacketNameEdit = PacketName;
                OnPropertyChanged(nameof(PacketNameEdit));
                UpdateVis = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Najpierw należy wybrać zbiór!");
            }

        }
        private void UpdateItems(object obj)
        {
            if (TextRows.GroupBy(t => t.EnglishWord).Any(g => g.Count() > 1))
            {
                MessageBox.Show("Słowa po angielsku nie mogą się powtarzać!");
                return;
            }
            if (TextRows.GroupBy(t => t.PolishTranslation).Any(g => g.Count() > 1))
            {
                MessageBox.Show("Słowa po polsku nie mogą się powtarzać!");
                return;
            }
            _dataService.UpdatePacket(TextRows, PacketName);
            TextRows.Clear();
            UpdateVis = Visibility.Hidden;
        }
        private void AddRow(object obj)
        {
            TextRows.Add(new Translation());
        }
        private void DeleteRow(object obj)
        {
            int translationID = Convert.ToInt32(obj);
            //_dataService.UpdatePacket(TextRows, PacketName);
            _dataService.DeleteRow(translationID);
            var rowToRemove = TextRows.FirstOrDefault(t => t.ID == translationID);
            TextRows.Remove(rowToRemove);
        }
        private void DeletePacket(object obj)
        {
            _dataService.DeletePacket(PacketName);
            TextRows.Clear();
            UpdateVis = Visibility.Hidden;
            PacketNamesList = _dataService.GetPacketsNames();
            PacketName = PacketNamesList[0];
        }

    }
}
