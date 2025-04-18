﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace UbStudyHelpGenerator.UbStandardObjects.Objects
{
    public class TOC_Entry
    {
        public short TranslationId { get; set; } = 0;
        public short Paper { get; set; } = 0;
        public short Section { get; set; } = 1;
        public short ParagraphNo { get; set; } = 1;
        public short Page { get; set; }
        public short Line { get; set; }
        public string Text { get; set; } = "";
        public bool IsExpanded { get; set; } = false;

        public static TOC_Entry CreateEntry(TOC_Entry entry, short newTranslationId)
        {
            return new TOC_Entry(newTranslationId, entry.Paper, entry.Section, entry.ParagraphNo, entry.Page, entry.Line);
        }

        public static TOC_Entry FromHref(string href)
        {
            TOC_Entry entry = new TOC_Entry();
            try
            {
                char[] sep = { ';', ':', '.', '-', '_', ' ' };
                string[] parts = href.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                switch (parts.Length)
                {
                    case 0:
                        break;
                    case 1:
                        entry.Paper = Convert.ToInt16(parts[0]);
                        entry.Section = 0;
                        entry.ParagraphNo = 1;
                        break;
                    case 2:
                        entry.Paper = Convert.ToInt16(parts[0]);
                        entry.Section = Convert.ToInt16(parts[1]);
                        entry.ParagraphNo = 1;
                        break;
                    default:
                        entry.Paper = Convert.ToInt16(parts[0]);
                        entry.Section = Convert.ToInt16(parts[1]);
                        entry.ParagraphNo = Convert.ToInt16(parts[2]);
                        break;
                }
            }
            catch
            {
                // In case of exeception, the entry is returned with what it already has
            }
            return entry;
        }


        [JsonIgnore]
        public string ParagraphID
        {
            get
            {
                return $"{Paper}:{Section}-{ParagraphNo} ({Page}.{Line})";
            }
        }

        [JsonIgnore]
        public string ParagraphIDNoPage
        {
            get
            {
                return $"{Paper}:{Section}-{ParagraphNo}";
            }
        }


        [JsonIgnore]
        public string Anchor
        {
            get
            {
                return $"U{Paper}_{Section}_{ParagraphNo}";
            }
        }

        [JsonIgnore]
        public string Ident
        {
            get
            {
                return $"{Paper}:{Section}-{ParagraphNo}";
            }
        }

        [JsonIgnore]
        public string Href
        {
            get
            {
                return $"{Paper};{Section};{ParagraphNo}";
            }
        }

        [JsonIgnore]
        public string AsIndex
        {
            get
            {
                return $"{Paper:000}{Section:000}{ParagraphNo:000}";
            }
        }


        [JsonIgnore]
        public string Description
        {
            get
            {
                Translation trans = StaticObjects.Book.Translations.Find(t => t.LanguageID == TranslationId);
                return $"{trans} - {ToString()}";
            }
        }



        /// <summary>
        /// Provides a xml serialization
        /// </summary>
        [JsonIgnore]
        public XmlElement Xml
        {
            get
            {
                XmlSerializer xsSubmit = new XmlSerializer(typeof(TOC_Entry));
                var xml = "";
                using (var sww = new StringWriter())
                {
                    using (XmlWriter writer = XmlWriter.Create(sww))
                    {
                        xsSubmit.Serialize(writer, this);
                        xml = sww.ToString(); // Your XML
                    }
                }

                return GetElement(xml);
            }
        }

        /// <summary>
        /// Provide a list of paper for this entrey when the entry is an entry paper
        /// </summary>
        [JsonIgnore]
        public List<TOC_Entry> Papers { get; set; } = new List<TOC_Entry>();

        /// <summary>
        /// Provide a list of sections for this entrey when the entry is an entry paper
        /// </summary>
        [JsonIgnore]
        public List<TOC_Entry> Sections { get; set; } = new List<TOC_Entry>();



        /// <summary>
        /// Parameterless constructor used for xml serialization
        /// </summary>
        public TOC_Entry()
        {

        }


        public TOC_Entry(Paragraph p, string text= null)
        {
            Text = text == null? p.Text : text;
            TranslationId = p.TranslationId;
            Paper = p.Paper;
            Section = p.Section;
            ParagraphNo = p.ParagraphNo;
            Page = p.Page;
            Line = p.Line;
        }


        public TOC_Entry(short translationId, short paper, short section, short paragraphNo, short page, short line)
        {
            TranslationId = translationId;
            Paper = paper;
            Section = section;
            ParagraphNo = paragraphNo;
            Page = page;
            Line = line;
            Text = "";
            IsExpanded = false;
        }

        public static TOC_Entry FromReference(string reference, ref string aMessage)
        {
            TOC_Entry entry = null;

            try
            {
                char[] sep = { ':', '-', '.', ' ' };
                string[] parts = reference.Split(sep, StringSplitOptions.RemoveEmptyEntries);
                entry = new TOC_Entry();
                entry.TranslationId = StaticObjects.Parameters.CurrentTranslation;
                entry.Paper = Convert.ToInt16(parts[0]);
                entry.Section = Convert.ToInt16(parts[1]);
                entry.ParagraphNo = Convert.ToInt16(parts[2]);
                entry.Page = 0;
                entry.Line = 0;
                entry.Text = "";
                entry.IsExpanded = false;
            }
            catch
            {
                aMessage = $"Invalid paragraph reference {reference}. It should type a valid combination of Paper/Section/Paragrap, separated by : . - or spaces";
                return null;
            }

            try
            {
                Paragraph par = StaticObjects.Book.GetTranslation(StaticObjects.Parameters.CurrentTranslation).Paper(entry.Paper).GetParagraph(entry);
                entry.Text = par.Text;
                aMessage = $"Jumping to {reference}.";
                return entry;
            }
            catch
            {
                aMessage = $"Paragraph not found {reference}. Try using an exiting paragraph reference";
                return null;
            }
        }


        protected bool SamePaperSection(TOC_Entry index)
        {
            return index.Paper == Paper && index.Section == Section && index.IsExpanded;
        }

        public bool SameTranslationPaper(TOC_Entry index)
        {
            return index.Paper == Paper && index.TranslationId == TranslationId;
        }

        public void CheckOldExpanded(List<TOC_Entry> listOldExpanded)
        {
            IsExpanded = listOldExpanded.Exists(SamePaperSection);
        }


        private static XmlElement GetElement(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc.DocumentElement;
        }

        #region Jump routines

        public static TOC_Entry FirstPaper(TOC_Entry entry)
        {
            entry.Paper = StaticObjects.Book.EnglishTranslation.FirstPaper(entry.Paper);
            entry.Section = 0;
            entry.ParagraphNo = 1;
            return entry;
        }


        /// <summary>
        /// Jump to previous paper ot the last
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static TOC_Entry PreviousPaper(TOC_Entry entry)
        {
            entry.Paper -= 1;
            entry.Section = 0;
            entry.ParagraphNo = 1;
            int index = StaticObjects.Book.EnglishTranslation.AllEntries().IndexOf(entry);
            // If not found or already in the first, return the last
            if (index <= 0)
            {
                entry.Paper = StaticObjects.Book.EnglishTranslation.AllEntries()[StaticObjects.Book.EnglishTranslation.AllEntries().Count - 1].Paper;
            }
            return entry;
        }


        /// <summary>
        /// Calculate the first paragraph in the book
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static TOC_Entry PreviousHRef(TOC_Entry entry)
        {
            int index = StaticObjects.Book.EnglishTranslation.AllEntries().IndexOf(entry);
            // If not found return the first
            if (index == -1) return new TOC_Entry();
            // If already in the first, return the last
            if (index == 0) return StaticObjects.Book.EnglishTranslation.AllEntries()[StaticObjects.Book.EnglishTranslation.AllEntries().Count - 1];
            return StaticObjects.Book.EnglishTranslation.AllEntries()[index - 1];
        }


        /// <summary>
        /// Calculate the next paragraph in the book
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static TOC_Entry NextHRef(TOC_Entry entry)
        {
            int index = StaticObjects.Book.EnglishTranslation.AllEntries().IndexOf(entry);
            // If not found or already in the last, return the first
            if (index == -1 || index == StaticObjects.Book.EnglishTranslation.AllEntries().Count - 1) return new TOC_Entry();
            // If in the first, return the last
            if (index == 0) return StaticObjects.Book.EnglishTranslation.AllEntries()[StaticObjects.Book.EnglishTranslation.AllEntries().Count - 1];
            return StaticObjects.Book.EnglishTranslation.AllEntries()[index + 1];
        }

        /// <summary>
        /// Jump to next paper or the first
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static TOC_Entry NextPaper(TOC_Entry entry)
        {
            entry.Paper += 1;
            entry.Section = 0;
            entry.ParagraphNo = 1;
            int index = StaticObjects.Book.EnglishTranslation.AllEntries().IndexOf(entry);
            // If not found or already in the last, return the first
            if (index == -1 || index == StaticObjects.Book.EnglishTranslation.AllEntries().Count - 1) return new TOC_Entry();
            // If found, return it
            return StaticObjects.Book.EnglishTranslation.AllEntries()[index];
        }


        public static TOC_Entry LastPaper(TOC_Entry entry)
        {
            entry.Paper = StaticObjects.Book.EnglishTranslation.LastPaper(entry.Paper);
            entry.Section = 0;
            entry.ParagraphNo = 1;
            return entry;
        }



        #endregion


        #region Operators
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            TOC_Entry entry = obj as TOC_Entry;
            if (entry == null) return false;
            if (entry.TranslationId != this.TranslationId) return false;
            if (entry.Paper != this.Paper) return false;
            if (entry.Section != Section) return false;
            if (entry.ParagraphNo != ParagraphNo) return false;
            return true;
        }

        public static bool operator ==(TOC_Entry e1, TOC_Entry e2)
        {
            if (System.Object.ReferenceEquals(e1, e2)) return true;
            if ((object)e1 == null || (object)e2 == null) return false;
            return e1.Equals(e2);
        }

        /// <summary>
        /// Special operator that ignores the translation Id
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        public static bool operator * (TOC_Entry e1, TOC_Entry e2)
        {
            if (System.Object.ReferenceEquals(e1, e2)) return true;
            if ((object)e1 == null || (object)e2 == null) return false;
            if (e1.Paper != e2.Paper) return false;
            if (e1.Section != e2.Section) return false;
            if (e1.ParagraphNo != e2.ParagraphNo) return false;
            return true;
        }


        public static bool operator !=(TOC_Entry e1, TOC_Entry e2)
        {
            return !(e1 == e2);
        }

        public static bool operator <(TOC_Entry e1, TOC_Entry e2)
        {
            if (System.Object.ReferenceEquals(e1, e2)) return false;
            if ((object)e1 == null || (object)e2 == null) return false;

            if (e1.Paper < e2.Paper) return true;
            if (e1.Paper > e2.Paper) return false;

            if (e1.Section < e2.Section) return true;
            if (e1.Section > e2.Section) return false;

            if (e1.ParagraphNo < e2.ParagraphNo) return true;
            return false;
        }

        public static bool operator >(TOC_Entry e1, TOC_Entry e2)
        {
            if (System.Object.ReferenceEquals(e1, e2)) return false;
            if ((object)e1 == null || (object)e2 == null) return false;

            if (e1.Paper > e2.Paper) return true;
            if (e1.Paper < e2.Paper) return false;

            if (e1.Section > e2.Section) return true;
            if (e1.Section < e2.Section) return false;

            if (e1.ParagraphNo > e2.ParagraphNo) return true;
            return false;
        }

        public int CompareTo(TOC_Entry entry)
        {
            if (this == entry)
            {
                return 0;
            }
            if (this < entry)
            {
                return -1;
            }
            return 1;
        }

        public int InverseCompareTo(TOC_Entry entry)
        {
            if (this == entry)
            {
                return 0;
            }
            if (this < entry)
            {
                return 1;
            }
            return -1;
        }


        public override int GetHashCode()
        {
            return Paper * 100000 + Section * 1000 + ParagraphNo;
        }
        #endregion

        public override string ToString()
        {
            return $"{ParagraphID} {Text}";
        }


    }
}
