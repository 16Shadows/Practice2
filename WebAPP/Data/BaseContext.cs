using WebAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPP.Data
{
    public class BaseContext: DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        { }

        // Tables for database
        //public DbSet<T> TableName => Set<T>();
    }
}
