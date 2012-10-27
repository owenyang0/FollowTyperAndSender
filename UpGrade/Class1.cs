using System;
using System.Net;
using System.Xml;
using System.IO;
using System.Reflection;

namespace UpGrade
{
    /// <summary>
    /// 更新完成触发的事件
    /// </summary>
    public delegate void UpdateState();
    /// <summary>
    /// 程序更新
    /// </summary>
    public class SoftUpdate
    {

        public string download;
        public string description1;
        public string description2;
        public string description3;
        public string Info;
        public string isStop;
        public string richBox;
        private const string updateUrl = "http://owenyang.35free.net/AutoUpdate.xml";//升级配置的XML文件地址

        #region 构造函数
        public SoftUpdate() { }

        /// <summary>
        /// 程序更新
        /// </summary>
        /// <param name="file">要更新的文件</param>
        public SoftUpdate(string file, string softName)
        {
            this.LoadFile = file;
            this.SoftName = softName;
        }
        #endregion

        #region 属性
        private string loadFile;
        private string newVerson;
        private string softName;
        private bool isUpdate;

        /// <summary>
        /// 或取是否需要更新
        /// </summary>
        public bool IsUpdate
        {
            get
            {
                checkUpdate();
                return isUpdate;
            }
        }

        /// <summary>
        /// 要检查更新的文件
        /// </summary>
        public string LoadFile
        {
            get { return loadFile; }
            set { loadFile = value; }
        }

        /// <summary>
        /// 程序集新版本
        /// </summary>
        public string NewVerson
        {
            get { return newVerson; }
        }

        /// <summary>
        /// 升级的名称
        /// </summary>
        public string SoftName
        {
            get { return softName; }
            set { softName = value; }
        }

        #endregion

        /// <summary>
        /// 更新完成时触发的事件
        /// </summary>
        public event UpdateState UpdateFinish;
        private void isFinish()
        {
            if (UpdateFinish != null)
                UpdateFinish();
        }

        /// <summary>
        /// 下载更新
        /// </summary>
        public void Update()
        {
            try
            {
                if (!isUpdate)
                    return;
                WebClient wc = new WebClient();
                string filename = "";
                string exten = download.Substring(download.LastIndexOf("."));
                if (loadFile.IndexOf(@"/") == -1)
                    filename = "Update_" + Path.GetFileNameWithoutExtension(loadFile) + exten;
                else
                    filename = Path.GetDirectoryName(loadFile) + "//Update_" + Path.GetFileNameWithoutExtension(loadFile) + exten;
                wc.DownloadFile(download, filename);
                wc.Dispose();
                isFinish();
            }
            catch
            {
                //throw new Exception("更新出现错误，网络连接失败！");
            }
        }

        /// <summary>
        /// 检查是否需要更新
        /// </summary>
        public void checkUpdate()
        {
            try
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(updateUrl);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                XmlNode list = xmlDoc.SelectSingleNode("Update");
                foreach (XmlNode node in list)
                {
                    if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == SoftName.ToLower())
                    {
                        foreach (XmlNode xml in node)
                        {
                            if (xml.Name == "Verson")
                                newVerson = xml.InnerText;
                            else if (xml.Name == "DownLoad")
                                download = xml.InnerText;
                            else if(xml .Name =="Description1")
                                description1 = xml.InnerText;
                            else if (xml.Name == "Description2")
                                description2= xml.InnerText;
                            else if (xml.Name == "Description3")
                                description3 = xml.InnerText;
                            else if (xml.Name == "Description3")
                                description3 = xml.InnerText;
                            else if (xml.Name == "Info")
                                Info = xml.InnerText;
                            else if (xml.Name == "isStop")
                                isStop = xml.InnerText;
                            else if (xml.Name == "richBox")
                                richBox = xml.InnerText;
                        }
                    }
                }

                Version ver = new Version(newVerson);
                Version verson = Assembly.LoadFrom(loadFile).GetName().Version;
                int tm = verson.CompareTo(ver);

                if (tm >= 0)
                    isUpdate = false;
                else
                    isUpdate = true;
            }
            catch 
            {
                //throw new Exception("更新出现错误，请确认网络连接无误后重试！");
            }
        }

        /// <summary>
        /// 获取要更新的文件
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.loadFile;
        }
    }
}
