using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogTools
{
    public class Search:Error
    {
        protected bool isHasDOC = false;
        /// <summary>
        /// 获得所有磁盘名称
        /// </summary>
        /// <returns></returns>
        public List<string> getAllDiskName()
        {
            List<string> diskName = new List<string>();
            try
            { 
                System.IO.DriveInfo[] allDrives = System.IO.DriveInfo.GetDrives();
                for (int i = 0; i < allDrives.Length; i++)
                {
                    diskName.Add(allDrives[i].Name);
                }
            }
            catch (Exception e)
            {
                this.showError(e.Message);
            }

            return diskName;
        }
 
        /// <summary>
        /// 搜索含有焊接记录文件夹的磁盘的文件夹内txt文档路径
        /// </summary>
        /// <returns></returns>
        public  List<string> getDiskFileNamePosition(List<string> ListDiskName)
        {
            List<string> listDOCPosition = new List<string>();
            for (int i = 0; i < ListDiskName.Count; i++)
            {
                string path = ListDiskName[i].ToString().Substring(0, 2) + @"/焊接记录/";
                try
                {
                    DirectoryInfo root = new DirectoryInfo(path);
                    FileInfo[] files = root.GetFiles("*.txt");
                    if (files.Length > 0)
                    {
                        for (int j = 0; j < files.Length; j++)
                        {
                            listDOCPosition.Add(path + files[j].Name);
                            Console.WriteLine(path + files[j].Name); 
                        }
                        isHasDOC = true;//搜索到文档标记为true
                        DS.Path = path;//存储搜索到的路径
                        Console.WriteLine("文件搜索成功，即将退出搜索"); 
                        break;
                    }
                }
                catch 
                {
                    //this.showError(e.Message);
                    isHasDOC = false;
                    Console.WriteLine(path + "  not found"); 

                } 
            }

            return listDOCPosition;
        }

        /// <summary>
        /// 获取指定文件夹下的文档
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> getAppointedFolderPositionDOC(string path)
        {
            List<string> listDOCPosition = new List<string>();

            string p = path;
            try
            {
                DirectoryInfo root = new DirectoryInfo(p);
                FileInfo[] files = root.GetFiles("*.txt");
                if (files.Length > 0)
                {
                    for (int j = 0; j < files.Length; j++)
                    {
                        listDOCPosition.Add(p + files[j].Name);
                        Console.WriteLine(p + " 搜索到 " + p + files[j].Name);
                    }
                    isHasDOC = true;//搜索到文档标记为true
                    DS.Path = p;//存储搜索到的路径
                    Console.WriteLine("获取指定文件夹下的文档成功，即将退出搜索"); 
                }
            }
            catch
            {
                //this.showError(e.Message);
                isHasDOC = false;
                Console.WriteLine(path + "   获取指定文件夹下的文档 失败");
            } 
            return listDOCPosition;
        }

        /// <summary>
        /// 获取指定文件夹下 指定范围 文档名称和文档路径
        /// </summary>
        /// <param name="foderList"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public  List<string> SearchRangeDOCNamePosition(string appointedFolder, DateTime startTime, DateTime endTime)
        {
            //指定时间范围的文档
            List<string> listAppointedRange = new List<string>(); 
            //文档搜索到再进行筛选
            if (isHasDOC)
            {
                List<string> listAllDOC = getAppointedFolderPositionDOC(appointedFolder);
                for (int i = 0; i < listAllDOC.Count; i++)
                {
                    string time = "20" + listAllDOC[i].ToString().Substring(listAllDOC[i].Length - 12, 8);
                    string y = time.Substring(0, 4);
                    string m = time.Substring(4, 2);
                    string d = time.Substring(6, 2);
                    string h = time.Substring(8, 2);
                    string ntime = y + "-" + m + "-" + d + " " + "00" + ":" + "00" + ":" + "00";
                    try
                    {
                        DateTime dt = Convert.ToDateTime(ntime);//文档时间有可能会出错
                        //将指定范围内的文档存储至列表
                        if (dt >= startTime && dt <= endTime)
                        {
                            string otime = "20" + listAllDOC[i].Substring(listAllDOC[i].Length - 12, 8); ;
                            string ny = otime.Substring(0, 4);
                            string nm = otime.Substring(4, 2);
                            string nd = otime.Substring(6, 2);
                            string nh = otime.Substring(8, 2);
                            string nntime = ny + "-" + nm + "-" + nd + " " + nh + " 时";
                            listAppointedRange.Add(nntime);
                        }
                    }
                    catch (Exception e)
                    {
                        //提示时间转换错误
                        this.showError(e.Message);
                    }
                }
            }
            else
                this.showError("搜索不到txt文档");
            
            return listAppointedRange;
        }

        public void showError(string err)
        {
            Console.WriteLine(err);
        }
    }
}
