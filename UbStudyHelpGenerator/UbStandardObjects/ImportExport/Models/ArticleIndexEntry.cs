using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models
{
    /// <summary>
    /// Implements the concept of an article index entry.
    /// </summary>
    public class ArticleIndexEntry
    {
        public string Titulo { get; set; }
        public string Sumario { get; set; }
        public string Link { get; set; }

        public override string ToString()
        {
            return $"Título: {Titulo}\nSumário: {Sumario}\nLink: {Link}\n";
        }
    }
}
