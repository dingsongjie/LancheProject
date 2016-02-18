using Lanche.Core.Dependency;
using Lanche.DynamicWebApi.Application;
using Lanche.MongoDB.DbContext;
using Lanche.MongoDB.Provider;
using Lanche.MongoDB.Repositories;
using MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTest
{
    public class MongoDbTestBiz : ApplicationBizBase
    {
        private readonly IMongoDbRepository<Car> _carRepository;
        public IIocManager manager { get; set; }
        public MongoDbTestBiz(IMongoDbRepository<Car> carRepository)
        {
            this._carRepository = carRepository;
        }
        public async  Task Add()
        {
            for(int i=0;i<100000;i++)
            {
                ///并发测试
                IMongoDbRepository<Car> car = new MongoDbRepositoryBase<FeturnMongoDbContext, Car>(new DefaultMongoDbDatabaseProvider<FeturnMongoDbContext>(new DefaultMongoDbContextProvider<FeturnMongoDbContext>()));
                await car.DeleteAsync(m => m.Name == "Alto");
                car.Insert(new Car() { Id = Guid.NewGuid(), Name = "Alto" });

            }
           
              
             
        }
        public Car Get()
        {
           return _carRepository.Single(m => m.Name =="Alto");
        }
    }
}
