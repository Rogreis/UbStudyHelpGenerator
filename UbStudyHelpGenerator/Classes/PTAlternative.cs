﻿using System;
using System.IO;
using System.Text.Json;
using System.Drawing;
using UbStandardObjects.Objects;
using System.Text;
using UbStudyHelpGenerator.Database;
using System.Collections.Generic;
using UBT_Tools_WorkLib;
using UbStandardObjects;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace UbStudyHelpGenerator.Classes
{



    public class PT_AlternativeRecords
    {
        public PT_AlternativeRecord[] Paragraphs { get; set; }
    }

    public class PT_AlternativeRecord
    {
        public int IndexWorK { get; set; }
        public int TranslationID { get; set; }
        public int Paper { get; set; }
        public int PK_Seq { get; set; }
        public int Section { get; set; }
        public int ParagraphNo { get; set; }
        public int Page { get; set; }
        public int Line { get; set; }
        public int FormatInt { get; set; }
        public string Text { get; set; }
        public int Status { get; set; }
        public string English { get; set; }
        public string Portugues2007 { get; set; }

        public string Identification
        {
            get
            {
                return string.Format("{0}:{1}-{2} ({3}.{4})", Paper, Section, ParagraphNo, Page, Line); ;
            }
        }

        public string AName
        {
            get
            {
                return string.Format("U{0}_{1}_{2}", Paper, Section, ParagraphNo); ;
            }
        }

        public bool IsPaperTitle
        {
            get
            {
                return Section == 0 && ParagraphNo == 0;
            }
        }

        public bool IsSectionTitle
        {
            get
            {
                return ParagraphNo == 0;
            }
        }

        public string ParaIdent
        {
            get
            {
                return Paper.ToString("000") + PK_Seq.ToString("000");
            }
        }

        public ParagraphStatus TranslationStatus
        {
            get
            {
                return (ParagraphStatus)Status;
            }
        }


    }

    // »»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»»

    #region Xml Classes

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/office/2006/xmlPackage")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/office/2006/xmlPackage", IsNullable = false)]
    public partial class package
    {

        private packagePart[] partField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("part")]
        public packagePart[] part
        {
            get
            {
                return this.partField;
            }
            set
            {
                this.partField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/office/2006/xmlPackage")]
    public partial class packagePart
    {

        private packagePartXmlData xmlDataField;

        private string nameField;

        private string contentTypeField;

        private ushort paddingField;

        private bool paddingFieldSpecified;

        /// <remarks/>
        public packagePartXmlData xmlData
        {
            get
            {
                return this.xmlDataField;
            }
            set
            {
                this.xmlDataField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string contentType
        {
            get
            {
                return this.contentTypeField;
            }
            set
            {
                this.contentTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort padding
        {
            get
            {
                return this.paddingField;
            }
            set
            {
                this.paddingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool paddingSpecified
        {
            get
            {
                return this.paddingFieldSpecified;
            }
            set
            {
                this.paddingFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/office/2006/xmlPackage")]
    public partial class packagePartXmlData
    {

        private Properties propertiesField;

        private coreProperties corePropertiesField;

        private fonts fontsField;

        private webSettings webSettingsField;

        private styles stylesField;

        private datastoreItem datastoreItemField;

        private gDocsCustomXmlDataStorage gDocsCustomXmlDataStorageField;

        private settings settingsField;

        private theme themeField;

        private document documentField;

        private RelationshipsRelationship[] relationshipsField;

        private string originalXmlStandaloneField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties")]
        public Properties Properties
        {
            get
            {
                return this.propertiesField;
            }
            set
            {
                this.propertiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/package/2006/metadata/core-properties")]
        public coreProperties coreProperties
        {
            get
            {
                return this.corePropertiesField;
            }
            set
            {
                this.corePropertiesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
        public fonts fonts
        {
            get
            {
                return this.fontsField;
            }
            set
            {
                this.fontsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
        public webSettings webSettings
        {
            get
            {
                return this.webSettingsField;
            }
            set
            {
                this.webSettingsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
        public styles styles
        {
            get
            {
                return this.stylesField;
            }
            set
            {
                this.stylesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml")]
        public datastoreItem datastoreItem
        {
            get
            {
                return this.datastoreItemField;
            }
            set
            {
                this.datastoreItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://customooxmlschemas.google.com/")]
        public gDocsCustomXmlDataStorage gDocsCustomXmlDataStorage
        {
            get
            {
                return this.gDocsCustomXmlDataStorageField;
            }
            set
            {
                this.gDocsCustomXmlDataStorageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
        public settings settings
        {
            get
            {
                return this.settingsField;
            }
            set
            {
                this.settingsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
        public theme theme
        {
            get
            {
                return this.themeField;
            }
            set
            {
                this.themeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
        public document document
        {
            get
            {
                return this.documentField;
            }
            set
            {
                this.documentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://schemas.openxmlformats.org/package/2006/relationships")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Relationship", IsNullable = false)]
        public RelationshipsRelationship[] Relationships
        {
            get
            {
                return this.relationshipsField;
            }
            set
            {
                this.relationshipsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string originalXmlStandalone
        {
            get
            {
                return this.originalXmlStandaloneField;
            }
            set
            {
                this.originalXmlStandaloneField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", IsNullable = false)]
    public partial class Properties
    {

        private string templateField;

        private byte totalTimeField;

        private byte pagesField;

        private ushort wordsField;

        private uint charactersField;

        private string applicationField;

        private byte docSecurityField;

        private ushort linesField;

        private byte paragraphsField;

        private bool scaleCropField;

        private object companyField;

        private bool linksUpToDateField;

        private uint charactersWithSpacesField;

        private bool sharedDocField;

        private bool hyperlinksChangedField;

        private decimal appVersionField;

        /// <remarks/>
        public string Template
        {
            get
            {
                return this.templateField;
            }
            set
            {
                this.templateField = value;
            }
        }

        /// <remarks/>
        public byte TotalTime
        {
            get
            {
                return this.totalTimeField;
            }
            set
            {
                this.totalTimeField = value;
            }
        }

        /// <remarks/>
        public byte Pages
        {
            get
            {
                return this.pagesField;
            }
            set
            {
                this.pagesField = value;
            }
        }

        /// <remarks/>
        public ushort Words
        {
            get
            {
                return this.wordsField;
            }
            set
            {
                this.wordsField = value;
            }
        }

        /// <remarks/>
        public uint Characters
        {
            get
            {
                return this.charactersField;
            }
            set
            {
                this.charactersField = value;
            }
        }

        /// <remarks/>
        public string Application
        {
            get
            {
                return this.applicationField;
            }
            set
            {
                this.applicationField = value;
            }
        }

        /// <remarks/>
        public byte DocSecurity
        {
            get
            {
                return this.docSecurityField;
            }
            set
            {
                this.docSecurityField = value;
            }
        }

        /// <remarks/>
        public ushort Lines
        {
            get
            {
                return this.linesField;
            }
            set
            {
                this.linesField = value;
            }
        }

        /// <remarks/>
        public byte Paragraphs
        {
            get
            {
                return this.paragraphsField;
            }
            set
            {
                this.paragraphsField = value;
            }
        }

        /// <remarks/>
        public bool ScaleCrop
        {
            get
            {
                return this.scaleCropField;
            }
            set
            {
                this.scaleCropField = value;
            }
        }

        /// <remarks/>
        public object Company
        {
            get
            {
                return this.companyField;
            }
            set
            {
                this.companyField = value;
            }
        }

        /// <remarks/>
        public bool LinksUpToDate
        {
            get
            {
                return this.linksUpToDateField;
            }
            set
            {
                this.linksUpToDateField = value;
            }
        }

        /// <remarks/>
        public uint CharactersWithSpaces
        {
            get
            {
                return this.charactersWithSpacesField;
            }
            set
            {
                this.charactersWithSpacesField = value;
            }
        }

        /// <remarks/>
        public bool SharedDoc
        {
            get
            {
                return this.sharedDocField;
            }
            set
            {
                this.sharedDocField = value;
            }
        }

        /// <remarks/>
        public bool HyperlinksChanged
        {
            get
            {
                return this.hyperlinksChangedField;
            }
            set
            {
                this.hyperlinksChangedField = value;
            }
        }

        /// <remarks/>
        public decimal AppVersion
        {
            get
            {
                return this.appVersionField;
            }
            set
            {
                this.appVersionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/package/2006/metadata/core-properties")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/package/2006/metadata/core-properties", IsNullable = false)]
    public partial class coreProperties
    {

        private object titleField;

        private string creatorField;

        private string lastModifiedByField;

        private byte revisionField;

        private System.DateTime createdField;

        private System.DateTime modifiedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://purl.org/dc/elements/1.1/")]
        public object title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://purl.org/dc/elements/1.1/")]
        public string creator
        {
            get
            {
                return this.creatorField;
            }
            set
            {
                this.creatorField = value;
            }
        }

        /// <remarks/>
        public string lastModifiedBy
        {
            get
            {
                return this.lastModifiedByField;
            }
            set
            {
                this.lastModifiedByField = value;
            }
        }

        /// <remarks/>
        public byte revision
        {
            get
            {
                return this.revisionField;
            }
            set
            {
                this.revisionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://purl.org/dc/terms/")]
        public System.DateTime created
        {
            get
            {
                return this.createdField;
            }
            set
            {
                this.createdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://purl.org/dc/terms/")]
        public System.DateTime modified
        {
            get
            {
                return this.modifiedField;
            }
            set
            {
                this.modifiedField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
    public partial class fonts
    {

        private fontsFont[] fontField;

        private string ignorableField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("font")]
        public fontsFont[] font
        {
            get
            {
                return this.fontField;
            }
            set
            {
                this.fontField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/markup-compatibility/2006")]
        public string Ignorable
        {
            get
            {
                return this.ignorableField;
            }
            set
            {
                this.ignorableField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class fontsFont
    {

        private fontsFontPanose1 panose1Field;

        private fontsFontCharset charsetField;

        private fontsFontFamily familyField;

        private fontsFontPitch pitchField;

        private fontsFontSig sigField;

        private string nameField;

        /// <remarks/>
        public fontsFontPanose1 panose1
        {
            get
            {
                return this.panose1Field;
            }
            set
            {
                this.panose1Field = value;
            }
        }

        /// <remarks/>
        public fontsFontCharset charset
        {
            get
            {
                return this.charsetField;
            }
            set
            {
                this.charsetField = value;
            }
        }

        /// <remarks/>
        public fontsFontFamily family
        {
            get
            {
                return this.familyField;
            }
            set
            {
                this.familyField = value;
            }
        }

        /// <remarks/>
        public fontsFontPitch pitch
        {
            get
            {
                return this.pitchField;
            }
            set
            {
                this.pitchField = value;
            }
        }

        /// <remarks/>
        public fontsFontSig sig
        {
            get
            {
                return this.sigField;
            }
            set
            {
                this.sigField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class fontsFontPanose1
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class fontsFontCharset
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class fontsFontFamily
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class fontsFontPitch
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class fontsFontSig
    {

        private string usb0Field;

        private string usb1Field;

        private byte usb2Field;

        private byte usb3Field;

        private string csb0Field;

        private byte csb1Field;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string usb0
        {
            get
            {
                return this.usb0Field;
            }
            set
            {
                this.usb0Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string usb1
        {
            get
            {
                return this.usb1Field;
            }
            set
            {
                this.usb1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte usb2
        {
            get
            {
                return this.usb2Field;
            }
            set
            {
                this.usb2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte usb3
        {
            get
            {
                return this.usb3Field;
            }
            set
            {
                this.usb3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string csb0
        {
            get
            {
                return this.csb0Field;
            }
            set
            {
                this.csb0Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte csb1
        {
            get
            {
                return this.csb1Field;
            }
            set
            {
                this.csb1Field = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
    public partial class webSettings
    {

        private string ignorableField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/markup-compatibility/2006")]
        public string Ignorable
        {
            get
            {
                return this.ignorableField;
            }
            set
            {
                this.ignorableField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
    public partial class styles
    {

        private stylesDocDefaults docDefaultsField;

        private stylesLatentStyles latentStylesField;

        private stylesStyle[] styleField;

        private string ignorableField;

        /// <remarks/>
        public stylesDocDefaults docDefaults
        {
            get
            {
                return this.docDefaultsField;
            }
            set
            {
                this.docDefaultsField = value;
            }
        }

        /// <remarks/>
        public stylesLatentStyles latentStyles
        {
            get
            {
                return this.latentStylesField;
            }
            set
            {
                this.latentStylesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("style")]
        public stylesStyle[] style
        {
            get
            {
                return this.styleField;
            }
            set
            {
                this.styleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/markup-compatibility/2006")]
        public string Ignorable
        {
            get
            {
                return this.ignorableField;
            }
            set
            {
                this.ignorableField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesDocDefaults
    {

        private stylesDocDefaultsRPrDefault rPrDefaultField;

        private object pPrDefaultField;

        /// <remarks/>
        public stylesDocDefaultsRPrDefault rPrDefault
        {
            get
            {
                return this.rPrDefaultField;
            }
            set
            {
                this.rPrDefaultField = value;
            }
        }

        /// <remarks/>
        public object pPrDefault
        {
            get
            {
                return this.pPrDefaultField;
            }
            set
            {
                this.pPrDefaultField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesDocDefaultsRPrDefault
    {

        private stylesDocDefaultsRPrDefaultRPr rPrField;

        /// <remarks/>
        public stylesDocDefaultsRPrDefaultRPr rPr
        {
            get
            {
                return this.rPrField;
            }
            set
            {
                this.rPrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesDocDefaultsRPrDefaultRPr
    {

        private stylesDocDefaultsRPrDefaultRPrRFonts rFontsField;

        private stylesDocDefaultsRPrDefaultRPrSZ szField;

        private stylesDocDefaultsRPrDefaultRPrSzCs szCsField;

        private stylesDocDefaultsRPrDefaultRPrLang langField;

        /// <remarks/>
        public stylesDocDefaultsRPrDefaultRPrRFonts rFonts
        {
            get
            {
                return this.rFontsField;
            }
            set
            {
                this.rFontsField = value;
            }
        }

        /// <remarks/>
        public stylesDocDefaultsRPrDefaultRPrSZ sz
        {
            get
            {
                return this.szField;
            }
            set
            {
                this.szField = value;
            }
        }

        /// <remarks/>
        public stylesDocDefaultsRPrDefaultRPrSzCs szCs
        {
            get
            {
                return this.szCsField;
            }
            set
            {
                this.szCsField = value;
            }
        }

        /// <remarks/>
        public stylesDocDefaultsRPrDefaultRPrLang lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesDocDefaultsRPrDefaultRPrRFonts
    {

        private string asciiField;

        private string eastAsiaField;

        private string hAnsiField;

        private string csField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string ascii
        {
            get
            {
                return this.asciiField;
            }
            set
            {
                this.asciiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string eastAsia
        {
            get
            {
                return this.eastAsiaField;
            }
            set
            {
                this.eastAsiaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string hAnsi
        {
            get
            {
                return this.hAnsiField;
            }
            set
            {
                this.hAnsiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string cs
        {
            get
            {
                return this.csField;
            }
            set
            {
                this.csField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesDocDefaultsRPrDefaultRPrSZ
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesDocDefaultsRPrDefaultRPrSzCs
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesDocDefaultsRPrDefaultRPrLang
    {

        private string valField;

        private string eastAsiaField;

        private string bidiField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string eastAsia
        {
            get
            {
                return this.eastAsiaField;
            }
            set
            {
                this.eastAsiaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string bidi
        {
            get
            {
                return this.bidiField;
            }
            set
            {
                this.bidiField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesLatentStyles
    {

        private stylesLatentStylesLsdException[] lsdExceptionField;

        private byte defLockedStateField;

        private byte defUIPriorityField;

        private byte defSemiHiddenField;

        private byte defUnhideWhenUsedField;

        private byte defQFormatField;

        private ushort countField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("lsdException")]
        public stylesLatentStylesLsdException[] lsdException
        {
            get
            {
                return this.lsdExceptionField;
            }
            set
            {
                this.lsdExceptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte defLockedState
        {
            get
            {
                return this.defLockedStateField;
            }
            set
            {
                this.defLockedStateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte defUIPriority
        {
            get
            {
                return this.defUIPriorityField;
            }
            set
            {
                this.defUIPriorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte defSemiHidden
        {
            get
            {
                return this.defSemiHiddenField;
            }
            set
            {
                this.defSemiHiddenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte defUnhideWhenUsed
        {
            get
            {
                return this.defUnhideWhenUsedField;
            }
            set
            {
                this.defUnhideWhenUsedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte defQFormat
        {
            get
            {
                return this.defQFormatField;
            }
            set
            {
                this.defQFormatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesLatentStylesLsdException
    {

        private string nameField;

        private byte uiPriorityField;

        private bool uiPriorityFieldSpecified;

        private byte qFormatField;

        private bool qFormatFieldSpecified;

        private byte semiHiddenField;

        private bool semiHiddenFieldSpecified;

        private byte unhideWhenUsedField;

        private bool unhideWhenUsedFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte uiPriority
        {
            get
            {
                return this.uiPriorityField;
            }
            set
            {
                this.uiPriorityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool uiPrioritySpecified
        {
            get
            {
                return this.uiPriorityFieldSpecified;
            }
            set
            {
                this.uiPriorityFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte qFormat
        {
            get
            {
                return this.qFormatField;
            }
            set
            {
                this.qFormatField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool qFormatSpecified
        {
            get
            {
                return this.qFormatFieldSpecified;
            }
            set
            {
                this.qFormatFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte semiHidden
        {
            get
            {
                return this.semiHiddenField;
            }
            set
            {
                this.semiHiddenField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool semiHiddenSpecified
        {
            get
            {
                return this.semiHiddenFieldSpecified;
            }
            set
            {
                this.semiHiddenFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte unhideWhenUsed
        {
            get
            {
                return this.unhideWhenUsedField;
            }
            set
            {
                this.unhideWhenUsedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool unhideWhenUsedSpecified
        {
            get
            {
                return this.unhideWhenUsedFieldSpecified;
            }
            set
            {
                this.unhideWhenUsedFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyle
    {

        private stylesStyleName nameField;

        private stylesStyleBasedOn basedOnField;

        private stylesStyleNext nextField;

        private stylesStyleUiPriority uiPriorityField;

        private object semiHiddenField;

        private object unhideWhenUsedField;

        private stylesStyleTblPr tblPrField;

        private object qFormatField;

        private stylesStylePPr pPrField;

        private stylesStyleRPr rPrField;

        private string typeField;

        private byte defaultField;

        private bool defaultFieldSpecified;

        private string styleIdField;

        private byte customStyleField;

        private bool customStyleFieldSpecified;

        /// <remarks/>
        public stylesStyleName name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public stylesStyleBasedOn basedOn
        {
            get
            {
                return this.basedOnField;
            }
            set
            {
                this.basedOnField = value;
            }
        }

        /// <remarks/>
        public stylesStyleNext next
        {
            get
            {
                return this.nextField;
            }
            set
            {
                this.nextField = value;
            }
        }

        /// <remarks/>
        public stylesStyleUiPriority uiPriority
        {
            get
            {
                return this.uiPriorityField;
            }
            set
            {
                this.uiPriorityField = value;
            }
        }

        /// <remarks/>
        public object semiHidden
        {
            get
            {
                return this.semiHiddenField;
            }
            set
            {
                this.semiHiddenField = value;
            }
        }

        /// <remarks/>
        public object unhideWhenUsed
        {
            get
            {
                return this.unhideWhenUsedField;
            }
            set
            {
                this.unhideWhenUsedField = value;
            }
        }

        /// <remarks/>
        public stylesStyleTblPr tblPr
        {
            get
            {
                return this.tblPrField;
            }
            set
            {
                this.tblPrField = value;
            }
        }

        /// <remarks/>
        public object qFormat
        {
            get
            {
                return this.qFormatField;
            }
            set
            {
                this.qFormatField = value;
            }
        }

        /// <remarks/>
        public stylesStylePPr pPr
        {
            get
            {
                return this.pPrField;
            }
            set
            {
                this.pPrField = value;
            }
        }

        /// <remarks/>
        public stylesStyleRPr rPr
        {
            get
            {
                return this.rPrField;
            }
            set
            {
                this.rPrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte @default
        {
            get
            {
                return this.defaultField;
            }
            set
            {
                this.defaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool defaultSpecified
        {
            get
            {
                return this.defaultFieldSpecified;
            }
            set
            {
                this.defaultFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string styleId
        {
            get
            {
                return this.styleIdField;
            }
            set
            {
                this.styleIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte customStyle
        {
            get
            {
                return this.customStyleField;
            }
            set
            {
                this.customStyleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool customStyleSpecified
        {
            get
            {
                return this.customStyleFieldSpecified;
            }
            set
            {
                this.customStyleFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleName
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleBasedOn
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleNext
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleUiPriority
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleTblPr
    {

        private stylesStyleTblPrTblStyleRowBandSize tblStyleRowBandSizeField;

        private stylesStyleTblPrTblStyleColBandSize tblStyleColBandSizeField;

        private stylesStyleTblPrTblInd tblIndField;

        private stylesStyleTblPrTblCellMar tblCellMarField;

        /// <remarks/>
        public stylesStyleTblPrTblStyleRowBandSize tblStyleRowBandSize
        {
            get
            {
                return this.tblStyleRowBandSizeField;
            }
            set
            {
                this.tblStyleRowBandSizeField = value;
            }
        }

        /// <remarks/>
        public stylesStyleTblPrTblStyleColBandSize tblStyleColBandSize
        {
            get
            {
                return this.tblStyleColBandSizeField;
            }
            set
            {
                this.tblStyleColBandSizeField = value;
            }
        }

        /// <remarks/>
        public stylesStyleTblPrTblInd tblInd
        {
            get
            {
                return this.tblIndField;
            }
            set
            {
                this.tblIndField = value;
            }
        }

        /// <remarks/>
        public stylesStyleTblPrTblCellMar tblCellMar
        {
            get
            {
                return this.tblCellMarField;
            }
            set
            {
                this.tblCellMarField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleTblPrTblStyleRowBandSize
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleTblPrTblStyleColBandSize
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleTblPrTblInd
    {

        private ushort wField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleTblPrTblCellMar
    {

        private stylesStyleTblPrTblCellMarTop topField;

        private stylesStyleTblPrTblCellMarLeft leftField;

        private stylesStyleTblPrTblCellMarBottom bottomField;

        private stylesStyleTblPrTblCellMarRight rightField;

        /// <remarks/>
        public stylesStyleTblPrTblCellMarTop top
        {
            get
            {
                return this.topField;
            }
            set
            {
                this.topField = value;
            }
        }

        /// <remarks/>
        public stylesStyleTblPrTblCellMarLeft left
        {
            get
            {
                return this.leftField;
            }
            set
            {
                this.leftField = value;
            }
        }

        /// <remarks/>
        public stylesStyleTblPrTblCellMarBottom bottom
        {
            get
            {
                return this.bottomField;
            }
            set
            {
                this.bottomField = value;
            }
        }

        /// <remarks/>
        public stylesStyleTblPrTblCellMarRight right
        {
            get
            {
                return this.rightField;
            }
            set
            {
                this.rightField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleTblPrTblCellMarTop
    {

        private ushort wField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleTblPrTblCellMarLeft
    {

        private ushort wField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleTblPrTblCellMarBottom
    {

        private ushort wField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleTblPrTblCellMarRight
    {

        private ushort wField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStylePPr
    {

        private stylesStylePPrShd shdField;

        private object keepNextField;

        private object keepLinesField;

        private stylesStylePPrSpacing spacingField;

        private stylesStylePPrOutlineLvl outlineLvlField;

        /// <remarks/>
        public stylesStylePPrShd shd
        {
            get
            {
                return this.shdField;
            }
            set
            {
                this.shdField = value;
            }
        }

        /// <remarks/>
        public object keepNext
        {
            get
            {
                return this.keepNextField;
            }
            set
            {
                this.keepNextField = value;
            }
        }

        /// <remarks/>
        public object keepLines
        {
            get
            {
                return this.keepLinesField;
            }
            set
            {
                this.keepLinesField = value;
            }
        }

        /// <remarks/>
        public stylesStylePPrSpacing spacing
        {
            get
            {
                return this.spacingField;
            }
            set
            {
                this.spacingField = value;
            }
        }

        /// <remarks/>
        public stylesStylePPrOutlineLvl outlineLvl
        {
            get
            {
                return this.outlineLvlField;
            }
            set
            {
                this.outlineLvlField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStylePPrShd
    {

        private string valField;

        private string colorField;

        private string fillField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string fill
        {
            get
            {
                return this.fillField;
            }
            set
            {
                this.fillField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStylePPrSpacing
    {

        private ushort beforeField;

        private byte afterField;

        private byte beforeAutospacingField;

        private bool beforeAutospacingFieldSpecified;

        private byte afterAutospacingField;

        private bool afterAutospacingFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort before
        {
            get
            {
                return this.beforeField;
            }
            set
            {
                this.beforeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte after
        {
            get
            {
                return this.afterField;
            }
            set
            {
                this.afterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte beforeAutospacing
        {
            get
            {
                return this.beforeAutospacingField;
            }
            set
            {
                this.beforeAutospacingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool beforeAutospacingSpecified
        {
            get
            {
                return this.beforeAutospacingFieldSpecified;
            }
            set
            {
                this.beforeAutospacingFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte afterAutospacing
        {
            get
            {
                return this.afterAutospacingField;
            }
            set
            {
                this.afterAutospacingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool afterAutospacingSpecified
        {
            get
            {
                return this.afterAutospacingFieldSpecified;
            }
            set
            {
                this.afterAutospacingFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStylePPrOutlineLvl
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleRPr
    {

        private object[] itemsField;

        private ItemsChoiceType[] itemsElementNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("bCs", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("color", typeof(stylesStyleRPrColor))]
        [System.Xml.Serialization.XmlElementAttribute("i", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("iCs", typeof(object))]
        [System.Xml.Serialization.XmlElementAttribute("rFonts", typeof(stylesStyleRPrRFonts))]
        [System.Xml.Serialization.XmlElementAttribute("sz", typeof(stylesStyleRPrSZ))]
        [System.Xml.Serialization.XmlElementAttribute("szCs", typeof(stylesStyleRPrSzCs))]
        [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName
        {
            get
            {
                return this.itemsElementNameField;
            }
            set
            {
                this.itemsElementNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleRPrColor
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleRPrRFonts
    {

        private string eastAsiaThemeField;

        private string asciiField;

        private string hAnsiField;

        private string hintField;

        private string eastAsiaField;

        private string csField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string eastAsiaTheme
        {
            get
            {
                return this.eastAsiaThemeField;
            }
            set
            {
                this.eastAsiaThemeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string ascii
        {
            get
            {
                return this.asciiField;
            }
            set
            {
                this.asciiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string hAnsi
        {
            get
            {
                return this.hAnsiField;
            }
            set
            {
                this.hAnsiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string hint
        {
            get
            {
                return this.hintField;
            }
            set
            {
                this.hintField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string eastAsia
        {
            get
            {
                return this.eastAsiaField;
            }
            set
            {
                this.eastAsiaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string cs
        {
            get
            {
                return this.csField;
            }
            set
            {
                this.csField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleRPrSZ
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class stylesStyleRPrSzCs
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IncludeInSchema = false)]
    public enum ItemsChoiceType
    {

        /// <remarks/>
        b,

        /// <remarks/>
        bCs,

        /// <remarks/>
        color,

        /// <remarks/>
        i,

        /// <remarks/>
        iCs,

        /// <remarks/>
        rFonts,

        /// <remarks/>
        sz,

        /// <remarks/>
        szCs,
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml", IsNullable = false)]
    public partial class datastoreItem
    {

        private datastoreItemSchemaRef[] schemaRefsField;

        private string itemIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("schemaRef", IsNullable = false)]
        public datastoreItemSchemaRef[] schemaRefs
        {
            get
            {
                return this.schemaRefsField;
            }
            set
            {
                this.schemaRefsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string itemID
        {
            get
            {
                return this.itemIDField;
            }
            set
            {
                this.itemIDField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/customXml")]
    public partial class datastoreItemSchemaRef
    {

        private string uriField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string uri
        {
            get
            {
                return this.uriField;
            }
            set
            {
                this.uriField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://customooxmlschemas.google.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://customooxmlschemas.google.com/", IsNullable = false)]
    public partial class gDocsCustomXmlDataStorage
    {

        private gDocsCustomXmlDataStorageDocsCustomData docsCustomDataField;

        /// <remarks/>
        public gDocsCustomXmlDataStorageDocsCustomData docsCustomData
        {
            get
            {
                return this.docsCustomDataField;
            }
            set
            {
                this.docsCustomDataField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://customooxmlschemas.google.com/")]
    public partial class gDocsCustomXmlDataStorageDocsCustomData
    {

        private string roundtripDataSignatureField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string roundtripDataSignature
        {
            get
            {
                return this.roundtripDataSignatureField;
            }
            set
            {
                this.roundtripDataSignatureField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
    public partial class settings
    {

        private settingsZoom zoomField;

        private settingsProofState proofStateField;

        private settingsDefaultTabStop defaultTabStopField;

        private settingsCharacterSpacingControl characterSpacingControlField;

        private settingsCompatSetting[] compatField;

        private settingsRsids rsidsField;

        private mathPr mathPrField;

        private settingsThemeFontLang themeFontLangField;

        private settingsClrSchemeMapping clrSchemeMappingField;

        private settingsShapeDefaults shapeDefaultsField;

        private settingsDecimalSymbol decimalSymbolField;

        private settingsListSeparator listSeparatorField;

        private docId docIdField;

        private docId1 docId1Field;

        private string ignorableField;

        /// <remarks/>
        public settingsZoom zoom
        {
            get
            {
                return this.zoomField;
            }
            set
            {
                this.zoomField = value;
            }
        }

        /// <remarks/>
        public settingsProofState proofState
        {
            get
            {
                return this.proofStateField;
            }
            set
            {
                this.proofStateField = value;
            }
        }

        /// <remarks/>
        public settingsDefaultTabStop defaultTabStop
        {
            get
            {
                return this.defaultTabStopField;
            }
            set
            {
                this.defaultTabStopField = value;
            }
        }

        /// <remarks/>
        public settingsCharacterSpacingControl characterSpacingControl
        {
            get
            {
                return this.characterSpacingControlField;
            }
            set
            {
                this.characterSpacingControlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("compatSetting", IsNullable = false)]
        public settingsCompatSetting[] compat
        {
            get
            {
                return this.compatField;
            }
            set
            {
                this.compatField = value;
            }
        }

        /// <remarks/>
        public settingsRsids rsids
        {
            get
            {
                return this.rsidsField;
            }
            set
            {
                this.rsidsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
        public mathPr mathPr
        {
            get
            {
                return this.mathPrField;
            }
            set
            {
                this.mathPrField = value;
            }
        }

        /// <remarks/>
        public settingsThemeFontLang themeFontLang
        {
            get
            {
                return this.themeFontLangField;
            }
            set
            {
                this.themeFontLangField = value;
            }
        }

        /// <remarks/>
        public settingsClrSchemeMapping clrSchemeMapping
        {
            get
            {
                return this.clrSchemeMappingField;
            }
            set
            {
                this.clrSchemeMappingField = value;
            }
        }

        /// <remarks/>
        public settingsShapeDefaults shapeDefaults
        {
            get
            {
                return this.shapeDefaultsField;
            }
            set
            {
                this.shapeDefaultsField = value;
            }
        }

        /// <remarks/>
        public settingsDecimalSymbol decimalSymbol
        {
            get
            {
                return this.decimalSymbolField;
            }
            set
            {
                this.decimalSymbolField = value;
            }
        }

        /// <remarks/>
        public settingsListSeparator listSeparator
        {
            get
            {
                return this.listSeparatorField;
            }
            set
            {
                this.listSeparatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/office/word/2010/wordml")]
        public docId docId
        {
            get
            {
                return this.docIdField;
            }
            set
            {
                this.docIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("docId", Namespace = "http://schemas.microsoft.com/office/word/2012/wordml")]
        public docId1 docId1
        {
            get
            {
                return this.docId1Field;
            }
            set
            {
                this.docId1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/markup-compatibility/2006")]
        public string Ignorable
        {
            get
            {
                return this.ignorableField;
            }
            set
            {
                this.ignorableField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsZoom
    {

        private byte percentField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte percent
        {
            get
            {
                return this.percentField;
            }
            set
            {
                this.percentField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsProofState
    {

        private string spellingField;

        private string grammarField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string spelling
        {
            get
            {
                return this.spellingField;
            }
            set
            {
                this.spellingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string grammar
        {
            get
            {
                return this.grammarField;
            }
            set
            {
                this.grammarField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsDefaultTabStop
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsCharacterSpacingControl
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsCompatSetting
    {

        private string nameField;

        private string uriField;

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string uri
        {
            get
            {
                return this.uriField;
            }
            set
            {
                this.uriField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsRsids
    {

        private settingsRsidsRsidRoot rsidRootField;

        private settingsRsidsRsid[] rsidField;

        /// <remarks/>
        public settingsRsidsRsidRoot rsidRoot
        {
            get
            {
                return this.rsidRootField;
            }
            set
            {
                this.rsidRootField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("rsid")]
        public settingsRsidsRsid[] rsid
        {
            get
            {
                return this.rsidField;
            }
            set
            {
                this.rsidField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsRsidsRsidRoot
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsRsidsRsid
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = false)]
    public partial class mathPr
    {

        private mathPrMathFont mathFontField;

        private mathPrBrkBin brkBinField;

        private mathPrBrkBinSub brkBinSubField;

        private mathPrSmallFrac smallFracField;

        private object dispDefField;

        private mathPrLMargin lMarginField;

        private mathPrRMargin rMarginField;

        private mathPrDefJc defJcField;

        private mathPrWrapIndent wrapIndentField;

        private mathPrIntLim intLimField;

        private mathPrNaryLim naryLimField;

        /// <remarks/>
        public mathPrMathFont mathFont
        {
            get
            {
                return this.mathFontField;
            }
            set
            {
                this.mathFontField = value;
            }
        }

        /// <remarks/>
        public mathPrBrkBin brkBin
        {
            get
            {
                return this.brkBinField;
            }
            set
            {
                this.brkBinField = value;
            }
        }

        /// <remarks/>
        public mathPrBrkBinSub brkBinSub
        {
            get
            {
                return this.brkBinSubField;
            }
            set
            {
                this.brkBinSubField = value;
            }
        }

        /// <remarks/>
        public mathPrSmallFrac smallFrac
        {
            get
            {
                return this.smallFracField;
            }
            set
            {
                this.smallFracField = value;
            }
        }

        /// <remarks/>
        public object dispDef
        {
            get
            {
                return this.dispDefField;
            }
            set
            {
                this.dispDefField = value;
            }
        }

        /// <remarks/>
        public mathPrLMargin lMargin
        {
            get
            {
                return this.lMarginField;
            }
            set
            {
                this.lMarginField = value;
            }
        }

        /// <remarks/>
        public mathPrRMargin rMargin
        {
            get
            {
                return this.rMarginField;
            }
            set
            {
                this.rMarginField = value;
            }
        }

        /// <remarks/>
        public mathPrDefJc defJc
        {
            get
            {
                return this.defJcField;
            }
            set
            {
                this.defJcField = value;
            }
        }

        /// <remarks/>
        public mathPrWrapIndent wrapIndent
        {
            get
            {
                return this.wrapIndentField;
            }
            set
            {
                this.wrapIndentField = value;
            }
        }

        /// <remarks/>
        public mathPrIntLim intLim
        {
            get
            {
                return this.intLimField;
            }
            set
            {
                this.intLimField = value;
            }
        }

        /// <remarks/>
        public mathPrNaryLim naryLim
        {
            get
            {
                return this.naryLimField;
            }
            set
            {
                this.naryLimField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrMathFont
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrBrkBin
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrBrkBinSub
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrSmallFrac
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrLMargin
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrRMargin
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrDefJc
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrWrapIndent
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrIntLim
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
    public partial class mathPrNaryLim
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsThemeFontLang
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsClrSchemeMapping
    {

        private string bg1Field;

        private string t1Field;

        private string bg2Field;

        private string t2Field;

        private string accent1Field;

        private string accent2Field;

        private string accent3Field;

        private string accent4Field;

        private string accent5Field;

        private string accent6Field;

        private string hyperlinkField;

        private string followedHyperlinkField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string bg1
        {
            get
            {
                return this.bg1Field;
            }
            set
            {
                this.bg1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string t1
        {
            get
            {
                return this.t1Field;
            }
            set
            {
                this.t1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string bg2
        {
            get
            {
                return this.bg2Field;
            }
            set
            {
                this.bg2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string t2
        {
            get
            {
                return this.t2Field;
            }
            set
            {
                this.t2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string accent1
        {
            get
            {
                return this.accent1Field;
            }
            set
            {
                this.accent1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string accent2
        {
            get
            {
                return this.accent2Field;
            }
            set
            {
                this.accent2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string accent3
        {
            get
            {
                return this.accent3Field;
            }
            set
            {
                this.accent3Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string accent4
        {
            get
            {
                return this.accent4Field;
            }
            set
            {
                this.accent4Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string accent5
        {
            get
            {
                return this.accent5Field;
            }
            set
            {
                this.accent5Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string accent6
        {
            get
            {
                return this.accent6Field;
            }
            set
            {
                this.accent6Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string hyperlink
        {
            get
            {
                return this.hyperlinkField;
            }
            set
            {
                this.hyperlinkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string followedHyperlink
        {
            get
            {
                return this.followedHyperlinkField;
            }
            set
            {
                this.followedHyperlinkField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsShapeDefaults
    {

        private shapedefaults shapedefaultsField;

        private shapelayout shapelayoutField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
        public shapedefaults shapedefaults
        {
            get
            {
                return this.shapedefaultsField;
            }
            set
            {
                this.shapedefaultsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "urn:schemas-microsoft-com:office:office")]
        public shapelayout shapelayout
        {
            get
            {
                return this.shapelayoutField;
            }
            set
            {
                this.shapelayoutField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:schemas-microsoft-com:office:office")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
    public partial class shapedefaults
    {

        private string extField;

        private ushort spidmaxField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:vml")]
        public string ext
        {
            get
            {
                return this.extField;
            }
            set
            {
                this.extField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort spidmax
        {
            get
            {
                return this.spidmaxField;
            }
            set
            {
                this.spidmaxField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:schemas-microsoft-com:office:office")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
    public partial class shapelayout
    {

        private shapelayoutIdmap idmapField;

        private string extField;

        /// <remarks/>
        public shapelayoutIdmap idmap
        {
            get
            {
                return this.idmapField;
            }
            set
            {
                this.idmapField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:vml")]
        public string ext
        {
            get
            {
                return this.extField;
            }
            set
            {
                this.extField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:schemas-microsoft-com:office:office")]
    public partial class shapelayoutIdmap
    {

        private string extField;

        private byte dataField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "urn:schemas-microsoft-com:vml")]
        public string ext
        {
            get
            {
                return this.extField;
            }
            set
            {
                this.extField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte data
        {
            get
            {
                return this.dataField;
            }
            set
            {
                this.dataField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsDecimalSymbol
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class settingsListSeparator
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/office/word/2010/wordml")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/office/word/2010/wordml", IsNullable = false)]
    public partial class docId
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/office/word/2012/wordml")]
    [System.Xml.Serialization.XmlRootAttribute("docId", Namespace = "http://schemas.microsoft.com/office/word/2012/wordml", IsNullable = false)]
    public partial class docId1
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = false)]
    public partial class theme
    {

        private themeThemeElements themeElementsField;

        private object objectDefaultsField;

        private object extraClrSchemeLstField;

        private themeExtLst extLstField;

        private string nameField;

        /// <remarks/>
        public themeThemeElements themeElements
        {
            get
            {
                return this.themeElementsField;
            }
            set
            {
                this.themeElementsField = value;
            }
        }

        /// <remarks/>
        public object objectDefaults
        {
            get
            {
                return this.objectDefaultsField;
            }
            set
            {
                this.objectDefaultsField = value;
            }
        }

        /// <remarks/>
        public object extraClrSchemeLst
        {
            get
            {
                return this.extraClrSchemeLstField;
            }
            set
            {
                this.extraClrSchemeLstField = value;
            }
        }

        /// <remarks/>
        public themeExtLst extLst
        {
            get
            {
                return this.extLstField;
            }
            set
            {
                this.extLstField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElements
    {

        private themeThemeElementsClrScheme clrSchemeField;

        private themeThemeElementsFontScheme fontSchemeField;

        private themeThemeElementsFmtScheme fmtSchemeField;

        /// <remarks/>
        public themeThemeElementsClrScheme clrScheme
        {
            get
            {
                return this.clrSchemeField;
            }
            set
            {
                this.clrSchemeField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFontScheme fontScheme
        {
            get
            {
                return this.fontSchemeField;
            }
            set
            {
                this.fontSchemeField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFmtScheme fmtScheme
        {
            get
            {
                return this.fmtSchemeField;
            }
            set
            {
                this.fmtSchemeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrScheme
    {

        private themeThemeElementsClrSchemeDk1 dk1Field;

        private themeThemeElementsClrSchemeLt1 lt1Field;

        private themeThemeElementsClrSchemeDk2 dk2Field;

        private themeThemeElementsClrSchemeLt2 lt2Field;

        private themeThemeElementsClrSchemeAccent1 accent1Field;

        private themeThemeElementsClrSchemeAccent2 accent2Field;

        private themeThemeElementsClrSchemeAccent3 accent3Field;

        private themeThemeElementsClrSchemeAccent4 accent4Field;

        private themeThemeElementsClrSchemeAccent5 accent5Field;

        private themeThemeElementsClrSchemeAccent6 accent6Field;

        private themeThemeElementsClrSchemeHlink hlinkField;

        private themeThemeElementsClrSchemeFolHlink folHlinkField;

        private string nameField;

        /// <remarks/>
        public themeThemeElementsClrSchemeDk1 dk1
        {
            get
            {
                return this.dk1Field;
            }
            set
            {
                this.dk1Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeLt1 lt1
        {
            get
            {
                return this.lt1Field;
            }
            set
            {
                this.lt1Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeDk2 dk2
        {
            get
            {
                return this.dk2Field;
            }
            set
            {
                this.dk2Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeLt2 lt2
        {
            get
            {
                return this.lt2Field;
            }
            set
            {
                this.lt2Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent1 accent1
        {
            get
            {
                return this.accent1Field;
            }
            set
            {
                this.accent1Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent2 accent2
        {
            get
            {
                return this.accent2Field;
            }
            set
            {
                this.accent2Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent3 accent3
        {
            get
            {
                return this.accent3Field;
            }
            set
            {
                this.accent3Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent4 accent4
        {
            get
            {
                return this.accent4Field;
            }
            set
            {
                this.accent4Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent5 accent5
        {
            get
            {
                return this.accent5Field;
            }
            set
            {
                this.accent5Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent6 accent6
        {
            get
            {
                return this.accent6Field;
            }
            set
            {
                this.accent6Field = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeHlink hlink
        {
            get
            {
                return this.hlinkField;
            }
            set
            {
                this.hlinkField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsClrSchemeFolHlink folHlink
        {
            get
            {
                return this.folHlinkField;
            }
            set
            {
                this.folHlinkField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeDk1
    {

        private themeThemeElementsClrSchemeDk1SysClr sysClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeDk1SysClr sysClr
        {
            get
            {
                return this.sysClrField;
            }
            set
            {
                this.sysClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeDk1SysClr
    {

        private string valField;

        private byte lastClrField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte lastClr
        {
            get
            {
                return this.lastClrField;
            }
            set
            {
                this.lastClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeLt1
    {

        private themeThemeElementsClrSchemeLt1SysClr sysClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeLt1SysClr sysClr
        {
            get
            {
                return this.sysClrField;
            }
            set
            {
                this.sysClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeLt1SysClr
    {

        private string valField;

        private string lastClrField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string lastClr
        {
            get
            {
                return this.lastClrField;
            }
            set
            {
                this.lastClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeDk2
    {

        private themeThemeElementsClrSchemeDk2SrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeDk2SrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeDk2SrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeLt2
    {

        private themeThemeElementsClrSchemeLt2SrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeLt2SrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeLt2SrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent1
    {

        private themeThemeElementsClrSchemeAccent1SrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent1SrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent1SrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent2
    {

        private themeThemeElementsClrSchemeAccent2SrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent2SrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent2SrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent3
    {

        private themeThemeElementsClrSchemeAccent3SrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent3SrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent3SrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent4
    {

        private themeThemeElementsClrSchemeAccent4SrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent4SrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent4SrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent5
    {

        private themeThemeElementsClrSchemeAccent5SrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent5SrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent5SrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent6
    {

        private themeThemeElementsClrSchemeAccent6SrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeAccent6SrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeAccent6SrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeHlink
    {

        private themeThemeElementsClrSchemeHlinkSrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeHlinkSrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeHlinkSrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeFolHlink
    {

        private themeThemeElementsClrSchemeFolHlinkSrgbClr srgbClrField;

        /// <remarks/>
        public themeThemeElementsClrSchemeFolHlinkSrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsClrSchemeFolHlinkSrgbClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontScheme
    {

        private themeThemeElementsFontSchemeMajorFont majorFontField;

        private themeThemeElementsFontSchemeMinorFont minorFontField;

        private string nameField;

        /// <remarks/>
        public themeThemeElementsFontSchemeMajorFont majorFont
        {
            get
            {
                return this.majorFontField;
            }
            set
            {
                this.majorFontField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFontSchemeMinorFont minorFont
        {
            get
            {
                return this.minorFontField;
            }
            set
            {
                this.minorFontField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMajorFont
    {

        private themeThemeElementsFontSchemeMajorFontLatin latinField;

        private themeThemeElementsFontSchemeMajorFontEA eaField;

        private themeThemeElementsFontSchemeMajorFontCS csField;

        private themeThemeElementsFontSchemeMajorFontFont[] fontField;

        /// <remarks/>
        public themeThemeElementsFontSchemeMajorFontLatin latin
        {
            get
            {
                return this.latinField;
            }
            set
            {
                this.latinField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFontSchemeMajorFontEA ea
        {
            get
            {
                return this.eaField;
            }
            set
            {
                this.eaField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFontSchemeMajorFontCS cs
        {
            get
            {
                return this.csField;
            }
            set
            {
                this.csField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("font")]
        public themeThemeElementsFontSchemeMajorFontFont[] font
        {
            get
            {
                return this.fontField;
            }
            set
            {
                this.fontField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMajorFontLatin
    {

        private string typefaceField;

        private string panoseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string typeface
        {
            get
            {
                return this.typefaceField;
            }
            set
            {
                this.typefaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string panose
        {
            get
            {
                return this.panoseField;
            }
            set
            {
                this.panoseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMajorFontEA
    {

        private string typefaceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string typeface
        {
            get
            {
                return this.typefaceField;
            }
            set
            {
                this.typefaceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMajorFontCS
    {

        private string typefaceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string typeface
        {
            get
            {
                return this.typefaceField;
            }
            set
            {
                this.typefaceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMajorFontFont
    {

        private string scriptField;

        private string typefaceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string script
        {
            get
            {
                return this.scriptField;
            }
            set
            {
                this.scriptField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string typeface
        {
            get
            {
                return this.typefaceField;
            }
            set
            {
                this.typefaceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMinorFont
    {

        private themeThemeElementsFontSchemeMinorFontLatin latinField;

        private themeThemeElementsFontSchemeMinorFontEA eaField;

        private themeThemeElementsFontSchemeMinorFontCS csField;

        private themeThemeElementsFontSchemeMinorFontFont[] fontField;

        /// <remarks/>
        public themeThemeElementsFontSchemeMinorFontLatin latin
        {
            get
            {
                return this.latinField;
            }
            set
            {
                this.latinField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFontSchemeMinorFontEA ea
        {
            get
            {
                return this.eaField;
            }
            set
            {
                this.eaField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFontSchemeMinorFontCS cs
        {
            get
            {
                return this.csField;
            }
            set
            {
                this.csField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("font")]
        public themeThemeElementsFontSchemeMinorFontFont[] font
        {
            get
            {
                return this.fontField;
            }
            set
            {
                this.fontField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMinorFontLatin
    {

        private string typefaceField;

        private string panoseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string typeface
        {
            get
            {
                return this.typefaceField;
            }
            set
            {
                this.typefaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string panose
        {
            get
            {
                return this.panoseField;
            }
            set
            {
                this.panoseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMinorFontEA
    {

        private string typefaceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string typeface
        {
            get
            {
                return this.typefaceField;
            }
            set
            {
                this.typefaceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMinorFontCS
    {

        private string typefaceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string typeface
        {
            get
            {
                return this.typefaceField;
            }
            set
            {
                this.typefaceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFontSchemeMinorFontFont
    {

        private string scriptField;

        private string typefaceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string script
        {
            get
            {
                return this.scriptField;
            }
            set
            {
                this.scriptField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string typeface
        {
            get
            {
                return this.typefaceField;
            }
            set
            {
                this.typefaceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtScheme
    {

        private themeThemeElementsFmtSchemeFillStyleLst fillStyleLstField;

        private themeThemeElementsFmtSchemeLN[] lnStyleLstField;

        private themeThemeElementsFmtSchemeEffectStyle[] effectStyleLstField;

        private themeThemeElementsFmtSchemeBgFillStyleLst bgFillStyleLstField;

        private string nameField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeFillStyleLst fillStyleLst
        {
            get
            {
                return this.fillStyleLstField;
            }
            set
            {
                this.fillStyleLstField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ln", IsNullable = false)]
        public themeThemeElementsFmtSchemeLN[] lnStyleLst
        {
            get
            {
                return this.lnStyleLstField;
            }
            set
            {
                this.lnStyleLstField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("effectStyle", IsNullable = false)]
        public themeThemeElementsFmtSchemeEffectStyle[] effectStyleLst
        {
            get
            {
                return this.effectStyleLstField;
            }
            set
            {
                this.effectStyleLstField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFmtSchemeBgFillStyleLst bgFillStyleLst
        {
            get
            {
                return this.bgFillStyleLstField;
            }
            set
            {
                this.bgFillStyleLstField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLst
    {

        private themeThemeElementsFmtSchemeFillStyleLstSolidFill solidFillField;

        private themeThemeElementsFmtSchemeFillStyleLstGradFill[] gradFillField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeFillStyleLstSolidFill solidFill
        {
            get
            {
                return this.solidFillField;
            }
            set
            {
                this.solidFillField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("gradFill")]
        public themeThemeElementsFmtSchemeFillStyleLstGradFill[] gradFill
        {
            get
            {
                return this.gradFillField;
            }
            set
            {
                this.gradFillField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstSolidFill
    {

        private themeThemeElementsFmtSchemeFillStyleLstSolidFillSchemeClr schemeClrField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeFillStyleLstSolidFillSchemeClr schemeClr
        {
            get
            {
                return this.schemeClrField;
            }
            set
            {
                this.schemeClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstSolidFillSchemeClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstGradFill
    {

        private themeThemeElementsFmtSchemeFillStyleLstGradFillGS[] gsLstField;

        private themeThemeElementsFmtSchemeFillStyleLstGradFillLin linField;

        private byte rotWithShapeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("gs", IsNullable = false)]
        public themeThemeElementsFmtSchemeFillStyleLstGradFillGS[] gsLst
        {
            get
            {
                return this.gsLstField;
            }
            set
            {
                this.gsLstField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFmtSchemeFillStyleLstGradFillLin lin
        {
            get
            {
                return this.linField;
            }
            set
            {
                this.linField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte rotWithShape
        {
            get
            {
                return this.rotWithShapeField;
            }
            set
            {
                this.rotWithShapeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstGradFillGS
    {

        private themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClr schemeClrField;

        private uint posField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClr schemeClr
        {
            get
            {
                return this.schemeClrField;
            }
            set
            {
                this.schemeClrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint pos
        {
            get
            {
                return this.posField;
            }
            set
            {
                this.posField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClr
    {

        private object[] itemsField;

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("lumMod", typeof(themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClrLumMod))]
        [System.Xml.Serialization.XmlElementAttribute("satMod", typeof(themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClrSatMod))]
        [System.Xml.Serialization.XmlElementAttribute("shade", typeof(themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClrShade))]
        [System.Xml.Serialization.XmlElementAttribute("tint", typeof(themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClrTint))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClrLumMod
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClrSatMod
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClrShade
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstGradFillGSSchemeClrTint
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeFillStyleLstGradFillLin
    {

        private uint angField;

        private byte scaledField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint ang
        {
            get
            {
                return this.angField;
            }
            set
            {
                this.angField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte scaled
        {
            get
            {
                return this.scaledField;
            }
            set
            {
                this.scaledField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeLN
    {

        private themeThemeElementsFmtSchemeLNSolidFill solidFillField;

        private themeThemeElementsFmtSchemeLNPrstDash prstDashField;

        private themeThemeElementsFmtSchemeLNMiter miterField;

        private ushort wField;

        private string capField;

        private string cmpdField;

        private string algnField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeLNSolidFill solidFill
        {
            get
            {
                return this.solidFillField;
            }
            set
            {
                this.solidFillField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFmtSchemeLNPrstDash prstDash
        {
            get
            {
                return this.prstDashField;
            }
            set
            {
                this.prstDashField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFmtSchemeLNMiter miter
        {
            get
            {
                return this.miterField;
            }
            set
            {
                this.miterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cap
        {
            get
            {
                return this.capField;
            }
            set
            {
                this.capField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string cmpd
        {
            get
            {
                return this.cmpdField;
            }
            set
            {
                this.cmpdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string algn
        {
            get
            {
                return this.algnField;
            }
            set
            {
                this.algnField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeLNSolidFill
    {

        private themeThemeElementsFmtSchemeLNSolidFillSchemeClr schemeClrField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeLNSolidFillSchemeClr schemeClr
        {
            get
            {
                return this.schemeClrField;
            }
            set
            {
                this.schemeClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeLNSolidFillSchemeClr
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeLNPrstDash
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeLNMiter
    {

        private uint limField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint lim
        {
            get
            {
                return this.limField;
            }
            set
            {
                this.limField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeEffectStyle
    {

        private themeThemeElementsFmtSchemeEffectStyleEffectLst effectLstField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeEffectStyleEffectLst effectLst
        {
            get
            {
                return this.effectLstField;
            }
            set
            {
                this.effectLstField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeEffectStyleEffectLst
    {

        private themeThemeElementsFmtSchemeEffectStyleEffectLstOuterShdw outerShdwField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeEffectStyleEffectLstOuterShdw outerShdw
        {
            get
            {
                return this.outerShdwField;
            }
            set
            {
                this.outerShdwField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeEffectStyleEffectLstOuterShdw
    {

        private themeThemeElementsFmtSchemeEffectStyleEffectLstOuterShdwSrgbClr srgbClrField;

        private ushort blurRadField;

        private ushort distField;

        private uint dirField;

        private string algnField;

        private byte rotWithShapeField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeEffectStyleEffectLstOuterShdwSrgbClr srgbClr
        {
            get
            {
                return this.srgbClrField;
            }
            set
            {
                this.srgbClrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort blurRad
        {
            get
            {
                return this.blurRadField;
            }
            set
            {
                this.blurRadField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort dist
        {
            get
            {
                return this.distField;
            }
            set
            {
                this.distField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint dir
        {
            get
            {
                return this.dirField;
            }
            set
            {
                this.dirField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string algn
        {
            get
            {
                return this.algnField;
            }
            set
            {
                this.algnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte rotWithShape
        {
            get
            {
                return this.rotWithShapeField;
            }
            set
            {
                this.rotWithShapeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeEffectStyleEffectLstOuterShdwSrgbClr
    {

        private themeThemeElementsFmtSchemeEffectStyleEffectLstOuterShdwSrgbClrAlpha alphaField;

        private byte valField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeEffectStyleEffectLstOuterShdwSrgbClrAlpha alpha
        {
            get
            {
                return this.alphaField;
            }
            set
            {
                this.alphaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeEffectStyleEffectLstOuterShdwSrgbClrAlpha
    {

        private ushort valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLst
    {

        private themeThemeElementsFmtSchemeBgFillStyleLstSolidFill[] solidFillField;

        private themeThemeElementsFmtSchemeBgFillStyleLstGradFill gradFillField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("solidFill")]
        public themeThemeElementsFmtSchemeBgFillStyleLstSolidFill[] solidFill
        {
            get
            {
                return this.solidFillField;
            }
            set
            {
                this.solidFillField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFmtSchemeBgFillStyleLstGradFill gradFill
        {
            get
            {
                return this.gradFillField;
            }
            set
            {
                this.gradFillField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstSolidFill
    {

        private themeThemeElementsFmtSchemeBgFillStyleLstSolidFillSchemeClr schemeClrField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeBgFillStyleLstSolidFillSchemeClr schemeClr
        {
            get
            {
                return this.schemeClrField;
            }
            set
            {
                this.schemeClrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstSolidFillSchemeClr
    {

        private themeThemeElementsFmtSchemeBgFillStyleLstSolidFillSchemeClrTint tintField;

        private themeThemeElementsFmtSchemeBgFillStyleLstSolidFillSchemeClrSatMod satModField;

        private string valField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeBgFillStyleLstSolidFillSchemeClrTint tint
        {
            get
            {
                return this.tintField;
            }
            set
            {
                this.tintField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFmtSchemeBgFillStyleLstSolidFillSchemeClrSatMod satMod
        {
            get
            {
                return this.satModField;
            }
            set
            {
                this.satModField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstSolidFillSchemeClrTint
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstSolidFillSchemeClrSatMod
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstGradFill
    {

        private themeThemeElementsFmtSchemeBgFillStyleLstGradFillGS[] gsLstField;

        private themeThemeElementsFmtSchemeBgFillStyleLstGradFillLin linField;

        private byte rotWithShapeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("gs", IsNullable = false)]
        public themeThemeElementsFmtSchemeBgFillStyleLstGradFillGS[] gsLst
        {
            get
            {
                return this.gsLstField;
            }
            set
            {
                this.gsLstField = value;
            }
        }

        /// <remarks/>
        public themeThemeElementsFmtSchemeBgFillStyleLstGradFillLin lin
        {
            get
            {
                return this.linField;
            }
            set
            {
                this.linField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte rotWithShape
        {
            get
            {
                return this.rotWithShapeField;
            }
            set
            {
                this.rotWithShapeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstGradFillGS
    {

        private themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClr schemeClrField;

        private uint posField;

        /// <remarks/>
        public themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClr schemeClr
        {
            get
            {
                return this.schemeClrField;
            }
            set
            {
                this.schemeClrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint pos
        {
            get
            {
                return this.posField;
            }
            set
            {
                this.posField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClr
    {

        private object[] itemsField;

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("lumMod", typeof(themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClrLumMod))]
        [System.Xml.Serialization.XmlElementAttribute("satMod", typeof(themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClrSatMod))]
        [System.Xml.Serialization.XmlElementAttribute("shade", typeof(themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClrShade))]
        [System.Xml.Serialization.XmlElementAttribute("tint", typeof(themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClrTint))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClrLumMod
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClrSatMod
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClrShade
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstGradFillGSSchemeClrTint
    {

        private uint valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeThemeElementsFmtSchemeBgFillStyleLstGradFillLin
    {

        private uint angField;

        private byte scaledField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public uint ang
        {
            get
            {
                return this.angField;
            }
            set
            {
                this.angField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte scaled
        {
            get
            {
                return this.scaledField;
            }
            set
            {
                this.scaledField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeExtLst
    {

        private themeExtLstExt extField;

        /// <remarks/>
        public themeExtLstExt ext
        {
            get
            {
                return this.extField;
            }
            set
            {
                this.extField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
    public partial class themeExtLstExt
    {

        private themeFamily themeFamilyField;

        private string uriField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/office/thememl/2012/main")]
        public themeFamily themeFamily
        {
            get
            {
                return this.themeFamilyField;
            }
            set
            {
                this.themeFamilyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string uri
        {
            get
            {
                return this.uriField;
            }
            set
            {
                this.uriField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/office/thememl/2012/main")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/office/thememl/2012/main", IsNullable = false)]
    public partial class themeFamily
    {

        private string nameField;

        private string idField;

        private string vidField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string vid
        {
            get
            {
                return this.vidField;
            }
            set
            {
                this.vidField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
    public partial class document
    {

        private documentBody bodyField;

        private string ignorableField;

        /// <remarks/>
        public documentBody body
        {
            get
            {
                return this.bodyField;
            }
            set
            {
                this.bodyField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/markup-compatibility/2006")]
        public string Ignorable
        {
            get
            {
                return this.ignorableField;
            }
            set
            {
                this.ignorableField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBody
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("p", typeof(documentBodyP))]
        [System.Xml.Serialization.XmlElementAttribute("sectPr", typeof(documentBodySectPr))]
        [System.Xml.Serialization.XmlElementAttribute("tbl", typeof(documentBodyTbl))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyP
    {

        private documentBodyPPPr pPrField;

        private string paraIdField;

        private uint textIdField;

        private uint rsidRField;

        private string rsidRDefaultField;

        private string rsidRPrField;

        /// <remarks/>
        public documentBodyPPPr pPr
        {
            get
            {
                return this.pPrField;
            }
            set
            {
                this.pPrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/office/word/2010/wordml")]
        public string paraId
        {
            get
            {
                return this.paraIdField;
            }
            set
            {
                this.paraIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/office/word/2010/wordml")]
        public uint textId
        {
            get
            {
                return this.textIdField;
            }
            set
            {
                this.textIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public uint rsidR
        {
            get
            {
                return this.rsidRField;
            }
            set
            {
                this.rsidRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string rsidRDefault
        {
            get
            {
                return this.rsidRDefaultField;
            }
            set
            {
                this.rsidRDefaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string rsidRPr
        {
            get
            {
                return this.rsidRPrField;
            }
            set
            {
                this.rsidRPrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPr
    {

        private documentBodyPPPrWidowControl widowControlField;

        private documentBodyPPPrPBdr pBdrField;

        private documentBodyPPPrSpacing spacingField;

        private documentBodyPPPrRPr rPrField;

        /// <remarks/>
        public documentBodyPPPrWidowControl widowControl
        {
            get
            {
                return this.widowControlField;
            }
            set
            {
                this.widowControlField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrPBdr pBdr
        {
            get
            {
                return this.pBdrField;
            }
            set
            {
                this.pBdrField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrSpacing spacing
        {
            get
            {
                return this.spacingField;
            }
            set
            {
                this.spacingField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrRPr rPr
        {
            get
            {
                return this.rPrField;
            }
            set
            {
                this.rPrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrWidowControl
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrPBdr
    {

        private documentBodyPPPrPBdrTop topField;

        private documentBodyPPPrPBdrLeft leftField;

        private documentBodyPPPrPBdrBottom bottomField;

        private documentBodyPPPrPBdrRight rightField;

        private documentBodyPPPrPBdrBetween betweenField;

        /// <remarks/>
        public documentBodyPPPrPBdrTop top
        {
            get
            {
                return this.topField;
            }
            set
            {
                this.topField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrPBdrLeft left
        {
            get
            {
                return this.leftField;
            }
            set
            {
                this.leftField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrPBdrBottom bottom
        {
            get
            {
                return this.bottomField;
            }
            set
            {
                this.bottomField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrPBdrRight right
        {
            get
            {
                return this.rightField;
            }
            set
            {
                this.rightField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrPBdrBetween between
        {
            get
            {
                return this.betweenField;
            }
            set
            {
                this.betweenField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrPBdrTop
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrPBdrLeft
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrPBdrBottom
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrPBdrRight
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrPBdrBetween
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrSpacing
    {

        private ushort lineField;

        private string lineRuleField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort line
        {
            get
            {
                return this.lineField;
            }
            set
            {
                this.lineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string lineRule
        {
            get
            {
                return this.lineRuleField;
            }
            set
            {
                this.lineRuleField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrRPr
    {

        private documentBodyPPPrRPrLang langField;

        private documentBodyPPPrRPrRFonts rFontsField;

        private documentBodyPPPrRPrColor colorField;

        private documentBodyPPPrRPrSZ szField;

        private documentBodyPPPrRPrSzCs szCsField;

        /// <remarks/>
        public documentBodyPPPrRPrLang lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrRPrRFonts rFonts
        {
            get
            {
                return this.rFontsField;
            }
            set
            {
                this.rFontsField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrRPrColor color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrRPrSZ sz
        {
            get
            {
                return this.szField;
            }
            set
            {
                this.szField = value;
            }
        }

        /// <remarks/>
        public documentBodyPPPrRPrSzCs szCs
        {
            get
            {
                return this.szCsField;
            }
            set
            {
                this.szCsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrRPrLang
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrRPrRFonts
    {

        private string asciiField;

        private string eastAsiaField;

        private string hAnsiField;

        private string csField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string ascii
        {
            get
            {
                return this.asciiField;
            }
            set
            {
                this.asciiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string eastAsia
        {
            get
            {
                return this.eastAsiaField;
            }
            set
            {
                this.eastAsiaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string hAnsi
        {
            get
            {
                return this.hAnsiField;
            }
            set
            {
                this.hAnsiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string cs
        {
            get
            {
                return this.csField;
            }
            set
            {
                this.csField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrRPrColor
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrRPrSZ
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyPPPrRPrSzCs
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodySectPr
    {

        private documentBodySectPrPgSz pgSzField;

        private documentBodySectPrPgMar pgMarField;

        private documentBodySectPrPgNumType pgNumTypeField;

        private documentBodySectPrCols colsField;

        private uint rsidRField;

        private string rsidRPrField;

        /// <remarks/>
        public documentBodySectPrPgSz pgSz
        {
            get
            {
                return this.pgSzField;
            }
            set
            {
                this.pgSzField = value;
            }
        }

        /// <remarks/>
        public documentBodySectPrPgMar pgMar
        {
            get
            {
                return this.pgMarField;
            }
            set
            {
                this.pgMarField = value;
            }
        }

        /// <remarks/>
        public documentBodySectPrPgNumType pgNumType
        {
            get
            {
                return this.pgNumTypeField;
            }
            set
            {
                this.pgNumTypeField = value;
            }
        }

        /// <remarks/>
        public documentBodySectPrCols cols
        {
            get
            {
                return this.colsField;
            }
            set
            {
                this.colsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public uint rsidR
        {
            get
            {
                return this.rsidRField;
            }
            set
            {
                this.rsidRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string rsidRPr
        {
            get
            {
                return this.rsidRPrField;
            }
            set
            {
                this.rsidRPrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodySectPrPgSz
    {

        private ushort wField;

        private ushort hField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort h
        {
            get
            {
                return this.hField;
            }
            set
            {
                this.hField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodySectPrPgMar
    {

        private ushort topField;

        private ushort rightField;

        private ushort bottomField;

        private ushort leftField;

        private ushort headerField;

        private ushort footerField;

        private byte gutterField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort top
        {
            get
            {
                return this.topField;
            }
            set
            {
                this.topField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort right
        {
            get
            {
                return this.rightField;
            }
            set
            {
                this.rightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort bottom
        {
            get
            {
                return this.bottomField;
            }
            set
            {
                this.bottomField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort left
        {
            get
            {
                return this.leftField;
            }
            set
            {
                this.leftField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort header
        {
            get
            {
                return this.headerField;
            }
            set
            {
                this.headerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort footer
        {
            get
            {
                return this.footerField;
            }
            set
            {
                this.footerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte gutter
        {
            get
            {
                return this.gutterField;
            }
            set
            {
                this.gutterField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodySectPrPgNumType
    {

        private byte startField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte start
        {
            get
            {
                return this.startField;
            }
            set
            {
                this.startField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodySectPrCols
    {

        private ushort spaceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort space
        {
            get
            {
                return this.spaceField;
            }
            set
            {
                this.spaceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTbl
    {

        private documentBodyTblTblPr tblPrField;

        private documentBodyTblGridCol[] tblGridField;

        private documentBodyTblTR[] trField;

        /// <remarks/>
        public documentBodyTblTblPr tblPr
        {
            get
            {
                return this.tblPrField;
            }
            set
            {
                this.tblPrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("gridCol", IsNullable = false)]
        public documentBodyTblGridCol[] tblGrid
        {
            get
            {
                return this.tblGridField;
            }
            set
            {
                this.tblGridField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tr")]
        public documentBodyTblTR[] tr
        {
            get
            {
                return this.trField;
            }
            set
            {
                this.trField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPr
    {

        private documentBodyTblTblPrTblStyle tblStyleField;

        private documentBodyTblTblPrTblW tblWField;

        private documentBodyTblTblPrTblBorders tblBordersField;

        private documentBodyTblTblPrTblLayout tblLayoutField;

        private documentBodyTblTblPrTblLook tblLookField;

        /// <remarks/>
        public documentBodyTblTblPrTblStyle tblStyle
        {
            get
            {
                return this.tblStyleField;
            }
            set
            {
                this.tblStyleField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTblPrTblW tblW
        {
            get
            {
                return this.tblWField;
            }
            set
            {
                this.tblWField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTblPrTblBorders tblBorders
        {
            get
            {
                return this.tblBordersField;
            }
            set
            {
                this.tblBordersField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTblPrTblLayout tblLayout
        {
            get
            {
                return this.tblLayoutField;
            }
            set
            {
                this.tblLayoutField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTblPrTblLook tblLook
        {
            get
            {
                return this.tblLookField;
            }
            set
            {
                this.tblLookField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPrTblStyle
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPrTblW
    {

        private ushort wField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPrTblBorders
    {

        private documentBodyTblTblPrTblBordersTop topField;

        private documentBodyTblTblPrTblBordersLeft leftField;

        private documentBodyTblTblPrTblBordersBottom bottomField;

        private documentBodyTblTblPrTblBordersRight rightField;

        /// <remarks/>
        public documentBodyTblTblPrTblBordersTop top
        {
            get
            {
                return this.topField;
            }
            set
            {
                this.topField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTblPrTblBordersLeft left
        {
            get
            {
                return this.leftField;
            }
            set
            {
                this.leftField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTblPrTblBordersBottom bottom
        {
            get
            {
                return this.bottomField;
            }
            set
            {
                this.bottomField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTblPrTblBordersRight right
        {
            get
            {
                return this.rightField;
            }
            set
            {
                this.rightField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPrTblBordersTop
    {

        private string valField;

        private byte szField;

        private ushort spaceField;

        private string colorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte sz
        {
            get
            {
                return this.szField;
            }
            set
            {
                this.szField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort space
        {
            get
            {
                return this.spaceField;
            }
            set
            {
                this.spaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPrTblBordersLeft
    {

        private string valField;

        private byte szField;

        private ushort spaceField;

        private string colorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte sz
        {
            get
            {
                return this.szField;
            }
            set
            {
                this.szField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort space
        {
            get
            {
                return this.spaceField;
            }
            set
            {
                this.spaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPrTblBordersBottom
    {

        private string valField;

        private byte szField;

        private ushort spaceField;

        private string colorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte sz
        {
            get
            {
                return this.szField;
            }
            set
            {
                this.szField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort space
        {
            get
            {
                return this.spaceField;
            }
            set
            {
                this.spaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPrTblBordersRight
    {

        private string valField;

        private byte szField;

        private ushort spaceField;

        private string colorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte sz
        {
            get
            {
                return this.szField;
            }
            set
            {
                this.szField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort space
        {
            get
            {
                return this.spaceField;
            }
            set
            {
                this.spaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPrTblLayout
    {

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTblPrTblLook
    {

        private string valField;

        private byte firstRowField;

        private byte lastRowField;

        private byte firstColumnField;

        private byte lastColumnField;

        private byte noHBandField;

        private byte noVBandField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte firstRow
        {
            get
            {
                return this.firstRowField;
            }
            set
            {
                this.firstRowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte lastRow
        {
            get
            {
                return this.lastRowField;
            }
            set
            {
                this.lastRowField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte firstColumn
        {
            get
            {
                return this.firstColumnField;
            }
            set
            {
                this.firstColumnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte lastColumn
        {
            get
            {
                return this.lastColumnField;
            }
            set
            {
                this.lastColumnField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte noHBand
        {
            get
            {
                return this.noHBandField;
            }
            set
            {
                this.noHBandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public byte noVBand
        {
            get
            {
                return this.noVBandField;
            }
            set
            {
                this.noVBandField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblGridCol
    {

        private ushort wField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTR
    {

        private documentBodyTblTRTC[] tcField;

        private uint rsidRField;

        private string paraIdField;

        private uint textIdField;

        private string rsidRPrField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("tc")]
        public documentBodyTblTRTC[] tc
        {
            get
            {
                return this.tcField;
            }
            set
            {
                this.tcField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public uint rsidR
        {
            get
            {
                return this.rsidRField;
            }
            set
            {
                this.rsidRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/office/word/2010/wordml")]
        public string paraId
        {
            get
            {
                return this.paraIdField;
            }
            set
            {
                this.paraIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/office/word/2010/wordml")]
        public uint textId
        {
            get
            {
                return this.textIdField;
            }
            set
            {
                this.textIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string rsidRPr
        {
            get
            {
                return this.rsidRPrField;
            }
            set
            {
                this.rsidRPrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTC
    {

        private documentBodyTblTRTCTcPr tcPrField;

        private documentBodyTblTRTCP pField;

        /// <remarks/>
        public documentBodyTblTRTCTcPr tcPr
        {
            get
            {
                return this.tcPrField;
            }
            set
            {
                this.tcPrField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCP p
        {
            get
            {
                return this.pField;
            }
            set
            {
                this.pField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCTcPr
    {

        private documentBodyTblTRTCTcPrTcW tcWField;

        private documentBodyTblTRTCTcPrTcBorders tcBordersField;

        /// <remarks/>
        public documentBodyTblTRTCTcPrTcW tcW
        {
            get
            {
                return this.tcWField;
            }
            set
            {
                this.tcWField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCTcPrTcBorders tcBorders
        {
            get
            {
                return this.tcBordersField;
            }
            set
            {
                this.tcBordersField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCTcPrTcW
    {

        private ushort wField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public ushort w
        {
            get
            {
                return this.wField;
            }
            set
            {
                this.wField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCTcPrTcBorders
    {

        private documentBodyTblTRTCTcPrTcBordersTop topField;

        private documentBodyTblTRTCTcPrTcBordersLeft leftField;

        private documentBodyTblTRTCTcPrTcBordersBottom bottomField;

        private documentBodyTblTRTCTcPrTcBordersRight rightField;

        /// <remarks/>
        public documentBodyTblTRTCTcPrTcBordersTop top
        {
            get
            {
                return this.topField;
            }
            set
            {
                this.topField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCTcPrTcBordersLeft left
        {
            get
            {
                return this.leftField;
            }
            set
            {
                this.leftField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCTcPrTcBordersBottom bottom
        {
            get
            {
                return this.bottomField;
            }
            set
            {
                this.bottomField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCTcPrTcBordersRight right
        {
            get
            {
                return this.rightField;
            }
            set
            {
                this.rightField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCTcPrTcBordersTop
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCTcPrTcBordersLeft
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCTcPrTcBordersBottom
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCTcPrTcBordersRight
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCP
    {

        private object[] itemsField;

        private string paraIdField;

        private uint textIdField;

        private uint rsidRField;

        private string rsidRDefaultField;

        private string rsidRPrField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("pPr", typeof(documentBodyTblTRTCPPPr))]
        [System.Xml.Serialization.XmlElementAttribute("proofErr", typeof(documentBodyTblTRTCPProofErr))]
        [System.Xml.Serialization.XmlElementAttribute("r", typeof(documentBodyTblTRTCPR))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/office/word/2010/wordml")]
        public string paraId
        {
            get
            {
                return this.paraIdField;
            }
            set
            {
                this.paraIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/office/word/2010/wordml")]
        public uint textId
        {
            get
            {
                return this.textIdField;
            }
            set
            {
                this.textIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public uint rsidR
        {
            get
            {
                return this.rsidRField;
            }
            set
            {
                this.rsidRField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string rsidRDefault
        {
            get
            {
                return this.rsidRDefaultField;
            }
            set
            {
                this.rsidRDefaultField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string rsidRPr
        {
            get
            {
                return this.rsidRPrField;
            }
            set
            {
                this.rsidRPrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPPPr
    {

        private documentBodyTblTRTCPPPrShd shdField;

        private documentBodyTblTRTCPPPrRPr rPrField;

        /// <remarks/>
        public documentBodyTblTRTCPPPrShd shd
        {
            get
            {
                return this.shdField;
            }
            set
            {
                this.shdField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCPPPrRPr rPr
        {
            get
            {
                return this.rPrField;
            }
            set
            {
                this.rPrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPPPrShd
    {

        private string valField;

        private string colorField;

        private string fillField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string fill
        {
            get
            {
                return this.fillField;
            }
            set
            {
                this.fillField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPPPrRPr
    {

        private documentBodyTblTRTCPPPrRPrRFonts rFontsField;

        private documentBodyTblTRTCPPPrRPrSZ szField;

        private documentBodyTblTRTCPPPrRPrSzCs szCsField;

        private documentBodyTblTRTCPPPrRPrLang langField;

        /// <remarks/>
        public documentBodyTblTRTCPPPrRPrRFonts rFonts
        {
            get
            {
                return this.rFontsField;
            }
            set
            {
                this.rFontsField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCPPPrRPrSZ sz
        {
            get
            {
                return this.szField;
            }
            set
            {
                this.szField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCPPPrRPrSzCs szCs
        {
            get
            {
                return this.szCsField;
            }
            set
            {
                this.szCsField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCPPPrRPrLang lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPPPrRPrRFonts
    {

        private string asciiField;

        private string eastAsiaField;

        private string hAnsiField;

        private string csField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string ascii
        {
            get
            {
                return this.asciiField;
            }
            set
            {
                this.asciiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string eastAsia
        {
            get
            {
                return this.eastAsiaField;
            }
            set
            {
                this.eastAsiaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string hAnsi
        {
            get
            {
                return this.hAnsiField;
            }
            set
            {
                this.hAnsiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string cs
        {
            get
            {
                return this.csField;
            }
            set
            {
                this.csField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPPPrRPrSZ
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPPPrRPrSzCs
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPPPrRPrLang
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPProofErr
    {

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPR
    {

        private documentBodyTblTRTCPRRPr rPrField;

        private object lastRenderedPageBreakField;

        private documentBodyTblTRTCPRT tField;

        private string rsidRPrField;

        /// <remarks/>
        public documentBodyTblTRTCPRRPr rPr
        {
            get
            {
                return this.rPrField;
            }
            set
            {
                this.rPrField = value;
            }
        }

        /// <remarks/>
        public object lastRenderedPageBreak
        {
            get
            {
                return this.lastRenderedPageBreakField;
            }
            set
            {
                this.lastRenderedPageBreakField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCPRT t
        {
            get
            {
                return this.tField;
            }
            set
            {
                this.tField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string rsidRPr
        {
            get
            {
                return this.rsidRPrField;
            }
            set
            {
                this.rsidRPrField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPRRPr
    {

        private documentBodyTblTRTCPRRPrRFonts rFontsField;

        private object bField;

        private object iField;

        private documentBodyTblTRTCPRRPrColor colorField;

        private documentBodyTblTRTCPRRPrSZ szField;

        private documentBodyTblTRTCPRRPrSzCs szCsField;

        private documentBodyTblTRTCPRRPrLang langField;

        /// <remarks/>
        public documentBodyTblTRTCPRRPrRFonts rFonts
        {
            get
            {
                return this.rFontsField;
            }
            set
            {
                this.rFontsField = value;
            }
        }

        /// <remarks/>
        public object b
        {
            get
            {
                return this.bField;
            }
            set
            {
                this.bField = value;
            }
        }

        /// <remarks/>
        public object i
        {
            get
            {
                return this.iField;
            }
            set
            {
                this.iField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCPRRPrColor color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCPRRPrSZ sz
        {
            get
            {
                return this.szField;
            }
            set
            {
                this.szField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCPRRPrSzCs szCs
        {
            get
            {
                return this.szCsField;
            }
            set
            {
                this.szCsField = value;
            }
        }

        /// <remarks/>
        public documentBodyTblTRTCPRRPrLang lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPRRPrRFonts
    {

        private string asciiField;

        private string eastAsiaField;

        private string hAnsiField;

        private string csField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string ascii
        {
            get
            {
                return this.asciiField;
            }
            set
            {
                this.asciiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string eastAsia
        {
            get
            {
                return this.eastAsiaField;
            }
            set
            {
                this.eastAsiaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string hAnsi
        {
            get
            {
                return this.hAnsiField;
            }
            set
            {
                this.hAnsiField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string cs
        {
            get
            {
                return this.csField;
            }
            set
            {
                this.csField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPRRPrColor
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPRRPrSZ
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPRRPrSzCs
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPRRPrLang
    {

        private string valField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string val
        {
            get
            {
                return this.valField;
            }
            set
            {
                this.valField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
    public partial class documentBodyTblTRTCPRT
    {

        private string spaceField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string space
        {
            get
            {
                return this.spaceField;
            }
            set
            {
                this.spaceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/package/2006/relationships")]
    public partial class RelationshipsRelationship
    {

        private string idField;

        private string typeField;

        private string targetField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Target
        {
            get
            {
                return this.targetField;
            }
            set
            {
                this.targetField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.openxmlformats.org/package/2006/relationships")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.openxmlformats.org/package/2006/relationships", IsNullable = false)]
    public partial class Relationships
    {

        private RelationshipsRelationship[] relationshipField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Relationship")]
        public RelationshipsRelationship[] Relationship
        {
            get
            {
                return this.relationshipField;
            }
            set
            {
                this.relationshipField = value;
            }
        }
    }


    #endregion

    // «««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««««

    internal class PTAlternative
    {
        UbtDatabaseSqlServer server = new UbtDatabaseSqlServer();
        HtmlGenerator htmlGenerator = new HtmlGenerator();

        public event ShowMessage ShowMessage = null;

        public event ShowPaperNumber ShowPaperNumber = null;

        public PTAlternative()
        {
            htmlGenerator.ShowMessage += HtmlGenerator_ShowMessage;
            htmlGenerator.ShowPaperNumber += HtmlGenerator_ShowPaperNumber;
        }

        private void FireShowMessage(string message)
        {
            ShowMessage?.Invoke(message);
        }

        private void FireShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }

        private void HtmlGenerator_ShowPaperNumber(short paperNo)
        {
            ShowPaperNumber?.Invoke(paperNo);
        }

        private void HtmlGenerator_ShowMessage(string message, bool isError = false, bool isFatal = false)
        {
            ShowMessage?.Invoke(message);
        }


        private Xceed.Document.NET.Paragraph CreateWordParagraph(Cell cell, string text)
        {
            Xceed.Document.NET.Paragraph p = cell.InsertParagraph(text);
            p.Font(new Xceed.Document.NET.Font("Verdana"))
                  .FontSize(10)
                  .Italic()
                  .Spacing(5)
                  .SpacingLine(22)
                  .SpacingAfter(5);
            return p;
        }

        public bool ExportTranslationAlternative()
        {
            ExportToWord word = new ExportToWord();

            try
            {
                for (short paperNo = 0; paperNo < 197; paperNo++)
                {
                    FireShowPaperNumber((short)paperNo);
                    FireShowMessage($"Generating paper {paperNo}");
                    List<PT_AlternativeRecord> list = server.GetPT_AlternativeRecords(paperNo);

                    string pathFile = Path.Combine(UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder, $"Paper{paperNo:000}.html");
                    if (File.Exists(pathFile))
                        File.Delete(pathFile);
                    string html = htmlGenerator.FormatPaper(list);
                    File.WriteAllText(pathFile, html, Encoding.UTF8);

                    //string pathDocx = Path.Combine(UbStandardObjects.StaticObjects.Parameters.RepositoryOutputPTAlternativeFolder, $"Paper{paperNo:000}.docx");
                    //if (File.Exists(pathDocx))
                    //    File.Delete(pathDocx);

                    //word.Export(pathDocx, list);

                }
                FireShowMessage("Finished");
                return true;
            }
            catch (Exception ex)
            {
                FireShowMessage($"Exporting translation alternative {ex.Message}");
                UbStandardObjects.StaticObjects.Logger.Error("Exporting translation alternative", ex);
                return false;
            }
        }
    }
}
