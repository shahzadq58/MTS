﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuctionInventory.Models
{
    public class Purchase
    {
        public long PurchaseID { get; set; }
        public Nullable<int> iPurchaseInvoiceNo { get; set; }

        [Required(ErrorMessage = " Please Select Invoice Date ")]
        public string strPurchaseInvoiceDate { get; set; }

        [Required(ErrorMessage = " Please Enter Supplier name ")]
        public string strSupplierName { get; set; }

        //[Required(ErrorMessage = " Please Enter Master Decleration Number ")]
        public string strMasterDecNo { get; set; }

       // [Required(ErrorMessage = " Please Enter BL Number ")]
        public string strBLNo { get; set; }

       // [Required(ErrorMessage = " Please Select Arrival Date ")]
        [DataType(DataType.Date)]
        public string strArrivalDate { get; set; }

        public string strInvoiceValue { get; set; }
        public Nullable<decimal> dmlConversionRate { get; set; }
        public Nullable<decimal> dcmlAED { get; set; }
        public Nullable<decimal> dcmlJYP { get; set; }

        public string strPurchaseInvoiceNo { get; set; }
        public Nullable<int> iSupplierID { get; set; }
        public Nullable<bool> IsStockReceived { get; set; }
        public Nullable<System.DateTime> dtPurchaseInvoiceDate { get; set; }
        public string strCustomerInvoiceNo { get; set; }
        public string strReferenceNumber { get; set; }
        public string strRemark { get; set; }
    }
}