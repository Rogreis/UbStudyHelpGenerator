using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Text.Json;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.Generators.Classes;

namespace UbStudyHelpGenerator.Generators
{
    public class GenerateFromDatabaseSqlServer : GenerateFromDatabase
    {

        /// <summary>
        /// Returns the Sql Server database connection
        /// </summary>
        protected SqlConnection SqlConnection
        {
            get { return Connection as SqlConnection; }
            set { connection = value; }
        }

        /// <summary>
        /// Execute a select into sql server connection and retuns a json string
        /// </summary>
        /// <param name="selectQuery"></param>
        /// <returns></returns>
        protected override string GetJsonStringFromDatabase(string selectQuery)
        {
            try
            {
                if (!ConnectionOpen())
                {
                    return null;
                }

                using (var cmd = new SqlCommand(selectQuery, (SqlConnection)Connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        string jsonResult = "";
                        while (reader.Read())
                        {
                            jsonResult += Convert.ToString(reader[0]);
                        }
                        return jsonResult;
                    }
                }
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage("Getting Json string from database", ex);
                return null;
            }
        }

        /// <summary>
        /// Create and open a sql server connection if not already open
        /// </summary>
        /// <returns></returns>
        protected override DbConnection CreateConnection()
        {
            if (Connection == null || Connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    Connection = new SqlConnection(ConnectionString);
                    Connection.Open();
                }
                catch (Exception ex)
                {
                    EventsControl.FireShowMessage("Error: getting paper json string from data server: string null", ex);
                    Connection = null;
                }
            }
            return Connection;
        }



        public GenerateFromDatabaseSqlServer(string connectionString) : base(connectionString)
        {
        }


        public override  bool GetAvailableTranslations()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select LanguageID, Description, TUB, PaperTranslation, TIN, UseBold, RightToLeft, CultureID ");
            sb.AppendLine("  from Languages ");
            sb.AppendLine(" where OkForUSe = 1 ");
            sb.AppendLine(" Order by Description ");
            sb.AppendLine("FOR JSON AUTO, ROOT('Translation')  ");

            string jsonString= GetJsonStringFromDatabase(sb.ToString());

            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };

            Translations = JsonSerializer.Deserialize<Translations>(jsonString, options);


            return true;
        }



        public override bool GetPaper(short TransaltionId, short Paper, ref List<Paragraph> Paragraphs)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from  ");
            sb.AppendLine("(SELECT ");
            sb.AppendLine(" U.Paper     as Paper,  ");
            sb.AppendLine(" U.PK_Seq    as PK_Seq,  ");
            sb.AppendLine(" P.Section   as Section, ");
            sb.AppendLine(" P.Paragraph as ParagraphNo,  ");
            sb.AppendLine(" P.Page      as Page, ");
            sb.AppendLine(" P.Line      as Line,  ");
            sb.AppendLine(" P.Format    as Format,  ");
            sb.AppendLine(" P.HtmlCode  as HtmlCode,  ");
            sb.AppendLine(" U.Text      as Text ");
            sb.AppendLine("FROM UB_Texts_Work U, PaperSectionParagraphSeq P  ");
            sb.AppendLine($"WHERE U.Paper      = {Paper} ");
            sb.AppendLine($"  AND U.LanguageID = {TransaltionId} ");
            sb.AppendLine("  AND P.Paper      = U.Paper  ");
            sb.AppendLine("  AND P.PK_Seq     = U.PK_Seq) a   ");
            sb.AppendLine("ORDER BY PK_Seq ");
            sb.AppendLine("FOR JSON AUTO, ROOT('Paragraphforofflinetool')  ");

            string jsonString = GetJsonStringFromDatabase(sb.ToString());
            if (!string.IsNullOrEmpty(jsonString))
            {
                //OfflineToolData offlineToolData = JsonConvert.DeserializeObject<OfflineToolData>(jsonString);
                //listParagraphs = offlineToolData.ParagraphForOfflineTool.ToList();
                //JsonStringReceived(jsonString, JsonStringType.SearchDataResults);
                return true;
            }
            else
            {
                EventsControl.FireShowMessage("Error: getting paper json string from data server: string null");
                return false;
            }
        }


        public override void Generate(string filesPath, string outputFiles)
        {
            throw new NotImplementedException();
        }

    }
}
