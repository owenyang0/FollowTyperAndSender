using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoUpdate
{
    public class UpdateItems
    {
        public string Version { get; set; }
        public string DownloadAddress { get; set; }
        public string Info { get; set; }
        public string Message { get; set; }
        public string IsStop { get; set; }

        public List<String> Descriptions = new List<string>();

        // 升级配置的XML文件地址
        private const string updateUrlAddress = "http://localhost:8000/AutoUpdate.xml";
        public string UpdateUrlAddress
        {
            get { return updateUrlAddress; }
        }

    }
}
