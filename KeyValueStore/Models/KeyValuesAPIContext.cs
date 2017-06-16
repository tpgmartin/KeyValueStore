using Microsoft.EntityFrameworkCore;
using System;
namespace KeyValueStore.Models
{
    public class KeyValuesAPIContext : DbContext
    {
        public KeyValuesAPIContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<KeyValue> KeyValues { get; set; }
    }
}
