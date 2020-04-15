using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BDCO.Domain.Aggregates
{
    public class Country : AggregateRoot
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string Nationality { get; set; }

        //.private readonly IRepository<viewDistrict> repoviewDistrict = new Repository<viewDistrict>();
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public List<Country> GetNationality()
        {
            string sql = string.Format(@"SELECT * FROM [dbo].[Country]");
            var result = _unitOfWork.context.Database.SqlQuery<Country>(sql).ToList();
            return result;
        }
    }
    public class Districts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DivisionCode { get; set; }
        public string District { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameBangla { get; set; }

        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<Districts> GetDistrict()
        {
            try
            {
                string sql = @"SELECT distinct d.* FROM GeoLocation.dbo.District d, GeoLocation rg WHERE d.DistrictCode=rg.DistrictCode ORDER BY [DistrictName]";
                List<Districts> result = unitOfWork.Repositories<Districts>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class Upazila
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubDistrictID { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameBangla { get; set; }

        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<Upazila> GetUpazila(UpazilaFilter filter)
        {
            try
            {
                string sql = @"SELECT distinct d.* FROM GeoLocation.dbo.Upazila d, GeoLocation rg WHERE (d.DistrictCode=rg.DistrictCode AND d.UpazilaCode=rg.UpazilaCode) AND  d.DistrictCode = '" + filter.DistrictCode + "' ORDER BY [UpazilaName]";
                List<Upazila> result = unitOfWork.Repositories<Upazila>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Upazila> GetUpazilaAll()
        {
            try
            {
                string sql = @"SELECT distinct d.* FROM GeoLocation.dbo.Upazila d, GeoLocation rg WHERE (d.DistrictCode=rg.DistrictCode AND d.UpazilaCode=rg.UpazilaCode) ORDER BY [UpazilaName]";
                List<Upazila> result = unitOfWork.Repositories<Upazila>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class UpazilaFilter
    {
        public string DistrictCode { get; set; }
    }

    public class Unions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnionID { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string UnionNameBangla { get; set; }


        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<Unions> GetUnions(UnionsFilter filter)
        {
            try
            {
                string sql = @"SELECT distinct d.* FROM GeoLocation.dbo.Unions  d, GeoLocation rg WHERE (d.DistrictCode=rg.DistrictCode AND d.UpazilaCode=rg.UpazilaCode AND d.UnionCode=rg.UnionCode) AND  d.DistrictCode = '" + filter.DistrictCode
                    + "' AND d.UpazilaCode = '" + filter.UpazilaCode + "' ORDER BY [UnionName]";
                List<Unions> result = unitOfWork.Repositories<Unions>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Unions> GetUnionAll()
        {
            try
            {
                string sql = @"SELECT distinct d.* FROM GeoLocation.dbo.Unions  d, GeoLocation rg WHERE (d.DistrictCode=rg.DistrictCode AND d.UpazilaCode=rg.UpazilaCode AND d.UnionCode=rg.UnionCode)  ORDER BY [UnionName]";
                List<Unions> result = unitOfWork.Repositories<Unions>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class UnionsFilter
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
    }

    public class Village
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public string VillageNameBangla { get; set; }


        private UnitOfWork unitOfWork = new UnitOfWork();

        public List<Village> GetVillage(VillageFilter filter)
        {
            try
            {
                string sql = @"SELECT distinct d.* FROM GeoLocation.dbo.Village  d, GeoLocation rg WHERE (d.DistrictCode=rg.DistrictCode AND d.UpazilaCode=rg.UpazilaCode AND d.UnionCode=rg.UnionCode AND d.VillageCode=rg.VillageCode) AND  d.DistrictCode = '" + filter.DistrictCode
                    + "' AND d.UpazilaCode = '" + filter.UpazilaCode + "' AND d.UnionCode = '" + filter.UnionCode + "' ORDER BY [VillageName]";
                List<Village> result = unitOfWork.Repositories<Village>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Village> GetVillageAll()
        {
            try
            {
                string sql = @"SELECT distinct d.* FROM GeoLocation.dbo.Village  d, GeoLocation rg WHERE (d.DistrictCode=rg.DistrictCode AND d.UpazilaCode=rg.UpazilaCode AND d.UnionCode=rg.UnionCode AND d.VillageCode=rg.VillageCode) ORDER BY [VillageName]";
                List<Village> result = unitOfWork.Repositories<Village>().GetRecordSet(sql).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public class VillageFilter
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
    }
    public class UserGeolocationForSave
    {
        public List<District> lstDistrict { get; set; }
        public List<Upazila> lstUpazila { get; set; }
        public List<Union> lstUnion { get; set; }
        public List<VillagePermission> lstVillage { get; set; }
        public List<CenterPermission> lstCenter { get; set; }
        public List<CampPermission> lstCamp { get; set; }
        public List<BlockPermission> lstBlock { get; set; }
        public int UserId { get; set; }
    }
    public class VillagePermission
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
    }
    public class UpazilaPermission
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameBangla { get; set; }
    }
    public class UnionPermission
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string UnionNameBangla { get; set; }
    }
    public class CenterPermission
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }   
        public string CenterId { get; set; }
       

    }
    public class CampPermission
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }       
        public int? CampId { get; set; }

    }
    public class BlockPermission
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public int? BlockId { get; set; }
        public string BlockName { get; set; }
        public string CenterId { get; set; }
        public int? CampId { get; set; }
        
    }

}
