using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DatalogTools
{
    /// <summary>
    /// WeldModelDetail.xaml 的交互逻辑
    /// </summary>
    public partial class WeldModelDetail : Window
    {
         
        public WeldModelDetail(WeldModel wm)
        {
            InitializeComponent();
            init(wm);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            IconHelper.RemoveIcon(this);
        }

        /// <summary>
        /// 初始化窗口参数
        /// </summary>
        private void init(WeldModel wm)
        { 
            this.MinHeight = 450;
            /************************************************************************   sp1   *************************************************/
            if (wm != null)
            {
                List<Label> listlb1 = new List<Label>();
                for (int i = 0; i < 7; i++)
                {
                    Label label = new Label();
                    listlb1.Add(label);
                }
              //  listlb1[0].Content = wm.Number;
                /*
                 工程编号、焊工编号、焊口编号、单位编号
                 */
                listlb1[1].Content = wm.Time;
                listlb1[2].Content = "工程编号：" + wm.ProjectNumber;
                listlb1[3].Content = "焊工编号：" + wm.WelderNumber;
                listlb1[4].Content = "焊口编号：" + wm.WeldNumber;
                listlb1[5].Content = "单位编号：" + wm.UnitNumber;
                listlb1[6].Content = "焊接模式：" + wm.WeldingModel;
                foreach (var item in listlb1)
                {
                    lstDetial.Items.Add(item);
                }

                /************************************************************************   sp2   *************************************************/
                List<SegmentNumberModel> listsnm = wm.SegmentNumber;
                List<Label> listlb2 = new List<Label>();
                for (int i = 0; i < listsnm.Count; i++)
                {
                    //段数
                    Label lb1 = new Label();
                    lb1.Content = wm.SegmentNumber[i].SegmentNumber;

                    Label lb2 = new Label();
                    if (wm.SegmentNumber[i].ElectricCurrent != null && wm.SegmentNumber[i].ElectricCurrent != "")//判断是否为电流 
                        lb2.Content = wm.SegmentNumber[i].ElectricCurrent; //电流
                    else
                        lb2.Content = wm.SegmentNumber[i].Voltage; //电压
                    //焊接时间
                    Label lb3 = new Label();
                    lb3.Content = wm.SegmentNumber[i].WeldTime;
                    //冷却时间
                    Label lb4 = new Label();
                    lb4.Content = wm.SegmentNumber[i].CooldingTime;
                    listlb2.Add(lb1);
                    listlb2.Add(lb2);
                    listlb2.Add(lb3);
                    listlb2.Add(lb4);
                }
                foreach (var item in listlb2)
                {
                    lstDetial.Items.Add(item);
                }
            }else
                Console.WriteLine("焊接模型对象为空");
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        { 
            try
            {
                PrintDialog dlg = new PrintDialog();
                if (dlg.ShowDialog() == true)
                {
                    dlg.PrintTicket.PageOrientation = PageOrientation.Portrait;
                    TextBlock visual = getVisual(dlg);
                    dlg.PrintVisual(visual, "doc");
                }
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.Message);
            }
          
        }

        private TextBlock getVisual(PrintDialog dlg)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Label lb in lstDetial.Items)
            {
                sb.Append("\t" + lb.Content);
                sb.Append("\r\n");
            }

            Run run = new Run(sb.ToString());
            TextBlock visual = new TextBlock();
            visual.Inlines.Add(run);
            visual.Margin = new Thickness(20, 5, 5, 5);
            visual.TextWrapping = TextWrapping.Wrap;
            visual.LayoutTransform = new ScaleTransform(1, 1);

            Size pageSize = new Size(dlg.PrintableAreaWidth, dlg.PrintableAreaHeight);
            visual.Measure(pageSize);
            visual.Arrange(new Rect(0, 0, pageSize.Width, pageSize.Height));
            return visual;
        }

        
   }
}
