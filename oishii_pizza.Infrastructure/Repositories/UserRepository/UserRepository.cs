using Microsoft.EntityFrameworkCore;
using oishii_pizza.Infrastructure.Common.BaseRepository;
using oishii_pizza.Infrastructure.DbContext;
using oishii_pizza.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Repositories.UserRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly OishiiPizzaDbContext _oishiiPizzaDbContext;
        public UserRepository(OishiiPizzaDbContext db) : base(db)
        {
            _oishiiPizzaDbContext = db;
        }
        public async Task<User> GetByUserNameAsync(string email)
        {
            var result = await _oishiiPizzaDbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return result;
        }
    }
}
