using FriendZone.API.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendZone.API.Data
{
    public class FriendZoneDbCondext : DbContext
    {
        public FriendZoneDbCondext(DbContextOptions<FriendZoneDbCondext> options) : base(options)
        {

        }

        public DbSet<Value> Values { get; set; }
    }
}
