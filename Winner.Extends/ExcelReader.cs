using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winner.Extends.Interfaces;

namespace Winner.Extends
{
    public class ExcelReader: IExcelReader
    {
        private static readonly string[] SupportFileExtensions = { ".xls", ".xlsx" };

        /// <summary>
        /// 读取到DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<DataTable> ToDataTableAsync(string fileName)
        {
            return await Task.Run(() => ToDataTable(fileName));
        }

        /// <summary>
        /// 读取到DataTable
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DataTable ToDataTable(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException(nameof(fileName));
            if (!File.Exists(fileName)) throw new FileNotFoundException("无效的文件路径", fileName);
            var ext = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(ext) ||
                SupportFileExtensions.All(t => !t.Equals(ext, StringComparison.OrdinalIgnoreCase)))
                throw new NotSupportedException("不支持的文件类型！");

            var dataTable = new DataTable();
            IWorkbook wb;
            using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                wb = WorkbookFactory.Create(file);
            }
            ISheet sheet = wb.GetSheetAt(0);
            dataTable.TableName = sheet.SheetName;

            int firstRowIndex = sheet.FirstRowNum;
            if (firstRowIndex >= 0)
            {
                // 读取并创建表格头部
                var headRowCells = ReadRowCells(sheet, firstRowIndex);
                var usedHeadCells = new List<DataCell>();
                foreach (var c in headRowCells)
                {
                    if (!string.IsNullOrWhiteSpace(c.Value) && !dataTable.Columns.Contains(c.Value))   // 重复列就不读取
                    {
                        usedHeadCells.Add(c);
                        var dc = new DataColumn(c.Value);
                        dataTable.Columns.Add(dc);
                    }
                }

                if (usedHeadCells.Any())
                {
                    // 读取数据
                    var dataStartRowIndex = firstRowIndex + 1;
                    int emptyRowCount = 0;
                    for (int i = dataStartRowIndex; i <= sheet.LastRowNum; i++)
                    {
                        var row = sheet.GetRow(i);
                        if (row != null)
                        {
                            if (!IsEmptyRow(row))
                            {
                                emptyRowCount = 0;
                                var dr = dataTable.NewRow();
                                foreach (var item in usedHeadCells)
                                {
                                    if (row.LastCellNum > item.ColumnIndex)
                                    {
                                        var cell = row.GetCell(item.ColumnIndex);
                                        if (cell != null)
                                        {
                                            dr[item.Value] = GetCellStringValue(cell);
                                        }
                                    }
                                }
                                dataTable.Rows.Add(dr);
                            }
                            else
                            {
                                emptyRowCount++;
                                if (emptyRowCount >= 3)
                                    break;
                            }
                        }
                    }
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 读取行数据
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private List<DataCell> ReadRowCells(ISheet sheet, int rowIndex)
        {
            var result = new List<DataCell>();
            if (rowIndex < sheet.FirstRowNum || rowIndex > sheet.LastRowNum)
            {
                return result;
            }

            var row = sheet.GetRow(rowIndex);
            for (int i = 0; i < row.LastCellNum; i++)
            {
                var cell = row.GetCell(i);
                if (cell != null)
                {
                    var dc = new DataCell { ColumnIndex = i, RowIndex = rowIndex, Value = GetCellStringValue(cell), CellType = cell.CellType };
                    result.Add(dc);
                }
            }

            return result;
        }

        /// <summary>
        /// excel行是否为空行
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsEmptyRow(IRow row)
        {
            bool isEmpty = true;
            foreach (ICell cell in row.Cells)
            {
                if (!string.IsNullOrEmpty(GetCellStringValue(cell)))
                {
                    isEmpty = false;
                    break;
                }
            }
            return isEmpty;
        }

        /// <summary>
        /// 取excel单元格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private string GetCellStringValue(ICell cell)
        {
            if (cell == null)
                return "";
            switch (cell.CellType)
            {
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                    return DateUtil.IsCellDateFormatted(cell)
                        ? cell.DateCellValue.ToString()
                        : cell.NumericCellValue.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                default:
                    return cell.ToString();
            }
        }
    }
    public class DataCell
    {
        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public string Value { get; set; }

        public CellType CellType { get; set; }
    }
}
