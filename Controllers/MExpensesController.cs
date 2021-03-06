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
using System.Security.Cryptography;
using System.IO;
using System.Text;
namespace AuctionInventory.Controllers
{
    [Permissions(Permissions.View)]
    public class MExpensesController : Controller
    {
        AuctionInventoryEntities auctionContext = new AuctionInventoryEntities();
        // GET: MExpenses
        public ActionResult Index()
        {
            ViewBag.PageName = "Expense Master List";
            return View();
        }

        #region CRUD
        public ActionResult GetAllExpense()
        {
            //List<Expenses> expense = new List<Expenses>();
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        ExpensesServiceClient expensesServiceClient = new ExpensesServiceClient();
            //        expense = expensesServiceClient.GetAllExpenses();
            //        if (expense.Count == 0 || expense == null)
            //        {
            //            ModelState.AddModelError("error", "No Record Found");
            //        }


            //    }
            //}
            //catch (Exception e)
            //{
            //    ModelState.AddModelError("error", "Something Wrong");
            //    expense = null;
            //    throw e;

            //}

            //return Json(new { data = expense }, JsonRequestBehavior.AllowGet);

            dynamic expenses = 0;

            try
            {
                if (ModelState.IsValid)
                {
                   
                    Services.ExpensesServiceClient expenseServiceClient = new Services.ExpensesServiceClient();
                    expenses = expenseServiceClient.GetAllExpenses();
                    //if (currency.Count == 0 || currency == null)
                    //{
                    //    ModelState.AddModelError("error", "No Record Found");
                    //}

                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                expenses = null;
                throw e;

            }
            return Json(expenses, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Save(string ID)
        {
            int id = 0;
            if (ID != "0")//ID=0 for new record
            {
                id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
            }
            Expenses expense = new Expenses();
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();
                    expense = service.GetExpenses(id);
                    ViewBag.PageName = "Create Expense Master";
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "something went wrong");
                expense = null;
                throw e;
            }
            return View(expense);
        }


        [HttpPost]
        public ActionResult Save(Expenses expense)
        {
            bool status = false;
            try
            {
                //if (ModelState.IsValid)
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();
                    status = service.SaveData(expense);
                    //return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Went Wrong");
                status = false;
                throw e;


            }

           // return RedirectToAction("Index");
            return new JsonResult { Data = new { status = status } };
        }



        //[HttpPost]
        //public ActionResult SaveAllExpenses(MExpense[] expense)
        //{

        //    try
        //    {
        //        //if (ModelState.IsValid)
        //        if (ModelState.IsValid)
        //        {
        //            foreach (var item in expense)
        //            {

        //                auctionContext.MExpenses.Add(item);
        //            }

        //            auctionContext.SaveChanges();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("error", "Something Went Wrong");

        //        throw e;


        //    }

        //    return View();
        //    // return new JsonResult { Data = new { status = status } };
        //}



        //[HttpGet]
        //public ActionResult Delete(string ID)
        //{
        //    int id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
        //    Expenses expense = new Expenses();
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {

        //            ExpensesServiceClient service = new ExpensesServiceClient();
        //            expense = service.GetExpenses(id);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("error", "Something Went Wrong");
        //        expense = null;
        //        throw e;
        //    }
        //    return View(expense);
        //}

        [HttpPost]

        public ActionResult Delete(int ID)
        {
            //int id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {

                    ExpensesServiceClient service = new ExpensesServiceClient();
                    status = service.Delete(ID);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Went Wrong!");
                status = false;
                throw e;
            }
            return View("Index");
            //return new JsonResult { Data = new { status = status } };

        }

        #endregion


        public ActionResult AllVehicleExpenses()
        {
            ViewBag.PageName = "Create All Vehicle Expenses";
            return View();
        }

        [HttpPost]
        public ActionResult ViewRowDataOfAllVehicleExpense(int id)
        {
            dynamic vehicleList = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();
                    vehicleList = service.ViewRowDataOfAllVehicleExpense(id);
                }
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("error", "Something went Wrong");
                vehicleList = null;
                throw ex;

            }
            return Json(vehicleList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AllVehicleExpensesList()
        {
            ViewBag.PageName = "All Vehicle Expenses List";
            return View();
        }


        public ActionResult SingleVehicleExpensesList()
        {
            ViewBag.PageName = "Single Vehicle Expenses List";
            return View();
        }

        [HttpGet]
        public ActionResult GetAllVehicleExpensesListData()
        {
            dynamic listOfAllVehicleExpenses = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();
                    listOfAllVehicleExpenses = service.GetAllVehicleExpensesListData();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Something Wrong");
                listOfAllVehicleExpenses = null;
                throw ex;
            }
            return Json(listOfAllVehicleExpenses, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetSingleVehicleExpensesListData()
        {


            dynamic listOfSingleVehicleExpenses = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();
                    listOfSingleVehicleExpenses = service.GetSingleVehicleExpensesListData();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Something Wrong");
                listOfSingleVehicleExpenses = null;
                throw ex;
            }
            return Json(listOfSingleVehicleExpenses, JsonRequestBehavior.AllowGet);

        }

        //[HttpGet]
        //public ActionResult GetAllVehicleExpensesListData()
        //{

        //    List<VehicleExpens> test = new List<VehicleExpens>();

        //    var results = test.GroupBy(x => x.iPurchaseInvoiceID).Select(y => new { iPurchaseInvoiceID = y.Key, iExpenseAmount = y.Sum(x => x.iExpenseAmount) });

        //    return Json(results, JsonRequestBehavior.AllowGet);


        //    //var result = from c in auctionContext.VehicleExpenses
        //    //             group c by new
        //    //             {
        //    //                 c.iPurchaseInvoiceID,
        //    //                 //c.iExpenseAmount,
        //    //                 //c.iTotalExpenseAmounrt,
        //    //                 //c.iExpenseID,

        //    //             } into grp
        //    //             select grp.First().iPurchaseInvoiceID;

        //    //return Json(result, JsonRequestBehavior.AllowGet);


        //    //var result = test.GroupBy(g => new { g.iPurchaseInvoiceID })
        //    //            .Select(g => g.First())
        //    //            .ToList();

        //    //return Json(result, JsonRequestBehavior.AllowGet);

        //    //var results = from p in auctionContext.VehicleExpenses
        //    //              group p.iExpenseAmount by p.iPurchaseInvoiceID into g
        //    //              select new { iPurchaseInvoiceID = g.Key, ExpenseAmount = g.ToList() };

        //    //return Json(results, JsonRequestBehavior.AllowGet);



        //    //using (AuctionInventoryEntities dc = new AuctionInventoryEntities())
        //    //{
        //    //    var jsonData = new
        //    //    {
        //    //        total = 1,
        //    //        page = 1,
        //    //        records = dc.VehicleExpenses.ToList().Count,
        //    //        rows = (
        //    //          from vehi in
        //    //              (from AM in dc.VehicleExpenses
        //    //               where AM.iPurchaseInvoiceID != null && AM.iPurchaseInvoiceID != 0
        //    //               group AM.iExpenseAmount by AM.iPurchaseInvoiceID into g

        //    //               select new
        //    //               {
        //    //                   //iVehicleExpenseID = AM.iVehicleExpenseID,
        //    //                   //iPurchaseInvoiceID = AM.iPurchaseInvoiceID,
        //    //                   iPurchaseInvoiceID = g.Key,

        //    //                   //iExpenseID = AM.iExpenseID,
        //    //                   iExpenseAmount = g.ToList(),
        //    //                   //iExpenseAmount = AM.iExpenseAmount,
        //    //                   //iTotalExpenseAmounrt = AM.iTotalExpenseAmounrt,
        //    //                   //iSpreadAmountPerVehicle = AM.iTotalExpenseAmounrt

        //    //               }).ToList()
        //    //          select new
        //    //          {
        //    //              id = vehi.iVehicleExpenseID,
        //    //              cell = new string[] {
        //    //   Convert.ToString(vehi.iVehicleExpenseID),Convert.ToString(vehi.iPurchaseInvoiceID),Convert.ToString( vehi.iExpenseID),Convert.ToString( vehi.iExpenseAmount),Convert.ToString(vehi.iTotalExpenseAmounrt)
        //    //            }
        //    //          }).ToArray()
        //    //    };
        //    //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //    //}
        //    ////return View();
        //}


        [HttpPost]
        public ActionResult GetExpenseByInvoiceID(int id)
        {

            dynamic listExpense = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();

                    listExpense = service.GetExpenseByInvoiceID(id);

                    //return Json(new { listPurchase }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                listExpense = null;
                throw e;
            }

            return Json(listExpense, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult GetExpenseByVehicleID(int id)
        {


            dynamic listExpenses = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();

                    listExpenses = service.GetExpenseByVehicleID(id);

                    //return Json(new { listPurchase }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                listExpenses = null;
                throw e;
            }

            return Json(listExpenses, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetAllVehicleExpensesByInvoiceID(int id)
        {
            dynamic listOfAllVehicleExpenseByInvoiceID = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();
                    listOfAllVehicleExpenseByInvoiceID = service.GetAllVehicleExpensesByInvoiceID(id);

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", "Something Wrong");
                listOfAllVehicleExpenseByInvoiceID = null;
                throw ex;
            }
            return Json(listOfAllVehicleExpenseByInvoiceID, JsonRequestBehavior.AllowGet);

        }


        public ActionResult SingleVehicleExpenses()
        {
            //PartyModel partyModel = new PartyModel();

            //ViewBag.Party = new SelectList(auctionContext.MParties, "iPartyID", "strFirstName", partyModel.iPartyID);
            ViewBag.PageName = "Create Single Vehicle Expenses";
            return View();
        }

         [HttpGet]
        public ActionResult ShowPartyData()
        {
            var partyList = auctionContext.MParties.ToList();
            //var partyName = partyList.Select(a => a.strFirstName).ToList();
            return Json(partyList, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult VehiclesByBLNO(string request)
        {
            dynamic listPurchase = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();

                    listPurchase = service.VehiclesByBLNO(request);

                    //return Json(new { listPurchase }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                listPurchase = null;
                throw e;
            }
            return Json(new { listPurchase }, JsonRequestBehavior.AllowGet);


        }


        
             [HttpPost]
        public ActionResult ExpenseFilterByDate(DateTime fromDate,DateTime toDate)
        {
            dynamic listExpense = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();

                    listExpense = service.ExpenseFilterByDate(fromDate, toDate);

                    //return Json(new { listPurchase }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                listExpense = null;
                throw e;
            }
            return Json(new { listExpense }, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public JsonResult AutoCompleteExpense(string prefix)
        {
            dynamic expenses = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();

                    expenses = service.AutoCompleteExpense(prefix);

                    //return Json(new { expenses }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                expenses = null;
                throw e;
            }
            return Json(expenses, JsonRequestBehavior.AllowGet);




        }



        [HttpPost]
        public JsonResult VehiclesByVehicleID(int request)
        {
            dynamic VehiclesList = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();

                    VehiclesList = service.VehiclesByVehicleID(request);

                    //return Json(new { VehiclesList }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                VehiclesList = null;
                throw e;
            }
            return Json(new { VehiclesList }, JsonRequestBehavior.AllowGet);



        }



        [HttpPost]
        public JsonResult AutoCompleteVehicles(string prefix)
        {
            dynamic vehicles = 0;

            try
            {
                if (ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();

                    vehicles = service.AutoCompleteVehicles(prefix);

                    //return Json(new { vehicles }, JsonRequestBehavior.AllowGet);
                }
            }

            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Wrong");
                vehicles = null;
                throw e;
            }
            return Json(vehicles, JsonRequestBehavior.AllowGet);




        }



        [HttpPost]       
        public ActionResult SaveVehicleExpense(List<VehicleExpenseModel> expense, int id)
        {
            bool status = false;
            try
            {
                if (expense != null && ModelState.IsValid)
                {
                    ExpensesServiceClient service = new ExpensesServiceClient();
                    status = service.SaveDataVehicleExpense(expense, id);
                    //return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", "Something Went Wrong");
                status = false;
                throw e;

            }

            //return View();
            return new JsonResult { Data = new { status = status } };
        }


        [HttpPost]
        public ActionResult SpreadExpenseAmount(decimal totalAmount, int purchaseInvoiceID)
        {
            bool status = false;

                try
                {
                    if (ModelState.IsValid)
                    {
                        ExpensesServiceClient service = new ExpensesServiceClient();
                        status = service.SpreadExpenseAmount(totalAmount, purchaseInvoiceID);
                        //return RedirectToAction("Index");
                       
                    }
                }
                catch (Exception e)
                {
                   
                    ModelState.AddModelError("error", "Something Went Wrong");
                    status = false;
                    throw e;

                }
            
            //return View();
            return new JsonResult { Data = new { status = status } };
        }



        
             [HttpPost]
        public ActionResult UndoSpreadExpenseAmount(int purchaseInvoiceID, decimal spreadAmount)
        {
            bool status = false;

                try
                {
                    if (ModelState.IsValid)
                    {
                        ExpensesServiceClient service = new ExpensesServiceClient();
                        status = service.UndoSpreadExpenseAmount(purchaseInvoiceID, spreadAmount);
                        //return RedirectToAction("Index");
                       
                    }
                }
                catch (Exception e)
                {
                   
                    ModelState.AddModelError("error", "Something Went Wrong");
                    status = false;
                    throw e;

                }
            
            //return View();
            return new JsonResult { Data = new { status = status } };
        }

            

        //[HttpPost]
        //public ActionResult SaveSingleVehicleExpense(List<VehicleExpenseModel> expense)
        //{
        //    bool status = false;
        //    try
        //    {
        //        if (expense != null && ModelState.IsValid)
        //        {
        //            ExpensesServiceClient service = new ExpensesServiceClient();
        //            status = service.SaveDataSingleVehicleExpense(expense);
        //            //return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ModelState.AddModelError("error", "Something Went Wrong");
        //        status = false;
        //        throw e;


        //    }

        //    //return View();
        //    return new JsonResult { Data = new { status = status } };
        //}


             #region Lots
             public ActionResult ExpenseLotList()
             {
                 ViewBag.PageName = "Expense Lot List";
                 return View();
             }


             public ActionResult GetAllLots()
             {
                 dynamic lots = 0;

                 try
                 {
                     if (ModelState.IsValid)
                     {
                         ExpensesServiceClient expenseServiceClient = new ExpensesServiceClient();
                         lots = expenseServiceClient.GetAllLots();

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
             public ActionResult SaveExpenseLot(string ID)
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
                         ExpensesServiceClient expensesServiceClient = new ExpensesServiceClient();
                         lot = expensesServiceClient.GetLots(id);
                         ViewBag.PageName = "Create Expense Lot";
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
             public ActionResult SaveExpenseLot(Lots lot)
             {

                 bool status = false;
                 try
                 {
                     if (ModelState.IsValid)
                     {
                         ExpensesServiceClient expensesServiceClient = new ExpensesServiceClient();

                         status = expensesServiceClient.SaveExpenseLot(lot);
                         //return RedirectToAction("Index");
                     }
                 }
                 catch (Exception e)
                 {
                     ModelState.AddModelError("error", "Something Went Wrong");
                     status = false;
                     throw e;

                 }
                 return View("ExpenseLotList", lot);
                 //   return new JsonResult { Data = new { status = status } };
             }


             [HttpGet]
             public ActionResult DeleteExpenseLot(string ID)
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
                         ExpensesServiceClient expenseServiceClient = new ExpensesServiceClient();
                         lot = expenseServiceClient.GetLots(id);

                     }
                 }
                 catch (Exception e)
                 {
                     ModelState.AddModelError("error", "something went wrong");
                     lot = null;
                     throw e;
                 }
                 return View("DeleteExpenseLot", lot);
             }

             [HttpPost]
             [ActionName("DeleteExpenseLot")]
             public ActionResult DeleteLot(string ID)
             {
                 int id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
                 bool status = false;
                 try
                 {
                     if (ModelState.IsValid)
                     {
                         Services.ExpensesServiceClient expenseServiceClient = new Services.ExpensesServiceClient();
                         status = expenseServiceClient.DeleteExpenseLot(id);
                     }
                 }
                 catch (Exception e)
                 {
                     ModelState.AddModelError("error", "Something Went Wrong!");
                     status = false;
                     throw e;
                 }

                 return View("DeleteExpenseLot");
                 //return new JsonResult { Data = new { status = status } };
             }

             #endregion

             #region generalExpense
             public ActionResult GeneralExpenses()
             {
                 ViewBag.PageName = "Create General Expense";
                 return View();
             }

             public ActionResult GeneralExpensesList()
             {
                 ViewBag.PageName = "General Expense List";
                 return View();
             }

             [HttpPost]
             public JsonResult AutoCompleteGeneralExpense(string prefix)
             {
                 dynamic expenses = 0;
                 try
                 {
                     if (ModelState.IsValid)
                     {
                         ExpensesServiceClient service = new ExpensesServiceClient();

                         expenses = service.AutoCompleteGeneralExpense(prefix);

                         //return Json(new { expenses }, JsonRequestBehavior.AllowGet);
                     }
                 }

                 catch (Exception e)
                 {
                     ModelState.AddModelError("error", "Something Wrong");
                     expenses = null;
                     throw e;
                 }
                 return Json(expenses, JsonRequestBehavior.AllowGet);

             }

             [HttpGet]
             public ActionResult SaveGeneralExpense(string ID)
             {
                 int id = 0;
                 if (ID != "0")
                 {
                     id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
                 }
                 GeneralExpenses GeneralExpenses = new GeneralExpenses();
                 GeneralExpenses.iGeneralExpensesID = id;

                 try
                 {
                     if (ModelState.IsValid)
                     {
                         ExpensesServiceClient expensesServiceClient = new ExpensesServiceClient();
                         GeneralExpenses = expensesServiceClient.GetGeneralExpense(id);
                         ViewBag.PageName = "Create General Expense";
                     }
                 }
                 catch (Exception e)
                 {
                     ModelState.AddModelError("error", "something went wrong");
                     GeneralExpenses = null;
                     throw e;
                 }
                 return View(GeneralExpenses);
             }

             [HttpPost]
             public ActionResult SaveGeneralExpense(GeneralExpenses genexpense)
             {
                 //bool status = true;
                 string savedBillNum = string.Empty;
                 try
                 {
                     if (genexpense != null && ModelState.IsValid)
                     {
                         ExpensesServiceClient service = new ExpensesServiceClient();
                         savedBillNum = service.saveGeneralExpense(genexpense);
                         //return RedirectToAction("Index");
                     }
                 }
                 catch (Exception e)
                 {
                     ModelState.AddModelError("error", "Something Went Wrong");
                     // status = false;
                     throw e;

                 }

                 //return View();
                 return new JsonResult { Data = new { billNumber = savedBillNum } };
             }


             public ActionResult GetAllGeneralExpenses()
             {


                 dynamic expenses = 0;

                 try
                 {
                     if (ModelState.IsValid)
                     {

                         Services.ExpensesServiceClient expenseServiceClient = new Services.ExpensesServiceClient();
                         expenses = expenseServiceClient.GetAllGeneralExpenses();
                         //if (currency.Count == 0 || currency == null)
                         //{
                         //    ModelState.AddModelError("error", "No Record Found");
                         //}

                     }
                 }
                 catch (Exception e)
                 {
                     ModelState.AddModelError("error", "Something Wrong");
                     expenses = null;
                     throw e;

                 }
                 return Json(expenses, JsonRequestBehavior.AllowGet);

             }
             [HttpPost]
             //[ActionName("DeleteExpenseLot")]
             public ActionResult DeleteGeneralExpense(int id)
             {

                 bool status = false;
                 try
                 {
                     if (ModelState.IsValid)
                     {
                         Services.ExpensesServiceClient expenseServiceClient = new Services.ExpensesServiceClient();
                         status = expenseServiceClient.DeleteGeneralExpense(id);
                     }
                 }
                 catch (Exception e)
                 {
                     ModelState.AddModelError("error", "Something Went Wrong!");
                     status = false;
                     throw e;
                 }

                 return View("GeneralExpensesList");
                 //return new JsonResult { Data = new { status = status } };
             }
             #endregion


             #region DeleteVehicleExpenses
             public ActionResult DeleteVehicleExpense(int ExpenseID)
             {
                 //int id = Convert.ToInt32(Helpers.CommonMethods.Decrypt(HttpUtility.UrlDecode(ID)));
                 bool status = false;
                 try
                 {
                     if (ModelState.IsValid)
                     {

                         ExpensesServiceClient service = new ExpensesServiceClient();
                         status = service.DeleteVehicleExpense(ExpenseID);
                     }
                 }
                 catch (Exception e)
                 {
                     ModelState.AddModelError("error", "Something Went Wrong!");
                     status = false;
                     throw e;
                 }
                 return View("Index");
                 //return new JsonResult { Data = new { status = status } };

             }

             #endregion
    }
}