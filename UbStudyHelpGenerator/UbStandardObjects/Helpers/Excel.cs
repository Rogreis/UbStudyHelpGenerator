using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UbStudyHelpGenerator.Classes;
using DataTable = System.Data.DataTable;

namespace UbStudyHelpGenerator.UbStandardObjects.Helpers
{
    public enum HorizontalAlignment
    {
        Left = 0,
        Center = 1,
        Right = 2
    }

    public class Excel
    {


        private Application oExcelApp = null;
        /// <summary>
        /// GEt the Excel application
        /// </summary>
        private Application ExcelApp
        {
            get
            {
                if (oExcelApp != null)
                {
                    return oExcelApp;
                }
                try
                {
                    oExcelApp = (Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application");
                }
                catch { }
                if (oExcelApp == null)
                {
                    oExcelApp = new Microsoft.Office.Interop.Excel.Application();
                }
                oExcelApp.Visible = true;
                return oExcelApp;
            }
        }

        private Workbook workbook = null;

        public DateTime ReportDate { get; private set; }

        public string DestinationFile { get; set; }

        /// <summary>
        /// Open a generic excel file
        /// </summary>
        /// <param name="pathExcelFile"></param>
        public Excel()
        {
        }

        public static void Duplicate(string origin, string destination, bool openDestination = true)
        {
            File.Copy(origin, destination, true);
            if (openDestination)
            {
                Process.Start(destination);
            }
        }

        /// <summary>
        /// If excel is open, try to get it
        /// Ignore errors
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Workbook GetExcelWorkbook(string name)
        {
            try
            {
                ExcelApp.Visible = true;
                foreach (Workbook workbook in ExcelApp.Workbooks)
                {
                    if (workbook.Name == name)
                    {
                        return workbook;
                    }
                }
            }
            catch
            {
            }
            return null;
        }

        /// <summary>
        /// Return the maximum number os columns and lines in a sheet
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="lines"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public bool GetTotalColumnsLines(string sheetName, ref int lines, ref int columns)
        {
            try
            {
                Worksheet sheet = workbook.Worksheets[sheetName];
                Range usedRange = sheet.UsedRange;
                lines = usedRange.Rows.Count;
                columns = usedRange.Columns.Count;
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Error getting total columns and lines from sheet {sheetName}", ex);
                return false;
            }
        }

        /// <summary>
        /// Get the horizontal column alignments
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public Dictionary<int, HorizontalAlignment> GetColumnAlignments(string sheetName)
            {
                Dictionary<int, HorizontalAlignment> columnAlignments = new Dictionary<int, HorizontalAlignment>();
                try
                {
                    Worksheet sheet = workbook.Worksheets[sheetName];
                    Range usedRange = sheet.UsedRange;
                    int columns = usedRange.Columns.Count;

                    for (int col = 1; col <= columns; col++)
                    {
                        Range columnRange = usedRange.Columns[col];
                        XlHAlign alignment = (XlHAlign)columnRange.HorizontalAlignment;
                        switch(alignment)
                        {
                            case XlHAlign.xlHAlignLeft:
                                columnAlignments.Add(col, HorizontalAlignment.Left);
                                break;
                            case XlHAlign.xlHAlignCenter:
                                columnAlignments.Add(col, HorizontalAlignment.Center);
                                break;
                            case XlHAlign.xlHAlignRight:
                                columnAlignments.Add(col, HorizontalAlignment.Right);
                                break;
                            default:
                                columnAlignments.Add(col, HorizontalAlignment.Left);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    EventsControl.FireShowMessage($"Error getting column alignments from sheet {sheetName}", ex);
                }
                return columnAlignments;
            }

    private void KillSpecificExcelFileProcess(string excelFileName)
        {
            var processes = from p in Process.GetProcessesByName("EXCEL")
                            select p;

            foreach (var process in processes)
            {
                if (process.MainWindowTitle.IndexOf(excelFileName) >= 0)
                {
                    process.Kill();
                }
            }
        }

        public void Dispose()
        {
            workbook.Save();
            workbook.Close();
            //xlApp.Quit();

            //if (!(workbook is null))
            //    Marshal.ReleaseComObject(workbook);
            //if (!(xlApp is null))
            //    Marshal.ReleaseComObject(xlApp);
        }



        /// <summary>
        /// Open excel forcing show
        /// </summary>
        /// <param name="pathExcelFile"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool Open(string pathExcelFile)
        {
            try
            {
                //KillSpecificExcelFileProcess(excelFileName);
                string excelFileName = Path.GetFileName(pathExcelFile);

                EventsControl.FireShowMessage($"Open Excel {excelFileName}");
                workbook = GetExcelWorkbook(excelFileName);
                if (workbook == null)
                {
                    EventsControl.FireShowMessage("workbook null");
                    if (File.Exists(pathExcelFile))
                    {
                        EventsControl.FireShowMessage("Openning existing excel file");
                        workbook = ExcelApp.Workbooks.Open(pathExcelFile, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows,
                            "\t", true, false, 0, true, 1, 0);

                        //System.Diagnostics.Process.Start(pathExcelFile);
                        //Thread.Sleep(5000);
                        //Program.Log.Info("Going to get the open excel file");
                        //workbook = GetExcelWorkbook(excelFileName);
                        if (workbook != null)
                        {
                            EventsControl.FireShowMessage("Workbook ok");
                            return true;
                        }
                    }
                    //Program.Log.Info("Trying to open then excel file");
                }
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Could not open excel file: {pathExcelFile}", ex);
                return false;
            }
        }

        /// <summary>
        /// Read a value from worksheet
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="lin"></param>
        /// <param name="col"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string ReadValue(string sheetName, int lin, int col)
        {
            Worksheet sheet = workbook.Worksheets[sheetName];
            string value = "";
            try
            {
                value = Convert.ToString(sheet.Cells[lin, col].Value2);
            }
            catch { }
            try
            {
                value = Convert.ToString(sheet.Cells[lin, col].FormulaR1C1);
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"*** Error reading from {sheetName}.{lin}.{col}: {ex.Message}");
            }
            return value;
        }

        public List<string> ReadStringList(string sheetName, int lin, int col)
        {
            Worksheet sheet = workbook.Worksheets[sheetName];
            string value = "";
            try
            {
                value = Convert.ToString(sheet.Cells[lin, col].Value2);
            }
            catch { }
            try
            {
                value = Convert.ToString(sheet.Cells[lin, col].FormulaR1C1);
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"*** Error reading from {sheetName}.{lin}.{col}: {ex.Message}");
            }

            char[] separator = { '\n' };
            string[] parts = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(parts);
        }

        public T ReadValue<T>(string sheetName, int lin, int col)
        {

            Worksheet sheet = workbook.Worksheets[sheetName];
            string value = "";
            try
            {
                value = Convert.ToString(sheet.Cells[lin, col].FormulaR1C1);
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"*** Error reading from {sheetName}.{lin}.{col}: {ex.Message}");
            }
            try
            {
                if (typeof(T) == typeof(bool))
                {
                    if (string.IsNullOrEmpty(value) || value.Trim() == "0")
                    {
                        return (T)Convert.ChangeType(false, typeof(T));
                    }
                    return (T)Convert.ChangeType(true, typeof(T));
                }
                if (typeof(T) == typeof(int) && string.IsNullOrEmpty(value))
                {
                    return default(T);
                }
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }
        }


