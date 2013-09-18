using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
namespace SqlGrades
{
   public class GradesHis
    {

        public void Add(string date, string para, double speed,
            int back, double hitkey, double keylong, int wronwor,
            int wrordscount, int keycount, string time)
        {
            string P_Connection = string.Format(//创建数据库连接字符串
              "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../rsc/Grades.mdb;User Id=Admin;Persist Security Info=True;Jet OLEDB:Database Password=owenyang");
            OleDbConnection P_OLEDBConnection = //创建连接对象
                new OleDbConnection(P_Connection);
            P_OLEDBConnection.Open();//连接到数据库
            string P_str = string.Format("insert into grade([date],para,speed,back,hitkey,keylong,wronwor,wordscount,keycount,[time]) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                date, para, speed, back, hitkey, keylong, wronwor, wrordscount, keycount, time);
            OleDbCommand P_OLEDBCommand = new OleDbCommand(//创建命令对象
                P_str, P_OLEDBConnection);
            P_OLEDBCommand.ExecuteNonQuery();

        }

        public void Delete(string strCommand)//date, string para, string speed, string back)
        {
            string P_Connection = string.Format(//创建数据库连接字符串
              "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../src/Grades.mdb;User Id=Admin;Persist Security Info=True;Jet OLEDB:Database Password=owenyang");
            OleDbConnection P_OLEDBConnection = //创建连接对象
                new OleDbConnection(P_Connection);//ID=1 and 
            P_OLEDBConnection.Open();//连接到数据库
            //string P_str = string.Format("delete from grade where date=#{0}# and para='{1}' and speed='{2}'and back='{3}'",
            //    date, para, speed, back);
            string P_str = string.Format(strCommand);
            OleDbCommand P_OLEDBCommand = new OleDbCommand(//创建命令对象
                P_str, P_OLEDBConnection);
            P_OLEDBCommand.ExecuteNonQuery();
        }

        public string command(string order)
        {
            string P_Connection = string.Format(//创建数据库连接字符串
             "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../rsc/Grades.mdb;User Id=Admin;Persist Security Info=True;Jet OLEDB:Database Password=owenyang"); 
            OleDbConnection P_OLEDBConnection = //创建连接对象
                new OleDbConnection(P_Connection);
            P_OLEDBConnection.Open();//连接到数据库            
            string P_str = string.Format(order);
            OleDbCommand P_OLEDBCommand = new OleDbCommand(//创建命令对象
                P_str, P_OLEDBConnection);
            OleDbDataReader P_Reader = //得到数据读取器
             P_OLEDBCommand.ExecuteReader();
            string tem = "";
            if (P_Reader.Read())
            {
                tem = P_Reader[0].ToString();
            }
            P_Reader.Close();
            return tem;

        }
       public List<task> Select()
       {
           string P_Connection = string.Format(//创建数据库连接字符串
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../rsc/Grades.mdb;User Id=Admin;Persist Security Info=True;Jet OLEDB:Database Password=owenyang"); 
           OleDbConnection P_OLEDBConnection = //创建连接对象
               new OleDbConnection(P_Connection);
           P_OLEDBConnection.Open();//连接到数据库     select Format(now(), 'yyyy-mm-dd')       
           string P_str = string.Format("select * from grade order by ID desc");
          // string P_str = string.Format("select * from grade where datediff('d',date,now())=0");
           OleDbCommand P_OLEDBCommand = new OleDbCommand(//创建命令对象
               P_str, P_OLEDBConnection);
           OleDbDataReader P_Reader = //得到数据读取器
            P_OLEDBCommand.ExecuteReader();
           List<task> P_Task = new List<task>();
           while (P_Reader.Read())//读取数据
           {
               P_Task.Add(new task() //将数据放入集合
               {
                   Date = Convert.ToDateTime(P_Reader[0]),
                   Para = P_Reader[1].ToString(),
                   Speed = P_Reader[2].ToString(),
                   Back = P_Reader[3].ToString(),
                   HitKey = P_Reader[4].ToString(),
                   KeyLong = P_Reader[5].ToString(),
                   WronWor = P_Reader[6].ToString(),
                   WordsCount = P_Reader[7].ToString(),
                   KeyCount = P_Reader[8].ToString(),
                   time = P_Reader[9].ToString(),
                   //ID = Convert.ToInt32(P_Reader[4].ToString()),


               });

           }
           P_Reader.Close();
           return P_Task;
       }
    }

  public class task
    {

        public DateTime Date { get; set; }
        public string Para { get; set; }
        public string Speed { get; set; }
        public string Back { get; set; }
        public string HitKey { get; set; }
        public string KeyLong { get; set; }
        public string WronWor { get; set; }
        public string WordsCount { get; set; }
        public string KeyCount { get; set; }
        public string time { get; set; }
    }
}
