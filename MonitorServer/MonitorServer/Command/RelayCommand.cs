﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitorServer.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, bool> canExecuteFunc;
        private readonly Action<object> executeAction;

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null) return;
            executeAction = execute;
            canExecuteFunc = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (canExecuteFunc == null) return true;
            return canExecuteFunc(parameter);
        }

        public void Execute(object parameter)
        {
            if (executeAction == null) return;
            executeAction(parameter);
        }
    }
}
