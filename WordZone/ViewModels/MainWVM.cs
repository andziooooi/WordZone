using System.Windows.Input;
using WordZone.Commands;
using WordZone.Services;

namespace WordZone.ViewModels
{
    public class MainWVM : BaseVM
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

        public ICommand NavigateToAddPacketPageCommand { get; }
        public ICommand NavigateToQuizPageCommand { get; }
        public ICommand NavigateToEditPacketPageCommand { get; }
        public ICommand NavigateToFlashCardsCommand { get; }
        private DataService _dataService;



        public MainWVM(DataService dataService)
        {

            NavigateToAddPacketPageCommand = new RelayCommand(_ => NavigateAddPacketPage());
            NavigateToQuizPageCommand = new RelayCommand(_ => NavigateToQuizPage(""));
            NavigateToEditPacketPageCommand = new RelayCommand(_ => NavigateToEditPacketPage());
            NavigateToFlashCardsCommand = new RelayCommand(_ => NavigateToFlashCards());
            _dataService = dataService;
        }
        private void NavigateToFlashCards()
        {
            CurrentViewModel = new FlashCardsPVM(_dataService);
        }
        private void NavigateAddPacketPage()
        {
            CurrentViewModel = new AddPacketPVM(_dataService,this);
        }
        private void NavigateToQuizPage(string Table)
        {
            CurrentViewModel = new QuizMenuPVM(Table,_dataService,this);
        }
        private void NavigateToEditPacketPage()
        {
            CurrentViewModel = new EditPacketVM(_dataService);
        }
        public void QRe(string Table)
        {
            NavigateToQuizPage(Table);
        }
    }
}
