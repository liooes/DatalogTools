using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DatalogTools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, Error
    { 

        Search search = new Search();
        Query query = new Query();

        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        /// <summary>
        /// 初始化界面数据
        /// </summary>
        private void init()
        {
            List<string> listDiskName = search.getAllDiskName(); //获得所有磁盘名称
            List<string> listDOC = initlstDOCItems(listDiskName); //加载文件名至列表
            try
            {
                initdpSelected(listDOC);//初始化日期控件选择项 
                // initDOCListSelected();//初始化文档列表选中值
                //initcbStartEndTime();//加载起始和结束时间
            }
            catch (Exception e)
            {
                showError(e.Message);
            }
        }

        /// <summary>
        /// 初始化文档列表选中值
        /// </summary>
        private void initDOCListSelected()
        {
            lstDOC.SelectedIndex = 0; //初始化文档列表选中值 
        }

        /// <summary>
        /// //初始化日期控件选择项
        /// </summary>
        /// <param name="listDOC"></param>
        private void initdpSelected(List<string> listDOC)
        {
            if (listDOC != null && listDOC.Count > 0)
            {
                string time = "20" + listDOC[0].ToString().Substring(8, 8);
                string y = time.Substring(0, 4);
                string m = time.Substring(4, 2);
                string d = time.Substring(6, 2);
                string h = time.Substring(8, 2);
                string ntime = y + "-" + m + "-" + d + " " + h + ":" + "00" + ":" + "00";
                try
                {
                    DateTime dt = Convert.ToDateTime(ntime);//文档时间有可能会出错 
                   dpStartTime.SelectedDate = dt;
                }
                catch (Exception e)
                {
                    //提示时间转换错误
                    showError("指定文件夹下有错误的时间文档命名 \n" + e.Message);
                }
            }
        }

        /// <summary>
        /// 加载文件名至列表(自动)
        /// </summary>
        /// <param name="listDiskName"></param>
        /// <returns></returns>
        private List<string> initlstDOCItems(List<string> listDiskName)
        {
            lstDOC.Items.Clear();//加载前清空所有数据  
             
                List<string> listDOC = search.getDiskFileNamePosition(listDiskName);//搜索txt
                if (listDOC.Count > 0)
                {
                    List<DateTime> listntime = new List<DateTime>();
                    for (int i = 0; i < listDOC.Count; i++)
                    {
                        try
                        {
                            string time = "20" + listDOC[i].ToString().Substring(8, 8);
                            string y = time.Substring(0, 4);
                            string m = time.Substring(4, 2);
                            string d = time.Substring(6, 2);
                            string h = time.Substring(8, 2);
                            string ntime = y + "-" + m + "-" + d + "    " + h + ":00:00";
                            listntime.Add(DateTime.Parse(ntime));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("加载文件名至列表(自动)" + e.Message);
                        }
                    }
                    lstDOCDescAdd(listntime);//焊接列表倒序添加 
                    initDOCListSelected();//加载完之后默认选择第一个值
                }
                return listDOC;
             
        }

       
        /// <summary>
        /// 加载文档名至列表（手动）
        /// </summary>
        /// <param name="listPathDOC">指定路径下的文档列表</param>
        private void LoadDOCTolstDOCItems(List<string> listPathDOC)
        {
            lstDOC.Items.Clear();//加载前清空所有数据 
            if (listPathDOC.Count > 0)
            {
                List<DateTime> listntime = new List<DateTime>();
                for (int i = 0; i < listPathDOC.Count; i++)
                {
                    try
                    {
                        //不同的路径长度不同
                        int len = (listPathDOC[i].ToString().Length - 12);
                        string time = listPathDOC[i].ToString().Substring(len, 8);

                        string y = time.Substring(0, 2);
                        string m = time.Substring(2, 2);
                        string d = time.Substring(4, 2);
                        string h = time.Substring(6, 2);
                        string ntime = "20" + y + "-" + m + "-" + d + "    " + h + ":00:00";
                        listntime.Add(DateTime.Parse(ntime));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("加载文件名至列表(手动)" + e.Message);
                    } 
                }
                lstDOCDescAdd(listntime);//焊接列表倒序添加 
                initDOCListSelected();//加载完之后默认选择第一个值 
            } 
        }

        /// <summary>
        /// 焊接列表倒序添加
        /// </summary>
        /// <param name="listntime"></param>
        private void lstDOCDescAdd(List<DateTime> listntime)
        {
            //排序
            listntime.Sort();
            //倒序添加
            for (int i = listntime.Count - 1; i >= 0; i--)
            {
                string y = listntime[i].ToString("yyyy");
                string m = listntime[i].ToString("MM");
                string d = listntime[i].ToString("dd");
                string h = listntime[i].ToString("HH");
                string st = y + "-" + m + "-" + d + " " + h + " 时";
                lstDOC.Items.Add(st);
            }
        }


        ///// <summary>
        ///// 加载文档名至列表 
        ///// </summary>
        ///// <param name="rangeTimelistDOC">指定时间范围列表数据</param>
        //private void LoadRangeTimelistDOCTolstDOCItems(List<string> rangeTimelistDOC)
        //{
        //    lstDOC.Items.Clear();//加载前清空所有数据 
        //    if (rangeTimelistDOC.Count > 0)
        //    {
        //        for (int i = 0; i < rangeTimelistDOC.Count; i++)
        //        {
        //            lstDOC.Items.Add(rangeTimelistDOC[i]);
        //        }
        //        initDOCListSelected();
        //    }
        //}

        ///// <summary>
        ///// 加载起始和结束时间
        ///// </summary>
        //private void initcbStartEndTime()
        //{
        //    //加载起始和结束时间
        //    for (int i = 0; i < 24; i++)
        //    {
        //        if (i <= 9)
        //        {
        //            cbStartTime.Items.Add("0" + i.ToString());
        //            cbEndTime.Items.Add("0" + i.ToString());
        //        }
        //        else
        //        {
        //            cbStartTime.Items.Add(i.ToString());
        //            cbEndTime.Items.Add(i.ToString());
        //        }

        //    }
        //    //设置默认选中值
        //    cbStartTime.SelectedIndex = 0;
        //    cbEndTime.SelectedIndex = 1;
        //}

        public void showError(string err)
        {
            System.Windows.MessageBox.Show(err);
        }

        /// <summary>
        /// 列表选择项更改 绑定集合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDOC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string str = lstDOC.SelectedValue.ToString(); 
                //2013-53-14    13 时 18
                //2019-01-13 14 时     15
                string yMd = str.Substring(2, 2) + str.Substring(5, 2) + str.Substring(8, 2);
                string hms = str.Substring(str.Length - 4, 2);
                string ntime = yMd + hms + ".txt";
                 
                List<WeldModel> list = query.ReadAppointedDOC(ntime);
                 
                binDingData(list); //绑定数据至集合
            }
            catch (Exception err)
            {
                binDingData(null); 
                Console.WriteLine(err.Message);
            }

        }

        /// <summary>
        /// 绑定数据至集合
        /// </summary>
        /// <param name="wmList"></param>
        private void binDingData(List<WeldModel> wmList)
        {
            if (wmList != null)
            {
                List<WeldModel> newwmList = formatWelderList(wmList);
                dg.ItemsSource = newwmList;
            }
            else
                dg.ItemsSource = null;
        }

        /// <summary>
        /// 格式化列表数据
        /// </summary>
        /// <param name="wmList"></param>
        /// <returns></returns>
        private List<WeldModel> formatWelderList(List<WeldModel> wmList)
        {
            List<WeldModel> nlist = new List<WeldModel>();
            if (wmList != null)
            {
                for (int i = 0; i < wmList.Count; i++)
                {
                    // Number  Time ProjectNumber WelderNumber WeldNumber UnitNumber WeldingModel SegmentNumber
                    WeldModel wm = new WeldModel();
                    wm.Number = wmList[i].Number;
                    wm.Time = wmList[i].Time;
                    wm.ProjectNumber = wmList[i].ProjectNumber.Substring(wmList[i].ProjectNumber.Length - 4, 4);
                    wm.WelderNumber = wmList[i].WelderNumber.Substring(wmList[i].WelderNumber.Length - 4, 4);
                    wm.WeldNumber = wmList[i].WeldNumber.Substring(wmList[i].WeldNumber.Length - 4, 4);
                    wm.UnitNumber = wmList[i].UnitNumber.Substring(wmList[i].UnitNumber.Length - 4, 4);
                    wm.WeldingModel = wmList[i].WeldingModel.Substring(wmList[i].WeldingModel.Length - 4, 4);
                    wm.SegmentNumber = wmList[i].SegmentNumber;//
                    nlist.Add(wm);
                }
                Console.WriteLine("集合数据格式化完毕");
            }
            else
                Console.WriteLine("集合数据为空");
            return nlist;
        }
 
        /// <summary>
        /// 集合选中项更改显示详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            //WeldModel wm = (WeldModel)dg.SelectedItem;
            //if (wm != null)
            //{
            //    WeldModelDetail wmd = new WeldModelDetail(wm);
            //    wmd.ShowDialog();
            //}
        }

        /// <summary>
        /// 拖动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void title_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMini_Click(object sender, RoutedEventArgs e)
        {
            WindowState = System.Windows.WindowState.Minimized;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            exit();
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_about_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_systemSetting_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_exit_Click(object sender, RoutedEventArgs e)
        {
            exit();
        }

        /// <summary>
        /// 选择文件夹菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_selectFolder_Click(object sender, RoutedEventArgs e)
        {
            string path = SelectedFolderPosition();
            if (path != null && path != "")
            {
                List<string> listDOC = search.getAppointedFolderPositionDOC(path + "\\");//文件夹下的文档
                LoadDOCTolstDOCItems(listDOC);

                if (listDOC != null && listDOC.Count <= 0)
                {
                    showError("指定文件夹下没有后缀为.txt的文档");
                }
            }
        }

        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <returns>文件夹路径</returns>
        public string SelectedFolderPosition()
        {
            string path = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = fbd.SelectedPath;
            }
            return path;
        }

        /// <summary>
        /// 筛选指定范围时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectedRangeTime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dts = (DateTime)dpStartTime.SelectedDate;
                DateTime dte = (DateTime)dpEndTime.SelectedDate;
                if (dts != null && dte != null)
                {
                    try
                    {
                        string str = lstDOC.SelectedValue.ToString();    //获得选中的文档
                        //2013-53-14    13 时 18
                        //2019-01-13 14 时     15
                        string yMd = str.Substring(2, 2) + str.Substring(5, 2) + str.Substring(8, 2);
                        string hms = str.Substring(str.Length - 4, 2);
                        string ntime = yMd + hms + ".txt";

                        List<WeldModel> list = query.ReadAppointedDOC(ntime);   //读取选中文档内容
                        //起始时间
                        string sy = dts.ToString("yyyy");
                        string sm = dts.ToString("MM");
                        string sd = dts.ToString("dd");
                        // string sh = dts.ToString("HH");
                        string st = sy + "-" + sm + "-" + sd + " " + "00:00:00";
                        DateTime ndts = DateTime.Parse(st);
                        //结束时间
                        string ey = dte.ToString("yyyy");
                        string em = dte.ToString("MM");
                        string ed = dte.ToString("dd");
                        // string eh = dts.ToString("HH");
                        string se = ey + "-" + em + "-" + ed + " " + "00:00:00";
                        DateTime ndte = DateTime.Parse(se);
                        //筛选数据 
                        for (int i = list.Count - 1; i >= 0; i--)
                        {
                            try
                            {
                                string stime = list[i].Time.ToString();

                                string stimey = stime.Substring(0, 4);
                                string stimem = stime.Substring(5, 2);
                                string stimed = stime.Substring(8, 2);
                                string sdoct = stimey + "-" + stimem + "-" + stimed + " " + "00:00:00";
                                DateTime dtdoc = DateTime.Parse(sdoct);

                                if (!(dtdoc >= ndts && dtdoc <= ndte))
                                {
                                    list.Remove(list[i]);
                                    //Console.WriteLine(list[i].Time);
                                }
                            }
                            catch (Exception er)
                            {
                                list.Remove(list[i]);
                                // Console.WriteLine(list[i].Time);
                                Console.WriteLine("筛选集合数据" + er.Message);
                            }
                        }
                        binDingData(list); //绑定数据至集合
                    }
                    catch (Exception err)
                    {
                        binDingData(null);
                        Console.WriteLine(err.Message);
                    }


                    //string time = dts.ToString();
                    //string y = time.Substring(0, 4);
                    //string m = time.Substring(4, 2);
                    //string d = time.Substring(6, 2); 
                    // string ntime = y + "-" + m + "-" + d + " " + h + ":" + "00" + ":" + "00";
                    //List<string> rangeTimelist =  search.SearchRangeDOCNamePosition(DS.Path, dts, dte);
                    //LoadRangeTimelistDOCTolstDOCItems(rangeTimelist);//将筛选的时间加载至列表
                }
            }
            catch (Exception err)
            {
                 showError("起始时间和结束时间不能为空！"+err.Message);
            }

           
        }

        /// <summary>
        /// 关闭所有窗口并退出应用程序
        /// </summary>
        private void exit()
        {
            this.Close();
            Environment.Exit(0);
        }

        /// <summary>
        /// 鼠标双击显示详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WeldModel wm = (WeldModel)dg.SelectedItem;
            if (wm != null)
            {
                WeldModelDetail wmd = new WeldModelDetail(wm);
                wmd.ShowDialog();
            }
        }

        /// <summary>
        /// 最大化最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                //双击执行
                if (WindowState == System.Windows.WindowState.Maximized)
                {
                    WindowState = System.Windows.WindowState.Normal;
                }else
                    WindowState = System.Windows.WindowState.Maximized;
            }
        }
    }
}

