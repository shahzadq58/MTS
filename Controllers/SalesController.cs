﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuctionInventoryDAL.Entity;
using AuctionInventoryDAL.Repositories;
using AuctionInventory.Services;
using AuctionInventory.Models;
using AuctionInventory.Helpers;
using AuctionInventory.MyRoleProvider;
namespace AuctionInventory.Controllers
{
    [Permissions(Permissions.View)]
    public class SalesController : Controller
    {
        private AuctionInventoryEntities auctionContext = new AuctionInventoryEntities();
        //
        // GET: /Sales/
        public ActionResult Index()
        {
            ViewBag.PageName = "Sales List";
            return View();
        }
       
        [HttpPost]
        public ActionResult DeleteSalesRecord(long iSaleID, int iSaleFrontEndID)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();

                    status = service.DeleteSalesRecord(iSaleID,iSaleFrontEndID);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Went Wrong!");
                status = false;
                throw e;
            }
            //return View("Index");
            return new JsonResult { Data = new { status = status } };

        }

        public ActionResult SalesPayment()
        {
            ViewBag.PageName = "Sales Payment List";
            return View();
        }

      
        [HttpPost]
        public ActionResult UpdateOnlySalesPayment(SalesPaymentModel salesPayment)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient saleServiceClient = new SaleServiceClient();
                    status = saleServiceClient.UpdateOnlySalesPayment(salesPayment);

                }
                // return RedirectToAction("GetPurchaseList", "TPurchase");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Went Wrong");
                status = false;
                throw e;
            }

            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            //return new JsonResult { Data = new { status = status ,purID=purchase.PurchaseID} };

        }


        

        [HttpPost]
        public ActionResult DeletePreviousSalesPayment(long id)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();

                    status = service.DeletePreviousSalesPayment(id);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Went Wrong!");
                status = false;
                throw e;
            }
            //return View("Index");
            return new JsonResult { Data = new { status = status } };

        }

        [HttpGet]
        public ActionResult ShowYardName()
        {
            //var yardList = (from t1 in auctionContext.MYards
                            
            //                select new
            //                {
                               
            //                    iYardID = t1.iYardID,
            //                    strYardName = t1.strYardName,
            //                    iYardLimit = t1.iYardLimit,
            //                }).OrderBy(x=>x.strYardName).ToList();
            var SalesyardList = (from t1 in auctionContext.MYards
                                 from t2 in auctionContext.Sales.Where(t2 => (long?)t2.iYardID == t1.iYardID).DefaultIfEmpty()
                                 //join t2 in auctionContext.Sales on t1.iYardID equals (long?)t2.iYardID
                                 orderby t1.iYardLimit descending
                                 select new
                                 {
                                     InSaleiYardID = t2.iYardID,
                                     iYardID = t1.iYardID,
                                     strYardName = t1.strYardName,
                                     iYardLimit = t1.iYardLimit,
                                 }).ToList();
            var yardListCount = SalesyardList.GroupBy(a => a.strYardName).Select(y =>
                new
                {
                    strYardName = y.Key,
                    iYardID = y.First().iYardID,
                    InSaleiYardID = y.First().InSaleiYardID,
                    iYardLimit = y.First().iYardLimit,
                    CountYardID = y.Count(),
                }).Distinct().ToList();
            return Json(yardListCount, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public ActionResult GetAllSalesPaymentList()
        {
            dynamic salesPaymentList = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    salesPaymentList = service.GetAllSalesPaymentList();
                    //return Json(vehicleList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                salesPaymentList = null;
                throw ex;
            }
            return Json(salesPaymentList, JsonRequestBehavior.AllowGet);


        }
        

             [HttpPost]
        public ActionResult GetPreviousPaymentInfo(int id)
        {
            dynamic salesPaymentList = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    salesPaymentList = service.GetPreviousPaymentInfo(id);
                    //return Json(vehicleList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                salesPaymentList = null;
                throw ex;
            }
            return Json(salesPaymentList, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public ActionResult GetAllPreviousSalesPaymentList(int id)
        {
            dynamic salesPaymentList = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    salesPaymentList = service.GetAllPreviousSalesPaymentList(id);
                    //return Json(vehicleList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                salesPaymentList = null;
                throw ex;
            }
            return Json(salesPaymentList, JsonRequestBehavior.AllowGet);


        }

        public ActionResult SaleVehicle()
        {
            ViewBag.PageName = " Create Sale";
            return View();
        }

        public ActionResult UpdateSaleVehicle(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetVehiclesDataBySalesDate(DateTime salesDate)
        {
            dynamic vehicleList = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    vehicleList = service.GetVehiclesDataBySalesDate(salesDate);
                    //return Json(vehicleList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                vehicleList = null;
                throw ex;
            }
            return Json(vehicleList, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public ActionResult GetSaleVehicleBySalesFrntID(int id)
        {
            AuctionInventoryEntities dc = new AuctionInventoryEntities();
            //List<Vehicle> listVehicle = (from t1 in auctionContext.Vehicles
            var listVehicle = (
                from t1 in dc.SalesVehicles
                join t2 in dc.Vehicles on t1.iVehicleID equals t2.iVehicleID
                join t3 in dc.TPurchases on t2.PurchaseID equals t3.PurchaseID
                where t1.iSaleFrontEndID == id


                select new
                {

                    iSalesVehicleID = t1.iSalesVehicleID,
                    iVehicleID = t2.iVehicleID,
                    iLotNum = t2.iLotNum,
                    strChassisNum = t2.strChassisNum,
                    strMake = t2.strMake,
                    iModel = t2.iModel,
                    //strCategory = t2.strCategory,
                    iYear = t2.iYear,
                    strColor = t2.strColor,
                    // strOrigin = t2.strOrigin,
                    //strLocation = t2.strLocation,
                    //iCustomAssesVal = t2.iCustomAssesVal,
                    //iDuty = t2.iDuty,
                    iCustomValInJPY = t2.iCustomValInJPY + t2.dcmlExpenseAmount,
                    //,strGrade =t1.strGrade,                                             

                    DHM = ((t2.iCustomValInJPY != null ? t2.iCustomValInJPY : 0) + (t2.dcmlExpenseAmount != null ? t2.dcmlExpenseAmount : 0)) * (t3.dmlConversionRate != null ? t3.dmlConversionRate : 0),
                                     
                    //dmlKM = t1.dmlKM,

                    //iDoor = t1.iDoor,

                    //weight = t1.weight,
                    //strHSCode = t1.strHSCode,
                    //ATMT = t1.ATMT,



                }).ToList();


            return Json(new { listVehicle }, JsonRequestBehavior.AllowGet);
        }


        //  [HttpPost]
        //public ActionResult GetSaleVehicleBySalesFrntID(int id)
        //{
        //    //AuctionInventoryEntities dc = new AuctionInventoryEntities();
        //    //var rows = (from AM in dc.SalesVehicles
        //    //            where (AM.iSaleFrontEndID == id)
        //    //            select new
        //    //            {

        //    //                iVehicleID = AM.iVehicleID
        //    //            }).ToList();
        //    //return Json(rows, JsonRequestBehavior.AllowGet);






        //    using (AuctionInventoryEntities dc = new AuctionInventoryEntities())
        //    {
        //        var jsonData = new
        //        {
        //            total = 1,
        //            page = 1,
        //            records = dc.Vehicles.ToList().Count,
        //            rows = (
        //              from vehi in
        //                  (from t1 in dc.SalesVehicles
        //                   join t2 in dc.Vehicles on t1.iVehicleID equals t2.iVehicleID 
        //                   where t1.iSaleFrontEndID == id

        //                   select new
        //                   {
        //                       iVehicleID = t2.iVehicleID,
        //                       iLotNum = t2.iLotNum,
        //                       strChassisNum = t2.strChassisNum,
        //                       iModel = t2.iModel,
        //                       iYear = t2.iYear,
        //                       color = t2.strColor,
        //                       iCustomValInJPY = t2.iCustomValInJPY
        //                       //,iCustomAssesVal = t2.iCustomAssesVal

        //                   }).ToList()
        //              select new
        //              {
        //                  id = vehi.iVehicleID,
        //                  cell = new string[] {
        //       Convert.ToString(vehi.iVehicleID),Convert.ToString(vehi.iLotNum),Convert.ToString( vehi.strChassisNum),Convert.ToString(vehi.iModel),Convert.ToString( vehi.iYear),Convert.ToString(vehi.color),Convert.ToString( vehi.iCustomValInJPY)
        //                }
        //              }).ToArray()
        //        };
        //        return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    }
        //    //return View();


        //}


        [HttpGet]
        public ActionResult GetSalesData()
        {
            dynamic salesList = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    salesList = service.GetSalesData();
                    //return Json(vehicleList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                salesList = null;
                throw ex;
            }
            return Json(salesList, JsonRequestBehavior.AllowGet);


        }

        

             [HttpPost]
        public JsonResult GetCustomerDetailsForSalesReports(string prefix)
        {
            dynamic customers = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    customers = service.GetCustomerDetailsForSalesReports(prefix);
                    //return Json(customers, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                customers = null;
                throw ex;
            }
            return Json(customers, JsonRequestBehavior.AllowGet);

        }
        


        [HttpPost]
        public JsonResult GetCustomerDetails(string prefix)
        {
            dynamic customers = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    customers = service.GetCustomerDetails(prefix);
                    //return Json(customers, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                customers = null;
                throw ex;
            }
            return Json(customers, JsonRequestBehavior.AllowGet);

        }
        

             [HttpPost]
        public JsonResult GetShowRoomCustomerDetails(string prefix)
        {
            dynamic customers = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    customers = service.GetShowRoomCustomerDetails(prefix);
                    //return Json(customers, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                customers = null;
                throw ex;
            }
            return Json(customers, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetCustomerDetailsBYCustomerID(int id)
        {
            AuctionInventoryEntities dc = new AuctionInventoryEntities();

            var customer = (
              from t1 in dc.MCustomers
              where t1.iCustomerID == id


              select new
              {

                  //iCustomerID = t1.iCustomerID,
                  //strFirstName = t1.strFirstName,
                  //strMiddleName = t1.strMiddleName,
                  //strLastName = t1.strLastName,
                  fltPhoneNumber = t1.fltPhoneNumber,
                  strCreditLimit = t1.strCreditLimit,
                  //Address = t1.Address


              }).ToList();


            return Json(customer, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult GetInvoice()
        {
            dynamic invNo = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    invNo = service.GetInvoice();
                    //return Json(invNo, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                invNo = null;
                throw ex;
            }
            return Json(invNo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetReceiptNo()
        {
            dynamic receiptNo = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    receiptNo = service.GetReceiptNo();
                    //return Json(invNo, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                receiptNo = null;
                throw ex;
            }
            return Json(receiptNo, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetSalesFrontEndID()
        {
            dynamic SalesFrontEndID = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    SalesFrontEndID = service.GetSalesFrontEndID();
                    //return Json(SalesFrontEndID, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                SalesFrontEndID = null;
                throw ex;
            }
            return Json(SalesFrontEndID, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Save(SaleModel sale, List<SalesVehicleModel> saleVehicles, SalesPaymentModel salesPaymentModel)
        {
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient saleServiceClient = new SaleServiceClient();
                    status = saleServiceClient.SaveSalesData(sale, saleVehicles, salesPaymentModel);

                }
                // return RedirectToAction("GetPurchaseList", "TPurchase");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Went Wrong");
                status = false;
                throw e;
            }

            return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            //return new JsonResult { Data = new { status = status ,purID=purchase.PurchaseID} };

        }

        public ActionResult SalesReport()
        {
            return View();
        }

           [HttpPost]
        public JsonResult GetAllSalesReportByDate(DateTime fromDate, DateTime toDate, int customerID)
        {
            dynamic salesReportByDate = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient saleServiceClient = new SaleServiceClient();
                    salesReportByDate = saleServiceClient.GetAllSalesReportByDate(fromDate, toDate, customerID);


                    //if (purchase.Count == 0 || purchase == null)
                    //{
                    //    ModelState.AddModelError("error", "No Record Found");
                    //}
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                salesReportByDate = null;
                throw e;
            }

            //return Json(purchaseReportByDate, JsonRequestBehavior.AllowGet);
            return Json(new { salesReportByDate }, JsonRequestBehavior.AllowGet);

        }

        //[HttpPost]
        //public JsonResult GetAllSalesReportByDate(DateTime fromDate, DateTime toDate)
        //{
        //    dynamic salesReportByDate = 0;

        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            SaleServiceClient saleServiceClient = new SaleServiceClient();
        //            salesReportByDate = saleServiceClient.GetAllSalesReportByDate(fromDate, toDate);


        //            //if (purchase.Count == 0 || purchase == null)
        //            //{
        //            //    ModelState.AddModelError("error", "No Record Found");
        //            //}
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("error", "Something Wrong");
        //        salesReportByDate = null;
        //        throw e;
        //    }

        //    //return Json(purchaseReportByDate, JsonRequestBehavior.AllowGet);
        //    return Json(new { salesReportByDate }, JsonRequestBehavior.AllowGet);

        //}

        //[HttpPost]       
        //public ActionResult Delete(int id)
        //{
        //    bool status = false;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            SaleServiceClient saleServiceClient = new SaleServiceClient();
        //            status = saleServiceClient.Delete(id);
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("error", "Something Went Wrong!");
        //        status = false;
        //        throw e;
        //    }

        //    return new JsonResult { Data = new { status = status } };

        //}


        [HttpGet]
        public JsonResult CheckCustomerIsBlockOrNot()
        {
            bool status = false;

            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient saleServiceClient = new SaleServiceClient();
                    status = saleServiceClient.CheckCustomerIsBlockOrNot();


                    //if (purchase.Count == 0 || purchase == null)
                    //{
                    //    ModelState.AddModelError("error", "No Record Found");
                    //}
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                status = false;
                throw e;
            }

            //return Json(purchaseReportByDate, JsonRequestBehavior.AllowGet);
            return Json(new { status = true }, JsonRequestBehavior.AllowGet);

        }


        #region Sales Lot

        //public ActionResult SaveSalesLot()
        //{
        //    //int id = 0;
        //    //if (ID != "0")
        //    //{
        //    //    id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
        //    //}
        //    //Lots lot = new Lots();

        //    //// ViewBag.category = new SelectList(db.MCategories, "iCategoryID", "strCategoryName", supplier.iSupplierCategory);
        //    ////ViewBag.currency = new SelectList(db.MCurrencies, "CurrencyID", "strCurrencyName", supplier.iCurrency);

        //    //try
        //    //{
        //    //    if (ModelState.IsValid)
        //    //    {

        //    //        Services.SupplierServiceClient supplierServiceClient = new Services.SupplierServiceClient();
        //    //        supplier = supplierServiceClient.GetSupplier(id);

        //    //    }
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    ModelState.AddModelError("error", "something went wrong");
        //    //    supplier = null;
        //    //    throw e;
        //    //}

        //    //// return View()
        //    //return View(supplier);
        //    //// return View(supplier);
        //    return View();
        //}
        public ActionResult SalesLotList()
        {
            ViewBag.PageName = "Sales Lot List";
            return View();
        }
        public ActionResult GetAllLots()
        {
            dynamic lots = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient saleServiceClient = new SaleServiceClient();
                    lots = saleServiceClient.GetAllLots();

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                lots = null;
                throw e;

            }
            return Json(lots, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult SaveSalesLot(string ID)
        {
            int id = 0;
            if (ID != "0")
            {
                id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
            }
            Lots lot = new Lots();
            lot.iLotID = id;

            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient saleServiceClient = new SaleServiceClient();
                    lot = saleServiceClient.GetLots(id);
                    ViewBag.PageName = "Create Sales Lot";
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "something went wrong");
                lot = null;
                throw e;
            }
            return View(lot);
        }

        [HttpPost]
        public ActionResult SaveSalesLot(Lots lot)
        {

            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient saleServiceClient = new SaleServiceClient();

                    status = saleServiceClient.SaveSlesLot(lot);
                    //return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Went Wrong");
                status = false;
                throw e;

            }
            return View("SalesLotList", lot);
            //   return new JsonResult { Data = new { status = status } };
        }


        [HttpGet]
        public ActionResult DeleteSalesLot(string ID)
        {
            int id = 0;
            if (ID != "0")
            {
                id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
            }
            Lots lot = new Lots();
            lot.iLotID = id;

            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient saleServiceClient = new SaleServiceClient();
                    lot = saleServiceClient.GetLots(id);

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "something went wrong");
                lot = null;
                throw e;
            }
            return View("DeleteSalesLot", lot);
        }

        [HttpPost]
        [ActionName("DeleteSalesLot")]
        public ActionResult DeleteLot(string ID)
        {
            int id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    Services.SaleServiceClient salesServiceClient = new Services.SaleServiceClient();
                    status = salesServiceClient.DeleteSalesLot(id);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Went Wrong!");
                status = false;
                throw e;
            }

            return View("SalesLotList");
            //return new JsonResult { Data = new { status = status } };
        }

        #endregion


        #region EditSales
        public ActionResult editSales()
        {
            ViewBag.PageName = "Edit Sales";
            return View();
        }

        [HttpGet]
        public ActionResult GetAllSalesData()
        {
            dynamic salesList = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    SaleServiceClient service = new SaleServiceClient();
                    salesList = service.GetAllSalesData();
                    //return Json(vehicleList, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something Went Wrong");
                salesList = null;
                throw ex;
            }
            return Json(salesList, JsonRequestBehavior.AllowGet);


        }
        #endregion

    }
}