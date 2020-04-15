using BDCO.Domain.Aggregates;
using BDCO.Domain.Identity;
using BDCO.Domain.Models;
using BDCO.Domain.Models.MemberInfo;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BDCO.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {
            Database.SetInitializer<DatabaseContext>(null);
        }
        public DbSet<DeviceInfo> DeviceInfo { get; set; }
        public DbSet<ServicePoint> ServicePoint { get; set; }
        public DbSet<UserServicePoint> UserServicePoint { get; set; }
        public DbSet<UserGeoLocation> UserGeoLocation { get; set; }
        public DbSet<CenterInfo> CenterInfo { get; set; }
        public DbSet<CampInfo> Camp { get; set; }
        public DbSet<GenderInfo> GenderInfo { get; set; }
        public DbSet<ProfileInfo > TeacherProfile { get; set; }
        public DbSet<LearningFacility> LearningFacility { get; set; }
        public DbSet<PartnerInfo> PartnerInfo { get; set; }
        public DbSet<TrainingCategory> TrainingCategory { get; set; }
        public DbSet<TrainingInfo> TrainingInfo { get; set; }
        public DbSet<TrainingInfoMaterial> TrainingInfoMaterial { get; set; }
        public DbSet<TrainingInfoTopic> TrainingInfoTopic { get; set; }
        public DbSet<TrainingMemberInfo> TrainingMemberInfo { get; set; }



        // End
        public DbSet<UsersRoles> UserRole { get; set; }
        public DbSet<Resources> Resources { get; set; }
        public DbSet<UserRoll> UserRoll { get; set; }
        public DbSet<ObservationalOrganization> ObservationalOrganization { get; set; }
        public DbSet<AspNetUsers> AspNetUsers { get; set; }        

        public DbSet<BlockInfo> BlockInfo { get; set; }
       

        public DbSet<UniqueId> UserUniqueId { get; set; }



        public DbSet<ReferralHospitalType> ReferralHospitalType { get; set; }
        public DbSet<ReferralHospital> ReferralHospital { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
