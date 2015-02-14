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
            weightMin = "2";
            weightMax = "15";

            sizeName = "test_via_Side";
            side1Min = "3.96";
            side2Min = "1.87";
            side3Min = "2.29";
            sidesSumMin = "8.12";
            volumeMin = "16.96";
            side1Max = "30.11";
            side2Max = "10.99";
            side3Max = "20.5";
            sidesSumMax = "61.6";
            volumeMax = "6783.63";
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
        public string weightMin;
        public string weightMax;

        public string sizeName;
        public string side1Min;
        public string side2Min;
        public string side3Min;
        public string sidesSumMin;
        public string volumeMin;
        public string side1Max;
        public string side2Max;
        public string side3Max;
        public string sidesSumMax;
        public string volumeMax;
    }
}