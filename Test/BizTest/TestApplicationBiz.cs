using Castle.Core.Logging;
using Lanche.Core.Application;
using Lanche.Core.Dependency;
using Lanche.Domain.Repository;
using Lanche.Domain.Repository.Paging;
using Lanche.DynamicWebApi.Application;
using Lanche.DynamicWebApi.Controller.Filters;
using Lanche.Entityframework.UnitOfWork.Repository;
using Lanche.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace UnitTest
{

    
    //localhost://api/services/test/test/
    public class TestApplicationBiz : ApplicationBizBase
    {
        private readonly IEfRepository<Students> _studentRepository;
        private readonly IUnitOfWorkManager _uowManger;
        private readonly ILogger _logger;
        public  IPagingRequestEntitySlover Slover { get; set; }

        public TestApplicationBiz(IEfRepository<Students> studentRepository, IUnitOfWorkManager uowManger,ILogger logger)
        {
            _studentRepository = studentRepository;
            _uowManger = uowManger;
            _logger = logger;
        }
        
        public virtual PagingEntity<Students> GetInPaging(int pageIndex, int PageSize, bool sort, string orderProperty)
        {
           
            _logger.Debug("ss");
            return _studentRepository.GetInPaging(m => m.IsDeleted == false, pageIndex, PageSize, orderProperty, sort);

        }
        // localhost://api/services/test/test/GetInPagingS
      //  [DefaultAuthorizeAttribute]
        public virtual List<Students> GetInPagingS()
        {
            throw new Exception("ss");
            var v = _studentRepository.GetAll().OrderBy(m => m.Age).Where(m => m.IsDeleted == false).Skip(1).Take(1).ToList();
            return v;
        }
        public virtual Students GetOne(string name)
        {
            return _studentRepository.Single(m => m.Name == name);
        }
        public virtual List<Students> GetList(string name)
        {
            return _studentRepository.GetAllList(m => m.Name != name);
        }
        public virtual int GetCount(string name)
        {

            return _studentRepository.Count(m => m.Name != name);
        }
        public async virtual Task<Students> Add(Students s)
        {
            s = new Students();
            s.Name = "ss";
            s.Age = 11;
            s.Id = Guid.NewGuid();
            s.LastModificationTime = new DateTime(1, 1, 1);
            return await _studentRepository.InsertAsync(s);
        }
        public virtual void AddBulk(IEnumerable<Students> students)
        {
            _studentRepository.BulkInsert(students);

        }
        public virtual int DeleteLot(int age)
        {
            return _studentRepository.Delete(m => m.Age > age);
        }
        public virtual void Delete(Students s)
        {
            _studentRepository.Delete(s);
        }

        public virtual void Update(Students student)
        {
            _studentRepository.Update(student);
        }
        public virtual void UpdateLot(int age, string name)
        {
            _studentRepository.Update(m => m.Age > age, m => new Students() { Name = name });
        }
        public virtual int SqlQuery(string sql)
        {
            return _studentRepository.SqlQuery<int>(sql).ToList()[0];
        }

        [UnitOfWork(isTransactional: true)]
        public virtual void TransactionMethod(Students s)
        {

            _studentRepository.InsertAsync(s);
            _uowManger.Current.SaveChanges();

            throw new Exception("dc");
        }
        
       
        public virtual Task<Students> GetOneAsync(string name)
        {
            return _studentRepository.SingleAsync(m => m.Name == name);
        }
       


    }
}
