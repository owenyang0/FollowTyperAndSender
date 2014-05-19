using DataOperation.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace DataOperation.DAL
{
    public class DataOperationDAL
    {
        //把公共的代码，封装到一个方法中，提高代码的利用性
        private static GradeField ToDataField(DataRow row)
        {
            GradeField gradeField = new GradeField();
            gradeField.CompletedDate = (DateTime)row["CompletedDate"];
            gradeField.Paragraph = (string)row["Paragraph"];
            gradeField.Speed = (double)row["Speed"];
            gradeField.BackSpace = (int)row["BackSpace"];
            gradeField.HitKey = (double)row["HitKey"];
            gradeField.KeyLong = (double)row["KeyLong"];
            gradeField.WrongWords = (int)row["WrongWords"];
            gradeField.WordsCount = (int)row["WordsCount"];
            gradeField.KeyCount = (int)row["KeyCount"];
            gradeField.CostTime = (string)row["CostTime"];

            return gradeField;
        }

        //根据ID获得 GetById
        public static GradeField GetById(long id)
        {
            DataTable dt =
            SqlHelper.ExecuteDataTable("Select * from  T_Grade where Id=@Id",
                new OleDbParameter("@Id", id));

            if (dt.Rows.Count <= 0)
                return null;
            else if (dt.Rows.Count > 1)
                throw new Exception("严重错误，查出多条数据");
            else
            {
                DataRow row = dt.Rows[0];
                return ToDataField(row);
            }
        }

        public static void DeleteById(long id)
        {
            SqlHelper.ExecuteNonQuery("Delete from T_Grade where ID=@Id",
                new OleDbParameter("@Id", id));
        }

        public static void Insert(GradeField gradeField)
        {
            SqlHelper.ExecuteNonQuery(@"Insert into T_Grade(
                CompletedDate, Paragraph, Speed, BackSpace, HitKey, KeyLong, WrongWords, WordsCount, KeyCount, CostTime)
                values(@CompletedDate, @Paragraph, @Speed, @BackSpace, @HitKey, @KeyLong, @WrongWords, @WordsCount, @KeyCount, @CostTime)",
                new OleDbParameter("@CompletedDate", gradeField.CompletedDate.ToString("G")),
                new OleDbParameter("@Paragraph", gradeField.Paragraph),
                new OleDbParameter("@Speed", gradeField.Speed),
                new OleDbParameter("@BackSpace", gradeField.BackSpace),
                new OleDbParameter("@HitKey", gradeField.HitKey),
                new OleDbParameter("@KeyLong", gradeField.KeyLong),
                new OleDbParameter("@WrongWords", gradeField.WrongWords),
                new OleDbParameter("@WordsCount", gradeField.WordsCount),
                new OleDbParameter("@KeyCount", gradeField.KeyCount),
                new OleDbParameter("@CostTime", gradeField.CostTime)
                );
        }

        public static void Update(GradeField gradeField)
        {
            SqlHelper.ExecuteNonQuery(@"Update T_Grade set
                CompletedDate=@CompletedDate,
                Pagragraph=@Pagragraph,
                Speed=@Speed,
                BackSpace=@BackSpace,
                HitKey=@HitKey,
                KeyLong=@KeyLong,
                WrongWords=@WrongWords,
                WordsCount=@WordsCount,
                KeyCount=@KeyCount,
                CostTime=@CostTime",
                new OleDbParameter("@CompletedDate", gradeField.CompletedDate.ToString("G")),
                new OleDbParameter("@Pagragraph", gradeField.Paragraph),
                new OleDbParameter("@Speed", gradeField.Speed),
                new OleDbParameter("@BackSpace", gradeField.BackSpace),
                new OleDbParameter("@HitKey", gradeField.HitKey),
                new OleDbParameter("@KeyLong", gradeField.KeyLong),
                new OleDbParameter("@WrongWords", gradeField.WrongWords),
                new OleDbParameter("@WordsCount", gradeField.WordsCount),
                new OleDbParameter("@KeyCount", gradeField.KeyCount),
                new OleDbParameter("@CostTime", gradeField.CostTime)
                );
        }

        public static List<GradeField> GetAll()
        {
            DataTable dt = SqlHelper.ExecuteDataTable("Select * from T_Grade");
            List<GradeField> gradeFields = new List<GradeField>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                GradeField gradeField = new GradeField();
                gradeField = ToDataField(row);
                gradeFields.Add(gradeField);
            }

            return gradeFields;
        }
    }
}
