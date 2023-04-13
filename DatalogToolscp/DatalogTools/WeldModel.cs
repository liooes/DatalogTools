using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatalogTools
{
    public class WeldModel
    {
        /*
         001
    2019.01.16 18:37:44
    Project number: 003
    Welder number: 001
    Weld Number: 001
    Unit number: 001
    Welding mode: voltage mode 
         
          Number  Time ProjectNumber WelderNumber WeldNumber UnitNumber WeldingModel SegmentNumber
         */
        private string number;
        /// <summary>
        /// 次数
        /// </summary>
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
         
        private string time;
        /// <summary>
        /// 时间
        /// </summary>
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
         
        private string projectNumber;
        /// <summary>
        /// 工程编号
        /// </summary>
        public string ProjectNumber
        {
            get { return projectNumber; }
            set { projectNumber = value; }
        }
         
        private string welderNumber;
        /// <summary>
        /// 焊工编号
        /// </summary>
        public string WelderNumber
        {
            get { return welderNumber; }
            set { welderNumber = value; }
        }
         
        private string weldNumber;
        /// <summary>
        /// 焊口编号
        /// </summary>
        public string WeldNumber
        {
            get { return weldNumber; }
            set { weldNumber = value; }
        }
         
        private string unitNumber;
        /// <summary>
        /// 单位编号
        /// </summary>
        public string UnitNumber
        {
            get { return unitNumber; }
            set { unitNumber = value; }
        }
         
        private string weldingModel;
        /// <summary>
        /// 焊接方式
        /// </summary>
        public string WeldingModel
        {
            get { return weldingModel; }
            set { weldingModel = value; }
        }
         
        private List<SegmentNumberModel> segmentNumber;
        /// <summary>
        /// 段数
        /// </summary>
        public List<SegmentNumberModel> SegmentNumber
        {
            get { return segmentNumber; }
            set { segmentNumber = value; }
        }

    }
}
