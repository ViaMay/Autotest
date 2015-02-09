using System;
using System.Threading;
using Autotests.WebPages.Pages;
using NUnit.Framework;

namespace Autotests.Tests
{
    public class ConstantVariablesTestBase : SimpleFunctionalTestBase
    {
//        protected override string ApplicationBaseUrl { get { return "dev.ddelivery.ru"; } }
        protected override string ApplicationBaseUrl { get { return "sprint.dev.ddelivery.ru"; } }

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
        }

        public string adminName;
        public string adminPass;

        public string userNameAndPass;
        public string userWarehouses;
        public string userShops;

        public string legalEntityName;
        public string сompanyName;
        public string deliveryPointName;


        public static void AsCertain(Action condition)
        {
            try
            {
                condition.Invoke();
            }
            catch (AssertionException assertionException)
            {
                Thread.CurrentThread.Abort();
            }
        }
    }
}