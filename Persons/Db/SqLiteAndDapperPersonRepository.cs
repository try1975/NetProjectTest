using System;
using System.Linq;
using Dapper;
using Persons.Abstractions;
using Persons.Model;

namespace Persons.Db
{
    public class SqLiteAndDapperPersonRepository : IPersonRepository
    {
        public IPerson Find(Guid id)
        {
            var dbConnection = SqLiteDb.GetConnection();
            var result =  dbConnection.Query<dynamic>("SELECT [Id], [Name], [BirthDay], [Age] FROM [Persons] WHERE Id =@Id ", 
                new { Id = id.ToString("D") }).SingleOrDefault();
            return MapOrderItems(result);
        }

        private IPerson MapOrderItems(dynamic result)
        {
            var person = new Person()
            {
                Id = Guid.Parse(result[0].Id),
                Name = result[0].Name,
                BirthDay = result[0].BirthDay,
                Age = result[0].Age
            };
            return person;
        }

        public void Insert(IPerson item)
        {
            var dbConnection = SqLiteDb.GetConnection();
            var rowsAffected = dbConnection.Execute(@"INSERT INTO Persons ([Id], [Name], [BirthDay], [Age]) values (@Id, @Name, @BirthDay, @Age)",
                    new { Id = item.Id.ToString("D"), Name = item.Name, BirthDay = item.BirthDay, Age=item.Age})
                ;

            //return rowsAffected > 0;
        }
    }
}