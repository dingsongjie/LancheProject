using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lanche.Core.Application;
using Lanche.Core.Dependency;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Lanche.UnitOfWork;
using System.IO;
using Lanche.Core.Domain.Repository.Paging;
using System.ComponentModel;
using Lanche.Entityframework.UnitOfWork;
using UnitTest.Helper;
using Lanche.Core;
using Lanche.Entityframework;
using Lanche.Core.Reflection;

namespace UnitTest
{
    [TestClass]
    public class ApplicationBizTest
    {
      
        private IIocManager _iocManager;

        public ApplicationBizTest()
        {
            Application_Start();
            
        }
        #region linq query

        [TestMethod]
        public void GetInPaging()
        {
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            PagingDTO<Students> dto = Service.GetInPaging(1, 1, true, "Age");
         
        }
        [TestMethod]
        public void GetOne()
        {
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            Students dto = Service.GetOne("dsad");


        }

        [TestMethod]
        public void GetLists()
        {
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            List<Students> dtos = Service.GetList("dsassd");
        }
        [TestMethod]
        public void GetCount()
        {
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            int dtos = Service.GetCount("dsassd");
        } 
        #endregion

        #region Add
        [TestMethod]
        public void Add()
        {
            Students s = new Students()
            {
                Id = Guid.NewGuid(),
                Age = 1234,
                Name = "dsad",
                IsDeleted = false,
                CreationTime = DateTime.Now,
                CreatorUserId = 11
            };
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            Students dto = Service.Add(s);
        }
        [TestMethod]
        public void AddLot()
        {
            List<Students> students = new List<Students>(){ 
                new Students{
                    Name="wxqx",
                    Id=Guid.NewGuid(),
                       Age = 1234,
                         IsDeleted = false,
                CreationTime = DateTime.Now,
                CreatorUserId = 11
                },
                new Students{
                    Name="wxwdwdqx",
                    Id=Guid.NewGuid(),
                       Age = 123444,
                         IsDeleted = false,
                CreationTime = DateTime.Now,
                CreatorUserId = 11222
                }
            };
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            Service.AddBulk(students);
        } 
        #endregion
        #region 更新
        [TestMethod]
        public void Update()
        {
            Students s = new Students()
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Age = 123433333,
                Name = "dsad",
                IsDeleted = false,
                CreationTime = DateTime.Now,
                CreatorUserId = 11
            };
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            Service.Update(s);
        }

        [TestMethod]
        public void UpdateLot()
        {
            Students s = new Students()
            {

                Age = 123433333,
                Name = "dsad",
                IsDeleted = false,
                CreationTime = DateTime.Now,
                CreatorUserId = 11
            };
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            Service.UpdateLot(222, "QQ");
        } 
        #endregion
        #region 删除
        [TestMethod]
        public void Delete()
        {
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            Service.DeleteLot(10000);
        }
        
        #endregion
        #region SQl语句
        [TestMethod]
        public void Sql()
        {
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            var count = Service.SqlQuery("select count(1) from Students");
        } 
        #endregion
        #region 事务
        [TestMethod]
        public void TransactionTest()
        {
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            Students s = new Students()
            {
                Id = Guid.NewGuid(),
                Age = 1234,
                Name = "dsad",
                IsDeleted = false,
                CreationTime = DateTime.Now,
                CreatorUserId = 11
            };
            Service.TransactionMethod(s);
        } 
        #endregion
        #region 异步
        [TestMethod]
        public void GetOneAsyncTest()
        {
            TestApplicationBiz Service = _iocManager.Resolve<TestApplicationBiz>();
            var v = Service.GetOneAsync("dsad");
            var s = v.Result;
        }
        
        #endregion

        #region ApplicationStart
        private void Application_Start()
        {
            TestAssemblyFinder finder = new TestAssemblyFinder();

            _iocManager = IocManager.Instance;
            _iocManager.Register(typeof(IAssemblyFinder), typeof(TestAssemblyFinder), DependencyLifeStyle.Singleton);
            CoreBootstrapper core = new CoreBootstrapper(_iocManager);
            core.Initialize();
          


        }
        
        #endregion
       
    
        
    
    }
}
