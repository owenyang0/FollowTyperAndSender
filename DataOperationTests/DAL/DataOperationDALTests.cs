using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataOperation.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataOperation.Model;
namespace DataOperation.DAL.Tests
{
    [TestClass()]
    public class DataOperationDALTests
    {
        private int dataCount = 5037;
        //[TestMethod()]
        //public void GetByIdTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void DeleteByIdTest()
        {
            DataOperationDAL.DeleteById(5063);
        }

        [TestMethod()]
        public void InsertTest()
        {
            GradeField gradeField = new GradeField();
            gradeField.CompletedDate = DateTime.Now;
            gradeField.Paragraph = "第N段";
            gradeField.Speed = 14.1234556;
            gradeField.BackSpace = 9;
            gradeField.HitKey = 5.123456;
            gradeField.KeyLong = 2.123456;
            gradeField.WrongWords = 3;
            gradeField.WordsCount = 98;
            gradeField.KeyCount = 123;
            gradeField.CostTime = "0:21.88";

            DataOperationDAL.Insert(gradeField);
            dataCount++;
            Assert.IsNotNull(gradeField);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            List<GradeField> dataField = DataOperationDAL.GetAll();
            Console.WriteLine(dataField.Count);
            Assert.AreEqual(dataCount, dataField.Count);
        }
    }
}