        /// <summary>
        /// Check for existing sheet, creating it if it is the case
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public bool CreateSheet(string sheetName)
        {
            try
            {
                Worksheet ws = workbook.Worksheets.OfType<Worksheet>().FirstOrDefault(w => w.Name == sheetName);
                if (ws == null)
                {
                    workbook.Worksheets.Add(sheetName);
                }
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Could not create sheet: {sheetName}", ex);
                return false;
            }
        }

        /// <summary
        /// Search for a value inside a column
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="colNo"></param>
        /// <param name="data"></param>
        /// <param name="linNo"></param>
        /// <returns></returns>
        public bool ColumnSearch(string sheetName, int colNo, string data, ref int linNo)
        {
            try
            {
                Worksheet sheet = workbook.Worksheets[sheetName];
                Range theRange = (Range)sheet.UsedRange;
                for (int i = 1; i <= theRange.Rows.Count; i++)
                {
                    bool equal = false;
                    try
                    {
                        // ignore non string
                        object oData = sheet.Cells[i, colNo].Value2;
                        equal = (oData?.ToString() ?? "").Trim() == data;
                    }
                    catch { }
                    if (equal)
                    {
                        linNo = i;
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Error searching inside column {colNo} for data={data}", ex);
                return false;
            }
        }

        // Extension method, call for any object, eg "if (x.IsNumeric())..."
        public bool IsNumeric(object x) { return (x == null ? false : IsNumeric(x.GetType())); }

        // Method where you know the type of the object
        public bool IsNumeric(Type type) { return IsNumeric(type, Type.GetTypeCode(type)); }

        // Method where you know the type and the type code of the object
        public bool IsNumeric(Type type, TypeCode typeCode) { return (typeCode == TypeCode.Decimal || (type.IsPrimitive && typeCode != TypeCode.Object && typeCode != TypeCode.Boolean && typeCode != TypeCode.Char)); }

        public bool ColumnSearchForInt(string sheetName, int colNo, int val, ref int linNo)
        {
            try
            {
                Worksheet sheet = workbook.Worksheets[sheetName];
                Range theRange = (Range)sheet.UsedRange;
                for (int i = 2; i <= theRange.Rows.Count; i++)
                {
                    try
                    {
                        // ignore non string
                        object oData = sheet.Cells[i, colNo].Value2;
                        if (IsNumeric(oData) && val == Convert.ToInt32(oData))
                        {
                            linNo = i;
                            return true;
                        }
                    }
                    catch { }
                }
                return false;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Searching column {colNo} for int value {val}", ex);
                return false;
            }
        }


        public object VLookup(string sheetName, int valToSearchFor)
        {
            /*

                Arg1
                Object
                Lookup_value - the value to search in the first column of the table array. 
                Lookup_value can be a value or a reference. 
                If lookup_value is smaller than the smallest value in the first column of table_array, VLOOKUP returns the #N/A error value.

                Arg2
                Object
                Table_array - two or more columns of data. Use a reference to a range or a range name. 
                The values in the first column of table_array are the values searched by lookup_value. 
                These values can be text, numbers, or logical values. Uppercase and lowercase text are equivalent.

                Arg3
                Object
                Col_index_num - the column number in table_array from which the matching value must be returned. 
                A col_index_num of 1 returns the value in the first column in table_array; 
                a col_index_num of 2 returns the value in the second column in table_array, and so on.

                Arg4
                Object
                Range_lookup - a logical value that specifies whether you want the 
                VLookup(Object, Object, Object, Object) method to find an exact match or an approximate match:             
             
             * */

            Worksheet sheet = workbook.Worksheets[sheetName];
            Microsoft.Office.Interop.Excel.Range range = sheet.Columns["A"];
            dynamic val = ExcelApp.WorksheetFunction.VLookup(valToSearchFor, range, 1, false);

            return val;
            // public object VLookup (object Arg1, object Arg2, object Arg3, object Arg4);
        }

        public bool SheetSearch(string sheetName)
        {
            try
            {
                foreach (Worksheet sheet in workbook.Worksheets)
                {
                    if (sheet.Name == sheetName)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Searching for sheet {sheetName}", ex);
                return false;
            }
        }



        public bool RenameSheet(string sheetOldName, string sheetNewName)
        {
            try
            {
                if (workbook.Worksheets[1].Name == sheetNewName)
                {
                    return true;
                }
                workbook.Worksheets[sheetOldName].Name = sheetNewName;
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Renaming sheet {sheetOldName} - {sheetNewName}", ex);
                return false;
            }
        }



        /// <summary>
        /// Fill a cell with a list content
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="linNo"></param>
        /// <param name="colNo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Write(string sheetName, int linNo, int colNo, List<string> data)
        {
            try
            {
                Worksheet sheet = workbook.Worksheets[sheetName];
                if (data.Count > 0)
                {
                    sheet.Cells[linNo, colNo].Value2 = "'" + String.Join("\n", data);
                }
                else
                {
                    sheet.Cells[linNo, colNo].Value2 = "";
                }
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Writing value {data} into lin {linNo} col {colNo}", ex);
                return false;
            }
        }

        /// <summary>
        /// Write a value in a sheet
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="linNo"></param>
        /// <param name="colNo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Write(string sheetName, int linNo, int colNo, string data, string style = null)
        {
            try
            {
                Worksheet sheet = workbook.Worksheets[sheetName];
                sheet.Cells[linNo, colNo].Value2 = "'" + data;
                //object oData = sheet.Cells[linNo, colNo].Value2;
                if (!string.IsNullOrEmpty(style))
                {
                    SetLineStyle(sheetName, linNo, colNo, style);
                }
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Writing value {data} into lin {linNo} col {colNo}", ex);
                return false;
            }
        }

        /// <summary>
        /// Write a fórmula in the cell
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="linNo"></param>
        /// <param name="colNo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool WriteFormula(string sheetName, int linNo, int colNo, string data)
        {
            try
            {
                Worksheet sheet = workbook.Worksheets[sheetName];
                sheet.Cells[linNo, colNo].FormulaR1C1 = data;
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Writing value {data} into lin {linNo} col {colNo}", ex);
                return false;
            }
        }


        /// <summary>
        /// Set a style for a line
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="lin"></param>
        /// <param name="col"></param>
        /// <param name="style"></param>
        public void SetLineStyle(string sheetName, int lin, int col, string style)
        {
            Worksheet sheet = workbook.Worksheets[sheetName];
            Microsoft.Office.Interop.Excel.Range range = sheet.Range[sheet.Cells[lin, 1], sheet.Cells[lin, col]];
            range.Style = style;
        }


        /// <summary>
        /// Add a comment in a column
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="linNo"></param>
        /// <param name="colNo"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public bool AddComment(string sheetName, int linNo, int colNo, string comment)
        {
            try
            {
                Worksheet sheet = workbook.Worksheets[sheetName];

                if (sheet.Cells[linNo, colNo].Comment != null)
                {
                    sheet.Cells[linNo, colNo].Comment.Delete();
                }
                sheet.Cells[linNo, colNo].AddComment(comment);

                var textFrame = sheet.Cells[linNo, colNo].Comment.Shape.TextFrame;
                textFrame.Characters().Font.Name = "Consolas";
                textFrame.Characters().Font.Size = 10;
                textFrame.Characters().Font.Bold = false;
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Adding comment to sheet name {sheetName} into lin {linNo} col {colNo}", ex);
                return false;
            }
        }

        //public bool GetTotalColumnsLines(string sheetName, ref int lines, ref int columns)
        //{
        //    Worksheet sheet = workbook.Worksheets[sheetName];
        //    Range theRange = (Range)sheet.UsedRange;
        //    lines = theRange.Rows.Count;
        //    columns = theRange.Columns.Count;
        //    return true;
        //}

        /// <summary>
        /// Delete a range of lines
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="startLine"></param>
        /// <param name="totalLines"></param>
        /// <returns></returns>
        public bool DeleteLines(string sheetName, int startLine, int totalLines)
        {
            try
            {
                Worksheet sheet = workbook.Worksheets[sheetName];
                Range range = (Range)sheet.Range[sheet.Cells[startLine, 1], sheet.Cells[startLine + totalLines, 1]];
                Range entireRow = range.EntireRow;
                entireRow.Delete(Type.Missing);
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage("Error deleting excel lines, see log.", ex);
                return true;
            }
        }

        public void CloseWriteFile()
        {
            workbook.Save();
            workbook.Close();
            ExcelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(ExcelApp);
            Thread.Sleep(2000);
        }

        public List<string> GetSheetsList()
        {
            try
            {
                List<string> list = new List<string>();
                foreach (Worksheet sheet in workbook.Worksheets)
                {
                    list.Add(sheet.Name);
                }
                return list;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage("Getting sheets list", ex);
                return null;
            }
        }

        /// <summary>
        /// Insert a block of cells in Excel
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="values"></param>
        public bool SetRange(string sheetName, object[,] values, int fisrtLine, int firstColumn)
        {
            try
            {
                int rowCount = values.GetUpperBound(0);
                int columnCount = values.GetUpperBound(1);
                Worksheet sheet = workbook.Worksheets[sheetName];
                Range range = sheet.Range[sheet.Cells[fisrtLine, firstColumn], sheet.Cells[fisrtLine + rowCount, firstColumn + columnCount]];
                range.Value = values;
                return true;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Setting range fro sheet {sheetName}", ex);
                return false;
            }
        }

        public List<List<object>> GetRange(string sheetName)
        {
            try
            {
                Worksheet sheet = workbook.Worksheets[sheetName];

                // Find the last real row
                int rowCount = sheet.Cells.Find("*", System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               XlSearchOrder.xlByRows, XlSearchDirection.xlPrevious,
                                               false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Row;

                // Find the last real column
                int columnCount = sheet.Cells.Find("*", System.Reflection.Missing.Value,
                                               System.Reflection.Missing.Value, System.Reflection.Missing.Value,
                                               XlSearchOrder.xlByColumns, XlSearchDirection.xlPrevious,
                                               false, System.Reflection.Missing.Value, System.Reflection.Missing.Value).Column;

                List<List<object>> rowValue = new List<List<object>>();
                Range range = sheet.Range[sheet.Cells[1, 1], sheet.Cells[rowCount, columnCount]];
                for (int i = 1; i <= range.Rows.Count; i++)
                {
                    List<object> colValue = new List<object>();
                    for (int j = 1; j <= range.Columns.Count; j++)
                    {
                        colValue.Add(range.Cells[i, j].Value);
                    }
                    rowValue.Add(colValue);
                }
                return rowValue;
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"GetRange from sheet {sheetName}", ex);
                return null;
            }
        }



        /// <summary>
        /// Write all data in data table 
        /// Always start in line 7
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public bool WriteTable(string sheetName, DataTable dt, int fisrtLine, int firstColumn)
        {
            try
            {
                if (dt is null || dt.Rows.Count == 0)
                {
                    EventsControl.FireShowMessage("DataTable empty or null");
                    return true;
                }

                object[,] values = new object[dt.Rows.Count, dt.Columns.Count];
                int linNo = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int colNo = 1;
                    foreach (DataColumn col in dt.Columns)
                    {
                        values[linNo, colNo] = row[col];
                        colNo++;
                    }
                    linNo++;
                }
                return SetRange(sheetName, values, fisrtLine, firstColumn);
            }
            catch (Exception ex)
            {
                EventsControl.FireShowMessage($"Could not write table {dt.TableName} into sheet {sheetName} ", ex);
                return false;
            }
        }

        /// <summary>
        /// Get a full range of values from a excel sheet
        /// </summary>
        /// <param name="pathExcel"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public List<List<object>> ExcelToJson(string pathExcel, string sheetName)
        {
            List<List<object>> values = GetRange(sheetName);
            GetRange(sheetName);
            return values;
        }


        public void CsvToExcel(string pathCsv, string sheetName)
        {
            ExcelApp.Visible = true;
            Workbook workbook = ExcelApp.Workbooks.Open(pathCsv, Format: 6);
            Worksheet worksheet = (Worksheet)workbook.Worksheets[1];

            QueryTables tables = worksheet.QueryTables;
            QueryTable table = tables.Add(
                Connection: "TEXT;" + pathCsv,
                Destination: worksheet.Range["A1"]
            );

            table.Name = "CSV Import";
            table.FieldNames = true;
            table.RowNumbers = false;
            table.FillAdjacentFormulas = false;
            table.PreserveFormatting = true;
            table.RefreshOnFileOpen = false;
            table.RefreshStyle = XlCellInsertionMode.xlInsertDeleteCells;
            table.SavePassword = false;
            table.SaveData = true;
            table.AdjustColumnWidth = true;
            table.RefreshPeriod = 0;
            table.TextFilePromptOnRefresh = false;
            table.TextFilePlatform = 65001;  // UTF-8
            table.TextFileStartRow = 1;
            table.TextFileParseType = XlTextParsingType.xlDelimited;
            table.TextFileTextQualifier = XlTextQualifier.xlTextQualifierDoubleQuote;
            table.TextFileConsecutiveDelimiter = false;
            table.TextFileTabDelimiter = false;
            table.TextFileSemicolonDelimiter = false;
            table.TextFileCommaDelimiter = true;
            table.TextFileSpaceDelimiter = false;
            table.TextFileTrailingMinusNumbers = true;
            table.Refresh(false);


        }


    }
}
