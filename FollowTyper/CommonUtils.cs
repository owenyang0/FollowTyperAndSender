using IWshRuntimeLibrary;
using System;
using System.Drawing;

namespace FollowTyper
{
    class CommonUtils
    {
        public static Font GetFont(string family, float size, string style)
        {
            int startIndexName = family.IndexOf("Name") + 5;
            int index = family.IndexOf(",");
            if (index == -1)
            {
                index = family.Length - 1;
            }

            string familyName = family.Substring(startIndexName, index - startIndexName);
            FontStyle regular = FontStyle.Regular;
            style = style.ToLower();

            if ((style.IndexOf("bold") > -1) && (style.IndexOf("italic") > -1))
            {
                regular = FontStyle.Italic | FontStyle.Bold;
            }
            else if (style.IndexOf("bold") > -1)
            {
                regular = FontStyle.Bold;
            }
            else if (style.IndexOf("italic") > -1)
            {
                regular = FontStyle.Italic;
            }

            return new Font(familyName, size, regular);
        }

        /// <summary>
        /// 创建快捷方式
        /// </summary>
        /// <param name="folderPath">快捷方式存放的位置</param>
        /// <param name="pathLink">指向连接的文件</param>
        /// <param name="linkName">快捷方式的文件</param>
        /// <param name="linkNote">快捷方式的备注</param>
        /// <param name="iconLocationPath">指定快捷方式的图标</param>
        public static void CreateShortcutLnk(string folderPath, string pathLink, string linkName, string linkNote, string iconLocationPath)
        {
            var shortcut = (IWshShortcut)new WshShell().CreateShortcut(folderPath + "\\" + linkName + ".lnk");
            shortcut.TargetPath = pathLink;
            shortcut.WindowStyle = 1;
            shortcut.Description = linkNote;
            shortcut.IconLocation = iconLocationPath;
            shortcut.WorkingDirectory = Environment.CurrentDirectory;
            shortcut.Save();
        }
    }
}
