using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.Entities.Base;
using TravelApp.Domain.Model;
using TravelApp.Domain.Repositories.Base;
using TravelApp.Infrastructure.Data;

namespace TravelApp.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        protected readonly TravelAppContext _dbContext;

        public Repository(TravelAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Result<T> Add(T entity)
        {
            Result<T> result = new Result<T>(); 
            try
            {
                var response = _dbContext.Set<T>().Add(entity);
                _dbContext.SaveChanges();
                result.SetData(entity);

               
            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }

        public Result Delete(T entity)
        {
            Result<T> result = new Result<T>();
            try
            {
                var response = _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
                result.SetData(entity);


            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }

        public Result<T> GetById(int id)
        {
            Result<T> result = new Result<T>();
            try
            {
                var response = _dbContext.Set<T>().Find(id);
               if( response != null)
                    result.SetData(response);
               else
                    result.SetFailure("Not Found");

            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }

        public Result<List<T>> GetlAll()
        {
            Result<List<T>> result = new Result<List<T>>();
            try
            {
                var response = _dbContext.Set<T>().ToList();
                if (response != null)
                    result.SetData(response);
                else
                    result.SetFailure("have not any data");

            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }

        public Result Update(T entity)
        {
            Result<T> result = new Result<T>();
            try
            {
              
                var response = _dbContext.Set<T>().Update(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();
                result.SetData(entity);


            }
            catch (Exception ex)
            {

                result.SetFailure(ex);
            }
            return result;
        }
    }
}
