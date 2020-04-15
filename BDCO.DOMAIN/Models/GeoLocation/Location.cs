using BDCO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDCO.Domain.Result;
using BDCO.Domain.RequestModels;

namespace BDCO.Domain.Aggregates
{
    public class viewDivision : AggregateRoot
    {

        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DivisionNameBangla { get; set; }
        private UnitOfWork _unitOfWork = new UnitOfWork();


        public List<viewDivision> GetviewDivisionInfo(Division query)
        {
            string sql = string.Format(@"SELECT [DivisionCode],[DivisionName],[DivisionNameBangla] FROM [SuchanaMIS].[dbo].[viewDivision]");
            var result = _unitOfWork.context.Database.SqlQuery<viewDivision>(sql).ToList();
            return result;
        }
    }

    public class viewNationality : AggregateRoot
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string Nationality { get; set; }

        //.private readonly IRepository<viewDistrict> repoviewDistrict = new Repository<viewDistrict>();
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public List<viewNationality> GetNationality()
        {
            string sql = string.Format(@"SELECT * FROM [dbo].[Country]");
            var result = _unitOfWork.context.Database.SqlQuery<viewNationality>(sql).ToList();
            return result;
        }
    }

    public class viewDistrict : AggregateRoot
    {
        public string DivisionCode { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string DistrictNameBangla { get; set; }

        //.private readonly IRepository<viewDistrict> repoviewDistrict = new Repository<viewDistrict>();
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public List<viewDistrict> GetDistrictInfo(District query)
        {
            string sql = string.Format(@"SELECT [DistrictCode],[DistrictName],[DistrictNameBangla] FROM [dbo].[viewDistrict]");
            var result = _unitOfWork.context.Database.SqlQuery<viewDistrict>(sql).ToList();
            return result;
        }

        public List<viewDistrict> GetDistrictInfoByUserId(DistrictByUserIdRM query)
        {
            //repoviewDistrict.SetContext(context);
            string sql = string.Format(@"SELECT [DistrictCode],[DistrictName],[DistrictNameBangla] FROM [dbo].[viewDistrict] 
                                         WHERE DistrictCode IN (SELECT DistrictCode FROM [dbo].[UserGeoLocation] WHERE UserID={0})",query.UserID);
            var result = _unitOfWork.context.Database.SqlQuery<viewDistrict>(sql).ToList();
            return result;
        }

        public List<viewDistrict> GetDistrictByDivisionCode(DistrictByDivisionRM query)
        {
            //repoviewDistrict.SetContext(context);
            string sql = string.Format(@"SELECT  [DistrictCode],[DistrictName],[DistrictNameBangla] FROM [dbo].[viewDistrict] WHERE [DivisionCode]={0}", query.DivisionCode);
            
            var result = _unitOfWork.context.Database.SqlQuery<viewDistrict>(sql).ToList();
            return result;
        }
        public List<viewDistrict> GetDistrictBySuchanaGeolocation(DistrictByGeolocationRM query)
        {
            //repoviewDistrict.SetContext(context);
            string sql = string.Format(@"SELECT Distinct vd.[DistrictName],vd.[DistrictNameBangla],vd.[DistrictCode] 
                                        FROM [SuchanaMIS].[dbo].[viewDistrict] vd 
                                        Inner join [dbo].[SuchanaGeolocation] sg on sg.DistrictCode=vd.DistrictCode");
            
            var result = _unitOfWork.context.Database.SqlQuery<viewDistrict>(sql).ToList();
            return result;
        }
    }

    public class viewUpazila : AggregateRoot
    {
        public string DistrictCode { get; set; }

        public string UpazilaCode { get; set; }
        public string UpazilaName { get; set; }
        public string UpazilaNameBangla { get; set; }
        private UnitOfWork _unitOfWork = new UnitOfWork();
        public List<viewUpazila> GetUpazilaInfo(Upazila upazila)
        {
            string sql = string.Format(@"SELECT  [UpazilaCode],[UpazilaNameBangla] FROM [dbo].[viewUpazila] WHERE [DistrictCode]='{0}'", upazila.DistrictCode);
            var result = _unitOfWork.context.Database.SqlQuery<viewUpazila>(sql).ToList();
            return result;
        }

        public List<viewUpazila> GetUpazilaByDistrictAndUserId(UpazilaByUserIdRM upazilaByUser)
        {
            string sql = "";
            if (upazilaByUser.NGOID == "0")
            {
                sql = string.Format(@"SELECT [UpazilaCode],[UpazilaName],[UpazilaNameBangla],[DistrictCode] FROM [dbo].[viewUpazila] 
                                        WHERE DistrictCode IN (SELECT DistrictCode FROM [dbo].[UserGeoLocation] WHERE UserID={0})
                                        AND (UpazilaCode IN (SELECT UpazilaCode FROM [dbo].[UserGeoLocation] WHERE UserID={1})
                                        OR DistrictCode IN (SELECT DistrictCode FROM [dbo].[UserGeoLocation] WHERE UserID={2} AND UpazilaCode='0'))", upazilaByUser.UserID, upazilaByUser.UserID, upazilaByUser.UserID);
            }
            else
            {
                sql = string.Format(@"SELECT [UpazilaCode],[UpazilaName],[UpazilaNameBangla],[DistrictCode] INTO #TEMP FROM [dbo].[viewUpazila]                                           
                                    WHERE DistrictCode IN (SELECT DistrictCode FROM [dbo].[UserGeoLocation] WHERE UserID={0})                                          
                                    AND (UpazilaCode IN (SELECT UpazilaCode FROM [dbo].[UserGeoLocation] WHERE UserID={1})                                          
                                    OR DistrictCode IN (SELECT DistrictCode FROM [dbo].[UserGeoLocation] WHERE UserID={2} AND UpazilaCode='0'))

                                    SELECT t.* FROM #TEMP t
                                    INNER JOIN [dbo].[IPGeoLocation] ipg on t.DistrictCode = ipg.DistrictCode AND t.UpazilaCode = ipg.UpazilaCode
                                    WHERE ipg.IPCode = {3}
                                    DROP TABLE #TEMP", upazilaByUser.UserID, upazilaByUser.UserID, upazilaByUser.UserID, upazilaByUser.NGOID);
            }
            var result = new List<viewUpazila>();
            result = _unitOfWork.context.Database.SqlQuery<viewUpazila>(sql).ToList();
            if (upazilaByUser.DistrictCode != null)
            {
                result = result.Where(x => x.DistrictCode == upazilaByUser.DistrictCode).ToList();
            }
            return result;
        }
    }

    public class viewUnion : AggregateRoot
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string UnionName { get; set; }
        public string UnionNameBangla { get; set; }

        public UnitOfWork _unitOfWork = new UnitOfWork();
        public List<viewUnion> GetUnionInfo(UnionRM union)
        {
            string Unionsql = string.Format(@"SELECT  [UnionCode] FROM [dbo].[UserGeoLocation] WHERE DistrictCode='{0}' and [UpazilaCode]='{1}' and  UserId='{2}'", union.DistrictCode, union.UpazilaCode, union.UserID);
            List<viewUnion> resulta = new List<viewUnion>();
            var unions = _unitOfWork.context.Database.SqlQuery<viewUnion>(Unionsql).ToList();
            List<viewUnion> result = new List<viewUnion>();

            if (unions.Count == 0 || unions.Count > 0 && unions[0].UnionCode == "0")
            {
                string sql = string.Format(@"SELECT  [UnionCode],[UnionNameBangla] FROM [dbo].[viewUnion] WHERE DistrictCode='{0}' and [UpazilaCode]='{1}'", union.DistrictCode, union.UpazilaCode);
                result = _unitOfWork.context.Database.SqlQuery<viewUnion>(sql).ToList();
                return result;
            }
            else
            {
                string st = "";
                for (int i = 0; i < unions.Count; i++)
                {
                    if (i == 0)
                    {
                        st += unions[i].UnionCode;
                    }
                    else
                    {
                        st += "," + unions[i].UnionCode;
                    }


                }

                string sql = string.Format(@"SELECT  [UnionCode],[UnionNameBangla] FROM [dbo].[viewUnion] WHERE DistrictCode='{0}' and [UpazilaCode]='{1}' and [UnionCode] in ({2})", union.DistrictCode, union.UpazilaCode, st);
                //List<viewUnion> result = new List<viewUnion>();
                result = _unitOfWork.context.Database.SqlQuery<viewUnion>(sql).ToList();
                return result;
            }

        }
    }

    public class viewVillage : AggregateRoot
    {
        public string DistrictCode { get; set; }
        public string UpazilaCode { get; set; }
        public string UnionCode { get; set; }
        public string VillageCode { get; set; }
        public string VillageName { get; set; }
        public string VillageNameBangla { get; set; }
        private UnitOfWork _unitOfWork = new UnitOfWork();
        //private readonly IRepository<viewVillage> repoviewVillage = new Repository<viewVillage>();

        public List<viewVillage> GetVillageInfo(VillageRM village)
        {

            //repoviewVillage.SetContext(context);
            string sql = string.Format(@"SELECT  [VillageCode] FROM [dbo].[UserGeoLocation] WHERE DistrictCode='{0}' and [UpazilaCode]='{1}' and  [UnionCode]='{2}' and  UserId='{3}'", village.DistrictCode, village.UpazilaCode, village.UnionCode, village.UserID);
            List<viewVillage> result = new List<viewVillage>();
            result = _unitOfWork.context.Database.SqlQuery<viewVillage>(sql).ToList();

            if (result.Count == 0 || result.Count > 0 && result[0].VillageCode == "0")
            {
                sql = string.Format(@"SELECT  [VillageCode],[VillageName] FROM [dbo].[viewVillage] WHERE DistrictCode='{0}' and UpazilaCode='{1}' and  [UnionCode]='{2}'", village.DistrictCode, village.UpazilaCode, village.UnionCode);
                result = _unitOfWork.context.Database.SqlQuery<viewVillage>(sql).ToList();
                return result;
            }
            else
            {
                string st = "";
                for (int i = 0; i < result.Count; i++)
                {
                    if (i == 0)
                    {
                        st += result[i].VillageCode;
                    }
                    else
                    {
                        st += "," + result[i].VillageCode;
                    }
                }
                sql = string.Format(@"SELECT  [VillageCode],[VillageName] FROM [dbo].[viewVillage] WHERE DistrictCode='{0}' and UpazilaCode='{1}' and  [UnionCode]='{2}' and [VillageCode] in ({3})", village.DistrictCode, village.UpazilaCode, village.UnionCode, st);
                result = _unitOfWork.context.Database.SqlQuery<viewVillage>(sql).ToList();
                return result;
            }

        }
    }
}
