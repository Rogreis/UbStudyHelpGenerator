using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Text.Json;
using UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    public class GetDataFiles
    {
        protected const string TubFilesFolder = "TUB_Files";

        protected const string ControlFileName = "AvailableTranslations.json";

        protected const string indexFileName = "Index.zip";

        protected const string translationAnnotationsFileName = "TranslationAnnotations";

        protected const string paragraphAnnotationsFileName = "TranslationParagraphAnnotations";

        //private string currentAnnotationsFileNAme = "Annotations";



        public GetDataFiles()
        {
        }

        #region File path creation

        /// <summary>
        /// Generates the translations control output file full path
        /// </summary>
        /// <returns></returns>
        protected string TranslationsOutputPath()
        {
            return Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, ControlFileName);

        }

        /// <summary>
        /// Generates the translations control input file full path
        /// </summary>
        /// <returns></returns>
        protected string TranslationsInputPath()
        {
            return Path.Combine(System.Windows.Forms.Application.StartupPath, $"Data/{ControlFileName}");
        }


        private string CalculateMD5(string filePath)
        {
            using (var md5Object = System.Security.Cryptography.MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var hash = md5Object.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }


        /// <summary>
        /// Generates the translation full path
        /// </summary>
        /// <param name="translationId"></param>
        /// <returns></returns>
        protected string TranslationFilePath(short translationId)
        {
            return Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, $"TR{translationId:000}.gz");
        }


        /// <summary>
        /// Generates the translation full path
        /// </summary>
        /// <param name="translationId"></param>
        /// <returns></returns>
        protected string TranslationJsonFilePath(short translationId)
        {
            return Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, $"TR{translationId:000}.json");
        }

        /// <summary>
        /// Generates the translation full path
        /// </summary>
        /// <param name="translationId"></param>
        /// <returns></returns>
        protected string TranslationAnnotationsJsonFilePath(short translationId)
        {
            return Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, $"{translationAnnotationsFileName}_{translationId:000}.json");
        }

        protected string ParagraphAnnotationsJsonFilePath(short translationId)
        {
            return Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, $"{paragraphAnnotationsFileName}_{translationId:000}.json");
        }

        #endregion



        /// <summary>
        /// Get public data from github
        /// <see href="https://raw.githubusercontent.com/Rogreis/TUB_Files/main"/>
        /// </summary>
        /// <param name="destinationFolder"></param>
        /// <param name="fileName"></param>
        /// <param name="isZip"></param>
        /// <returns></returns>
        protected byte[] GetGitHubBinaryFile(string fileName, bool isZip = false)
        {
            try
            {
                // https://raw.githubusercontent.com/Rogreis/TUB_Files/main/UbHelpTextControl.xml
                UbStandardObjects.StaticObjects.Logger.Info("Downloading files from github.");
                string url = isZip ? $"https://github.com/Rogreis/TUB_Files/raw/main/{fileName}" :
                    $"https://raw.githubusercontent.com/Rogreis/TUB_Files/main/{fileName}";
                UbStandardObjects.StaticObjects.Logger.Info("Url: " + url);

                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("a", "a");
                    byte[] bytes = wc.DownloadData(url);
                    return bytes;
                }
            }
            catch (Exception ex)
            {
                string message = $"Error getting file {fileName}: {ex.Message}. May be you do not have the correct data to use this tool.";
                UbStandardObjects.StaticObjects.Logger.Error(message, ex);
                return null;
            }
        }

        /// <summary>
        /// Copy streams
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        protected static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        /// <summary>
        /// Unzip a Gzipped translation file
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        protected string BytesToString(byte[] bytes, bool isZip = true)
        {
            if (!isZip)
            {
                return Encoding.UTF8.GetString(bytes);
            }
            else
            {
                using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    {
                        //gs.CopyTo(mso);
                        CopyTo(gs, mso);
                    }

                    return Encoding.UTF8.GetString(mso.ToArray());
                }
            }
        }

        #region Load & Store Annotations
        protected void StoreJsonAnnotations(TOC_Entry entry, string jsonAnnotations)
        {
            string path = TranslationAnnotationsJsonFilePath(entry.TranslationId);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            if (string.IsNullOrWhiteSpace(jsonAnnotations))
            {
                return;
            }
            File.WriteAllText(path, jsonAnnotations);
        }

        /// <summary>
        /// Loads a list of all TOC/Annotation done for a paper
        /// </summary>
        /// <param name="translationId"></param>
        /// <returns></returns>
        protected string LoadJsonAnnotations(short translationId)
        {
            string pathAnnotationsFile = TranslationAnnotationsJsonFilePath(translationId);
            if (File.Exists(pathAnnotationsFile))
            {
                return File.ReadAllText(pathAnnotationsFile);
            }
            return null;
        }

        #endregion


        /// <summary>
        /// Get all papers from the zipped file
        /// </summary>
        /// <param name="translationId"></param>
        /// <param name="isZip"></param>
        /// <returns></returns>
        protected string GetFile(short translationId, bool isZip = true)
        {
            try
            {
                string json = "";

                string translationJsonFilePath = TranslationJsonFilePath(translationId);
                if (File.Exists(translationJsonFilePath))
                {
                    json = File.ReadAllText(translationJsonFilePath);
                    return json;
                }

                string translationStartupPath = TranslationFilePath(translationId);
                if (File.Exists(translationStartupPath))
                {
                    StaticObjects.Logger.Info("File exists: " + translationStartupPath);
                    byte[] bytes = File.ReadAllBytes(translationStartupPath);
                    json = BytesToString(bytes, isZip);
                    File.WriteAllText(translationJsonFilePath, json);
                    return json;
                }
                else
                {
                    StaticObjects.Logger.Error($"Translation not found {translationId}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                StaticObjects.Logger.Error("GetFile", ex);
                return null;
            }
        }


        /// <summary>
        /// Get the translations list from a local file
        /// </summary>
        /// <returns></returns>
        public List<Translation> GetTranslations()
        {
            string path = TranslationsInputPath();
            string json = File.ReadAllText(path);
            StaticObjects.FireSendMessage("Reading traslations from json file");

            List<Translation> list = Translations.DeserializeJson(json);
            StaticObjects.FireSendMessage("Recalculating hash for each one");
            foreach (Translation t in list)
            {
                string translatioPath = TranslationFilePath(t.LanguageID);
                t.Hash = CalculateMD5(translatioPath);
                StaticObjects.FireSendMessage($"{t}  {t.Hash}");
            }
            SetTranslations(list);
            return list;
        }


        /// <summary>
        /// Get loca translations list and store in the repository
        /// Data changes need to be done in the json file (Data/AvailableTranslations.json)
        /// </summary>
        /// <returns></returns>
        public bool SetTranslations(List<Translation> translations)
        {
            try
            {
                string path = TranslationsOutputPath();
                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true,
                    WriteIndented = true,
                    IncludeFields = true
                };

                string json = JsonSerializer.Serialize<List<Translation>>(translations, options);

                File.WriteAllText(path, json);
                StaticObjects.FireSendMessage($"Translations stored into file: {path}");
                return true;
            }
            catch (Exception ex)
            {
                StaticObjects.Logger.Error("SetTranslations", ex);
                return false;
            }
        }

        /// <summary>
        /// Get a translation from a local file
        /// </summary>
        /// <param name="translationId"></param>
        /// <returns></returns>
        public Translation GetTranslation(short translationId, bool initializePapers = true)
        {
            Translation translation = StaticObjects.Book.GetTranslation(translationId);
            if (translation == null)
            {
                translation = new Translation();
            }

            if (!initializePapers) return translation;

            if (translation.Papers.Count > 0)
            {
                if (!translation.CheckData())
                {
                    return null;
                }

                return translation;
            }

            // FIX ME: IsEditingTranslation is hard codded here, but needs to come from repository
            if (translationId == 2)
            {
                TranslationEdit translatioEdit = translation as TranslationEdit;
                if (translatioEdit == null)
                {
                    StaticObjects.Book.Translations.Remove(translation);
                    translatioEdit = new TranslationEdit(translation, StaticObjects.Parameters.EditParagraphsRepositoryFolder);
                    StaticObjects.Book.Translations.Add(translatioEdit);
                }
                translatioEdit.IsEditingTranslation = true;
                translatioEdit.Annotations = null;
                return translation;
            }
            else
            {
                string json = GetFile(translationId, true);
                translation.GetData(json);

                // Loading annotations
                translation.Annotations = null;
                return translation;
            }


        }

        /// <summary>
        /// Get the zipped format table json and unzipp it to return
        /// </summary>
        /// <returns></returns>
        public string GetFormatTable()
        {
            try
            {
                string formatTableZippedPath = Path.Combine(StaticObjects.Parameters.TUB_Files_RepositoryFolder, @"FormatTable.gz");

                if (!File.Exists(formatTableZippedPath))
                {
                    StaticObjects.Logger.Error($"Format Table not found {formatTableZippedPath}");
                    return null;
                }

                StaticObjects.Logger.Info("File exists: " + formatTableZippedPath);
                byte[] bytes = File.ReadAllBytes(formatTableZippedPath);
                return BytesToString(bytes);
            }
            catch (Exception ex)
            {
                StaticObjects.Logger.Error("GetFormatTable", ex);
                return null;
            }
        }


        /// <summary>
        /// Get the json string for a paper notes
        /// </summary>
        /// <returns></returns>
        public string GetNotes(short paperNo)
        {
            try
            {
                string filePath = Path.Combine(StaticObjects.Parameters.EditParagraphsRepositoryFolder, $@"{ParagraphEdit.FolderPath(paperNo)}\Notes.json");
                string jsonString = File.ReadAllText(filePath);
                return "";
            }
            catch (Exception ex)
            {
                StaticObjects.Logger.Error("GetFormatTable", ex);
                return null;
            }
        }

    }

}
