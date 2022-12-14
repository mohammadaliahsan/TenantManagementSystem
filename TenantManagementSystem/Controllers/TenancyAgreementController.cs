using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TenantManagementSystem.BLL;
using TenantManagementSystem.Models;

namespace TenantManagementSystem.Controllers
{
    public class TenancyAgreementController : Controller
    {


        LesseeLessorManager aLesseeLessorManager = new LesseeLessorManager();
        BranchManager aBranchManager = new BranchManager();
        //BranchManager aCompanyManager = new CompanyManager();
        AreaManager aAreaManager = new AreaManager();
        CityManager aCityManager = new CityManager();
        PropertyManager aPropertyManager = new PropertyManager();
        TenancyAgreementManager aTenancyAgreementManager = new TenancyAgreementManager();
        CompanyManager aCompanyManager = new CompanyManager();
        ChequeDetailsManager aChequeDetailsManager = new ChequeDetailsManager();

        [HttpGet]
        public ActionResult ViewTenancyAgreement()
        {

            List<TenancyAgreement> tenancyAgreementList = aTenancyAgreementManager.GetAllTenancyAgreement();
            //PropertyList = PropertyList.Where(l => l.PropertyType != Convert.ToInt16(Property.PT.Flat)).ToList();
            ViewBag.Tenancy = tenancyAgreementList;
            //newly added
            ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor();
            ViewBag.building = aPropertyManager.GetAllProperty();
            ViewBag.Property = aPropertyManager.GetAllProperty();
            //ViewBag.Company = aCompanyManager.GetAllCompany();
            //ViewBag.Branch = aBranchManager.GetAllBranch();

            if (tenancyAgreementList == null)
                ViewBag.Message = "Something has gone ";

            return View(tenancyAgreementList);
        }

        [HttpPost]
        public JsonResult ViewRenewalTenancyAgreement(int? BuildingId, DateTime? StartDate, DateTime? EndDate)
        {
            List<TenancyAgreement> TenancyAgreementList = aTenancyAgreementManager.GetAllTenancyAgreementView();
            TenancyAgreementList = TenancyAgreementList.Where(t => (t.StartDate >= StartDate && t.EndDate <= EndDate) && t.BuildingId == BuildingId).ToList();

            //ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessor)).ToList();
            //ViewBag.building = aPropertyManager.GetAllProperty();
            ViewBag.Building = aPropertyManager.GetAllProperty().Where(l => l.PropertyType == Convert.ToInt16(Property.PT.Building)).ToList();
            //ViewBag.Property = aPropertyManager.GetAllProperty();
            //ViewBag.Company = aCompanyManager.GetAllCompany();
            //ViewBag.Branch = aBranchManager.GetAllBranch();


            return Json(TenancyAgreementList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ViewChequeDetailsByAgeementNo(string agreementNo)
        {

            List<ChequeDetails> chequeDetailsList = aChequeDetailsManager.GetAllChequeDetailsView();
            chequeDetailsList = chequeDetailsList.Where(t => t.TenantAgreementNumber == agreementNo).ToList();

            return Json(chequeDetailsList, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult ViewRenewalTenancyAgreement()
        {

            //List<ChequeDetails> chequeDetailsList = aChequeDetailsManager.GetAllChequeDetails();
            //PropertyList = PropertyList.Where(l => l.PropertyType != Convert.ToInt16(Property.PT.Flat)).ToList();
            //ViewBag.Tenancy = aTenancyAgreementManager.GetAllTenancyAgreement();
            //newly comment
            //ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor();
            ViewBag.Building = aPropertyManager.GetAllProperty().Where(l => l.PropertyType == Convert.ToInt16(Property.PT.Building)).ToList();
            //ViewBag.Flat = aPropertyManager.GetAllProperty();
            //ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessor)).ToList();
            //ViewBag.Company = aCompanyManager.GetAllCompany();
            //ViewBag.Branch = aBranchManager.GetAllBranch();


            return View();
        }

        [HttpPost]
        public ActionResult SaveTenancyAgreement(TenancyAgreement aTenancyAgreement)
        {
            //aTenancyAgreement.AgreementNumber = aNumberSequenceManager.GetNumberSequence("TNT");
            aTenancyAgreement.CreatedBy = Convert.ToInt16(Session["Id"]);
            aTenancyAgreement.CreatedDate = DateTime.Now;
            aTenancyAgreement.CompanyId = Convert.ToInt16(Session["CompanyId"]);
            aTenancyAgreement.BranchId = Convert.ToInt16(Session["BranchId"]);
            aTenancyAgreement.IsStarted = true;
            ViewBag.Building = aPropertyManager.GetAllProperty().Where(t => t.PropertyType == Convert.ToInt16(Property.PT.Building));
            ViewBag.Flat = aPropertyManager.GetAllPropertyUO().Where(t => t.PropertyType == Convert.ToInt16(Property.PT.Flat));

            ViewBag.Tenancy = aTenancyAgreementManager.GetAllTenancyAgreement();
            ViewBag.Message = aTenancyAgreementManager.Save(aTenancyAgreement);
            ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessor));
            ViewBag.Lessee = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessee));

            //ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.Id == Convert.ToInt16(LesseeLessor.RC.Lessor));
            aTenancyAgreement.CreatedBy = Convert.ToInt16(Session["Id"]);
            aTenancyAgreement.CreatedDate = DateTime.Now;

            return View(aTenancyAgreement);
        }

