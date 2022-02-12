using Notlarim102.Common;
using Notlarim102.Core.DataAccess;
using Notlarim102.DataAccessLayer;
using Notlarim102.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notlarim102.DataAccessLayer.EntityFramework
{
   public class Repository<T>:RepositoryBase,IDataAccess<T> where T:class
    {
        NotlarimContext db;
        DbSet<T> objSet;
        public Repository()
        {
            db = RepositoryBase.CreateContext();
            objSet = db.Set<T>();
        }
       public List<T> List()
        {
            return objSet.ToList();
        }
        public List<T> List(Expression<Func<T,bool>>erusult)
        {
            return objSet.Where(erusult).ToList();
           // db.Categories.Where(x => x.Id == 5).ToList();
        }
        public int Insert(T obj)
        {
            objSet.Add(obj);
            
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;
                DateTime now = DateTime.Now;
                o.CreatedOn = now;
                o.ModifiedOn = now;
                o.ModifiedUserName = App.Common.GetCurrentUsername();//"system";
            }
            return Save();
        }
        public int Save()
        {
            return db.SaveChanges();
        }
        public int Delete(T obj)
        {
            objSet.Remove(obj);
            return Save();
        }
        public int Update(T obj)
        {
            if (obj is MyEntityBase)
            {
                MyEntityBase o = obj as MyEntityBase;                               
                o.ModifiedOn = DateTime.Now;
                o.ModifiedUserName = App.Common.GetCurrentUsername();//"system";
            }
            return Save();
        }
        public T Find(Expression<Func<T, bool>> eresult)
        {
            return objSet.FirstOrDefault(eresult);
        }

        public IQueryable<T> QList()
        {
            return objSet.AsQueryable<T>();
        }
    }
}
