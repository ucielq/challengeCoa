using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCOA.Models;

namespace WebApiCOA.Data
{
    public class WebApiCOAContext : DbContext
    {
        public WebApiCOAContext (DbContextOptions<WebApiCOAContext> options)
            : base(options)
        {
        }

        public DbSet<WebApiCOA.Models.Usuario> Usuario { get; set; }
    }
}
