using JsonFormatterPlus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.Json;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.Properties;

namespace UbStudyHelpGenerator.Database
{



    internal class UbtDatabaseSqlServer : UbtDatabase
    {

        /// <summary>
        /// Create a connection to database, do not open it
        /// </summary>
        /// <returns></returns>
        protected override DbConnection CreateConnection()
        {
            Connection = new SqlConnection(UbStandardObjects.StaticObjects.Parameters.SqlServerConnectionString);
            return Connection;
        }


        public string GetJsonStringFromDatabase(string selectQuery)
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
                UbStandardObjects.StaticObjects.Logger.Error("Getting json string from data server", ex);
                return null;
            }
        }


        public override string GetTranslationsJsonString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [LanguageID] ");
            sb.AppendLine("      ,[Description] ");
            sb.AppendLine("      ,[Version] ");
            sb.AppendLine("      ,[TIN] ");
            sb.AppendLine("      ,[TUB] ");
            sb.AppendLine("      ,[TextButton] ");
            sb.AppendLine("      ,[CultureID] ");
            sb.AppendLine("      ,[UseBold] ");
            sb.AppendLine("      ,[RightToLeft] ");
            sb.AppendLine("      ,[StartingYear] ");
            sb.AppendLine("      ,[EndingYear] ");
            sb.AppendLine("	     ,[PaperTranslation] ");
            sb.AppendLine("  FROM [UBT].[dbo].[Languages] L where [OkForUse] = 1 order by [Description] ");
            sb.AppendLine("FOR JSON AUTO, ROOT('TranslationsFromSqlServer');  ");
            return GetJsonStringFromDatabase(sb.ToString());
        }



        public override Paper GetPaper(short LanguageID, short PaperNo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Select * from (SELECT W.LanguageID as TranslationID, W.Paper,W.PK_Seq,P.Section, P.Paragraph as ParagraphNo, P.Page, P.Line, P.Format FormatInt,[Text],[Status] ");
            sb.AppendLine("  FROM [UBT].[dbo].[UB_Texts_Work] W, [dbo].[ParagraphDescription] P ");
            sb.AppendLine($" WHERE W.Paper= {PaperNo} and W.LanguageID = {LanguageID} and P.Paper = W.Paper and P.PK_Seq = W.PK_Seq) X order by PK_Seq ");
            sb.AppendLine("FOR JSON AUTO, ROOT('Paragraphs');  ");
            string jsonString = GetJsonStringFromDatabase(sb.ToString());
            // Remove exemplar tags
            Paper paper = new Paper(jsonString);
            foreach (Paragraph paragraph in paper.Paragraphs)
            {
                paragraph.Text = Exemplar.ExemplarToHtml(paragraph.Text);
            }
            return paper;
        }


        public override int GetTranslationPapers(Translation translation)
        {
            FireShowMessage($"Loading translation {translation}");
            for (short paperNo = 0; paperNo < 197; paperNo++)
            {
                FireShowPaperNumber(paperNo);
                translation.Papers.Add(GetPaper(translation.LanguageID, paperNo));
            }
            FireShowMessage($"{translation.Papers.Count} papers loaded");
            return translation.Papers.Count;
        }

        public override int RunCommand(string sql)
        {
            try
            {
                int val = -1;
                if (ConnectionOpen())
                {
                    using (var cmd = new SqlCommand(sql, (SqlConnection)Connection))
                    {
                        val = cmd.ExecuteNonQuery();
                    }
                }
                return val;


            }
            catch (Exception ex)
            {
                FireShowMessage("");
                FireShowMessage($"RunCommand error: {sql}");
                FireShowMessage($"{ex}");
                return -1;
            }
        }


        public List<PT_AlternativeRecord> GetPT_AlternativeRecords(short paperNo)
        {
            StringBuilder sb = new StringBuilder();

            // 3 columns
            //sb.AppendLine("Select * from (SELECT W.IndexWorK, W.LanguageID as TranslationID, W.Paper,W.PK_Seq,P.Section, P.Paragraph as ParagraphNo, P.Page, P.Line, P.Format FormatInt,[Text],[Status], ");
            //sb.AppendLine("	(Select Text from [UB_Texts_Work] W1 where W1.Paper = W.Paper and W1.PK_Seq = W.PK_Seq and W1.LanguageID = 0) as English, ");
            //sb.AppendLine("	(Select Text from [UB_Texts_Work] W1 where W1.Paper = W.Paper and W1.PK_Seq = W.PK_Seq and W1.LanguageID = 34) as Portugues2007 ");
            //sb.AppendLine("  FROM [UBT].[dbo].[UB_Texts_Work] W, [dbo].[ParagraphDescription] P ");
            //sb.AppendLine($" WHERE W.Paper= {paperNo} and W.LanguageID = 2 and P.Paper = W.Paper and P.PK_Seq = W.PK_Seq and W.UserName = 'Caio') X order by PK_Seq ");
            //sb.AppendLine(" FOR JSON AUTO, ROOT('Paragraphs') ");

            // Only Pt Alternative
            sb.AppendLine("select dbo.FormatIdentity(W.Paper, W.Pk_Seq) as [Identity], W.IndexWorK, W.Pk_Seq, W.Paper, dbo.Section(W.Paper, W.Pk_Seq) as Section, dbo.Paragraph(W.Paper, W.Pk_Seq) as ParagraphNo, [Text] ");
            sb.AppendLine("  FROM [UBT].[dbo].[UB_Texts_Work] W ");
            sb.AppendLine($" WHERE W.Paper= {paperNo} and W.LanguageID = 2 order by PK_Seq ");
            //sb.AppendLine(" AND W.LastDate > Convert(datetime, '2018-11-18 12:36:25.970')  ");
            sb.AppendLine(" FOR JSON AUTO, ROOT('Paragraphs') ");



            string jsonString = GetJsonStringFromDatabase(sb.ToString());

            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };

            var records = JsonSerializer.Deserialize<PT_AlternativeRecords>(jsonString, options);

            // Remove exemplar tags
            Paper paper = new Paper(jsonString);
            List<PT_AlternativeRecord> list = new List<PT_AlternativeRecord>(records.Paragraphs);
            return list;
        }


        /// <summary>
        /// Get the json string for a description
        /// </summary>
        /// <returns></returns>
        public string GetParagraphsFormat()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select dbo.[FormatIdentity](Paper, Pk_seq) as [FormatIdentity],  ");
            sb.AppendLine("       Paper,  ");
            sb.AppendLine("	   dbo.Section(Paper, Pk_seq) as Section,  ");
            sb.AppendLine("	   dbo.Paragraph(Paper, Pk_seq) as Paragraph,  ");
            sb.AppendLine("	   dbo.Page(Paper, Pk_seq) as Page,  ");
            sb.AppendLine("	   dbo.Line(Paper, Pk_seq) as Line,  ");
            sb.AppendLine("	   dbo.Format(Paper, Pk_seq) as Format  ");
            sb.AppendLine("  from [PtAlternativeOficial]  ");
            sb.AppendLine(" where Paper >= 0  ");
            sb.AppendLine("order by Paper, Pk_seq "); 
            sb.AppendLine(" FOR JSON AUTO, ROOT('ParagraphsFormat') ");

            return GetJsonStringFromDatabase(sb.ToString());
        }


        public DataTable Query(string sql)
        {
            try
            {
                if (!ConnectionOpen())
                {
                    return null;
                }

                DataTable dataTable = new DataTable();
                using (var cmd = new SqlCommand(sql, (SqlConnection)Connection))
                {
                    // create data adapter
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    // this will query your database and return the result to your datatable
                    da.Fill(dataTable);
                    da.Dispose();
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                UbStandardObjects.StaticObjects.Logger.Error("Query server", ex);
                return null;
            }
        }

        #region Temporary routines to be deleted in the future

        public List<PT_AlternativeRecord> GetPT_FixedAlternativeRecords()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select dbo.FormatIdentity(W.Paper, W.Pk_Seq) as [Identity], W.IndexWorK, W.Pk_Seq, W.Paper, dbo.Section(W.Paper, W.Pk_Seq) as Section, dbo.Paragraph(W.Paper, W.Pk_Seq) as ParagraphNo, [Text] ");
            sb.AppendLine("select dbo.FormatIdentity(W.Paper, W.Pk_Seq) as [Identity], W.IndexWorK, W.Pk_Seq, W.Paper, dbo.Section(W.Paper, W.Pk_Seq) as Section, dbo.Paragraph(W.Paper, W.Pk_Seq) as ParagraphNo, [Text] ");
            sb.AppendLine("  FROM [UBT].[dbo].[UB_Texts_Work] W ");
            sb.AppendLine($" WHERE W.LanguageID = 2 and W.UserName = 'Caio' ");
            sb.AppendLine(" AND W.LastDate > Convert(datetime, '2018-11-18 12:36:25.970') order by Paper, PK_Seq  ");
            sb.AppendLine(" FOR JSON AUTO, ROOT('Paragraphs') ");

            string jsonString = GetJsonStringFromDatabase(sb.ToString());

            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };

            var records = JsonSerializer.Deserialize<PT_AlternativeRecords>(jsonString, options);
            List<PT_AlternativeRecord> list = new List<PT_AlternativeRecord>(records.Paragraphs);
            return list;
        }

        #endregion

    }
}
