using Autotests.WebPages.Pages;

namespace Autotests.Tests
{
    public class ConstVariablesTestBase : SimpleFunctionalTestBase
    {
        protected override string ApplicationBaseUrl { get { return "dev.ddelivery.ru"; } }
//        protected override string ApplicationBaseUrl { get { return "sprint.dev.ddelivery.ru"; } }

        public override void SetUp()
        {
            base.SetUp();

            adminName = "tester@ddelivery.ru";
            adminPass = "tester@ddelivery.ru";

            legalEntityName = "test_legalEntity";
            сompanyName = "test_via";
            deliveryPointName = "test_deliveryPoint";

            userNameAndPass = "tester@user.ru";
            userWarehouses = "test_userWarehouses_via";
            userShops = "test_userShops_via";

            weightName = "test_via_Weight";
            weightMin = 2;
            weightMax = 15;

            sideName = "test_via_Side";
            side1Min = 3.96;
            side2Min = 1.87;
            side3Min = 2.29;
            sidesSumMin = 17.50;
            volumeMin = 16.96;
            side1Max = 60.11;
            side2Max = 40.99;
            side3Max = 50.5;
            sidesSumMax = 100.6;
            volumeMax = 6783.63;
        }

        public string adminName;
        public string adminPass;

        public string userNameAndPass;
        public string userWarehouses;
        public string userShops;

        public string legalEntityName;
        public string сompanyName;
        public string deliveryPointName;

        public string weightName;
        public double weightMin;
        public double weightMax;

        public string sideName;
        public double side1Min;
        public double side2Min;
        public double side3Min;
        public double sidesSumMin;
        public double volumeMin;
        public double side1Max;
        public double side2Max;
        public double side3Max;
        public double sidesSumMax;
        public double volumeMax;
    }
}