        //

        [HttpGet]
        public ActionResult SaveTenancyAgreement()
        {
            //ViewBag.Company = aCompanyManager.GetAllCompany();
            //ViewBag.Branch = aBranchManager.GetAllBranch();

            TenancyAgreement aTenancyAgreement = new TenancyAgreement();

            ViewBag.TenancyAgreement = aTenancyAgreementManager.GetAllTenancyAgreement();
            //ViewBag.Area = aAreaManager.GetAllArea();

            ViewBag.Building = aPropertyManager.GetAllProperty().Where(t => t.PropertyType == Convert.ToInt16(Property.PT.Building));
            ViewBag.Flat = aPropertyManager.GetAllPropertyUO().Where(t => t.PropertyType == Convert.ToInt16(Property.PT.Flat));

            ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessor));
            ViewBag.Lessee = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessee));
            //ViewBag.Branch = adeDepartmentManager.GetAllDepartments();
            //ViewBag.Designations = aDesignationManager.GetAllDesignations();



            return View(aTenancyAgreement);



        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            TenancyAgreement aTenancyAgreement = aTenancyAgreementManager.GetAllTenancyAgreementById(id);
            ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessor));
            ViewBag.Lessee = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessee));
            ViewBag.building = aPropertyManager.GetAllProperty();
            ViewBag.Property = aPropertyManager.GetAllProperty();
            ViewBag.Flat = aPropertyManager.GetAllProperty().Where(t => t.PropertyType == Convert.ToInt16(Property.PT.Flat));

            return View("Edit", aTenancyAgreement);
        }

        [HttpPost]
        public ActionResult Edit(int id, TenancyAgreement aTenancyAgreement)
        {
            //ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            ViewBag.TenancyAgreement = aTenancyAgreementManager.GetAllTenancyAgreement();
            // ViewBag.Designations = aDesignationManager.GetAllDesignations();
            //ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor();
            ViewBag.Lessor = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessor));
            ViewBag.Lessee = aLesseeLessorManager.GetAllLesseeLessor().Where(l => l.RecordType == Convert.ToInt16(LesseeLessor.RC.Lessee));
            ViewBag.building = aPropertyManager.GetAllProperty();
            ViewBag.Property = aPropertyManager.GetAllProperty();
            ViewBag.Flat = aPropertyManager.GetAllProperty().Where(t => t.PropertyType == Convert.ToInt16(Property.PT.Flat));

            //aTenancyAgreement.AgreementNumber = aNumberSequenceManager.GetNumberSequence("TNT");
            aTenancyAgreement.UpdatedBy = Convert.ToInt16(Session["Id"]);
            aTenancyAgreement.UpdatedDate = DateTime.Now;
            ViewBag.Message = aTenancyAgreementManager.Update(aTenancyAgreement);
            return View(aTenancyAgreement);
        }


        [HttpGet]
        public JsonResult IsAgreementExist(TenancyAgreement aTenancyAgreement)
        {
            bool isExist = false;

            try
            {
                isExist = aTenancyAgreementManager.GetAllTenancyAgreement().FirstOrDefault(l => l.AgreementNumber.ToUpper() == aTenancyAgreement.AgreementNumber.ToUpper()) != null;
            }
            catch (Exception ex)
            {
                isExist = false;
            }
            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]

        public JsonResult GetTenancyAgreementById(int TenancyAgreementId)
        {
            List<TenancyAgreement> tenancyList = aTenancyAgreementManager.GetAllTenancyAgreement();
            TenancyAgreement TenancyAgreement = tenancyList.FirstOrDefault(l => l.Id == TenancyAgreementId);
            return Json(TenancyAgreement);
        }
        [HttpGet]

        public JsonResult GetAllTenancyAgreement()
        {
            List<TenancyAgreement> tenancyList = aTenancyAgreementManager.GetAllTenancyAgreement();

            return Json(tenancyList);
        }


        [HttpPost]
        public JsonResult SelectPropertyByLessorId(int lessorId)
        {
            List<Property> propertyList = aPropertyManager.GetAllProperty().Where(t => t.LessorId == lessorId).ToList();
            return Json(propertyList);
        }

        [HttpPost]
        public JsonResult SelectFlatShopsByLessorId(int lessorId)
        {
            List<Property> propertyList = aPropertyManager.GetAllProperty().Where(t => t.LessorId == lessorId && (t.PropertyType == Convert.ToInt16(Property.PT.Flat) || t.PropertyType == Convert.ToInt16(Property.PT.Shop))).ToList();
            return Json(propertyList);
        }

        [HttpPost]
        public JsonResult SelectBuildingsByLessorId(int lessorId)
        {
            List<Property> propertyList = aPropertyManager.GetAllProperty().Where(t => t.LessorId == lessorId && t.PropertyType == Convert.ToInt16(Property.PT.Building)).ToList();
            return Json(propertyList);
        }


        [HttpPost]
        public JsonResult SelectFlatByBuildingId(int buildingId)
        {
            List<Property> propertyList = aPropertyManager.GetAllPropertyUO().Where(t => t.BuildingId == buildingId).ToList();


            return Json(propertyList);
        }


    }
}