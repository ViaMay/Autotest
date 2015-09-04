using System;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages;

namespace Autotests.WebPages
{
    public class ConstVariablesTestBase : SimpleFunctionalTestBase
    {
//     public override string ApplicationBaseUrl { get { return "dev:Grass$Clash8pelvis@dev.ddelivery.ru"; } }
        public override string ApplicationBaseUrl { get { return "stage.ddelivery.ru"; } }

//        public override string ApplicationBaseUrl { get { return "cabinet.ddelivery.ru"; } }

        public override void SetUp()
        {
            base.SetUp();
            
            adminName = "v.e@ddelivery.ru";
            adminPass = "LbREWCnpHa";

            legalEntityName = "test_legalEntity";
            legalUserName = "test_legalUser";
            legalPickupName = "test_legalPickup";
            companyName = "test_via";
            companyPickupName = "test_Pickup";
            deliveryPointName = "test_deliverypoint";

            userNameAndPass = "tester@user.ru";
            pickupNameAndPass = "tester@pickup.ru";
            userWarehouseName = "test_userWarehouses_via";
            userShopName = "test_userShops_via";

            weightName = "test_via_Weight";
            weightMin = 2;
            weightMax = 16;

            sideName = "test_via_Side";
            side1Min = 1;
            side2Min = 2;
            side3Min = 3;
            side1Max = 40;
            side2Max = 50;
            side3Max = 60;

            marginsPickup = 11;
            marginsCourirs = 12;
            marginsSelf = 13;

            deliveryPointAddress = "Ленинский проспект 127";
            deliveryPointLongitude = "37.477079";
            deliveryPointLatitude = "55.645873";
            
            deliveryPointAddress2 = "ул. Салова, 27Литер АД, пом. 35";
            deliveryPointLongitude2 = "30.372815";
            deliveryPointLatitude2 = "59.895735";

            apiRequest = new Api(ApplicationBaseUrl);
            TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
            DateTime moscowDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, moscowTimeZone);
            nowDate = moscowDateTime;
        }

        public string adminName;
        public string adminPass;

        public string userNameAndPass;
        public string pickupNameAndPass;
        public string userWarehouseName;
        public string userShopName;

        public string legalEntityName;
        public string legalUserName;
        public string legalPickupName;
        public string companyName;
        public string companyPickupName;
        public string deliveryPointName;

        public string weightName;
        public double weightMin;
        public double weightMax;

        public string sideName;
        public double side1Min;
        public double side2Min;
        public double side3Min;
        public double side1Max;
        public double side2Max;
        public double side3Max;

        public string deliveryPointAddress;
        public string deliveryPointLongitude;
        public string deliveryPointLatitude;
        public string deliveryPointAddress2;
        public string deliveryPointLongitude2;
        public string deliveryPointLatitude2;

        public int marginsPickup;
        public int marginsCourirs;
        public int marginsSelf;

        public Api apiRequest;
        public DateTime nowDate;
    }
}