using Lanche.Core.Application;
using Lanche.Domain.Repository;
using Lanche.Domain.Repository.Paging;
using Lanche.Entityframework.UnitOfWork.Repository;
using Lanche.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class TestApplicationBiz : IApplicationBiz
    {
        private readonly IEfRepository<Students> _studentRepository;
        private readonly IUnitOfWorkManager _uowManger;
      
        public TestApplicationBiz(IEfRepository<Students> studentRepository, IUnitOfWorkManager uowManger)
        {
            _studentRepository = studentRepository;
            _uowManger = uowManger;
        }
        public virtual PagingEntity<Students> GetInPaging(int pageIndex, int PageSize, bool sort, string orderProperty)
        {
           
            return _studentRepository.GetInPaging(m => m.IsDeleted == false, pageIndex, PageSize, orderProperty, sort);

        }
        public virtual List<Students> GetInPagingS()
        {

            var v= _studentRepository.GetAll().OrderBy(m => m.Age).Where(m => m.IsDeleted == false).Skip(1).Take(1).ToList();
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
        public virtual int GetCount(string  name)
        {
            return _studentRepository.Count(m => m.Name != name);
        }
        public virtual Students Add(Students s)
        {
              return _studentRepository.Insert(s);
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
        public virtual void UpdateLot(int age ,string name)
        {
            _studentRepository.Update(m => m.Age > age, m => new Students() { Name = name });
        }
        public virtual int SqlQuery(string sql)
        {
           return _studentRepository.SqlQuery<int>(sql).ToList()[0];
        }
     
		  [UnitOfWork(isTransactional:true)]
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
