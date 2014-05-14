using System;
using System.Net;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace AutoUpdate
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
        #region 属性
        private string loadFile;
        private string softName;
        private bool needUpdate;

        /// <summary>
        /// 或取是否需要更新
        /// </summary>
        public bool NeedUpdate
        {
            get
            {
                checkUpdate();
                return needUpdate;
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
        /// 升级的名称
        /// </summary>
        public string SoftName
        {
            get { return softName; }
            set { softName = value; }
        }

        public UpdateItems updateItems = new UpdateItems();
        #endregion

        #region 构造函数
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

        /// <summary>
        /// 检查是否需要更新
        /// </summary>
        public void checkUpdate()
        {
            try
            {
                WebClient webClient = new WebClient();
                XmlDocument xmlDoc = new XmlDocument();
                Stream stream = webClient.OpenRead(updateItems.UpdateUrlAddress);
                xmlDoc.Load(stream);
                XmlNode node = xmlDoc.SelectSingleNode("Update").FirstChild;
                if (node.Name == "Soft" && node.Attributes["Name"].Value.ToLower() == SoftName.ToLower())
                {
                    foreach (XmlNode xml in node)
                    {
                        switch (xml.Name)
                        {
                            case "version":
                                updateItems.Version = xml.InnerText;
                                break;
                            case "downloadAddress":
                                updateItems.DownloadAddress = xml.InnerText;
                                break;
                            case "descriptions":
                                updateItems.Descriptions.Add(xml.InnerText);
                                break;
                            case "info":
                                updateItems.Info = xml.InnerText;
                                break;
                            case "isStop":
                                updateItems.IsStop = xml.InnerText;
                                break;
                            case "message":
                                updateItems.Message = xml.InnerText;
                                break;
                        }
                    }
                }

                Version newVersion = new Version(updateItems.Version);
                Version oldVerson = Assembly.LoadFrom(loadFile).GetName().Version;
                needUpdate = newVersion.CompareTo(oldVerson) > 0 ? true : false;
            }
            catch
            {
                throw new Exception("更新出现错误，请确认网络连接无误后重试！");
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
