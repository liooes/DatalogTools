using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogTools
{
    interface Error
    { 
        /// <summary>
        /// 显示错误
        /// </summary>
        /// <param name="err"></param>
        void showError(string err);
    }
}
