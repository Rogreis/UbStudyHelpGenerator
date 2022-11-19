using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UbStandardObjects;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Database;

namespace UbStudyHelpGenerator.Classes
{
    internal class GetDataFilesGenerator : GetDataFiles
    {

        public event ShowPaperNumber ShowPaperNumber = null;


        private UbtDatabaseSqlServer Server = new UbtDatabaseSqlServer();

        public GetDataFilesGenerator(UbtDatabaseSqlServer server, Parameters param) : base(param)
        {
            Server = server;
            //Server.ShowMessage += Server_ShowMessage;
            Server.ShowPaperNumber += Server_ShowPaperNumber;
        }

        private void Server_ShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }

        private void Server_ShowMessage(string message, bool isError = false, bool isFatal = false)
        {
            StaticObjects.FireSendMessage(message, isError, isFatal);
        }

        private void DeleteFile(string pathFile)
        {
            if (File.Exists(pathFile))
                File.Delete(pathFile);
        }


        //public override List<Translation> GetTranslations()
        //{
        //    string jsonString = Server.GetTranslationsJsonString();
        //    string pathTranslations = Path.Combine(UbStandardObjects.StaticObjects.Parameters.RepositoryOutputFolder, "Translations.json");
        //    DeleteFile(pathTranslations);
        //    File.WriteAllText(pathTranslations, jsonString);
        //    return Translations.DeserializeJson(jsonString);
        //}


        //protected override List<UbAnnotationsStoreData> LoadAnnotations(short translationId)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void StoreAnnotations(TOC_Entry entry, List<UbAnnotationsStoreData> annotations)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
