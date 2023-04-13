using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogTools
{
   public class DS
    {
       /// <summary>
       /// 存储搜索到的路径
       /// </summary>
       public static string Path = "";
      // public static WeldModel wm { get; set; }

       private  List<WeldModel> wModel = new List<WeldModel>();
       private List<string> docNameList = new List<string>();
      

       /// <summary>
        /// 将指定文档数据载入集合
       /// </summary>
       /// <param name="wm"></param>
       /// <returns></returns>
       public List<WeldModel> LoadAppointedDOCToDS(List<WeldModel> wm)
       {  
           wModel = wm;
           return wModel;
       }

       /// <summary>
       /// 将文档名称载入集合
       /// </summary>
       /// <param name="DOCNameList"></param>
       /// <returns></returns>
       public List<string> LoadDOCListToDS(List<string> DOCNameList){
            
           docNameList = DOCNameList;
           return docNameList;
       }

    }
}
