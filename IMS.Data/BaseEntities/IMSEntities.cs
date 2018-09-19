using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;

namespace IMS.Data
{
    public partial class IMSEntities : DbContext
    {
        public IMSEntities()
            : base("IMSEntities")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
    }
}
