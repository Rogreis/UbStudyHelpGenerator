using System;
using System.Collections.Generic;
using System.Text;

namespace UbStudyHelpGenerator.Classes
{
    /// <summary>
    /// Change TUB text formating from/to exemplar notation
    /// Exemplar noitation was originally developd by Troy Bishop and after that enlarged by Larry Watkins to fit UF needs.
    /// Below, unchenged in this first version, a final definition as proposed by Larry in 2015 and the one we are using to store texts in database.
    /// </summary>
    public class Exemplar
    {
        /*
         * 
            #######################################################################
            #
            # Exemplar2XHTM_Codes.txt
            #
            # CONTROL FILE - Conversion between exemplar codes and XHTML codes
            # Comments imbeded in the list
            #
            # Italics (<i>) is the primary code used by English and its translations.
            # Some translations may use BOLD (<strong>) or underline.
            # Other than these few codes the rest are special cases for the markup
            # of the English text... comments where the SRT committee has recommended
            # altrations to the 1955 text, footnotes, links to the SRT document,
            # text color change for Jesus' words, text color changes for italicized
            # Jesus' words, small caps -- all in the English text but not found in
            # translations.
            #
            #######################################################################

            #Exemplar : XHTML
            {a{a{   : '<a href='

            {b{b{   : '<strong>'
            }b}b}   : '</strong>'
            {c{c{   : '<!--'
            }c}c}   : '-->'

            # <i> inside <font> --> {d{d{    </i> inside <font> --> }d}d} to produce colored italics
            # <font> --> {d{d{              -- }s}s} used instead
            {d{d{ : '<em>'
            }d}d} : '</em>'

            {e{e{   : '<a id="U'
            }e}e}   : '"></a>'
            }a}a}   : '</a>'
            }x}x}   : '> *</a>'

            # Superscript (st, nd, rd, etc.):
            # {f{f{   : '<span style="vertical-align: super; font-size: smaller">'  --{h{h{ used instead
            # {g{g{   : '<font color="#663333">' -- replaced by {r{r{
            # }g}g}   : '</span> '              -- }s}s} used instead
            {h{h{   : '<sup>'
            }h}h}   : '</sup>'
            {i{i{   : '<em>'
            }i}i}   : '</em>'
            {k{k{   : '<span class="SCaps">'
            }k}k}   : '</span>'
            {p{p{   : '<p>'
            }p}p}   : '</p>'
            {r{r{   : '<span class="Colored">'
            }r}r}   : '</span>'
            }s}s}   : '</span>'
            {t{t{   : '<span class="ColItal">'
            }t}t}   : '</span>'
            {u{u{   : '<span class="UL">'
            # }u}u}   : '</span>'            -- }s}s} used instead
            {x{x{   : '<a href=TextStandardization.htm#U'

            {img_1} : '<img src="urantia_logo.gif" alt="concentric circles, TM, CR" width="110" height="110" />'
            {img_2} : '<img src="urantia_logo2.gif" alt="concentric circles, TM" width="110" height="82" />'
            {img_3} : '<img src="urantia_logo3.gif" alt="tiny concentric circles" width="23" height="21" />'
            # {blockquote}  : '<blockquote>'  -- in template, not exemplar
            # {/blockquote} : '</blockquote>' -- in template, not exemplar
            {br}    : '<br />'
            ”       : &rdquo;
            “       : &ldquo;
            \’      : &rsquo;
            \`      : &lsquo;
            \‘      : &lsquo;
             *— *   : &mdash;
            –       : &ndash;
            #
            # {n{n{, {z{z{ are program controlled external link codes
            {n{n{ --> '<a name=Footnotes.htm#'
            # }n}n} --> '">'

        */

