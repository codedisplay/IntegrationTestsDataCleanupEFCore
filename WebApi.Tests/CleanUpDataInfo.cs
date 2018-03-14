using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Tests
{
    public class CleanUpDataInfo
    {
        public EntityEntry EntityEntry { get; set; }
        public object Entity { get; set; }
        public PropertyValues Values { get; set; }
        public EntityState EntityState { get; set; }
        public int SaveChangesAttempt { get; set; }
        public DateTime DateTime { get; set; }
    }
}
