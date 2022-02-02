using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UbStudyHelpGenerator.Classes
{
    public static class EventsControl
    {
        public delegate void ShowMessageDelegate(string message);

        public static event ShowMessageDelegate ShowMessage = null;

		/// <summary>
		/// Fire a message
		/// </summary>
		/// <param name="message"></param>
        public static void FireShowMessage(string message)
        {
            ShowMessage?.Invoke(message);
        }

		/// <summary>
		/// Fire error messages with an exception
		/// </summary>
		/// <param name="local"></param>
		/// <param name="message"></param>
		/// <param name="ex"></param>
		public static void FireShowMessage(string message, Exception ex)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"*** Error: {message}");
			sb.AppendLine("    Exception messages:");
			Exception ex2 = ex;
			while (ex2 != null)
			{
				sb.AppendLine($"       {ex2.Message}");
				ex2 = ex.InnerException;
			}
			FireShowMessage(sb.ToString());
		}



	}
}
