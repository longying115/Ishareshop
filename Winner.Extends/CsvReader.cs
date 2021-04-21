using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Winner.Extends
{
    public class CsvReader
    {
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
            if (string.IsNullOrEmpty(ext) || !".csv".Equals(ext, StringComparison.OrdinalIgnoreCase))
                throw new NotSupportedException("不支持的文件类型！");

            var dataTable = new DataTable();
            using (var reader = new CSVReaderHelper(new StreamReader(fileName, FileHelper.GetFileEncodeType(fileName))))
            {
                // 读取并创建表格头部
                int rowIndex = 0;
                var headRowCells = ReadRowCells(rowIndex, reader.ReadRow());
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

                int emptyRowCount = 0;
                while (true)
                {
                    rowIndex++;
                    var row = reader.ReadRow(); // 第二行开始是数据
                    if (row != null && row.Count > 0)
                    {
                        if (!IsEmptyRow(row))
                        {
                            emptyRowCount = 0;
                            var dr = dataTable.NewRow();
                            foreach (var item in usedHeadCells)
                            {
                                if (dataTable.Columns.Count > item.ColumnIndex)
                                    dr[item.ColumnIndex] = row[item.ColumnIndex];
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
                    else
                    {
                        break;
                    }
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 读取行数据
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        private List<DataCell> ReadRowCells(int rowIndex, List<object> row)
        {
            var result = new List<DataCell>();
            for (int i = 0; i < row.Count; i++)
            {
                var dc = new DataCell { ColumnIndex = i, RowIndex = rowIndex, Value = row[i] + "" };
                result.Add(dc);
            }

            return result;
        }

        /// <summary>
        /// 是否为空行
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private bool IsEmptyRow(List<object> row)
        {
            foreach (var cell in row)
            {
                if (cell != null && !string.IsNullOrEmpty(cell.ToString()))
                    return false;
            }

            return true;
        }
    }
}
