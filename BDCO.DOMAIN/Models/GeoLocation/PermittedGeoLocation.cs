using System.Collections.Generic;
using System.Linq;
using BDCO.Domain.Query;
using BDCO.Domain.Models;

namespace BDCO.Domain.Aggregates
{
    public class PermittedGeoLocationViewModels
    {
        public List<District> District { get; set; }
        public List<Upazila> Upazila { get; set; }
        public List<Union> Unions { get; set; }
        public List<Village> Village { get; set; }
        public List<CenterInfo> CenterInfo { get; set; }
        public List<CampInfo> CampInfo { get; set; }
        public long TotalRecord { get; set; }
    }
    public class PermittedGeoLocation : AggregateRoot
    {

        //public readonly IRepository<Village> repositoryVillage = new Repository<Village>();
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public PermittedGeoLocationViewModels GetPermittedGeoLocation(PermittedGeoLocationRM query)
        {

            var result = new PermittedGeoLocationViewModels();
            string sql = string.Format(@"SELECT DISTINCT S.DistrictCode ,D.DistrictName, D.DistrictNameBangla
                                            FROM dbo.TPDGeoLocation s
                                            inner join GeoLocation.dbo.District d on d.DistrictCode=s.DistrictCode
                                            where s.DistrictCode in (select DistrictCode from UserGeoLocation where UserID='{0}')", query.UserId);
            result.District = _unitOfWork.context.Database.SqlQuery<District>(sql).ToList();

            sql = string.Format(@"SELECT DISTINCT  s.DistrictCode,u.UpazilaCode ,u.UpazilaName,u.UpazilaNameBangla
                                        FROM dbo.TPDGeoLocation s
                                        inner join GeoLocation.dbo.Upazila u on u.DistrictCode=s.DistrictCode 
                                        where  
										U.DistrictCode in (select DistrictCode from UserGeoLocation where UserID='{0}') AND
										U.UpazilaCode in (select UpazilaCode from UserGeoLocation where UserID='{0}')", query.UserId);
            result.Upazila = _unitOfWork.context.Database.SqlQuery<Upazila>(sql).ToList();

            sql = string.Format(@"SELECT distinct   s.DistrictCode,s.UpazilaCode,s.UnionCode ,u.UnionName,u.UnionNameBangla
                                        FROM dbo.TPDGeoLocation s
                                        inner join GeoLocation.dbo.Unions U on U.UnionCode=s.UnionCode and U.DistrictCode=s.DistrictCode and u.UpazilaCode=s.UpazilaCode 
                                        where  
										U.DistrictCode in (select DistrictCode from UserGeoLocation where UserID='{0}') AND
										U.UpazilaCode in (select UpazilaCode from UserGeoLocation where UserID='{0}') AND
                                        U.UnionCode in (select UnionCode from UserGeoLocation where UserID='{0}')", query.UserId);
            result.Unions = _unitOfWork.context.Database.SqlQuery<Union>(sql).ToList();

            sql = string.Format(@" SELECT DISTINCT s.DistrictCode,s.UpazilaCode,s.UnionCode,s.VillageCode,v.VillageName,v.VillageNameBangla 
                                        FROM dbo.TPDGeoLocation s
                                        inner join GeoLocation.dbo.Upazila u on u.UpazilaCode=s.UpazilaCode
                                        inner join GeoLocation.dbo.Village v on v.VillageCode=s.VillageCode and v.UnionCode=s.UnionCode and v.UpazilaCode=s.UpazilaCode  and v.DistrictCode=s.DistrictCode  
                                        where  
                                        v.DistrictCode in (select DistrictCode from UserGeoLocation where UserID='{0}') AND
                                        v.UpazilaCode in (select UpazilaCode from UserGeoLocation where UserID='{0}') AND
                                        v.UnionCode in (select UnionCode from UserGeoLocation where UserID='{0}') AND
                                        v.VillageCode in (select VillageCode from UserGeoLocation where UserID='{0}')", query.UserId);
            result.Village = _unitOfWork.context.Database.SqlQuery<Village>(sql).ToList();

            string centerSql = string.Format(@"SELECT DISTINCT C.*
                                                    FROM UserGeolocation UG                                                     
                                                    INNER JOIN CenterInfo C  ON C.CenterId IN(select Value from dbo.fn_Split( UG.CenterId,','))
                                                    WHERE UG.UserId = {0}", query.UserId);
            result.CenterInfo = context.Database.SqlQuery<CenterInfo>(centerSql).ToList();

            string campSql = string.Format(@"SELECT DISTINCT C.*
                                                    FROM UserGeolocation UG                                                     
                                                    INNER JOIN CampInfo C ON C.CampId = UG.CampId
                                                    WHERE UG.UserId = {0}", query.UserId);
            result.CampInfo = context.Database.SqlQuery<CampInfo>(campSql).ToList();

            return result;
        }
    }
    public class GeoLocationView : AggregateRoot
    {
        public int UserID { get; set; }
        public string BenID { get; set; }
        public string BenNameEn { get; set; }
        public string BenNameBn { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DivisionNameBangla { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameBangla { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameBangla { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string UnionNameBangla { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public string VillageNameBangla { get; set; }
        public string HusName { get; set; }
        public string FatName { get; set; }
        public string BenMobile { get; set; }


        public UnitOfWork _unitOfWork = new UnitOfWork();

        public List<GeoLocationView> GetAllById(GeoLocationRM query)
        {
            string sql = string.Format(@"SELECT DISTINCT
                    ug.UserID
                   ,bm.BenID as BenID
                   ,bm.BenNameEn as BenNameEn
                   ,bm.BenNameBn as BenNameBn
                   ,vg.[DivisionCode]
                      ,vg.[DivisionName]
                      ,vg.[DivisionNameBangla]
                      ,vg.[DistrictCode]
                      ,vg.[DistrictName]
                      ,vg.[DistrictNameBangla]
                      ,vg.[UpazilaCode]
                      ,vg.[UpazilaName]
                      ,vg.[UpazilaNameBangla]
                      ,vg.[UnionCode]
                      ,vg.[UnionName]
                      ,vg.[UnionNameBangla]
                      ,vg.[VillageCode]
                      ,vg.[VillageName]
                      ,vg.[VillageNameBangla]
                      ,bm.[HusName]
                      ,bm.[FatName]
                      ,ISNULL(bm.[Cell], '') as BenMobile
                  FROM [GeoLocation].[dbo].[viewGeoLocation] vg 
                  inner join RRMIS.dbo.UserGeoLocation ug on vg.DistrictCode = ug.DistrictCode AND (vg.UpazilaCode = ug.UpazilaCode OR ug.UpazilaCode=0) AND (vg.UnionCode = ug.UnionCode OR ug.UnionCode=0)
                  inner join RRMIS.dbo.BasicInfo bm on vg.DistrictCode = bm.DistrictCode AND vg.UpazilaCode = bm.UpazilaCode  AND vg.UnionCode = bm.UnionCode AND vg.VillageCode = bm.VillageCode
                  WHERE ug.UserID ='{0}'", query.UserID);

            var result = _unitOfWork.context.Database.SqlQuery<GeoLocationView>(sql).ToList();
            return result;


        }


    }


    // Not using it retuns district upazila union village as a concated string
    public class TPDGeoLocationView : AggregateRoot
    {
        public string DistrictCodes { get; set; }
        public string UpazilaCodes { get; set; }
        public string UnionCodes { get; set; }
        public string VillageCodes { get; set; }

        public UnitOfWork _unitOfWork = new UnitOfWork();

        public TPDGeoLocationView GetAllSuchanaGeolocation()
        {
            TPDGeoLocationView result = new TPDGeoLocationView();
            string sql = string.Format(@"EXEC GetTPDGeoLocation");
            result = _unitOfWork.context.Database.SqlQuery<TPDGeoLocationView>(sql).FirstOrDefault();
            return result;
        }
    }


    public class Division : AggregateRoot
    {
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DivisionNameBangla { get; set; }
    }

    public class District : AggregateRoot
    {
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameBangla { get; set; }

        private UnitOfWork _unitOfWork = new UnitOfWork();
        public List<District> GetDistrict(PermittedGeoLocation query)
        {
            List<District> result = new List<District>();

            string sql = string.Format(@"SELECT DISTINCT S.DistrictCode ,D.DistrictName, D.DistrictNameBangla
                                        FROM dbo.TPDGeoLocation s
                                        inner join GeoLocation.dbo.District d on d.DistrictCode=s.DistrictCode");

            result = _unitOfWork.context.Database.SqlQuery<District>(sql).ToList();
            return result;
        }
    }

    public class Upazilas : AggregateRoot
    {
        // public int UpazilaID { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameBangla { get; set; }

        private UnitOfWork _unitOfWork = new UnitOfWork();
        //private readonly IRepository<Upazila> repoUpazila = new Repository<Upazila>();
        public List<Upazila> GetUpazila(TPDGeoLocationRM query)
        {
            List<Upazila> result = new List<Upazila>();
            string sql = string.Format(@"SELECT DISTINCT  s.DistrictCode,u.UpazilaCode ,u.UpazilaName,u.UpazilaNameBangla
                                        FROM dbo.TPDGeoLocation s
                                        inner join GeoLocation.dbo.Upazila u on u.DistrictCode=s.DistrictCode where [GeoType]='{0}'", query.GeoType);
            result = _unitOfWork.context.Database.SqlQuery<Upazila>(sql).ToList();
            return result;
        }
        public List<Upazila> GetUpazilaByDistrictCode(Upazila query)
        {
            List<Upazila> result = new List<Upazila>();

            string sql = string.Format(@"SELECT DISTINCT  s.DistrictCode,u.UpazilaCode ,u.UpazilaName,u.UpazilaNameBangla
                                        FROM dbo.TPDGeoLocation s
                                        inner join GeoLocation.dbo.Upazila u on u.DistrictCode=s.DistrictCode where s.DistrictCode='{0}' and u.UpazilaName like '%{1}%'", query.DistrictCode, query.UpazilaName);
            result = _unitOfWork.context.Database.SqlQuery<Upazila>(sql).ToList();
            return result;
        }
    }


    public class Union : AggregateRoot
    {
        // public int UnionID { get; set; }
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        // public string MunicipalityID { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string UnionNameBangla { get; set; }

        private UnitOfWork _unitOfWork = new UnitOfWork();

        public List<Union> GetUnion(TPDGeoLocationRM query)
        {
            //repoUnion.SetContext(context);
            List<Union> result = new List<Union>();

            string sql = string.Format(@"SELECT distinct   s.DistrictCode,s.UpazilaCode,s.UnionCode ,u.UnionName,u.UnionNameBangla
                                        FROM dbo.TPDGeoLocation s
                                        inner join GeoLocation.dbo.Unions U on U.UnionCode=s.UnionCode and U.DistrictCode=s.DistrictCode and u.UpazilaCode=s.UpazilaCode 
                                        where u.GeoType='{0}'", query.GeoType);
            result = _unitOfWork.context.Database.SqlQuery<Union>(sql).ToList();
            return result;
        }
        public List<Union> GetUnionByDistrictCodeUpazilaCodeAndUnionName(Union query)
        {
            List<Union> result = new List<Union>();

            string sql = string.Format(@"SELECT distinct   s.DistrictCode,s.UpazilaCode,s.UnionCode ,u.UnionName,u.UnionNameBangla
                                        FROM dbo.TPDGeoLocation s
                                        inner join GeoLocation.dbo.Unions U on U.UnionCode=s.UnionCode and U.DistrictCode=s.DistrictCode and u.UpazilaCode=s.UpazilaCode 
                                        where s.DistrictCode ='{0}' AND s.UpazilaCode='{1}' AND u.UnionName like '%{2}%'", query.DistrictCode, query.UpazilaCode, query.UnionName);
            result = _unitOfWork.context.Database.SqlQuery<Union>(sql).ToList();
            return result;
        }
    }

    public class Villages : AggregateRoot
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public string VillageNameBangla { get; set; }




        private UnitOfWork _unitOfWork = new UnitOfWork();
        //private readonly IRepository<Village> repoVillage = new Repository<Village>();
        public List<Village> GetVillage(TPDGeoLocationRM query)
        {
            List<Village> result = new List<Village>();

            string sql = string.Format(@" SELECT DISTINCT s.DistrictCode,s.UpazilaCode,s.UnionCode,s.VillageCode,v.VillageName,v.VillageNameBangla 
				                           FROM dbo.TPDGeoLocation s
				                           inner join GeoLocation.dbo.Upazila u on u.UpazilaCode=s.UpazilaCode
                                           inner join GeoLocation.dbo.Village v on v.VillageCode=s.VillageCode and v.UnionCode=s.UnionCode and v.UpazilaCode=s.UpazilaCode  and v.DistrictCode=s.DistrictCode  
				                           where u.GeoType='{0}'", query.GeoType);
            result = _unitOfWork.context.Database.SqlQuery<Village>(sql).ToList();
            return result;
        }

        public List<Village> GetVillageByDistrictCodeUpazilaCodeAndVillageName(Village query)
        {
            List<Village> result = new List<Village>();

            string sql = string.Format(@" SELECT DISTINCT s.DistrictCode,s.UpazilaCode,s.UnionCode,s.VillageCode,v.VillageName,v.VillageNameBangla 
				                           FROM dbo.TPDGeoLocation s
				                           inner join GeoLocation.dbo.Upazila u on u.UpazilaCode=s.UpazilaCode
                                           inner join GeoLocation.dbo.Village v on v.VillageCode=s.VillageCode and v.UnionCode=s.UnionCode and v.UpazilaCode=s.UpazilaCode  and v.DistrictCode=s.DistrictCode  
				                           where s.DistrictCode ='{0}' AND s.UpazilaCode='{1}' AND v.VillageName like '%{2}%'", query.DistrictCode, query.UpazilaCode, query.VillageName);
            result = _unitOfWork.context.Database.SqlQuery<Village>(sql).ToList();
            return result;
        }
    }
}
