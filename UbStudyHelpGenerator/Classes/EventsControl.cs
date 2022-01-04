using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbStudyHelpGenerator.Classes
{
    public static class EventsControl
    {
        public delegate void ShowMessageDelegate(string message);

        public static event ShowMessageDelegate ShowMessage = null;


        public static void FireShowMessage(string message)
        {
            ShowMessage?.Invoke(message);
        }

    }
}
