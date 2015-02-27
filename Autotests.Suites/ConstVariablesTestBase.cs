using Autotests.WebPages.Pages;

namespace Autotests.Tests
{
    public class ConstVariablesTestBase : SimpleFunctionalTestBase
    {
//        protected override string ApplicationBaseUrl { get { return "dev.ddelivery.ru"; } }
        protected override string ApplicationBaseUrl { get { return "sprint.dev.ddelivery.ru"; } }

        public override void SetUp()
        {
            base.SetUp();

            adminName = "tester@ddelivery.ru";
            adminPass = "tester@ddelivery.ru";

            legalEntityName = "test_legalEntity";
            companyName = "test_via";
            deliveryPointName = "test_deliverypoint";

            userNameAndPass = "tester@user.ru";
            userWarehouses = "test_userWarehouses_via";
            userShops = "test_userShops_via";

            weightName = "test_via_Weight";
            weightMin = 2;
            weightMax = 15;

            sideName = "test_via_Side";
            side1Min = 1;
            side2Min = 2;
            side3Min = 3;
            side1Max = 40;
            side2Max = 50;
            side3Max = 60;

            deliveryPointAddress = "Ленинский проспект 127";
            deliveryPointLongitude = "37.477078814299";
            deliveryPointLatitude = "55.645872547535";
        }

        public string adminName;
        public string adminPass;

        public string userNameAndPass;
        public string userWarehouses;
        public string userShops;

        public string legalEntityName;
        public string companyName;
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
    }
}