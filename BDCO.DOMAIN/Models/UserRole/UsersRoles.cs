using BDCO.Core.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDCO.Domain.Models
{
    [Table("AspNetRoles")]
    public class UsersRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        private UnitOfWork unitOfWork = new UnitOfWork();

        public CommandResult Save(UsersRoles institute)
        {
            try
            {
                if (institute.Id == 0)
                    unitOfWork.Repositories<UsersRoles>().Insert(institute);
                else
                    unitOfWork.Repositories<UsersRoles>().Update(institute);

                unitOfWork.SaveChange();

                return new CommandResult()
                {
                    Success = true,
                    Message = "Record Saved successfully."
                };
            }
            catch (Exception ex)
            {
                return new CommandResult()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public CommandResult Delete(int Id)
        {
            try
            {
                var student = unitOfWork.Repositories<UsersRoles>().FindBy(s => s.Id == Id).FirstOrDefault();
                if (student != null)
                {
                    unitOfWork.Repositories<UsersRoles>().Delete(student);
                    unitOfWork.SaveChange();
                }

                return new CommandResult()
                {
                    Success = true,
                    Message = "Record Deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new CommandResult()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        public List<UsersRoles> GetUsersRoleAll()
        {
            try
            {
                string sql = @"SELECT * FROM AspNetRoles ORDER BY Name";

                List<UsersRoles> result = unitOfWork.Repositories<UsersRoles>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UsersRoles> GetActiveUsersRole()
        {
            try
            {
                string sql = @"SELECT * FROM AspNetRoles Where Active=1 ORDER BY Name";

                List<UsersRoles> result = unitOfWork.Repositories<UsersRoles>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
