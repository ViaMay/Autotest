using System;
using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class DaemonTests : ConstVariablesTestBase
    {
        [Test, Description("Получить перечень городов по началу названия")]
        public void AutocompleteTest()
        {
//            Вводим название москва
            var responseCities = (ApiResponse.ResponseDeamonСities)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "autocomplete"},
                        {"q", "москва"}
                    });
            Assert.IsTrue(responseCities.Success);
//            получаем 8 мест
            Assert.AreEqual(responseCities.Options.Count(), 8);

//            ищем по id 151184 в этом списке москву
            var responseRowCity = FindRowById("151184", responseCities);
            Assert.AreEqual(responseRowCity.Area, "");
            Assert.AreEqual(responseRowCity.Name, "Москва");
            Assert.AreEqual(responseRowCity.NameIndex, "москва");
            Assert.AreEqual(responseRowCity.Importance, "100");
            Assert.AreEqual(responseRowCity.PostalCode, "101000");
            Assert.AreEqual(responseRowCity.Region, "г. Москва");
            Assert.AreEqual(responseRowCity.RegionId, "77");
            Assert.AreEqual(responseRowCity.Type, "г");
            Assert.AreEqual(responseRowCity.Kladr, "77000000000");

//            потом ищем деревню
            responseRowCity = FindRowById("127859", responseCities);
            Assert.AreEqual(responseRowCity.Area, "Пеновский");
            Assert.AreEqual(responseRowCity.Name, "Москва");
            Assert.AreEqual(responseRowCity.NameIndex, "москва");
            Assert.AreEqual(responseRowCity.Importance, "0");
            Assert.AreEqual(responseRowCity.PostalCode, "172796");
            Assert.AreEqual(responseRowCity.Region, "Тверская обл.");
            Assert.AreEqual(responseRowCity.RegionId, "69");
            Assert.AreEqual(responseRowCity.Type, "д");
            Assert.AreEqual(responseRowCity.Kladr, "69025000073");

//            вводим начало с Санкт-Петербург
            responseCities = (ApiResponse.ResponseDeamonСities)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "autocomplete"},
                        {"q", "Санкт-Петербург"}
                    });
            Assert.IsTrue(responseCities.Success);
//            получаем список из одного города
            Assert.AreEqual(responseCities.Options.Count(), 1);
//            проверяем что данные по Санкт-петербургу
            responseRowCity = responseCities.Options[0];
            Assert.AreEqual(responseRowCity.Id, "151185");
            Assert.AreEqual(responseRowCity.Area, "");
            Assert.AreEqual(responseRowCity.Name, "Санкт-Петербург");
            Assert.AreEqual(responseRowCity.NameIndex, "санкт-петербург");
            Assert.AreEqual(responseRowCity.Importance, "100");
            Assert.AreEqual(responseRowCity.PostalCode, "190000");
            Assert.AreEqual(responseRowCity.Region, "г. Санкт-Петербург");
            Assert.AreEqual(responseRowCity.RegionId, "78");
            Assert.AreEqual(responseRowCity.Type, "г");
            Assert.AreEqual(responseRowCity.Kladr, "78000000000");

//            вводим не существующее название qwe
            responseCities = (ApiResponse.ResponseDeamonСities)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "autocomplete"},
                        {"q", "qwe"}
                    });
            Assert.IsTrue(responseCities.Success);
//            проверяем что количество 0
            Assert.AreEqual(responseCities.Options.Count(), 0);

//            ничего не вовводим
            responseCities = (ApiResponse.ResponseDeamonСities)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "autocomplete"},
                        {"q", ""}
                    });
            Assert.IsTrue(responseCities.Success);
//            проверяем что количество 100
            Assert.AreEqual(responseCities.Options.Count(), 100);
        }

        [Test, Description("Получить город по ID (только RU)")]
        public void CityIdTest()
        {
//            вводим id Снегири
            var responseCity = (ApiResponse.ResponseDeamonСity)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "city"},
                        {"_id", "712"}
                    });
            Assert.IsTrue(responseCity.Success);
//            получаем данные по этому пункту
            var responseResult = responseCity.Result;
            Assert.AreEqual(responseResult.Id, "712");
            Assert.AreEqual(responseResult.Area, "Истринский");
            Assert.AreEqual(responseResult.Name, "Снегири");
            Assert.AreEqual(responseResult.NameIndex, "снегири");
            Assert.AreEqual(responseResult.Importance, "90");
            Assert.AreEqual(responseResult.PostalCode, "143590");
            Assert.AreEqual(responseResult.Region, "Московская обл.");
            Assert.AreEqual(responseResult.RegionId, "50");
            Assert.AreEqual(responseResult.Type, "пгт");
            Assert.AreEqual(responseResult.Kladr, "50009031000");

//            вводим не сущестующий Id
            responseCity = (ApiResponse.ResponseDeamonСity)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "city"},
                        {"_id", "qwe"}
                    });
            Assert.IsTrue(responseCity.Success);
//            получаем результат null
            Assert.AreEqual(responseCity.Result, null);

//            вводим пустой id
            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "city"},
                        {"_id", ""}
                    });
            Assert.IsFalse(responseFail.Success);
//            получаем ошибку
            Assert.AreEqual(responseFail.Error, "city _id not found");
        }

        [Test, Description("Получить город по IP (только RU)")]
        public void GeoIpTest()
        {
//            вводим ip москвы
            var responseCity = (ApiResponse.ResponseDeamonСity)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "geoip"},
                        {"ip", "195.239.0.254"}
                    });
            Assert.IsTrue(responseCity.Success);
