﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        public ICommand NavigateToAddPacketPageCommand { get; }
        public ICommand NavigateToQuizPageCommand { get; }

        private DataService _dataService;



        public MainWVM(DataService dataService)
        {
            NavigateToAddPacketPageCommand = new RelayCommand(_ => NavigateAddPacketPage());
            NavigateToQuizPageCommand = new RelayCommand(_ => NavigateToSecondPage());
            _dataService = dataService;
        }
        private void NavigateAddPacketPage()
        {
            CurrentViewModel = new AddPacketPVM(_dataService,this);
        }
        private void NavigateToSecondPage()
        {
            CurrentViewModel = new QuizPVM(_dataService,this);
        }


        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
