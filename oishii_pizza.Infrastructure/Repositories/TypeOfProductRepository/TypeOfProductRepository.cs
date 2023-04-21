using oishii_pizza.Infrastructure.Common.BaseRepository;
using oishii_pizza.Infrastructure.DbContext;
using oishii_pizza.Infrastructure.Entities;
using oishii_pizza.Infrastructure.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oishii_pizza.Infrastructure.Repositories.TypeOfProductRepository
{
    public class TypeOfProductRepository : BaseRepository<TypeOfProduct>, ITypeOfProductRepository
    {
        private readonly OishiiPizzaDbContext _oishiiPizzaDbContext;
        public TypeOfProductRepository(OishiiPizzaDbContext db) : base(db)
        {
            _oishiiPizzaDbContext = db;
        }
    }
}
