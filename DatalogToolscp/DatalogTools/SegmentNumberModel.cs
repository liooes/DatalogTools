using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatalogTools
{
    public class SegmentNumberModel
    {
        private string segmentNumber;

        /// <summary>
        /// 段数
        /// </summary>
        public string SegmentNumber
        {
            get { return segmentNumber; }
            set { segmentNumber = value; }
        }
       
        private string voltage;
        /// <summary>
        /// 电压
        /// </summary>
        public string Voltage
        {
            get { return voltage; }
            set { voltage = value; }
        }

        private string electricCurrent;

        /// <summary>
        /// 电流
        /// </summary>
        public string ElectricCurrent
        {
            get { return electricCurrent; }
            set { electricCurrent = value; }
        }

        private string weldTime;
        /// <summary>
        /// 焊接时间
        /// </summary>
        public string WeldTime
        {
            get { return weldTime; }
            set { weldTime = value; }
        }

       
        private string cooldingTime;
        /// <summary>
        /// 冷却时间
        /// </summary>
        public string CooldingTime
        {
            get { return cooldingTime; }
            set { cooldingTime = value; }
        }


    }
}
