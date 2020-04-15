using Domain.Aggregates;
using Domain.Aggregates.Common;
using Domain.Aggregates.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models;
using RapidFireLib.Models.IdentityModels;

namespace Domain.Contexts
{
    public class DefaultMSSQLContext : RFCoreDbContext
    {
        public DefaultMSSQLContext(SAASType sAASType) : base("DefaultConnection", sAASType) { }
        public DbSet<AspNetUser> AspNetUser { get; set; }
        public DbSet<AspNetRole> AspNetRole { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<SubjectTest> SubjectTest { get; set; }
        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<GroupMemberInfoE> GroupMemberInfoE { get; set; }
        public DbSet<GroupInfoE> GroupInfoE { get; set; }
        public DbSet<DataVerificationLog> DataVerificationLog { get; set; }

        //public DbSet<IdentityUserClaim<string>> Claims { get; set; }
        //public DbSet<IdentityUserLogin<string>> Logins { get; set; }
        //public DbSet<IdentityUserToken<string>> Tokens { get; set; }
        //public DbSet<TwoFactorRecoveryCode> RecoveryCodes { get; set; }


        //Navbar Actions DbSets
        public DbSet<AuditTrail> AuditTrail { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Approval> Approval { get; set; }
        public DbSet<QuickSearch> QuickSearch { get; set; }
        //Navbar Actions DbSets
        


        #region Checklist
        public DbSet<TeacherTrainingChecklist> TeacherTrainingChecklist { get; set; }
        #endregion

        #region Batch
        public DbSet<BatchInfo> BatchInfo { get; set; }
        public DbSet<BatchMemberInfo> BatchMemberInfo { get; set; }

        public DbSet<BatchIndex> BatchIndex { get; set; }
        #endregion

        #region Training
        public DbSet<TrainingInfo> TrainingInfo { get; set; }
        public DbSet<TrainingMemberInfo> TrainingMemberInfo { get; set; }
        public DbSet<TrainingInfoTopic> TrainingInfoTopic { get; set; }
        public DbSet<TrainingInfoMaterial> TrainingInfoMaterial { get; set; }

        public DbSet<TrainingCategory> TrainingCategory { get; set; }
        public DbSet<TrainingMaterial> TrainingMaterial { get; set; }
        public DbSet<TrainingTopic> TrainingTopic { get; set; }
        #endregion

        #region
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        #endregion

        //public DbSet<DataVerificationLog> DataVerificationLog { get; set; }
        //public DbSet<GroupMemberInfoE> GroupMemberInfoE { get; set; }
        public DbSet<UserGeolocation> UserGeo { get; set; }
        //public DbSet<Village> Village { get; set; }
        //User School Geo
        public DbQuery<UserGeo> UserGeolocationView { get; set; }

        public DbQuery<District> District { get; set; }
        public DbQuery<Upazila> Upazila { get; set; }
        public DbQuery<Union> Union { get; set; }
        public DbQuery<Village> Village { get; set; }
        public DbSet<CampInfo> CampInfo { get; set; }
        public DbSet<BlockInfo> BlockInfo { get; set; }
        public DbSet<CompetencyTest> CompetencyTest { get; set; }
        public DbSet<DocAndRecordChecklist> DocAndRecordChecklist { get; set; }
        public DbSet<InterviewQuestionnaire> InterviewQuestionnaire { get; set; }
        public DbSet<DeviceInfo> DeviceInfo { get; set; }
        public DbSet<PartnerInfo> PartnerInfo { get; set; }
        public DbSet<ObservationChecklist> ObservationChecklist { get; set; }
        public DbSet<LearningFacility> LearningFacility { get; set; }
        public DbSet<ProfileInfo> ProfileInfo { get; set; }

    }
}
