using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BDCO.Web.Models;
using System;
using BDCO.Domain;
using BDCO.Domain.Aggregates;
using BDCO.Core.Command;
using System.Collections.Generic;
using BDCO.Domain.RequestModels;
using BDCO.Domain.RequestParams;
using BDCO.Domain.Identity;
using BDCO.Domain.Models;

namespace BDCO.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private UnitOfWork unitOfWork = new UnitOfWork();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        #region User Geo Location
        [Authorize]
        public ActionResult UserGeolocation()
        {
            return View();
        }
        public JsonResult GetUsergeolocation(int UserId)
        {
            try
            {
                DatabaseContext context = new DatabaseContext();
                var DistrictList = context.UserGeoLocation.Where(x => x.UserID == UserId).Select(a => new
                {
                    DistrictCode = a.DistrictCode
                }).Distinct().ToList();
                var UpazilaList = context.UserGeoLocation.Where(x => x.UserID == UserId).Select(a => new
                {
                    DistrictCode = a.DistrictCode,
                    UpazilaCode = a.UpazilaCode
                }).Distinct().ToList();

                var UnionList = context.UserGeoLocation.Where(x => x.UserID == UserId).Select(a => new
                {
                    DistrictCode = a.DistrictCode,
                    UpazilaCode = a.UpazilaCode,
                    UnionCode = a.UnionCode
                }).Distinct().ToList();

                var VillageList = context.UserGeoLocation.Where(x => x.UserID == UserId).Select(a => new
                {
                    DistrictCode = a.DistrictCode,
                    UpazilaCode = a.UpazilaCode,
                    UnionCode = a.UnionCode,
                    VillageCode = a.VillageCode
                }).Distinct().ToList();
                string sqlCenter = $@"SELECT DISTINCT * FROM CenterInfo Where CenterId IN (SELECT VALUE FROM dbo.fn_Split( (SELECT top 1 CenterId FROM UserGeoLocation Where UserId='{UserId}'),','))";
                var CenterList = unitOfWork.GenericRepositories<CenterInfo>().GetRecordSet(sqlCenter).ToList();
                //var CenterList = context.UserGeoLocation.Where(x => x.UserID == UserId).Select(a => new
                //{
                //    DistrictCode = a.DistrictCode,
                //    UpazilaCode = a.UpazilaCode,
                //    UnionCode = a.UnionCode,
                //    VillageCode = a.VillageCode,
                //    CenterId = a.CenterId
                //}).Distinct().ToList();
                var CampList = context.UserGeoLocation.Where(x => x.UserID == UserId).Select(a => new
                {
                    DistrictCode = a.DistrictCode,
                    UpazilaCode = a.UpazilaCode,
                    UnionCode = a.UnionCode,
                    VillageCode = a.VillageCode,
                    CampId = a.CampId
                }).Distinct().ToList();

                string sqlBlock = $@"SELECT DISTINCT * FROM BlockInfo Where BlockId IN  (SELECT  BlockId FROM UserGeoLocation Where UserId='{UserId}')";
                var BlockList = unitOfWork.GenericRepositories<BlockInfo>().GetRecordSet(sqlBlock).ToList();
                //var BlockList = context.UserGeoLocation.Where(x => x.UserID == UserId).Select(a => new
                //{
                //    DistrictCode = a.DistrictCode,
                //    UpazilaCode = a.UpazilaCode,
                //    UnionCode = a.UnionCode,
                //    VillageCode = a.VillageCode,
                //    CenterId=a.CenterId,
                //    CampId=a.CampId,
                //    BlockId = a.BlockId
                //}).Distinct().ToList();

                return Json(new { success = true, Data = new { DistrictList, UpazilaList, UnionList, VillageList,CenterList, CampList, BlockList } }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult GetDistrict(District query)
        {

            try
            {

                var geo = new viewDistrict().GetDistrictInfo(query);
                return Json(new { success = true, Data = geo }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult GetUnion(UserGeolocationForSave userGeo)
        {

            try
            {
                DatabaseContext context = new DatabaseContext();
                string sql = string.Format(@"SELECT DISTINCT sg.DistrictCode,geod.DistrictNameBangla as DistrictName, sg.UpazilaCode,geou.UpazilaNameBangla as UpazilaName,sg.UnionCode,geoun.UnionName,geoun.UnionNameBangla FROM GeoLocation sg 
                LEFT JOIN Geolocation.dbo.Unions geoun on sg.DistrictCode = geoun.DistrictCode and sg.UpazilaCode = geoun.UpazilaCode and sg.UnionCode = geoun.UnionCode
                LEFT JOIN Geolocation.dbo.Upazila geou on sg.DistrictCode = geou.DistrictCode and sg.UpazilaCode = geou.UpazilaCode
                LEFT JOIN Geolocation.dbo.District geod on sg.DistrictCode = geod.DistrictCode ");
                var des = context.Database.SqlQuery<UnionPermission>(sql).ToList();
                List<UnionPermission> UnionList = new List<UnionPermission>();
                if (userGeo.lstUpazila != null && userGeo.lstUpazila.Count > 0)
                {
                    foreach (var item in userGeo.lstUpazila)
                    {
                        UnionList.AddRange(des.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode).ToList());
                    }

                }
                else
                {
                    UnionList = des;
                }
                return Json(new { success = true, Data = UnionList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetUpazila(UserGeolocationForSave userGeo)
        {

            try
            {

                DatabaseContext context = new DatabaseContext();
                string sql = string.Format(@"SELECT DISTINCT sg.DistrictCode,geod.DistrictNameBangla as DistrictName, sg.UpazilaCode,geou.UpazilaNameBangla,geou.UpazilaName FROM GeoLocation sg 
                LEFT JOIN Geolocation.dbo.Upazila geou on sg.DistrictCode = geou.DistrictCode and sg.UpazilaCode = geou.UpazilaCode
                LEFT JOIN Geolocation.dbo.District geod on sg.DistrictCode = geod.DistrictCode ");
                var des = context.Database.SqlQuery<UpazilaPermission>(sql).ToList();
                List<UpazilaPermission> UpazilaList = new List<UpazilaPermission>();
                if (userGeo.lstDistrict != null && userGeo.lstDistrict.Count > 0)
                {
                    foreach (var item in userGeo.lstDistrict)
                    {
                        UpazilaList.AddRange(des.Where(x => x.DistrictCode == item.DistrictCode).ToList());
                    }

                }
                else
                {
                    UpazilaList = des;
                }
                return Json(new { success = true, Data = UpazilaList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [Authorize]
        public JsonResult SaveUserGeolocation(UserGeolocationForSave userGeo)
        {
            try
            {
                DatabaseContext context = new DatabaseContext();
                string centerIds = "0";
                if(userGeo!=null)
                {
                    if (userGeo.lstCenter != null && userGeo.lstCenter.Count() > 0)
                    {
                        var arr = userGeo.lstCenter.Select(x => x.CenterId).ToArray();
                        centerIds=string.Join(",", arr);
                    }
                }
                string ihsql = string.Format(@"INSERT INTO [dbo].[UserGeolocationHistroy]([UserID],[DivisionCode],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],CenterId,	CampId,[BlockId],[FromDate],[ToDate])
		           (SELECT [UserID],[DivisionCode],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],CenterId,	CampId,[BlockId],[Date],GETDATE() FROM [dbo].[UserGeoLocation] WHERE UserId = '{0}')", userGeo.UserId);

                context.Database.ExecuteSqlCommand(ihsql);

                string sql = string.Format(@"DELETE FROM UserGeolocation WHERE UserId = {0}", userGeo.UserId);
                context.Database.ExecuteSqlCommand(sql);

                if (userGeo.lstBlock != null && userGeo.lstBlock.Count > 0)
                {
                    foreach (var item in userGeo.lstBlock)
                    {
                        sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],CenterId,	CampId, [BlockId],[Date])VALUES
                                           ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',GETDATE() )", userGeo.UserId, item.DistrictCode, item.UpazilaCode, item.UnionCode, item.VillageCode,centerIds,item.CampId, item.BlockId);
                        context.Database.ExecuteSqlCommand(sql);
                    }
                }

                if (userGeo.lstVillage != null && userGeo.lstVillage.Count > 0)
                {
                    foreach (var item in userGeo.lstVillage)
                    {
                        //sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],[Date])VALUES
                        //                   ('{0}','{1}','{2}','{3}','{4}',GETDATE() )", userGeo.UserId, item.DistrictCode, item.UpazilaCode, item.UnionCode, item.VillageCode);
                        //context.Database.ExecuteSqlCommand(sql);

                        if (userGeo.lstBlock != null && userGeo.lstBlock.Count > 0)
                        {
                            var isExist = userGeo.lstVillage.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode && x.VillageCode == item.VillageCode).ToList();
                            if (isExist == null)
                            {
                                sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],CenterId,CampId,[BlockId],[Date])VALUES
                                           ('{0}','{1}','{2}','{3}','{4}','{5}','0','0',GETDATE() )", userGeo.UserId, item.DistrictCode, item.UpazilaCode, item.UnionCode, item.VillageCode, centerIds);
                                context.Database.ExecuteSqlCommand(sql);
                            }
                        }
                        else
                        {
                            if (userGeo.lstCamp != null && userGeo.lstCamp.Count > 0)
                            {
                                foreach (var camp in userGeo.lstCamp.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode && x.VillageCode == item.VillageCode).ToList())
                                {
                                    sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],CenterId,CampId,[BlockId],[Date])VALUES
                                           ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','0',GETDATE() )", userGeo.UserId, item.DistrictCode, item.UpazilaCode, item.UnionCode, item.VillageCode, centerIds,camp.CampId);
                                    context.Database.ExecuteSqlCommand(sql);
                                }
                                
                            }
                            else
                            {
                                sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],CenterId,CampId,[BlockId],[Date])VALUES
                                           ('{0}','{1}','{2}','{3}','{4}','{5}','0','0',GETDATE() )", userGeo.UserId, item.DistrictCode, item.UpazilaCode, item.UnionCode, item.VillageCode, centerIds);
                                context.Database.ExecuteSqlCommand(sql);
                            }
                        }
                    }
                }

                if (userGeo.lstUnion != null && userGeo.lstUnion.Count > 0)
                {
                    foreach (var item in userGeo.lstUnion)
                    {
                        if (userGeo.lstBlock == null && userGeo.lstVillage != null)
                        {
                            var isExist = userGeo.lstVillage.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode).ToList();
                            if (isExist == null)
                            {
                                sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],[CenterId],[CampId],[BlockId],[Date])VALUES
                                           ('{0}','{1}','{2}','{3}','0','{4}','0','0',GETDATE() )", userGeo.UserId, item.DistrictCode, item.UpazilaCode, item.UnionCode, centerIds);
                                context.Database.ExecuteSqlCommand(sql);
                            }
                        }
                        else
                        {

                            var isExistInBlock = 0;
                            var isExistInVillage = 0;
                            if (userGeo.lstBlock != null)
                            {
                                var data = userGeo.lstBlock.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode).ToList();
                                if (data != null)
                                {
                                    isExistInBlock = 1;
                                }
                            }
                            if (userGeo.lstVillage != null)
                            {
                                var data = userGeo.lstVillage.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode).ToList();
                                if (data != null)
                                {
                                    isExistInVillage = 1;
                                }
                            }
                            if (isExistInBlock == 0 && isExistInVillage == 0)
                            {
                                sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],[CenterId],[CampId],[BlockId],[Date])VALUES
                                           ('{0}','{1}','{2}','{3}','0','{4}','0','0',GETDATE() )", userGeo.UserId, item.DistrictCode, item.UpazilaCode, item.UnionCode, centerIds);
                                context.Database.ExecuteSqlCommand(sql);
                            }
                        }


                    }
                }

                if (userGeo.lstUpazila != null && userGeo.lstUpazila.Count > 0)
                {
                    foreach (var item in userGeo.lstUpazila)
                    {
                        if (userGeo.lstBlock == null && userGeo.lstVillage == null && userGeo.lstUnion == null)
                        {
                            sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],[CenterId],[CampId],[BlockId],[Date])VALUES
                                           ('{0}','{1}','{2}','0','0','{3}','0','0',GETDATE() )", userGeo.UserId, item.DistrictCode, item.UpazilaCode, centerIds);
                            context.Database.ExecuteSqlCommand(sql);
                        }
                        else
                        {
                            var isExistInBlock = 0;
                            var isExistInUnion = 0;
                            var isExistInVillage = 0;
                            if (userGeo.lstBlock != null)
                            {
                                var data = userGeo.lstBlock.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode).ToList();
                                if (data != null)
                                {
                                    isExistInBlock = 1;
                                }
                            }
                            if (userGeo.lstVillage != null)
                            {
                                var data = userGeo.lstVillage.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode).ToList();
                                if (data != null)
                                {
                                    isExistInVillage = 1;
                                }
                            }
                            if (userGeo.lstUnion != null)
                            {
                                var data = userGeo.lstUnion.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode).ToList();
                                if (data != null)
                                {
                                    isExistInUnion = 1;
                                }
                            }

                            if (isExistInBlock == 0 && isExistInUnion == 0 && isExistInVillage == 0)
                            {
                                sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],[CenterId],[CampId],[BlockId],[S],[Date])VALUES
                                           ('{0}','{1}','{2}','0','0','{3}','0','0',GETDATE() )", userGeo.UserId, item.DistrictCode, item.UpazilaCode, centerIds);
                                context.Database.ExecuteSqlCommand(sql);
                            }
                        }

                        /// end ///
                        //if (userGeo.lstUnion != null && userGeo.lstUnion.Count > 0)
                        //{
                        //    var isExist = userGeo.lstUnion.Where(x => x.UpazilaCode == item.UpazilaCode).ToList();
                        //    if (isExist == null)
                        //    {
                        //        sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DivisionCode],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode])VALUES
                        //                   ('{0}','60','{1}','{2}','0','0' )", userGeo.UserId, item.DistrictCode, item.UpazilaCode);
                        //        context.Database.ExecuteSqlCommand(sql);
                        //    } 
                        //}
                        //else
                        //{
                        //    sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DivisionCode],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode])VALUES
                        //                   ('{0}','60','{1}','{2}','0','0' )", userGeo.UserId, item.DistrictCode, item.UpazilaCode);
                        //    context.Database.ExecuteSqlCommand(sql);
                        //}
                    }
                }

                if (userGeo.lstDistrict != null && userGeo.lstDistrict.Count > 0)
                {
                    foreach (var item in userGeo.lstDistrict)
                    {
                        if (userGeo.lstVillage == null && userGeo.lstUnion == null && userGeo.lstUpazila == null)
                        {
                            sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],[CenterId],[CampId],[BlockId],[Date])VALUES
                                           ('{0}','{1}','0','0','0','{2}','0','0',GETDATE() )", userGeo.UserId, item.DistrictCode, centerIds);
                            context.Database.ExecuteSqlCommand(sql);
                        }
                        else
                        {
                            var isExistInBlock = 0;
                            var isExistInUnion = 0;
                            var isExistInUpazila = 0;
                            var isExistInVillage = 0;
                            if (userGeo.lstBlock != null)
                            {
                                var data = userGeo.lstBlock.Where(x => x.DistrictCode == item.DistrictCode).ToList();
                                if (data != null)
                                {
                                    isExistInBlock = 1;
                                }
                            }
                            if (userGeo.lstVillage != null)
                            {
                                var data = userGeo.lstVillage.Where(x => x.DistrictCode == item.DistrictCode).ToList();
                                if (data != null)
                                {
                                    isExistInVillage = 1;
                                }
                            }
                            if (userGeo.lstUnion != null)
                            {
                                var data = userGeo.lstUnion.Where(x => x.DistrictCode == item.DistrictCode).ToList();
                                if (data != null)
                                {
                                    isExistInUnion = 1;
                                }
                            }
                            if (userGeo.lstUpazila != null)
                            {
                                var data = userGeo.lstUpazila.Where(x => x.DistrictCode == item.DistrictCode).ToList();
                                if (data != null)
                                {
                                    isExistInUpazila = 1;
                                }
                            }

                            if (isExistInBlock == 0 && isExistInUnion == 0 && isExistInUpazila == 0 && isExistInVillage == 0)
                            {
                                sql = string.Format(@"INSERT INTO [dbo].[UserGeoLocation] ([UserID],[DistrictCode],[UpazilaCode],[UnionCode],[VillageCode],[CenterId],[CampId],[BlockId],[Date])VALUES
                                           ('{0}','{1}','0','0','0','{2}','0','0',GETDATE() )", userGeo.UserId, item.DistrictCode, centerIds);
                                context.Database.ExecuteSqlCommand(sql);
                            }
                        }
                    }
                }
                return Json(new { success = true, Data = "Save successful" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region ServicePoint
        public ActionResult UserServicePoint()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult SaveUserServicePoint(UserServicePointForSave userRcN)
        {
            try
            {
                DatabaseContext context = new DatabaseContext();

                string ihsql = string.Format(@"INSERT INTO [dbo].[UserServicePointHistroy]([UserID],[ServicePointId],[FromDate],[ToDate])
		           (SELECT [UserID],[ServicePointId],[Date],GETDATE() FROM [dbo].[UserServicePoint] WHERE UserId = '{0}')", userRcN.UserId);

                context.Database.ExecuteSqlCommand(ihsql);

                string sql = string.Format(@"DELETE FROM UserServicePoint WHERE UserId = {0}", userRcN.UserId);
                context.Database.ExecuteSqlCommand(sql);

                if (userRcN.lstRcN != null && userRcN.lstRcN.Count > 0)
                {
                    foreach (var item in userRcN.lstRcN)
                    {
                        sql = string.Format(@"INSERT INTO [dbo].[UserServicePoint] ([UserID],[ServicePointId],[Date])VALUES
                                           ('{0}','{1}',GETDATE() )", userRcN.UserId, item.ServicePointId);
                        context.Database.ExecuteSqlCommand(sql);
                    }
                }

                return Json(new { success = true, Data = "Save successful" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Data = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetServicePointInfo(ServicePointForPermission query)
        {
            DatabaseContext context = new DatabaseContext();

            List<ServicePoint> lstServicePoint = new List<ServicePoint>();
            List<UserServicePoint> ServicePointList = new List<UserServicePoint>();
            if (query.UserId == null)
                lstServicePoint = context.ServicePoint.ToList();
            else
            {
                var userInfo = context.AspNetUsers.Where(x => x.UserID == query.UserId).FirstOrDefault();
                lstServicePoint = context.ServicePoint.Where(r => r.UserType == userInfo.GeoType).OrderByDescending(r => r.UserType).ToList();
                ServicePointList = context.UserServicePoint.Where(x => x.UserID == query.UserId).ToList();
            }
            return Json(new { success = true, Data = new { lstServicePoint, ServicePointList } }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        [Authorize]
        public ActionResult RegisterUser()
        {
            return View();
        }
        public JsonResult GetObservationalOrgList()
        {
            try
            {
                var result = new Domain.Aggregates.ObservationalOrganization().GetaObservationalOrganizationList();
                return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetUserRolls()
        {
            try
            {
                DatabaseContext context = new DatabaseContext();
                var result = context.UserRoll.ToList();
                return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        public ActionResult UserList()
        {
            return View();
        }

        public JsonResult GetVillage(UserGeolocationForSave userGeo)
        {

            try
            {
                DatabaseContext context = new DatabaseContext();
                string sql = string.Format(@"SELECT DISTINCT sg.DistrictCode,geod.DistrictNameBangla as DistrictName, sg.UpazilaCode,geou.UpazilaNameBangla as UpazilaName,sg.UnionCode,geoun.UnionNameBangla as UnionName, geov.VillageCode, geov.VillageName FROM GeoLocation sg 
	            LEFT JOIN Geolocation.dbo.Village geov on sg.DistrictCode = geov.DistrictCode and sg.UpazilaCode = geov.UpazilaCode and sg.UnionCode = geov.UnionCode and sg.VillageCode = geov.VillageCode
                LEFT JOIN Geolocation.dbo.Unions geoun on sg.DistrictCode = geoun.DistrictCode and sg.UpazilaCode = geoun.UpazilaCode and sg.UnionCode = geoun.UnionCode
                LEFT JOIN Geolocation.dbo.Upazila geou on sg.DistrictCode = geou.DistrictCode and sg.UpazilaCode = geou.UpazilaCode
                LEFT JOIN Geolocation.dbo.District geod on sg.DistrictCode = geod.DistrictCode ");
                var des = context.Database.SqlQuery<VillagePermission>(sql).ToList();
                List<VillagePermission> VillageList = new List<VillagePermission>();
                if (userGeo.lstUnion != null && userGeo.lstUnion.Count > 0)
                {
                    foreach (var item in userGeo.lstUnion)
                    {
                        VillageList.AddRange(des.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode).ToList());
                    }
                }
                else
                {
                    VillageList = des;
                }
                return Json(new { success = true, Data = VillageList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCenterInfo(UserGeolocationForSave userGeo)
        {

            try
            {
                var centers = unitOfWork.GenericRepositories<CenterInfo>().GetAll().ToList();
                List<CenterInfo> CenterList = new List<CenterInfo>();
                if (userGeo.lstVillage != null && userGeo.lstVillage.Count > 0)
                {
                    foreach (var item in userGeo.lstVillage)
                    {
                        //CenterList.AddRange(centers.Where(x => x.DistrictCode == item.DistrictCode 
                        //&& x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode 
                        //&& x.VillageCode == item.VillageCode 
                        //&& item.CenterId.Split(',').Contains(x.CenterId.ToString())).ToList());
                        CenterList.AddRange(centers.Where(x => x.DistrictCode == item.DistrictCode
                       && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode
                       && x.VillageCode == item.VillageCode).ToList());
                    }
                }
                else
                {
                    CenterList = centers;
                }
                return Json(new { success = true, Data = CenterList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetCampInfo(UserGeolocationForSave userGeo)
        {

            try
            {
                var camps = unitOfWork.GenericRepositories<CampInfo>().GetAll().ToList();
                List<CampInfo> CampList = new List<CampInfo>();
                if (userGeo.lstVillage != null && userGeo.lstVillage.Count > 0)
                {
                    foreach (var item in userGeo.lstVillage)
                    {
                        //CampList.AddRange(camps.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode && x.VillageCode == item.VillageCode 
                        //&&  x.CampId==item.CampId).ToList());
                        CampList.AddRange(camps.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode && x.VillageCode == item.VillageCode).ToList());
                    }
                }
                else
                {
                    CampList = camps;
                }
                return Json(new { success = true, Data = CampList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetBlock(UserGeolocationForSave userGeo)
        {

            try
            {
                DatabaseContext context = new DatabaseContext();
                string sql = string.Format(@"SELECT DISTINCT sg.DistrictCode,geod.DistrictNameBangla as DistrictName, sg.UpazilaCode,geou.UpazilaNameBangla as UpazilaName,sg.UnionCode,geoun.UnionNameBangla as UnionName, geov.VillageCode, geov.VillageName, 
                cast( C.CenterId as nvarchar(max)) CenterId,C.CenterName,Camp.CampId,Camp.CampName , sb.BlockId , sb.BlockName+' ('+geov.VillageName+')' as BlockName
                FROM GeoLocation sg 
                INNER JOIN BlockInfo sb ON sg.DistrictCode = sb.DistrictCode AND sg.UpazilaCode = sb.UpazilaCode AND sg.UnionCode = sb.UnionCode AND sg.VillageCode =sb.VillageCode 
                INNER JOIN CampInfo Camp  ON sg.DistrictCode = Camp.DistrictCode AND sg.UpazilaCode = Camp.UpazilaCode AND sg.UnionCode = Camp.UnionCode AND sg.VillageCode =Camp.VillageCode 
                INNER JOIN CenterInfo C  ON sg.DistrictCode = C.DistrictCode AND sg.UpazilaCode = C.UpazilaCode AND sg.UnionCode = C.UnionCode AND sg.VillageCode =C.VillageCode 
                LEFT JOIN Geolocation.dbo.Village geov on sg.DistrictCode = geov.DistrictCode and sg.UpazilaCode = geov.UpazilaCode and sg.UnionCode = geov.UnionCode and sg.VillageCode = geov.VillageCode
                LEFT JOIN Geolocation.dbo.Unions geoun on sg.DistrictCode = geoun.DistrictCode and sg.UpazilaCode = geoun.UpazilaCode and sg.UnionCode = geoun.UnionCode
                LEFT JOIN Geolocation.dbo.Upazila geou on sg.DistrictCode = geou.DistrictCode and sg.UpazilaCode = geou.UpazilaCode
                LEFT JOIN Geolocation.dbo.District geod on sg.DistrictCode = geod.DistrictCode  ");
                var des = context.Database.SqlQuery<BlockPermission>(sql).ToList();
                List<BlockPermission> BlockList = new List<BlockPermission>();
                if (userGeo.lstVillage != null && userGeo.lstVillage.Count > 0)
                {
                    foreach (var item in userGeo.lstVillage)
                    {
                        BlockList.AddRange(des.Where(x => x.DistrictCode == item.DistrictCode && x.UpazilaCode == item.UpazilaCode && x.UnionCode == item.UnionCode && x.VillageCode == item.VillageCode).ToList());
                    }
                }
                else
                {
                    BlockList = des;
                }
                return Json(new { success = true, Data = BlockList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public JsonResult GetUserInfoList()
        {
            try
            {
                DatabaseContext context = new DatabaseContext();
                var result = context.AspNetUsers.ToList();
                return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetUserInfobyId(int UserId)
        {
            try
            {
                DatabaseContext context = new DatabaseContext();
                var result = context.AspNetUsers.Where(x => x.UserID == UserId).FirstOrDefault();
                //var ip = context.ObservationalOrganization.Where(x => x.Name == result.Organization).FirstOrDefault();
                //if (ip != null)
                //{
                //    result.Organization = ip.ID + "";
                //}
                return Json(new { success = true, Data = result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

            // return Json("Hello", JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveUser(UserRM command)
        {
            command.UserId = User.CurrentUserID();
            CommandResult commandResult = new AspNetUsers().SaveUser(command);
            if (commandResult.Success)
                return Json(new { success = true, Data = commandResult.Message }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, Data = commandResult.Message }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken()]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, data = getModelStateErrorMessage(), returnUrl = returnUrl }, JsonRequestBehavior.AllowGet);
                }
                var user = await UserManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    return Json(new { success = false, data = "User Not Found", returnUrl = returnUrl }, JsonRequestBehavior.AllowGet);

                }
                if (user.OldPassword == model.Password && user.IsActive == true)
                {
                    await SignInManager.SignInAsync(user, true, false);
                    return Json(new { success = true, data = "Login Success", returnUrl = returnUrl }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    if (user.IsActive == false)
                    {
                        return Json(new { success = false, data = "Your account has been deactivated!", returnUrl = returnUrl }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { success = false, data = "Invalid login attempt.", returnUrl = returnUrl }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, data = ex.Message, returnUrl = returnUrl }, JsonRequestBehavior.AllowGet);
            }
            //try
            //{
            //    UnitOfWork unitOfWork = new UnitOfWork();
            //    if (ModelState.IsValid)
            //    {
            //        var user = unitOfWork.GenericRepositories<AspNetUsers>().FindBy(x => x.UserName == model.UserName && x.OldPassword == model.Password).FirstOrDefault();
            //        if (user != null)
            //        {
            //            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            //            switch (result)
            //            {
            //                case SignInStatus.Success:
            //                    {
            //                        return Json(new { success = true, data = "Login Success", returnUrl = returnUrl }, JsonRequestBehavior.AllowGet);
            //                    }
            //                case SignInStatus.LockedOut:
            //                    return View("Lockout");
            //                case SignInStatus.RequiresVerification:
            //                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //                case SignInStatus.Failure:
            //                default:
            //                    ModelState.AddModelError("", "Invalid login attempt.");
            //                    return Json(new { success = false, data = getModelStateErrorMessage() }, JsonRequestBehavior.AllowGet);
            //            }
            //        }
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return Json(new { success = false, data = getModelStateErrorMessage() }, JsonRequestBehavior.AllowGet);

            //    }
            //    else
            //    {
            //        return Json(new { success = false, data = getModelStateErrorMessage() }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return Json(new { success = false, data = ex.InnerException.InnerException.Message.ToString() }, JsonRequestBehavior.AllowGet);
            //}
        }

        public string getModelStateErrorMessage()
        {
            string msg = "";
            foreach (string key in ModelState.Keys)
            {
                foreach (var error in ModelState[key].Errors)
                {
                    msg += error.ErrorMessage + "\r\n";
                }
            }
            return msg;
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    DisplayName = model.DisplayName,
                    UserName = model.UserName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email
                };

                //if (User.IsAdmin() == 0)
                //{
                //    user.IsAdmin = 2;
                //}

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Register", "Account");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword()
        {
            return View();
            // return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var userName = User.UserName();
        //    var user = await UserManager.FindByNameAsync(userName);

        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UserManager.ResetPasswordAsync(user.Id, null, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            try
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return Json(new { success = true, data = getModelStateErrorMessage() }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}