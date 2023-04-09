using WebApi3_DAL.Contracts;
using WebApi3_DAL.Model;

namespace WebApi3_BAL.Services
{
    public class ServiceAppUser
    {
        public readonly IRepository<AppUser> _repository;
        public ServiceAppUser(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        //Create Method
        public async Task<AppUser> AddUser(AppUser appUser)
        {
            try
            {
                if (appUser == null)
                {
                    throw new ArgumentNullException(nameof(appUser));
                }

                else
                {
                    return await _repository.Create(appUser);
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void DeleteUser(string Id)
        {
            try
            {
                if (Id != null)
                {
                    var obj = _repository.GetAll().Where(x => x.Id == Id).FirstOrDefault();
                    if (obj != null)
                    {
                        _repository.Delete(obj);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void UpdateUser(string Id) {
            try
            {
                if (Id != null)
                {
                    var obj = _repository.GetAll().Where(x =>x.Id == Id).FirstOrDefault();
                    if (obj != null)
                    {
                        _repository.Update(obj);
                    }
                }
            }
            catch(Exception) {
                throw;
            }
        }
    }
}