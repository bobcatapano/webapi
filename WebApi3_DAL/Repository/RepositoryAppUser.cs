﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi3_DAL.Contracts;
using WebApi3_DAL.Model;
using WebApi3_DAL.Data;
using Microsoft.Extensions.Logging;
using System.Xml;

namespace WebApi3_DAL.Repository
{
    public class RepositoryAppUser : IRepository<AppUser>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;

        public RepositoryAppUser(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<AppUser> Create(AppUser appuser)
        {
            try
            {
                if (appuser != null) {
                  var obj = _appDbContext.Add<AppUser>(appuser);
                    await _appDbContext.SaveChangesAsync();
                    return obj.Entity;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception) {
                throw;
            }
        }

        public void Delete(AppUser appuser)
        {
            try
            { if (appuser != null)
              {
                    var obj = _appDbContext.Remove(appuser);
                    if (obj != null)
                    {
                        _appDbContext.SaveChangesAsync();
                    }
              }
            }
            catch (Exception) {
                throw;
            }
        }

        public void DeleteAll(AppUser _object)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppUser> GetAll()
        {
            try
            {
                var obj = _appDbContext.AppUsers.ToList();
                if (obj != null)
                    return obj;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AppUser GetById(string id)
        {
            try
            {
              if (id != null)
              {
                    var Obj =_appDbContext.AppUsers.FirstOrDefault(x => x.Id == id);
                    if (Obj != null)
                        return Obj;
                    else
                        return null;
              }
              else
               {
                    return null;
               }

            }
            catch(Exception)
            {
                throw;
            }
        }

        public void Update(AppUser appuser)
        {
            try
            {
                if (appuser != null)
                {
                    var obj = _appDbContext.Update(appuser);
                    if (obj != null)
                        _appDbContext.SaveChanges();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
