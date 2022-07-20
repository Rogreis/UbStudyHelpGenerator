using System;
using System.Collections.Generic;
using System.Data.Common;
using UbStandardObjects;
using UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.Database
{
    internal abstract class UbtDatabase
    {

		public event ShowMessage ShowMessage = null;

		public event ShowPaperNumber ShowPaperNumber = null;

		/// <summary>
		/// Stores the connection object 
		/// </summary>
		protected DbConnection connection = null;

		/// <summary>
		/// Returns the database connection
		/// </summary>
		protected DbConnection Connection
		{
			get { return connection; }
			set { connection = value; }
		}

		/// <summary>
		/// Checks whether the connection is created and open
		/// </summary>
		/// <returns></returns>
		protected bool ConnectionOpen()
		{
			if (Connection == null)
			{
				Connection = CreateConnection();
			}
			if (Connection.State != System.Data.ConnectionState.Open)
			{
				try
				{
					Connection.Open();
				}
				catch (Exception ex)
				{
					UbStandardObjects.StaticObjects.Logger.Error("Trying to open data connection", ex);
					return false;
				}
			}
			return true;
		}


		/// <summary>
		/// Create a connection to database, do not open it
		/// </summary>
		/// <returns></returns>
		protected abstract DbConnection CreateConnection();


		protected void FireShowMessage(string message, bool isError = false, bool isFatal = false)
		{
			ShowMessage?.Invoke(message, isError, isFatal);
		}

		protected void FireShowPaperNumber(short paperNo)
		{
			ShowPaperNumber?.Invoke(paperNo);
		}

		#region Abstract interface

		public abstract string GetTranslationsJsonString();

		public abstract Paper GetPaper(short LanguageID, short PaperNo);

		public abstract int GetTranslationPapers(Translation translation);

		public abstract int RunCommand(string sql);

		#endregion

	}
}
