using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using DapperExtensions.Sql;

namespace SuspeSys.Client.Sqlite.Repository
{
    public class ConnectionFactory
    {
        private static string dbFile = "sus.sqlite";

        public static string ConnectionConfig = "Data Source=" + FilePath + ";Pooling=True;Max Pool Size=10;FailIfMissing=True;Journal Mode=Off;Synchronous = OFF";

        private static string FilePath
        {
            get
            {
                return System.IO.Path.Combine(System.Environment.CurrentDirectory, dbFile);
            }
        }

        private static IDbConnection CreateConnection<T>() where T : IDbConnection, new()
        {
            DapperExtensions.DapperExtensions.SqlDialect = new SqliteDialect();
            IDbConnection connection = new T();
            connection.ConnectionString = ConnectionConfig;
            connection.Open();
            return connection;
        }

        public static IDbConnection CreateSqlConnection()
        {
            //判断文件是否存在，不存在创建
            if (!File.Exists(FilePath))
            {
                //File.Create(FilePath);

                CreateTable();
            }


            return CreateConnection<SQLiteConnection>();
        }

        private static void CreateTable()
        {
            #region Sql
            string sql = @"CREATE TABLE [BasicInfo](
                                [Id] INTEGER PRIMARY KEY AUTOINCREMENT,
                                [Name] VARCHAR(20) NOT NULL UNIQUE, 
                                [Value] VARCHAR(500), 
                                [Description] NVARCHAR(100), 
                                [CreatedDate] DATETIME);

                            CREATE TABLE[Clients](

                               [Id] VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE, 
                                [ClientName] NVARCHAR(40) NOT NULL UNIQUE, 
                                [IsDefault] BOOL NOT NULL, 
                                [CreatedDate] DATETIME NOT NULL, 
                                [ModifiedDate] DATETIME);

                            CREATE TABLE[DatabaseConnection](

                               [Id] INTEGER PRIMARY KEY AUTOINCREMENT,

                               [Alias] VARCHAR(30) NOT NULL UNIQUE, 
                                [ServerIP] VARCHAR2(30) NOT NULL,
                                [UserId] VARCHAR(30) NOT NULL,
                                [Password] VARCHAR(50) NOT NULL,
                                [DBName] VARCHAR(50) NOT NULL,
                                [IsDefault] BOOL NOT NULL, 
                                [CreatedDate] DATETIME NOT NULL, 
                                [EnableTime] DATETIME NOT NULL, 
                                [ModifiedDate] DATETIME);

                            CREATE TABLE[Users](

                               [Id] VARCHAR(36) PRIMARY KEY NOT NULL UNIQUE, 
                                [UserName] VARCHAR(20) NOT NULL UNIQUE, 
                                [Password] VARCHAR(30) NOT NULL UNIQUE, 
                                [CreatedDate] DATETIME NOT NULL,
                                [SaveUserName] BOOL NOT NULL, 
                                [SavePassword] BOOL NOT NULL, 
                                [IsDefault] BOOL NOT NULL, 
                                [ModifiedDate] DATETIME);";

            #endregion

            using (SQLiteConnection conn = new SQLiteConnection())
            {

                SQLiteConnection.CreateFile(FilePath);
                conn.ConnectionString = ConnectionConfig;
                conn.Open();
                
                conn.Execute(sql);
            }
        }
    }
}
