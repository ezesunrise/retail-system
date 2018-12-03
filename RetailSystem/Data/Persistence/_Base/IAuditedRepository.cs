using RetailSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RetailSystem.Data
{
    public interface IAuditedRepository<T> : IRepository<T> where T: AuditedEntity
    {

    }
}