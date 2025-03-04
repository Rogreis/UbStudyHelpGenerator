using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.Classes
{
    public static class EventsControl
    {
        public delegate void ShowMessageDelegate(string message);
        public delegate void EntryEditedDelegate(TOC_Entry entry);

        public static event ShowMessageDelegate ShowMessage = null;
        public static event EntryEditedDelegate EntryEdited = null;


        public static void FireShowMessage(string message)
        {
            ShowMessage?.Invoke(message);
        }

        public static void FireShowMessage(string message, Exception ex)
        {
            ShowMessage?.Invoke($"{message} - {ex.Message}");
        }


        public static void FireEntryEdited(TOC_Entry entry)
        {
            EntryEdited?.Invoke(entry);
        }


    }
}
