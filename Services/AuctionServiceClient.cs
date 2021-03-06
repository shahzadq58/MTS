﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuctionInventory.Models;
using AuctionInventoryDAL.Repositories;
using AuctionInventoryDAL.Entity;

namespace AuctionInventory.Services
{
    public class AuctionServiceClient
    {
        public bool SaveDataAuctionList(List<AuctionListModel> auction)
        {
            bool status = true;
            AuctionRepository repo = new AuctionRepository();
            status = repo.SaveRepoAuctionList(ParserAddAuctionList(auction));
            return status;
        }

        public dynamic GetAuctionListData()
        {
            //List<Vehicles> listVehicles = new List<Vehicles>();
            AuctionRepository repo = new AuctionRepository();
            dynamic vehicle = repo.GetAuctionListData();
            // listVehicles = ParserGetAllVehicles(vehicle);
            return vehicle;
        }

        public dynamic GetAuctionListVehicles()
        {
            //List<Vehicles> listVehicles = new List<Vehicles>();
            AuctionRepository repo = new AuctionRepository();
            dynamic vehicle = repo.GetRepoAuctionListVehicles();
            // listVehicles = ParserGetAllVehicles(vehicle);
            return vehicle;
        }



        public dynamic GetVehiclesForPDF(int id)
        {
            //List<Vehicles> listVehicles = new List<Vehicles>();
            AuctionRepository repo = new AuctionRepository();
            dynamic vehicle = repo.GetVehicleForAuctionListPDF(id);
            //listVehicles = ParserVehiclesForPDF(vehicle);
            return vehicle;

        }

        public dynamic AuctionFrontEndID()
        {

            AuctionRepository repo = new AuctionRepository();
            dynamic vehicle = repo.AuctionFrontEnd();
            //listVehicles = ParserVehiclesForPDF(vehicle);
            return vehicle;

        }

        public dynamic AuctionRemainingVehicleLists(DateTime AuctionDate)
        {
            dynamic auctionList = 0;
            AuctionRepository repo = new AuctionRepository();
            auctionList = repo.AuctionRemainingVehicleLists(AuctionDate);
            return auctionList;
        }

        public dynamic RecordCount()
        {
            dynamic count = 0;
            AuctionRepository repo = new AuctionRepository();
            count = repo.RecordCount();
            return count;
        }
        public dynamic GetAuctionListDataBYAuctionDate(DateTime date)
        {
            dynamic auctionList = 0;
            AuctionRepository repo = new AuctionRepository();
            auctionList = repo.GetAuctionListDataBYAuctionDate(date);
            return auctionList;
        }
        private List<Vehicles> ParserGetAllVehicles(dynamic responseData)
        {
            List<Vehicles> listVehicles = new List<Vehicles>();

            foreach (var data in responseData)
            {
                if (data != null)
                {
                    Vehicles vehicles = new Vehicles();

                    vehicles.iVehicleID = data.iVehicleID;
                    vehicles.iLotNum = data.iLotNum;
                    vehicles.strChassisNum = data.strChassisNum;
                    vehicles.iModel = data.iModel;
                    vehicles.iYear = data.iYear;
                    vehicles.strColor = data.strColor;
                    vehicles.iCustomValInJPY = data.iCustomValInJPY;
                    vehicles.iCustomAssesVal = data.iCustomAssesVal;

                    listVehicles.Add(vehicles);
                }
            }
            return listVehicles;
        }

        private List<AuctionList> ParserAddAuctionList(List<AuctionListModel> auction)
        {

            List<AuctionList> AllAuctionList = new List<AuctionList>();

            foreach (var item in auction)
            {


                if (item != null)
                {
                    AuctionList auctionList = new AuctionList();

                    auctionList.iAuctionListID = item.iAuctionListID;
                    auctionList.iVehicleID = item.iVehicleID;
                    auctionList.strAuctionDate = item.strAuctionDate;
                    auctionList.iAuctionFrontEndID = item.iAuctionFrontEndID;

                    auctionList.dtAuctionDate = item.dtAuctionDate;
                    auctionList.AuctionStatus = item.AuctionStatus;
                    
                    AllAuctionList.Add(auctionList);
                }
            }
            return AllAuctionList;
        }


        private List<Vehicle> ParserVehiclesForPDF(dynamic data)
        {
            List<Vehicle> eVehicleList = new List<Vehicle>();

            foreach (var dataList in data)
            {
                if (dataList != null)
                {
                    Vehicle vehicle = new Vehicle();
                    //vehicle.iVehicleID = dataList.iVehicleID;
                    vehicle.iLotNum = dataList.iLotNum;
                    vehicle.strChassisNum = dataList.strChassisNum;
                    vehicle.iModel = dataList.iModel;
                    vehicle.iYear = dataList.iYear;
                    vehicle.strColor = dataList.strColor;
                    vehicle.iCustomAssesVal = dataList.iCustomAssesVal;

                    // vehicle.iCustomValInJPY = dataList.iCustomValInJPY;
                    eVehicleList.Add(vehicle);
                }
            }

            return eVehicleList;
        }

        //Delete auction List
        public bool Delete(DateTime aucDate)
        {
            AuctionRepository repo = new AuctionRepository();
            return repo.Delete(aucDate);
        }

    }
}