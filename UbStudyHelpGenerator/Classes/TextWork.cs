using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace UbStudyHelpGenerator.Classes
{

    public enum TextTag
    {
        Normal,
        Bold,
        Italic,
        Superscript,
        Highlighted
    }

    public class UbTextTag
    {
        public TextTag Tag { get; set; } = TextTag.Normal;
        public string Text { get; set; } = "";
        public UbTextTag(TextTag tag, string text)
        {
            Tag = tag;
            Text = text;
        }
    }



    /// <summary>
    /// Works with a text string to remove html tags and create an element list for WPF Inline
    /// </summary>
    public class TextWork
    {
        private const int maxTextSixe = 80;

        private StringBuilder sbText;


        private Dictionary<string, string> EncodedCharacters = new Dictionary<string, string>()
        {
            {"&amp;",  "&"},
            {"&lt;",   "<"},
            {"&gt;",   ">"},
            {"&quot;", "\""},
            {"&apos;", "'"}
        };


        private class HtmlTag
        {
            public const string markWithoutTag = "|~S~|";
            public const string indicatorTag = "|~I~|";

            public static string[] Separators = { markWithoutTag };

            public string Start { get; set; }
            public string End { get; set; }
            public TextTag Tag { get; set; }

            public string MarkStart
            {
                get
                {
                    return markWithoutTag + indicatorTag + ((int)Tag).ToString("00");
                }
            }

            public string MarkEnd
            {
                get
                {
                    return markWithoutTag;
                }
            }

            public HtmlTag(string start, string end, TextTag tag)
            {
                Start = start;
                End = end;
                Tag = tag;
            }

        }

        private List<HtmlTag> HtmlTags = new List<HtmlTag>()
        {
            new HtmlTag("<b>", "</b>",  TextTag.Bold),
            new HtmlTag("<em>", "</em>",  TextTag.Italic),
            new HtmlTag("<sup>", "</sup>",  TextTag.Superscript),
            new HtmlTag("<word>", "</word>", TextTag.Highlighted)
        };


        private string DecodedText
        {
            get
            {
                return sbText.ToString();
            }
        }

        private int MaxSize
        {
            get
            {
                return maxTextSixe;
            }
        }

        public TextWork()
        {
        }

        public TextWork(string text)
        {
            LoadText(text);
        }




        private string RemoveAll(string input, string toReplace, string newValue)
        {
            return Regex.Replace(input, toReplace, newValue);
        }



        #region Eliminate accentued letters

        /// <summary>
        /// Replaces Accented Characters with Closest Equivalents
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        /// Test line:   string result = ToSimpleCharacters("áéíóú ÁÉÍÓÚ Çç  Ü ü Ö ö Ñ ñ ÄËÏÖÜ äëïöü ");
        /// <remarks>Based on code from: 
        /// http://blogs.msdn.com/b/michkap/archive/2007/05/14/2629747.aspx</remarks>
        private string _ToSimpleCharacters = null;
        protected string ToSimpleCharacters(string original)
        {
            if (!string.IsNullOrEmpty(_ToSimpleCharacters)) return _ToSimpleCharacters;
            if (string.IsNullOrEmpty(original)) return string.Empty;
            string stFormD = original.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);

                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    if (Lookup.ContainsKey(stFormD[ich]))
                    {
                        sb.Append(Lookup[stFormD[ich]]);
                    }
                    else
                    {
                        sb.Append(stFormD[ich]);
                    }
                }
            }
            _ToSimpleCharacters = (sb.ToString().Normalize(NormalizationForm.FormC));
            return _ToSimpleCharacters;
        }

        private Dictionary<char, string> _lookup;

        [JsonIgnore]
        private Dictionary<char, string> Lookup
        {
            get
            {
                if (_lookup == null)
                {
                    _lookup = new Dictionary<char, string>();
                    _lookup[char.ConvertFromUtf32(230)[0]] = "ae";//_lookup['æ']="ae";
                    _lookup[char.ConvertFromUtf32(198)[0]] = "Ae";//_lookup['Æ']="Ae";
                    _lookup[char.ConvertFromUtf32(240)[0]] = "d";//_lookup['ð']="d";
                }
                return _lookup;
            }
        }


        #endregion


        public void LoadText(string text)
        {
            /*
             Encoded chars
                  &lt;    <
                  &gt;    >

             Possible Html tags found in the text
                  "<span class=\"SCaps\">", "</span>"
                  "<b>", "</b>"
                  "<em>", "</em>"
                  "<br />"
                  "<sup>", "</sup>"
                  <span class="Colored">
                  </span>
            */
            sbText = new StringBuilder(SecurityElement.Escape(text));
            foreach (KeyValuePair<string, string> pair in EncodedCharacters)
            {
                sbText.Replace(pair.Key, pair.Value);
            }
            sbText.Replace("<span class=\"Colored\">", "");
            sbText.Replace("<span class=\"SCaps\">", "");
            sbText.Replace("</span>", "");
        }


        /// <summary>
        /// Returns the html fro the current paragraph
        /// </summary>
        /// <returns></returns>
        public string GetHtml()
        {
            return sbText.ToString();
        }


    }
}
