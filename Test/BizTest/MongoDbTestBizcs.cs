using Lanche.DynamicWebApi.Application;
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
        public MongoDbTestBiz(IMongoDbRepository<Car> carRepository)
        {
            this._carRepository = carRepository;
        }
        public void Add()
        {
            _carRepository.Insert(new Car() { Id = Guid.NewGuid(), Name = "Alto" });
        }
        public Car Get()
        {
           return _carRepository.Single(m => m.Name =="Alto");
        }
    }
}
