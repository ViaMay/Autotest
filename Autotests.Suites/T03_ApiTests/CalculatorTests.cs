using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.T03_ApiTests
{
    public class CalculatorTests : ConstVariablesTestBase
    {
        [Test, Description("Расчитать цену самовывоза")]
        public void  CalculatorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var shopsPage = LoadPage<ShopsPage>("/admin/shops/?&filters[name]=" + userShopName);
            var keyShopPublic = shopsPage.Table.GetRow(0).KeyPublic.GetText();

            var deliveryPointsPage = LoadPage<DeliveryPointsPage>("/admin/deliverypoints/?&filters[name]=" + deliveryPointName);
            var deliveriPoinId = deliveryPointsPage.Table.GetRow(0).ID.GetText();
            
            var responseCalculator = (Api.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json", 
                new NameValueCollection
                {
                    {"type", "1"},
                    {"city_to", ""},
                    {"delivery_point", deliveriPoinId},
                    {"dimension_side1", "4"},
                    {"dimension_side2", "4"},
                    {"dimension_side3", "4"},
                    {"weight", "4"},
                    {"declared_price", "1000"},
                    {"payment_price", "1000"}
                });

            Assert.AreEqual(responseCalculator.MessageCalculation[0].DeliveryCompanyName, companyName);

            //            responseCalculator.MessageCalculation[0].DeliveryCompanyName = 
            var responseCalculator2 = apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
    new NameValueCollection
                {
                    {"name", userWarehouseName + "_Api"},
                    {"flat", "138"},
                    {"city", "416"},
                    {"contact_person", "contact_person"},
                    {"contact_phone", "contact_phone"},
                    {"contact_email", userNameAndPass},
                    {"schedule", "schedule"},
                    {"street", "street"},
                    {"house", "house"}
                });
//            var responseCalculator2 = apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json", d);
//            obj = new JSONObject(response);
//            param = obj.getBoolean("success");
//            if (param == true)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по пункут выдачи)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/17e16dea8853a5519a77825cb492876b/calculator.json"+vars + "\n" + "\n");
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по пункут выдачи)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//            vars = "?type=1&city_to=151185&delivery_point=&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == true)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по городу доставки)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/17e16dea8853a5519a77825cb492876b/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по городу доставки)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//
//            vars = "?type=1&city_to=151185&delivery_point=" + autotest_dev_point + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + "1234567890" + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == false)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка api-key)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка api-key)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + 1234567890 + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//            vars = "?type=1&city_to=Оклахома&delivery_point=" + autotest_dev_point + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == false)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка to_city)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка to_city)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//            vars = "?type=1&city_to=151185&delivery_point=" + autotest_dev_point + "&dimension_side1=0&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == false)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (валидация сторон)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (валидация сотрон)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//            /* 
//             vars = "?type=1&city_to=151185&delivery_point="+autotest_dev_point+"&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//             response = GET(baseUrl+":80/api/v1/"+magazine1_code+"/calculator.json"+vars, "Calculator.json");
//             obj = new JSONObject(response);  param = obj.getBoolean("success");
//             if (param == false) {
//                 logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка диапазона сторон из справочника)" + "\n");
//                 logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                 //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//	        	
//             } else {
//                 logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка диапазона сотрон из справочника)" + "\n");
//                 logger.info("Результат: Ошибка!" + "\n" + response);
//                 logger.info("Запрос: " + baseUrl+":80/api/v1/"+magazine1_code+"/calculator.json"+vars + "\n" + "\n");
//	        	
//             }
//             */
//
//            vars = "?type=1&city_to=151185&delivery_point=" + autotest_dev_point + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == false)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка declared_price)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка declared_price)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//
//            vars = "?type=1&city_to=151185&delivery_point=" + autotest_dev_point + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=Тисча";
//            response = GET(baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == true)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка payment_price)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка payment_price)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine1_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//            /*
//            vars = "?type=1&city_to=151185&delivery_point="+autotest_dev_point+"&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=&payment_price=1000";
//            response = GET(baseUrl+":80/api/v1/"+magazine1_code+"/calculator.json"+vars, "Calculator.json");
//            obj = new JSONObject(response);  param = obj.getBoolean("success");
//            if (param == false) {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка weight из справочника)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//	        	
//            } else {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка weight  из справочника)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl+":80/api/v1/"+magazine1_code+"/calculator.json"+vars + "\n" + "\n");
//	        	
//            }
//	        
//            */
//
//            //=============================== Рассчет самовывоза по другому магазину ======================================================= 
//
//
//            vars = "?type=1&city_to=&delivery_point=" + autotest_dev_point_2 + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == true)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по пункут выдачи)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/17e16dea8853a5519a77825cb492876b/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по пункут выдачи)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//            vars = "?type=1&city_to=151185&delivery_point=&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == true)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по городу доставки)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/17e16dea8853a5519a77825cb492876b/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по городу доставки)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//
//            vars = "?type=1&city_to=151185&delivery_point=" + autotest_dev_point_2 + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + "1234567890" + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == false)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка api-key)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка api-key)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + 1234567890 + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//            vars = "?type=1&city_to=Оклахома&delivery_point=" + autotest_dev_point_2 + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == false)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка to_city)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка to_city)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//            vars = "?type=1&city_to=151185&delivery_point=" + autotest_dev_point_2 + "&dimension_side1=0&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == false)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (валидация сторон)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (валидация сотрон)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//            /*
//            vars = "?type=1&city_to=151185&delivery_point="+autotest_dev_point_2+"&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl+":80/api/v1/"+magazine2_code+"/calculator.json"+vars, "Calculator.json");
//            obj = new JSONObject(response);  param = obj.getBoolean("success");
//            if (param == false) {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка диапазона сторон из справочника)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//	        	
//            } else {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка диапазона сотрон из справочника)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl+":80/api/v1/"+magazine2_code+"/calculator.json"+vars + "\n" + "\n");
//	        	
//            }
//            */
//
//            vars = "?type=1&city_to=151185&delivery_point=" + autotest_dev_point_2 + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == false)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка declared_price)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка declared_price)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//
//            vars = "?type=1&city_to=151185&delivery_point=" + autotest_dev_point_2 + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=Тисча";
//            response = GET(baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == true)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка payment_price)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка payment_price)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
//
//            /*    vars = "?type=1&city_to=151185&delivery_point="+autotest_dev_point_2+"&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=&payment_price=1000";
//                response = GET(baseUrl+":80/api/v1/"+magazine2_code+"/calculator.json"+vars, "Calculator.json");
//                obj = new JSONObject(response);  param = obj.getBoolean("success");
//                if (param == false) {
//                    logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка weight из справочника)" + "\n");
//                    logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                    //logger.info("Запрос: " + baseUrl+":80/api/v1/"+1234567890+"/calculator.json"+vars + "\n" + "\n");
//	        	
//                } else {
//                    logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (проверка weight  из справочника)" + "\n");
//                    logger.info("Результат: Ошибка!" + "\n" + response);
//                    logger.info("Запрос: " + baseUrl+":80/api/v1/"+magazine2_code+"/calculator.json"+vars + "\n" + "\n");
//	        	
//                }
//	                
//                */
//
//
//            vars = "?type=1&city_to=&delivery_point=" + autotest_dev_point_2 + "&dimension_side1=1&dimension_side2=1&dimension_side3=1&weight=1&declared_price=1000&payment_price=1000";
//            response = GET(baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars, "Calculator.json");
//            obj = new JSONObject(response); param = obj.getBoolean("success");
//            if (param == true)
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по пункут выдачи)" + "\n");
//                logger.info("Результат: Тест кейс прошел успешно." + "\n");
//                //logger.info("Запрос: " + baseUrl+":80/api/v1/17e16dea8853a5519a77825cb492876b/calculator.json"+vars + "\n" + "\n");
//
//            }
//            else
//            {
//                logger.info("Метод Calculator.json. Тест кейс Расчитать цену самомвывоза (по пункут выдачи)" + "\n");
//                logger.info("Результат: Ошибка!" + "\n" + response);
//                logger.info("Запрос: " + baseUrl + ":80/api/v1/" + magazine2_code + "/calculator.json" + vars + "\n" + "\n");
//
//            }
        }
        private string userId;
    }
}