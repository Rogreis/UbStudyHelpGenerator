﻿using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        //// From https://getbootstrap.com/docs/5.0/utilities/background/
        //// https://www.w3schools.com/tags/ref_colornames.asp
        //private const string ClassParagraphStarted = "p-3 mb-2 bg-secondary text-white";
        //private const string ClassParagraphWorking = "p-3 mb-2 bg-warning text-dark";
        //private const string ClassParagraphDoubt = "p-3 mb-2 bg-danger text-white";
        //private const string ClassParagraphOk = "p-3 mb-2 bg-primary text-white";

        //private const string ClassParagraphDarkTheme = "p-3 mb-2 bg-dark text-white";
        //private const string ClassParagraphLightTheme = "p-3 mb-2 bg-transparent text-dark";


        /// <summary>
        /// Last position in the text, default for first paragraph
        /// </summary>
        public TOC_Entry Entry { get; set; } = new TOC_Entry(0, 0, 1, 0, 0, 0);

		public short EnglishTranslationId { get; set; } = 0;

		public short EditTranslationId { get; set; } = 2;

        public short WorkTranslationId { get; set; } = -1;  // -1 indicate not to be shown

        public string TextReferenceFilePath { get; set; } = "";

        public string LastCommitUsedForPTAlternative { get; set; } = "b46a5f308920f4182a066c1e79ece2864d37c8b2";


        public bool ShowCompare { get; set; } = false;
        
		public int SearchPageSize { get; set; } = 20;

		public bool ShowParagraphIdentification { get; set; } = true;

		public bool ShowBilingual { get; set; } = true;

        public TextShowOption TextShowOption { get; set; } = TextShowOption.LeftRight;

        /// <summary>
        /// Max items stored for  search and index text
        /// </summary>
        public int MaxExpressionsStored { get; set; } = 50;

		public List<string> SearchStrings { get; set; } = new List<string>();

		public List<string> IndexLetters { get; set; } = new List<string>();

		public bool SimpleSearchIncludePartI { get; set; } = true;

		public bool SimpleSearchIncludePartII { get; set; } = true;

		public bool SimpleSearchIncludePartIII { get; set; } = true;

		public bool SimpleSearchIncludePartIV { get; set; } = true;

		public bool SimpleSearchCurrentPaperOnly { get; set; } = false;

		public double SpliterDistance { get; set; } = 550;  // BUG: Default value needs to be proportional to user screen resolution

		public List<string> SearchIndexEntries { get; set; } = new List<string>();

		public List<TOC_Entry> TrackEntries { get; set; } = new List<TOC_Entry>();

		public string LastTrackFileSaved { get; set; } = "";


		public string InputHtmlFilesPath { get; set; } = "";

		public string IndexDownloadedFiles { get; set; } = "";

		public string IndexOutputFilesPath { get; set; } = "";

		public string SqlServerConnectionString { get; set; }

		public virtual ColorSerial HighlightColor { get; set; } = new ColorSerial(0, 0, 102, 255); // rgb(0, 102, 255)

		// Quick search
		public string SearchFor { get; set; } = "";

		public string SimilarSearchFor { get; set; } = "";

		public string CloseSearchDistance { get; set; } = "5";

		public string CloseSearchFirstWord { get; set; } = "";

		public string CloseSearchSecondWord { get; set; } = "";

		public List<string> CloseSearchWords { get; set; } = new List<string>();

		public short CurrentTranslation { get; set; } = 0;

		public double AnnotationWindowWidth { get; set; } = 800;

		public double AnnotationWindowHeight { get; set; } = 450;

	
		/// <summary>
		/// Current data folder
		/// </summary>
		public string ApplicationDataFolder { get; set; } = "";

        /// <summary>
        /// Folder to store local lucene index search data
        /// </summary>
        public string IndexSearchFolders { get; set; } = "";

        /// <summary>
        /// Folder to store local lucene TUB search data
        /// </summary>
        public string TubSearchFolders { get; set; } = "";

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

		/// <summary>
		/// Github paragraphs repository
		/// </summary>
		public string UrlRepository { get; set; } = null;

        public float FontSize { get; set; } = 10;

        public string FontFamily { get; set; } = "Georgia,Verdana,Arial,Helvetica";

        public bool IsDarkTheme { get; set; } = true;

        public short LastGTPPaper { get; set; } = 101;


 
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