//            проверчем что город нашелся москва
            var responseResult = responseCity.Result;
            Assert.AreEqual(responseResult.CityId, "151184");
            Assert.AreEqual(responseResult.Country, "RU");
            Assert.AreEqual(responseResult.Area, "");
            Assert.AreEqual(responseResult.City, "москва");
            Assert.AreEqual(responseResult.PostalCode, "101000");
            Assert.AreEqual(responseResult.Region, "Москва");
            Assert.AreEqual(responseResult.RegionId, "77");
            Assert.AreEqual(responseResult.Type, "г");
            Assert.AreEqual(responseResult.Kladr, "77000000000");

//            вводим ip Санкт-Петербурга
            responseCity = (ApiResponse.ResponseDeamonСity)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "geoip"},
                        {"ip", "195.131.50.254"}
                    });
//            проверяем что город нашелся 
            Assert.IsTrue(responseCity.Success);
            responseResult = responseCity.Result;
            Assert.AreEqual(responseResult.CityId, "151185");
            Assert.AreEqual(responseResult.Country, "RU");
            Assert.AreEqual(responseResult.Area, "");
            Assert.AreEqual(responseResult.City, "санкт-петербург");
            Assert.AreEqual(responseResult.PostalCode, "190000");
            Assert.AreEqual(responseResult.Region, "Санкт-Петербург");
            Assert.AreEqual(responseResult.RegionId, "78");
            Assert.AreEqual(responseResult.Type, "г");
            Assert.AreEqual(responseResult.Kladr, "78000000000");

//            ничего неуказываем - проверяем что что то нашлось
            responseCity = (ApiResponse.ResponseDeamonСity)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "geoip"},
                        {"ip", ""}
                    });
            Assert.IsTrue(responseCity.Success);
            Assert.AreEqual(responseResult.Country, "RU");

//            ничего неуказываем - проверяем что что то нашлось
            responseCity = (ApiResponse.ResponseDeamonСity)apiRequest.GET("daemon/",
       new NameValueCollection
                    {
                        {"_action", "geoip"}
                    });
            Assert.IsTrue(responseCity.Success);
            Assert.AreEqual(responseResult.Country, "RU");
        }

        [Test, Description("Получить список пунктов самовывоза")]
        public void DeliveryPointsTest()
        {
 //            запрашиваем список точек по Москве
            var responseDeliveryPoints = (ApiResponse.ResponseDeamonPoints)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "delivery_points"},
                        {"cities", "151184"},
                        {"companies", companyName},
                        {"short", ""}
                    });
            Assert.IsTrue(responseDeliveryPoints.Success);
//            проверяем что наша точка доставки есть в полученном списке
            var responseRowDeliveryPoint = FindRowByName(deliveryPointName, responseDeliveryPoints);
            Assert.AreEqual(responseRowDeliveryPoint.CityId, "151184");
            Assert.AreEqual(responseRowDeliveryPoint.City, "Москва");
            Assert.AreEqual(responseRowDeliveryPoint.CityType, "г");
            Assert.IsTrue(responseRowDeliveryPoint.HasFittingRoom);
            Assert.IsTrue(responseRowDeliveryPoint.IsCard);
            Assert.IsTrue(responseRowDeliveryPoint.IsCash);
            Assert.AreEqual(responseRowDeliveryPoint.Address, deliveryPointAddress);
            Assert.AreEqual(responseRowDeliveryPoint.Latitude, deliveryPointLatitude);
            Assert.AreEqual(responseRowDeliveryPoint.Longitude, deliveryPointLongitude);
            Assert.AreEqual(responseRowDeliveryPoint.Kladr, "77000000000");
            Assert.AreEqual(responseRowDeliveryPoint.RegionId, "77");
            Assert.AreEqual(responseRowDeliveryPoint.Status, "2");
            Assert.AreEqual(responseRowDeliveryPoint.Type, "2");

//            запрашиваем список точек по Санкт-Петербургу
            responseDeliveryPoints = (ApiResponse.ResponseDeamonPoints)apiRequest.GET("daemon/",
                   new NameValueCollection
                    {
                        {"_action", "delivery_points"},
                        {"cities", "151185"},
                        {"companies", companyName},
                        {"short", ""}
                    });
            Assert.IsTrue(responseDeliveryPoints.Success);

//           проверяем что нашей точки в этом списке нету
            for (var i = 0; i < responseDeliveryPoints.Points.Count(); i++)
            {
                if (responseDeliveryPoints.Points[i].Name == deliveryPointName)
                    throw new Exception(string.Format("Найдена точка доставки с именем {0} в списке всех точек доставки", deliveryPointName));
            }
        }

        public ApiResponse.OptionsPoints FindRowByName(string name, ApiResponse.ResponseDeamonPoints responseDeliveryPoints)
        {
            for (var i = 0; i < responseDeliveryPoints.Points.Count(); i++)
            {
                if (responseDeliveryPoints.Points[i].Name == name)
                    return responseDeliveryPoints.Points[i];
            }
            throw new Exception(string.Format("не найдена точка доставки с именем {0} в списке всех точек доставки", name));
        }
        public ApiResponse.OptionsCity FindRowById(string id, ApiResponse.ResponseDeamonСities responseDeamonСities)
        {
            for (var i = 0; i < responseDeamonСities.Options.Count(); i++)
            {
                if (responseDeamonСities.Options[i].Id == id)
                    return responseDeamonСities.Options[i];
            }
            throw new Exception(string.Format("не найден город с Id {0} в списке всех городов", id));
        }
    }
}