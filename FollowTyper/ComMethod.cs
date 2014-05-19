using System;
using System.Drawing;
using IWshRuntimeLibrary;

namespace FollowTyper
{
    class ComMethod
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
    }
    public class Lnk
    {
        //"FolderPath"快捷方式存放的位置
        //"PathLink"指向连接的文件
        //"LnkName"快捷方式的文件
        //"LnkNote"快捷方式的备注
        //"IconLocationPath"指定快捷方式的图标
        public void CreateShortcutLnk(string FolderPath, string PathLink, string LnkName, string LnkNote, string IconLocationPath)
        {
            WshShell shell = new WshShell();
            IWshShortcut Shortcut = (IWshShortcut)shell.CreateShortcut(FolderPath + "\\" + LnkName + ".lnk");
            Shortcut.TargetPath = PathLink;
            Shortcut.WindowStyle = 1;
            Shortcut.Description = LnkNote;
            Shortcut.IconLocation = IconLocationPath;
            Shortcut.WorkingDirectory = System.Environment.CurrentDirectory;
            Shortcut.Save();

        }
    }

}
