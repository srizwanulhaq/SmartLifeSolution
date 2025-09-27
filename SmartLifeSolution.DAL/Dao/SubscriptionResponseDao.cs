using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLifeSolution.DAL.Dao
{
    public class SubscriptionResponseDao
    {
        public string applicationID { get; set; }
        public string applicationName { get; set; }
        public string data { get; set; }
        public string devEUI { get; set; }
        public string deviceName { get; set; }
        public int fCnt { get; set; }
        public int fPort { get; set; }
        public List<RxInfo> rxInfo { get; set; }
        public DateTime time { get; set; }
        public TxInfo txInfo { get; set; }
    }
    public class DataRate
    {
        public int bandwidth { get; set; }
        public string modulation { get; set; }
        public int spreadFactor { get; set; }
    }

    public class RxInfo
    {
        public int altitude { get; set; }
        public int latitude { get; set; }
        public double loRaSNR { get; set; }
        public int longitude { get; set; }
        public string mac { get; set; }
        public string name { get; set; }
        public int rssi { get; set; }
        public DateTime time { get; set; }
    }

    public class TxInfo
    {
        public bool adr { get; set; }
        public string codeRate { get; set; }
        public DataRate dataRate { get; set; }
        public int frequency { get; set; }
    }


}

