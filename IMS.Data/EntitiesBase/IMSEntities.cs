using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IMS.Data
{
    public partial class IMSEntities:DbContext
    {
        public IMSEntities(DbContextOptions<IMSEntities> options) : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
    }
}
