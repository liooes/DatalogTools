using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogTools
{
    interface Print
    {
        /// <summary>
        /// 打印指定数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool PrintAppointedData(string data);
    }
}
