using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WordZone.Commands;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class MainWVM : INotifyPropertyChanged
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
        private Visibility _menu;
        public Visibility Menu
        {
            get => _menu;
            set 
            { 
                _menu = value;
                OnPropertyChanged(nameof(Menu));
            }

        }

        public ICommand NavigateToAddPacketPageCommand { get; }
        public ICommand NavigateToQuizPageCommand { get; }

        private DataService _dataService;



        public MainWVM(DataService dataService)
        {
            NavigateToAddPacketPageCommand = new RelayCommand(_ => NavigateAddPacketPage());
            NavigateToQuizPageCommand = new RelayCommand(_ => NavigateToSecondPage());
            _dataService = dataService;
            Menu = Visibility.Visible;
        }
        private void NavigateAddPacketPage()
        {
            Menu = Visibility.Hidden;
            CurrentViewModel = new AddPacketPVM(_dataService,this);
        }
        private void NavigateToSecondPage()
        {
            Menu = Visibility.Hidden;
            CurrentViewModel = new QuizPVM(_dataService,this);
        }


        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
