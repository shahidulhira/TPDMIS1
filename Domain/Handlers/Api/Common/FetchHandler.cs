using Domain.Aggregates;
using RapidFireLib.Lib.Api;
using RapidFireLib.Lib.Core;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using static RapidFireLib.Lib.Api.WebApi;

namespace Domain.Handlers.Api.Common
{
    public class FetchGeolocationHandler : IApiHandler
    {
        public object Handle(Mode modePrePost, ProcessType processType, object model, Db db)
        {
            PermittedGeoLocation modelCasted = null;
            if (modePrePost == Mode.Pre)
                if (processType == ProcessType.Get)
                    modelCasted = GetPermittedGeoLocation1(model);
            return modelCasted;
        }

        private PermittedGeoLocation GetPermittedGeoLocation1(object model)
        {
            PermittedGeoLocation permittedGeoLocation = new PermittedGeoLocation();
            List<UserGeo> userGeolocationView = new List<UserGeo>();
            userGeolocationView = ((List<object>) model).Cast<UserGeo>().ToList();
            permittedGeoLocation.District = userGeolocationView.GroupBy(p => p.DistrictCode).Select(x =>
                    new District() {DistrictCode = x.First().DistrictCode, DistrictName = x.First().DistrictName,DistrictNameBangla = x.First().DistrictNameBangla}).ToList();
            
            permittedGeoLocation.Upazila = userGeolocationView.GroupBy(m =>
                new
                {
                    m.DistrictCode, m.UpazilaCode
                }).Select(x =>
                new Upazila()
                {
                    DistrictCode = x.First().DistrictCode, UpazilaCode = x.First().UpazilaCode,
                    UpazilaName = x.First().UpazilaName,
                    UpazilaNameBangla = x.First().UpazilaNameBangla
                }).ToList();
            
            permittedGeoLocation.Unions = userGeolocationView
                .GroupBy(p => new {p.DistrictCode, p.UpazilaCode, p.UnionCode}).Select(x =>
                    new Union()
                    {
                        DistrictCode = x.First().DistrictCode,
                        UpazilaCode = x.First().UpazilaCode,
                        UnionCode = x.First().UnionCode,
                        UnionName = x.First().UnionName,
                        UnionNameBangla = x.First().UnionNameBangla
                    }).ToList();
            
            permittedGeoLocation.Village = userGeolocationView
                .GroupBy(p => new {p.DistrictCode, p.UpazilaCode, p.UnionCode, p.VillageCode}).Select(x =>
                    new Village()
                    {
                        DistrictCode = x.First().DistrictCode,
                        UpazilaCode = x.First().UpazilaCode,
                        UnionCode = x.First().UnionCode,
                        VillageCode = x.First().VillageCode,
                        VillageName = x.First().VillageName,
                        VillageNameBangla = string.IsNullOrEmpty(x.First().VillageNameBangla)? x.First().VillageName:x.First().VillageNameBangla
                    }).Distinct().ToList();

            return permittedGeoLocation;
        }

        private PermittedGeoLocation GetPermittedGeoLocation(int userId, Db db)
        {
            var result = new PermittedGeoLocation();
            string sql = string.Format(@"SELECT DISTINCT S.DistrictCode ,D.DistrictName, D.DistrictNameBangla
                                            FROM dbo.Geolocation s
                                            inner join GeoLocation.dbo.District d on d.DistrictCode=s.DistrictCode
                                            where s.DistrictCode in (select DistrictCode from UserGeoLocation where UserID='{0}')",
                userId);
            result.District = db.Get<District>(sql);

            sql = string.Format(@"SELECT DISTINCT  s.DistrictCode,u.UpazilaCode ,u.UpazilaName,u.UpazilaNameBangla
                                        FROM dbo.Geolocation s
                                        inner join GeoLocation.dbo.Upazila u on u.DistrictCode=s.DistrictCode 
                                        where  
										U.DistrictCode in (select DistrictCode from UserGeoLocation where UserID='{0}') AND
										U.UpazilaCode in (select UpazilaCode from UserGeoLocation where UserID='{0}')",
                userId);
            result.Upazila = db.Get<Upazila>(sql);

            sql = string.Format(
                @"SELECT distinct   s.DistrictCode,s.UpazilaCode,s.UnionCode ,u.UnionName,u.UnionNameBangla
                                        FROM dbo.Geolocation s
                                        inner join GeoLocation.dbo.Unions U on U.UnionCode=s.UnionCode and U.DistrictCode=s.DistrictCode and u.UpazilaCode=s.UpazilaCode 
                                        where  
										U.DistrictCode in (select DistrictCode from UserGeoLocation where UserID='{0}') AND
										U.UpazilaCode in (select UpazilaCode from UserGeoLocation where UserID='{0}') AND
                                        U.UnionCode in (select UnionCode from UserGeoLocation where UserID='{0}')",
                userId);
            result.Unions = db.Get<Union>(sql);

            sql = string.Format(
                @" SELECT DISTINCT s.DistrictCode,s.UpazilaCode,s.UnionCode,s.VillageCode,v.VillageName,v.VillageNameBangla 
                                        FROM dbo.Geolocation s
                                        inner join GeoLocation.dbo.Upazila u on u.UpazilaCode=s.UpazilaCode
                                        inner join GeoLocation.dbo.Village v on v.VillageCode=s.VillageCode and v.UnionCode=s.UnionCode and v.UpazilaCode=s.UpazilaCode  and v.DistrictCode=s.DistrictCode  
                                        where  
                                        v.DistrictCode in (select DistrictCode from UserGeoLocation where UserID='{0}') AND
                                        v.UpazilaCode in (select UpazilaCode from UserGeoLocation where UserID='{0}') AND
                                        v.UnionCode in (select UnionCode from UserGeoLocation where UserID='{0}') AND
                                        v.VillageCode in (select VillageCode from UserGeoLocation where UserID='{0}')",
                userId);
            result.Village = db.Get<Village>(sql);
            return result;
        }
    }
}