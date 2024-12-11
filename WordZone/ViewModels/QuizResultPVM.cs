using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WordZone.Commands;

namespace WordZone.ViewModels
{
    public class QuizResultPVM
    {
        private MainWVM _mainWVM;
        private List<int> _listOfBad;
        private Dictionary<string, string> _dictionary;
        private Dictionary<string, string> _dictionaryBad;
        private Dictionary<string, string> _dictionaryGood;
        private string _tableName;

        public ICommand BackToMenuCommand { get; }
        public ICommand ReDoCommand { get; }
        public ICommand NewPackCommand { get; }

        public string Points { get; }
        public string NumberOfElements { get; }

        public Dictionary<string, string> DictionaryBad
        {
            get { return _dictionaryBad; }
            set
            {
                _dictionaryBad = value;
                OnPropertyChanged();
            }
        }
        public Dictionary<string, string> DictionaryGood
        {
            get { return _dictionaryGood; }
            set
            {
                _dictionaryGood = value;
                OnPropertyChanged();
            }
        }
        public QuizResultPVM(string tablename,List<int> listOfBad,Dictionary<string,string> dict,MainWVM mainWVM,int points)
        {
            _tableName = tablename;
            _mainWVM = mainWVM;
            _listOfBad = listOfBad;
            _dictionary = dict;
            _dictionaryBad = new Dictionary<string, string>();
            _dictionaryGood= new Dictionary<string, string>();
            Points = points.ToString();
            NumberOfElements = _dictionary.Count.ToString();
            Results();

            BackToMenuCommand = new RelayCommand(BackToMenu);
            ReDoCommand = new RelayCommand(ReDo);
            NewPackCommand = new RelayCommand(NewPack);
        }
        private void Results()
        {
            Dictionary<string, string> helper = new Dictionary<string, string>();
            foreach (var item in _listOfBad)
            {
                var KeyToAdd = _dictionary.Keys.ElementAt(item);
                var ValToAdd = _dictionary[KeyToAdd];
                helper.Add(KeyToAdd, ValToAdd);
            }
            DictionaryBad = helper;

            foreach (KeyValuePair<string, string> h in DictionaryBad)
            {
                if (_dictionary.ContainsKey(h.Key))
                {
                    _dictionary.Remove(h.Key);
                }
            }
            DictionaryGood = _dictionary;

        }
        private void NewPack(object obj)
        {
            _mainWVM.QRe("");
        }
        private void ReDo(object obj)
        {
            _mainWVM.QRe(_tableName);
        }
        private void BackToMenu(object obj)
        {
            _mainWVM.CurrentViewModel = null;
        }
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
