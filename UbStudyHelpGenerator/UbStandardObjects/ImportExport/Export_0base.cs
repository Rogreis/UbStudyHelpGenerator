using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using UbStudyHelpGenerator.UbStandardObjects.ImportExport.Models;

namespace UbStudyHelpGenerator.UbStandardObjects.ImportExport
{
    public abstract class Export_0base : _0CommonBase
    {

        private bool Initialized = false;

        private void AddToZip(ZipArchive archive, string qsData)
        {
            // Create a new ZIP entry
            ZipArchiveEntry entry = archive.CreateEntry("qsd", CompressionLevel.Optimal);

            // Write content to the entry
            using (StreamWriter writer = new StreamWriter(entry.Open(), Encoding.UTF8))
            {
                writer.Write(qsData);
            }
        }

        protected void CompressToZip(string data, string zipPath)
        {
            // Create a ZIP file
            using (FileStream zipFile = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Create, leaveOpen: false))
            {
                // Add StringBuilder content as a file
                AddToZip(archive, data);
            }

        }

        protected string UnzipFromZip(string pathZip)
        {

            using (FileStream zipStream = new FileStream(pathZip, FileMode.Open))
            using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Read))
            {
                // Find the entry named "qsd"
                ZipArchiveEntry entry = archive.GetEntry("qsd");

                if (entry == null)
                    throw new FileNotFoundException("Entry 'qsd' not found in the ZIP archive.");

                // Read and return the content
                using (StreamReader reader = new StreamReader(entry.Open(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        protected void ClearFile(string pathFile)
        {
            if (File.Exists(pathFile))
                File.Delete(pathFile);
        }

        protected List<ParagraphExport> GetPaper(LiteDatabase db, string collectionName, short paper)
        {
            var col = db.GetCollection<ParagraphExport>(collectionName);
            return col.Find(x => x.Paper == paper)
                           .OrderBy(x => x.Pk_seq)
                           .ToList();
        }


        protected abstract void FillPaper(LiteDatabase db, string destyinationFolder, short paperNo);


    }

}
