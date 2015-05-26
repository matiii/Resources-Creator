using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using ResourcesCreator.Core.Model;
using System.Threading;
using System.Windows;

namespace ResourcesCreator.Core
{
    public class LogViewModel
    {
        private static readonly ObservableCollection<LogData> logs = new ObservableCollection<LogData>();

        public ObservableCollection<LogData> Collection
        {
            get { return logs; }
        }

        public void Add(DateTime time, string message)
        {
            //Dispatcher.CurrentDispatcher.Invoke(() => { logs.Add(new LogData { Date = time, Message = message }); });
            Application.Current.Dispatcher.Invoke(
                () => { logs.Add(new LogData {Date = time, Message = message}); });
        }
    }
}
