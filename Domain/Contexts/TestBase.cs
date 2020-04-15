using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models;

namespace Domain.Contexts
{
    public class TestBase : RFCoreDbContext
    {
        public TestBase(SAASType sAASType) : base("MSSQLConnection", sAASType) { }
        public DbSet<Student> Student { get; set; }
        public DbSet<AuditTrail> AuditTrail { get; set; }
    }
}
