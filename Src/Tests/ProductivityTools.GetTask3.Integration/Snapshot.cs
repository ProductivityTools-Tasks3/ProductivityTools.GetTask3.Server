using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ProductivityTools.GetTask3.IntegrationTests
{
//    public static class Snapshot
//    {
//        private static void CreateCommand(string queryString, string connectionString)
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                SqlCommand command = new SqlCommand(queryString, connection);
//                command.Connection.Open();
//                command.ExecuteNonQuery();
//            }
//        }

//        public static void CreateSnapshot(string connectionString)
//        {
//            string sqlCommand = @"
//IF EXISTS  
//(  
//    SELECT name FROM sys.databases WHERE name = N'GetTask3Test_Snapshot'  
//)  
//DROP DATABASE GetTask3Test_Snapshot  

  
//--Create the database snapshot  
//CREATE DATABASE GetTask3Test_Snapshot ON  
//    (  
//        NAME = GetTask3Test,  
//        FILENAME =  'D:\Trash\GetTask3Test_Snapshot.ss'  
//    )  
//AS SNAPSHOT OF GetTask3Test";
           
//            CreateCommand(sqlCommand, connectionString);
//        }

//        public static void RestoreFromSnapshot(string connectionString)
//        {
//            string sqlCommand = @"
//USE [master];

//DECLARE @kill varchar(8000) = '';  
//SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
//FROM sys.dm_exec_sessions
//WHERE database_id  = db_id('GetTask3Test')

//EXEC(@kill);


//use master
//RESTORE DATABASE GetTask3Test  
//            FROM DATABASE_SNAPSHOT = 'GetTask3Test_Snapshot';
//DROP DATABASE GetTask3Test_Snapshot ";
//            CreateCommand(sqlCommand, connectionString);
//        }
//    }
}
