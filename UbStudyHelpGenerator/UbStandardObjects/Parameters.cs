using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using UbStudyHelpGenerator.Classes;
using UbStudyHelpGenerator.UbStandardObjects.Objects;

namespace UbStudyHelpGenerator.UbStandardObjects
{

    public enum TextShowOption
    {
        RightOnly = 0,
        LeftRight = 1,
        LeftMiddleRight = 2,
        LeftRightCompare = 3,
        LeftMiddleRightCompare = 4
    }


    public struct ColorSerial
    {
		public byte A { get; set; }
		public byte R { get; set; }
		public byte G { get; set; }
		public byte B { get; set; }

		public ColorSerial(byte a, byte r, byte g, byte b)
        {
			A = a;
			R = r;
			G = g;	
			B = b;	
        }
	}

	public class Parameters
	{


        /// <summary>
        /// Last position in the text, default for first paragraph
        /// </summary>
        public TOC_Entry Entry { get; set; } = new TOC_Entry(0, 0, 1, 0, 0, 0);

		public short EnglishTranslationId { get; set; } = 0;

		public short EditTranslationId { get; set; } = 2;

        public short WorkTranslationId { get; set; } = -1;  // -1 indicate not to be shown

        public string TextReferenceFilePath { get; set; } = "";

        // Usdados para a tela de edição
        public short TranslationUpLeft { get; set; } = 0;
        public short TranslationUpRight { get; set; } = 0;
        public short TranslationDownLeft { get; set; } = 0;
        public short LastDocumentToRecover { get; set; } = -1; // Verificar se foi traduzido pelo Caio
        public short LastDocumentToChangeStatus { get; set; } = -1;



        public string InputHtmlFilesPath { get; set; } = "";

		public string IndexDownloadedFiles { get; set; } = "";

		public string IndexOutputFilesPath { get; set; } = "";

		public string SqlServerConnectionString { get; set; }

		public short CurrentTranslation { get; set; } = 0;

	

        /// <summary>
        /// Repository for translations
        /// </summary>
        public string TUB_Files_RepositoryFolder { get; set; } = "";

        /// <summary>
        /// Github source for translations
        /// </summary>
        public string TUB_Files_Url { get; set; } = "https://github.com/Rogreis/TUB_Files.git";

        /// <summary>
        /// Repository for editing translation
        /// </summary>
        public string EditParagraphsRepositoryFolder { get; set; } = null;

        /// <summary>
        /// Github source for editing translation
        /// </summary>
        public string EditParagraphsUrl { get; set; } = "https://github.com/Rogreis/PtAlternative.git";
        
        /// <summary>
        /// Full book pages local repository
        /// </summary>
        public string EditBookRepositoryFolder { get; set; } = null;

        public float FontSize { get; set; } = 10;

        public string FontFamily { get; set; } = "Georgia,Verdana,Arial,Helvetica";

        public bool IsDarkTheme { get; set; } = true;

        public short LastGTPPaper { get; set; } = 101;

        public short LastPaperStatusChanged { get; set; } = 0;
        public string EntriesUsed { get; set; } = "";
        public TOC_Entry LastEditedEntry { get; set; } = new TOC_Entry();

        public string SerializeComboBoxItems(ComboBox comboBox)
        {
            if (comboBox.Items.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach (string item in comboBox.Items)
            {
                sb.AppendLine(item);
            }
            return sb.ToString();
        }

        public void DeSerializeComboBoxItems(string serializedString, ComboBox comboBox)
        {
            comboBox.Items.Clear();
            string[] items = serializedString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            comboBox.Items.AddRange(items);
        }


        /// <summary>
        /// Serialize the parameters instance
        /// </summary>
        /// <param name="p"></param>
        /// <param name="pathParameters"></param>
        public static void Serialize(Parameters p, string pathParameters)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize<Parameters>(p);
                File.WriteAllText(pathParameters, jsonString);
            }
            catch { }
        }

        /// <summary>
        /// Deserialize the parameters instance
        /// </summary>
        /// <param name="pathParameters"></param>
        /// <returns></returns>
        public static Parameters Deserialize(string pathParameters)
        {
            try
            {
                var jsonString = File.ReadAllText(pathParameters);

                var options = new JsonSerializerOptions
                {
                    AllowTrailingCommas = true
                };
                return JsonSerializer.Deserialize<Parameters>(jsonString, options);
            }
            catch
            {
                return new Parameters();
            }
        }


    }
}
