using BDCO.Domain;
using BDCO.Domain.Aggregates;
using BDCO.Domain.Models;
using BDCO.Domain.Models.MemberInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDCO.Web.Controllers
{
    public class CommonController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public JsonResult GetCamp()
        {
            CampInfo camp = new CampInfo();
            return Json(camp.GetCamp(), JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GetAllBlockID(string DistrictCode, string UpazilaCode, string UnionCode, string VillageCode)
        {
            BlockRequestParams filter = new BlockRequestParams();
            filter.DistrictCode = DistrictCode;
            filter.UpazilaCode = UpazilaCode;
            filter.UnionCode = UnionCode;
            filter.VillageCode = VillageCode;
            BlockIDList bObj = new BlockIDList();
            return Json(bObj.GetBlockIDList(filter), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBlock(string DistrictCode, string UpazilaCode, string UnionCode, string VillageCode)
        {
            BlockRequestParams filter = new BlockRequestParams();
            filter.DistrictCode = DistrictCode;
            filter.UpazilaCode = UpazilaCode;
            filter.UnionCode = UnionCode;
            filter.VillageCode = VillageCode;
            BlockInfo block = new BlockInfo();
            return Json(block.GetAllBlock(filter), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGenderInfo()
        {
            GenderInfo genderInfo = new GenderInfo();
            return Json(genderInfo.GetGenderInfo(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNationality()
        {
            Country genderInfo = new Country();
            return Json(genderInfo.GetNationality(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetServicePoint()
        {
            ServicePoint servicePoint = new ServicePoint();
            return Json(servicePoint.GetServicePoint(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistrict()
        {
            Districts district = new Districts();
            return Json(district.GetDistrict(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUpazila(UpazilaFilter filter)
        {
            Upazila upazila = new Upazila();
            return Json(upazila.GetUpazila(filter), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUpazilaAll()
        {
            Upazila upazila = new Upazila();
            return Json(upazila.GetUpazilaAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnions(UnionsFilter filter)
        {
            Unions unions = new Unions();
            return Json(unions.GetUnions(filter), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnionAll()
        {
            Unions unions = new Unions();
            return Json(unions.GetUnionAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVillage(VillageFilter filter)
        {
            Village village = new Village();
            return Json(village.GetVillage(filter), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVillageAll()
        {
            Village village = new Village();
            return Json(village.GetVillageAll(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBlock(BlockRequestParams filter)
        {
            BlockInfo block = new BlockInfo();
            return Json(block.GetAllBlock(filter), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBlockAll()
        {
            BlockInfo block = new BlockInfo();
            return Json(block.GetAllBlock(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCenterAll()
        {
            CenterInfo res = new CenterInfo();
            return Json(res.GetAllCenter(), JsonRequestBehavior.AllowGet);
        }
    }
}