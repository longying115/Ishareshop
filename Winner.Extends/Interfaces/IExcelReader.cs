using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Winner.Extends.Interfaces
{
    public interface IExcelReader
    {
        Task<DataTable> ToDataTableAsync(string fileName);
        DataTable ToDataTable(string fileName);
    }
}
