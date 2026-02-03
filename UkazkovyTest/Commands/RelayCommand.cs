using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace UkazkovyTest.Commands
{
    public class RelayCommand: ICommand
    {
        //Predani prikazu
        //Musi se overit zda lze vykonat a pak vykonani

        public event EventHandler? CanExecuteChanged;

        private Action<object> _Execute {  get; set; }

        private Predicate<object> _CanExecute {  get; set; }

        public RelayCommand(Action<object> ExecuteMethod, Predicate<object> CanExecuteMethod) 
        {
            _Execute = ExecuteMethod;
            _CanExecute = CanExecuteMethod;

        }

        public bool CanExecute(object? parameter)
        {
            return _CanExecute(parameter);
        }

        public void Execute(object? parameter)
        {

            _Execute(parameter);
        }
    }
}