        /*
            Vault_ListExemplarCodes.pl Message File

            Exemplar codes used in the English exemplar fileset

            Counts	Code
            1838	}i}i}
            145	}c}c}
            130	{c{c{
            12	}h}h}
            12	{h{h{
            1112	}r}r}
            1186	{r{r{
            5	}k}k}
            1	{img_1}
            59	}d}d}
            75	{x{x{
            5	{k{k{
            81	}x}x}
            1	{img_3}
            1838	{i{i{

              Total files read: 201
              Total exemplar codes in English fileset: 3248
              Unique exemplar codes: 34


            End of ListExemplarCodes.pl
            Ended at September 17, 2015 - 5:5.56pm

          * */

        private static void removeTags(StringBuilder sb, string startTag, string endTag)
        {
            int indStart = sb.ToString().IndexOf(startTag);
            if (indStart < 0)
                return;
            do
            {
                int indEnd = sb.ToString().IndexOf(endTag);
                if (indEnd < 0)
                    return;
                indEnd += endTag.Length;
                sb.Remove(indStart, indEnd - indStart);
                indStart = sb.ToString().IndexOf(startTag);
            } while (indStart >= 0);
            string xxx = sb.ToString();
        }

        private static void replaceTags(StringBuilder sb, string startTag, string endTag, string startReplaceTag, string endReplaceTag)
        {
            sb.Replace(startTag, startReplaceTag);
            sb.Replace(endTag, endReplaceTag);
        }

        public static string ExemplarToHtml(string exemplarText,
                                            bool keepStandardization = false,
                                            bool keepColored = false)
        {
            StringBuilder sb = new StringBuilder(exemplarText);

            // Some tags are just removed
            // Text Standardization 
            if (keepStandardization)
                replaceTags(sb, "{x{x{", "}x}x}", "<a href=TextStandardization.htm#U", ">*</a>");
            else
                removeTags(sb, "{x{x{", "}x}x}");

            // SCaps tag
            replaceTags(sb, "{k{k{", "}k}k}", "<span class=\"SCaps\">", "</span>");

            // Italics
            replaceTags(sb, "{i{i{", "}i}i}", "<em>", "</em>");
            replaceTags(sb, "{d{d{", "}d}d}", "<em>", "</em>");

            // Text Standardization 
            if (!keepStandardization)
            {
                //replaceTags(sb, "{r{r{", "}x}x}", "<span class=\"Colored\">", "</span>");
                replaceTags(sb, "{r{r{", "}r}r}", "<span class=\"Colored\">", "</span>");

            }
            else
            {
                //removeTags(sb, "{r{r{", "}x}x}");
                removeTags(sb, "{r{r{", "}r}r}");
            }

            // Html comment
            removeTags(sb, "{c{c{", "}c}c}");
            removeTags(sb, "{z{z{", "}z}z}");

            // Sup
            replaceTags(sb, "{h{h{", "}h}h}", "<sup>", "</sup>");


            // Bold
            replaceTags(sb, "{b{b{", "}b}b}", "<b>", "</b>");

            // Tags just removed
            // Comments
            removeTags(sb, "{c{c{", "}c}c}");
            removeTags(sb, "{u{u{", "}u}u}");
            removeTags(sb, "{f{f{", "}s}s}");

            // Replacements
            sb.Replace("{img_1}", "<img src=\"urantia_logo.gif\" alt=\"concentric circles, TM, CR\" width=\"110\" height=\"110\" />");
            sb.Replace("{img_2}", "<img src=\"urantia_logo2.gif\" alt=\"concentric circles, TM\" width=\"110\" height=\"82\" />");
            sb.Replace("{img_3}", "<img src=\"urantia_logo3.gif\" alt=\"tiny concentric circles\" width=\"23\" height=\"21\" />");
            sb.Replace("&apos", "");

            //sb.Replace("{{{br}}}", "<br />");
            //sb.Replace("{br}", "<br />");
            sb.Replace("{{{br}}}", "");
            sb.Replace("{br}", "");

            sb.Replace("{n{n{", "");

            return sb.ToString(); ;
        }
    }
}
