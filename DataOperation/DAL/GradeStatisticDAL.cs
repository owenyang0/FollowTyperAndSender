using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataOperation.DAL
{
    public class GradeStatisticDAL
    {
        private static string startTime, endTime;
        private static int totalDays;

        public static int GetTotalCounts()
        {
            return (int)SqlHelper.
                ExecuteScalar("SELECT COUNT(ID) FROM T_Grade");
        }

        public static double getTotalWords()
        {
            return (double)SqlHelper.
                ExecuteScalar("SELECT SUM(WORDSCOUNT) FROM T_Grade");
        }

        public static double getTotalKeys()
        {
            return (double)SqlHelper.
                ExecuteScalar("SELECT SUM(KEYCOUNT) FROM T_Grade");
        }

        public static double getAverageSpeed()
        {
            return (double)SqlHelper.
                ExecuteScalar("SELECT AVG(SPEED) FROM T_Grade");
        }

        public static double getAverageHits()
        {
            return (double)SqlHelper.
                ExecuteScalar("SELECT AVG(HITKEY) FROM T_Grade");
        }

        public static double getAverageKeyLong()
        {
            return (double)SqlHelper.
                ExecuteScalar("SELECT AVG(KEYLONG) FROM T_Grade");
        }

        public static double getTopSpeed()
        {
            return (double)SqlHelper.
                ExecuteScalar("SELECT TOP 1 SPEED FROM T_Grade ORDER BY SPEED DESC");
        }

        public static double getBestHits()
        {
            return (double)SqlHelper.
                ExecuteScalar("SELECT TOP 1 HITKEY FROM T_Grade ORDER BY HITKEY DESC");
        }

        public static double getBestKeyLong()
        {
            return (double)SqlHelper.
                ExecuteScalar("SELECT TOP 1 KEYLONG FROM T_Grade ORDER BY KEYLONG DESC");
        }

        private static string getStartTime()
        {
            startTime = String.IsNullOrEmpty(startTime)
                ? (string)SqlHelper.
                    ExecuteScalar(
                        "SELECT TOP 1 FORMAT(COMPLETEDDATE, 'YYYY-MM-DD') FROM T_GRADE ORDER BY  FORMAT(COMPLETEDDATE, 'YYYY-MM-DD') ASC")
                : startTime;

            return startTime;
        }

        private static string getEndTime()
        {
            endTime = String.IsNullOrEmpty(endTime)
                ? (string)SqlHelper.
                    ExecuteScalar(
                        "SELECT TOP 1 FORMAT(COMPLETEDDATE, 'YYYY-MM-DD') FROM T_GRADE ORDER BY  FORMAT(COMPLETEDDATE, 'YYYY-MM-DD') DESC")
                : endTime;

            return endTime;
        }

        private static int getTotalDays()
        {
            totalDays = (int)SqlHelper.
                ExecuteScalar("SELECT DATEDIFF('D',#" + getStartTime() + "#,NOW())FROM T_Grade");
            return totalDays;
        }

        public static string getTimeSpan()
        {
            return getStartTime() + " to " + getEndTime();
        }

        public static double getEverydayWords()
        {
            return getTotalWords() / getTotalDays();
        }

        public static double getTodayWords()
        {
            object todayWords = SqlHelper.
                ExecuteScalar("SELECT SUM(WORDSCOUNT) FROM T_GRADE WHERE DATEDIFF('D',COMPLETEDDATE,NOW())=0");
            return SqlHelper.FromDBValue(todayWords) == null ? 0 : (double) getTodaySpeed();
        }

        public static double getTodaySpeed()
        {
            object todaySpeedObj = SqlHelper.
                ExecuteScalar("SELECT AVG(SPEED) FROM T_GRADE WHERE DATEDIFF('D',COMPLETEDDATE,now())=0");
            return SqlHelper.FromDBValue(todaySpeedObj) == null ? 0 : (double) todaySpeedObj;
        }
    }
}
