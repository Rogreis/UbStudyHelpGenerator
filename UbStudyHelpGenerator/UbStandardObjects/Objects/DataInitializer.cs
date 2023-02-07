using System;
using System.IO;
using UbStandardObjects;
using UbStudyHelp.Classes;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.UbStandardObjects.Helpers;
using static System.Environment;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    internal class DataInitializer
    {
        private static GitHelper GitHelper = GitHelper.Instance;

        private static string DataFolder()
        {

            string processName = System.IO.Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            var commonpath = GetFolderPath(SpecialFolder.CommonApplicationData);
            return Path.Combine(commonpath, processName);
        }


        private static string MakeProgramDataFolder(string fileName)
        {
            string folder = DataFolder();
            Directory.CreateDirectory(folder);
            return Path.Combine(folder, fileName);
        }

        /// <summary>
        /// Inicialize the list of available translations
        /// </summary>
        /// <param name="dataFiles"></param>
        /// <returns></returns>
        private static bool InicializeTranslations(GetDataFiles dataFiles)
        {
            try
            {
                if (StaticObjects.Book.Translations == null)
                {
                    StaticObjects.Book.Translations = dataFiles.GetTranslations();
                }
                return true;
            }
            catch (Exception ex)
            {
                string message = $"Could not initialize available translations. See log.";
                StaticObjects.Logger.Error(message, ex);
                StaticObjects.Logger.FatalError(message);
                return false;
            }
        }

        /// <summary>
        /// Initialize the format table used for editing translations
        /// </summary>
        public static bool GetFormatTable(GetDataFiles dataFiles)
        {
            try
            {
                if (StaticObjects.Book.FormatTableObject == null)
                {
                    StaticObjects.Book.FormatTableObject = new FormatTable(dataFiles.GetFormatTable());
                }
                return true;
            }
            catch (Exception ex)
            {
                StaticObjects.Logger.Error($"Missing format table. May be you do not have the correct data to use this tool.", ex);
                return false;
            }
        }


        private static bool InitTranslation(GetDataFiles dataFiles, short translationId, ref Translation trans)
        {
            EventsControl.FireShowMessage($"Getting translation {translationId}");
            trans = null;
            if (translationId < 0) return true;
            trans = dataFiles.GetTranslation(translationId);
            if (trans == null)
            {
                StaticObjects.Logger.Error($"Non existing translation: {translationId}");
                return false;
            }
            EventsControl.FireShowMessage($"Getting translation {trans.Description}");
            if (trans.IsEditingTranslation)
            {
                EventsControl.FireShowMessage($"Getting editying translation {trans.Description}");
                EventsControl.FireShowMessage("Checking folders for editing translation");
                if (!Directory.Exists(StaticObjects.Parameters.EditBookRepositoryFolder))
                {
                    StaticObjects.Logger.Error("There is no repositoty set for editing translation");
                    return false;
                }
                if (!GitHelper.IsValid(StaticObjects.Parameters.EditBookRepositoryFolder))
                {
                    StaticObjects.Logger.Error($"Folder is not a valid respository: {StaticObjects.Parameters.EditBookRepositoryFolder}");
                    return false;
                }
                EventsControl.FireShowMessage("Found a valid repository");
                trans = new TranslationEdit(trans, StaticObjects.Parameters.EditParagraphsRepositoryFolder);
                // Format table must exist for editing translation
                if (!GetFormatTable(dataFiles))
                {
                    return false;
                }
            }
            return trans.CheckData();
        }


        public static bool InitLogger()
        {
            try
            {
                // Log for errors
                string pathLog = MakeProgramDataFolder("UbStudyHelp.log");
                StaticObjects.Logger = new LogGenerator();
                StaticObjects.Logger.Initialize(pathLog, false);
                StaticObjects.Logger.Info("»»»» Startup");
                return true;
            }
            catch (Exception ex)
            {
                string message = "Could not initialize logger";
                StaticObjects.Logger.Error(message, ex);
                StaticObjects.Logger.FatalError(message);
                return false;
            }
        }

        public static bool InitParameters()
        {
            try
            {
                Program.PathParameters = MakeProgramDataFolder("UbStudyHelp.json");
                if (!File.Exists(Program.PathParameters))
                {
                    StaticObjects.Logger.Info("Parameters not found, creating a new one: " + Program.PathParameters);
                }
                StaticObjects.Parameters = Parameters.Deserialize(Program.PathParameters);
                StaticObjects.Parameters.EditTranslationId = 2; // REMOVER


                //// Set folders and URLs used
                //// This must be set in the parameters
                //StaticObjects.Parameters.ApplicationDataFolder = MakeProgramDataFolder("");
                //StaticObjects.Parameters.IndexSearchFolders = MakeProgramDataFolder("IndexSearch");
                //StaticObjects.Parameters.TubSearchFolders = MakeProgramDataFolder("TubSearch");


                //// This application has a differente location for TUB Files
                //StaticObjects.Parameters.TUB_Files_RepositoryFolder = MakeProgramDataFolder("TUB_Files");
                //StaticObjects.Parameters.EditParagraphsRepositoryFolder = MakeProgramDataFolder("PtAlternative");
                //// Paths not used in this program
                ////StaticObjects.Parameters.EditBookRepositoryFolder = "C:\\Trabalho\\Github\\Rogerio\\TUB_PT_BR";
                ////StaticObjects.Parameters.UrlRepository = "https://github.com/Rogreis/PtAlternative";

                return true;
            }
            catch (Exception ex)
            {
                string message = "Could not initialize parameters";
                StaticObjects.Logger.Error(message, ex);
                StaticObjects.Logger.FatalError(message);
                return false;
            }
        }

        public static bool VerifyRepositories()
        {
            try
            {
                bool ret = true;
                // Verify respository existence
                if (!GitHelper.IsValid(StaticObjects.Parameters.TUB_Files_RepositoryFolder))
                {
                    StaticObjects.FireSendMessage("TUB_Files_RepositoryFolder is not a repository");
                    ret= false;
                }
                if (!GitHelper.IsValid(StaticObjects.Parameters.EditParagraphsRepositoryFolder))
                {
                    StaticObjects.FireSendMessage("TUB_Files_RepositoryFolder is not a repository");
                    ret = false;
                }
                if (!GitHelper.IsValid(StaticObjects.Parameters.EditBookRepositoryFolder))
                {
                    StaticObjects.FireSendMessage("EditBookRepositoryFolder is not a repository");
                    ret = false;
                }
                return ret;
            }
            catch (Exception ex)
            {
                string message = "Could not verify translations 2. See log.";
                StaticObjects.Logger.Error(message, ex);
                StaticObjects.Logger.FatalError(message);
                return false;
            }
        }

        public static bool InitTranslations()
        {
            try
            {

                GetDataFiles dataFiles = new GetDataFiles();
                StaticObjects.Book = new Book();


                StaticObjects.FireSendMessage("Getting translations list");
                if (!InicializeTranslations(dataFiles))
                {
                    StaticObjects.FireSendMessage("InicializeTranslations failed");
                    return false;
                }

                if (!InitTranslation(dataFiles, StaticObjects.Parameters.EnglishTranslationId, ref StaticObjects.Book.EnglishTranslation))
                {
                    StaticObjects.FireSendMessage($"InitTranslation failed for {StaticObjects.Book.EnglishTranslation}");
                    return false;
                }

                if (!InitTranslation(dataFiles, StaticObjects.Parameters.WorkTranslationId, ref StaticObjects.Book.WorkTranslation))
                {
                    StaticObjects.FireSendMessage($"InitTranslation failed for {StaticObjects.Book.WorkTranslation}");
                    return false;
                }

                Translation trans= null;
                if (!InitTranslation(dataFiles, StaticObjects.Parameters.EditTranslationId, ref trans))
                {
                    StaticObjects.FireSendMessage($"InitTranslation failed for {StaticObjects.Book.EditTranslation}");
                    return false;
                }
                StaticObjects.Book.EditTranslation = (TranslationEdit)trans;

                return true;
            }
            catch (Exception ex)
            {
                string message = "Could not initialize translations 2. See log.";
                StaticObjects.Logger.Error(message, ex);
                StaticObjects.Logger.FatalError(message);
                return false;
            }
        }



    }
}
