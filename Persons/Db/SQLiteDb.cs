using System;
using System.Data.SQLite;
using System.IO;
using Dapper;

namespace Persons.Db
{
    internal static class SqLiteDb
    {
        private const string DbFilePath = "./TestDb.sqlite";

        static SqLiteDb()
        {
            SqlMapper.AddTypeHandler(new GuidTypeHandler());

            if (File.Exists(DbFilePath)) return;
            SQLiteConnection.CreateFile(DbFilePath);
            using (var dbConnection = GetConnection())
            {
                dbConnection.Execute(@"create table Persons
                    (
                        Id                          varchar(100) not null PRIMARY KEY,
                        Name                        varchar(100) not null,
                        BirthDay                    datetime not null,
                        Age                         integer not null
                    )");
            }
        }

        internal static SQLiteConnection GetConnection()
        {
            var dbConnection = new SQLiteConnection($"Data Source={DbFilePath};Version=3;");
            dbConnection.Open();
            return dbConnection;
        }
    }

    public class GuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }

        public override void SetValue(System.Data.IDbDataParameter parameter, Guid value)
        {
            parameter.Value = value.ToString("D");
        }
    }


}