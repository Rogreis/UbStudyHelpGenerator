using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.Generators.Classes;
using UbStudyHelpGenerator.Properties;

namespace UbStudyHelpGenerator.Generators
{
    public abstract class GenerateFromDatabase : Generator
    {

        protected string ConnectionString { get; set; } = "";

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
        /// List of all available translations
        /// </summary>
        public Translations Translations = null;


        /// <summary>
        /// Create a connection to database, do not open it
        /// </summary>
        /// <returns></returns>
        protected abstract DbConnection CreateConnection();

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
                    EventsControl.FireShowMessage("", ex);
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Execute a select into connection and get lines
        /// </summary>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        protected abstract string GetJsonStringFromDatabase(string selectQuery);

        public abstract bool GetPaper(short TransaltionId, short Paper, ref List<Paragraph> Paragraphs);

        public abstract bool GetAvailableTranslations();


        public GenerateFromDatabase(string connectionString)
        {
            ConnectionString = connectionString;
        }


    }
}
