using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatalogTools
{
    public class Query
    {
        /// <summary>
        /// 查询当日焊接数量
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<WeldModel> QueryWeldCountToDay(DateTime startTime, DateTime endTime)
        {
            return null;
        }

        /// <summary>
        /// 读取指定文档内容
        /// </summary>
        /// <param name="docPosition">文档列表的选中项  yyyy-MM-dd hh:mm:ss</param>
        /// <returns></returns>
        public List<WeldModel> ReadAppointedDOC(string docPosition)
        {
            StreamReader sr = new StreamReader(DS.Path + docPosition, Encoding.Default);
            List<WeldModel> listweldModel = new List<WeldModel>();
            string content;
            while ((content = sr.ReadLine()) != null)
            {
                if (content.ToString() != "")
                {
                    WeldModel wm = new WeldModel(); //焊接整体对象
                    List<SegmentNumberModel> listsnm = new List<SegmentNumberModel>(); //焊接整体对象中的段数列表属性

                    /*********************************************    焊接整体对象  上半部分       *********************************************/
                    do
                    {
                        if (content.ToString().Contains('第') && content.ToString().Contains('次'))
                        {
                            wm.Number = content;
                        }
                        else if (content.Contains('年') || (content.ToString().Contains('.') && content.ToString().Contains(':')))
                        {
                            wm.Time = content;
                        }
                        else if (content.Contains('工') && content.ToString().Contains('程'))
                        {
                            wm.ProjectNumber = content;
                        }
                        else if (content.Contains('焊') && content.ToString().Contains('工'))
                        {
                            wm.WelderNumber = content;
                        }
                        else if (content.Contains('焊') && content.ToString().Contains('口'))
                        {
                            wm.WeldNumber = content;
                        }
                        else if (content.Contains('单') && content.ToString().Contains('位'))
                        {
                            wm.UnitNumber = content;
                        }
                        else if (content.Contains('焊') && content.ToString().Contains('接'))
                        {
                            wm.WeldingModel = content;
                        }
                    } while ((content = sr.ReadLine()) != null && content != "" && !content.ToString().Contains('第') && !content.ToString().Contains('段'));

                    /*********************************************    焊接整体对象  下半部分（段数）          *********************************************/
                    do//读取指定段数内所有数据
                    {
                        if (content.Contains('第') && content.ToString().Contains('段'))//有多段
                        {
                            SegmentNumberModel snm = new SegmentNumberModel();//段数对象
                            snm.SegmentNumber = content;
                            for (int i = 1; i <= 3; i++)
                            {
                                if ((content = sr.ReadLine()) != null && content != "" && !content.Contains('第') && !content.Contains('次'))
                                {
                                    if (content.Contains('电') && content.ToString().Contains('流'))
                                    {
                                        snm.ElectricCurrent = content;
                                    }
                                    else if (content.Contains('电') && content.ToString().Contains('压'))
                                    {
                                        snm.Voltage = content;
                                    }
                                    else if (content.Contains('焊') && content.ToString().Contains('接') && content.ToString().Contains('时') && content.ToString().Contains('间'))
                                    {
                                        snm.WeldTime = content;
                                    }
                                    else if (content.Contains('冷') && content.ToString().Contains('却'))
                                    {
                                        snm.CooldingTime = content;
                                    }
                                }
                                else
                                    break;
                            }
                            listsnm.Add(snm);//循环存储多个段数  
                        }
                    } while ((content = sr.ReadLine()) != null && content != "" && !content.Contains('次'));
                    wm.SegmentNumber = listsnm;//存储的段数集合   
                    listweldModel.Add(wm);//循环存储焊接模型至集合
                }
                else
                    continue;
            }


            return listweldModel;
        }


    }
}
