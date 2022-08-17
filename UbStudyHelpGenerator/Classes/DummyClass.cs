using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using UbStandardObjects.Objects;
using UbStudyHelpGenerator.Database;

namespace UbStudyHelpGenerator.Classes
{

    //public class ParagraphXMl
    //{

    //    #region XmlElement functions


    //    protected bool IsNull(XElement elem)
    //    {
    //        if (elem == null) return true;
    //        XAttribute atrib = elem.Attribute("{http://www.w3.org/2001/XMLSchema-instance}nil");
    //        if (atrib != null && atrib.Value == "true") return true;
    //        return false;
    //    }


    //    protected bool GetBool(XElement xElem)
    //    {
    //        if (IsNull(xElem)) return false;
    //        string val = GetString(xElem);
    //        if (string.IsNullOrEmpty(val)) return false;
    //        if (val == "1" || val.ToLower() == "true") return true;
    //        return false;
    //    }


    //    protected short GetShort(XElement xElem)
    //    {
    //        if (IsNull(xElem)) return 0;
    //        short aux = 0;
    //        short.TryParse(xElem.Value, out aux);
    //        return aux;
    //    }

    //    protected string GetString(XElement xElem)
    //    {
    //        if (IsNull(xElem)) return "";
    //        string sReturnedValue = xElem.Value;
    //        if (xElem.HasElements)
    //        {
    //            sReturnedValue = "";
    //            XNode node = xElem.FirstNode;
    //            StringBuilder sb = new StringBuilder();
    //            do
    //            {
    //                switch (node.NodeType)
    //                {
    //                    case XmlNodeType.Text:
    //                        sb.Append((node as XText).Value);
    //                        break;
    //                    case XmlNodeType.Element:
    //                        switch ((node as XElement).Name.LocalName.ToLower())
    //                        {
    //                            case "i":
    //                                sb.Append(node.ToString());
    //                                break;
    //                            case "b":
    //                                sb.Append(node.ToString());
    //                                break;
    //                            case "high":
    //                                sb.Append(node.ToString());
    //                                break;
    //                        }
    //                        break;
    //                }
    //            }
    //            while ((node = node.NextNode) != null);
    //            sReturnedValue = sb.ToString();
    //        }
    //        return sReturnedValue;
    //    }


    //    private Dictionary<string, string> DictionryInvalidXmlCharacters()
    //    {
    //        return new Dictionary<string, string>()
    //             {
    //                 {"&",  "&amp;"},  // must be the first
	   //              {"<",  "&lt;"},
    //                 {">",  "&gt;"},
    //                 {"\"",  "&quot;"},
    //                 {"'", "&apos;"}
    //             };
    //    }

    //    protected string XmlDecodeString(string xmlIn)
    //    {
    //        foreach (KeyValuePair<string, string> pair in DictionryInvalidXmlCharacters())
    //            xmlIn = xmlIn.Replace(pair.Value, pair.Key);
    //        return xmlIn;
    //    }

    //    protected string XmlEncodeString(string xmlIn)
    //    {
    //        foreach (KeyValuePair<string, string> pair in DictionryInvalidXmlCharacters())
    //            xmlIn = xmlIn.Replace(pair.Key, pair.Value);
    //        return xmlIn;
    //    }


    //    protected string XmlToString(XElement xElem)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        XmlWriterSettings xws = new XmlWriterSettings();
    //        xws.OmitXmlDeclaration = true;
    //        xws.Indent = true;
    //        XmlWriter xw = XmlWriter.Create(sb, xws);
    //        xElem.WriteTo(xw);
    //        xw.Flush();
    //        return sb.ToString();
    //    }





    //    #endregion

    //    public short TranslationID { get; set; }
    //    public short Paper { get; set; }
    //    public short PK_Seq { get; set; }
    //    public short Section { get; set; }
    //    public short ParagraphNo { get; set; }
    //    public short Page { get; set; }
    //    public short Line { get; set; }

    //    public string Text { get; set; }

    //    public enHtmlType Format { get; set; }

    //    public TOC_Entry Entry { get; private set; }


    //    public ParagraphXMl(XElement xElemParagraph)
    //    {
    //        TranslationID = GetShort(xElemParagraph.Element("TranslationID"));
    //        Paper = GetShort(xElemParagraph.Element("Paper"));
    //        PK_Seq = GetShort(xElemParagraph.Element("PK_Seq"));
    //        Section = GetShort(xElemParagraph.Element("Section"));
    //        ParagraphNo = GetShort(xElemParagraph.Element("ParagraphNo"));
    //        Page = GetShort(xElemParagraph.Element("Page"));
    //        Line = GetShort(xElemParagraph.Element("Line"));
    //        Text = GetString(xElemParagraph.Element("Text"));
    //        Format = (enHtmlType)GetShort(xElemParagraph.Element("Format"));
    //        Entry = new TOC_Entry(TranslationID, Paper, Section, ParagraphNo, Page, Line);
    //    }


    //    public string Identification
    //    {
    //        get
    //        {
    //            return string.Format("{0}:{1}-{2} ({3}.{4})", Paper, Section, ParagraphNo, Page, Line); ;
    //        }
    //    }

    //    public string AName
    //    {
    //        get
    //        {
    //            return string.Format("U{0}_{1}_{2}", Paper, Section, ParagraphNo); ;
    //        }
    //    }




    //    public override string ToString()
    //    {
    //        string partText = string.IsNullOrEmpty(Text) ? "" : Text;
    //        return $"{Identification} {partText}";
    //    }

    //}




    //internal class PaperXml
    //{
    //    public PaperXml(string PathXmlFile, UbtDatabaseSqlServer server)
    //    {

    //        XElement xElem = XElement.Load(PathXmlFile);

    //        foreach (XElement xElemPar in xElem.Descendants("Paragraph"))
    //        {
    //            ParagraphXMl p = new ParagraphXMl(xElemPar);
    //            //string command = $"INSERT INTO [dbo].[NewParDescr]([Paper],[PK_Seq],[Section],[Paragraph]) VALUES({p.Paper}, {p.PK_Seq}, {p.Section}, {p.ParagraphNo})";
    //            string command = $"UPDATE [dbo].[NewParDescr] SET [Format] = {(int)p.Format} WHERE [Paper] = {p.Paper} and [PK_Seq] = {p.PK_Seq}";
    //            // 
    //            int count = server.RunCommand(command);
    //            if (count < 1)
    //            {
    //                Debug.WriteLine($"ERROR:  {command}");
    //            }
    //        }
    //    }

    //}


    //internal class DummyClass
    //{
    //    public void Gera()
    //    {
    //        UbtDatabaseSqlServer server = new UbtDatabaseSqlServer();
    //        string pathXmlFiles = @"C:\Trabalho\Lixo\L000";
    //        for (int paper = 0; paper < 197; paper++)
    //        {
    //            string pathPaper = Path.Combine(pathXmlFiles, "Paper" + paper.ToString("000") + ".xml");
    //            PaperXml paperXml = new PaperXml(pathPaper, server);
    //        }
    //    }
    //}
}
