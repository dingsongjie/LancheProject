using Lanche.MongoDB.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb
{
    public class FeturnMongoDbContext:MongoDbContext
    {
        public MongoDbSet<Car> Car { get; set; }

        public FeturnMongoDbContext()
            : base("Feture", "Test")
        {

        }
        
    }
    public class Car
    {
        public Guid Id{get;set;}
        public string Name{get;set;}
        
    }
}
