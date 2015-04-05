using System.Runtime.Serialization;

namespace Autotests.Utilities.ApiTestCore
{
    public class ApiResponse
    {
        [DataContract]
        public class Response
        {
            [DataMember(Name = "success")]
            public bool Success { get; set; }
        }

        [DataContract]
        public class ResponseFail : Response
        {
            [DataMember(Name = "response")]
            public FailMessage Message { get; set; }
        }

        [DataContract]
        public class ResponseFailOrder : Response
        {
            [DataMember(Name = "response")]
            public FailOrderMessage Message { get; set; }
        }

        [DataContract]
        public class ResponseAddObject : Response
        {
            [DataMember(Name = "response")]
            public AddMessage Message { get; set; }
        }

        [DataContract]
        public class ResponseAddOrder : Response
        {
            [DataMember(Name = "response")]
            public AddOrderMessage Message { get; set; }
        }

        [DataContract]
        public class ResponseCalculation : Response
        {
            [DataMember(Name = "response")]
            public MessageCalculation[] Message { get; set; }
        }

        [DataContract]
        public class ResponseStatus : Response
        {
            [DataMember(Name = "response")]
            public MessageStatus Message { get; set; }
        }

        [DataContract]
        public class ResponseOrderInfo : Response
        {
            [DataMember(Name = "response")]
            public MessageOrderInfo Message { get; set; }
        }

        [DataContract]
        public class ResponseTrueCancel : Response
        {
            [DataMember(Name = "response")]
            public MessageTrueCancal Message { get; set; }
        }
       [DataContract]
        public class AddMessage
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "key")]
            public string Key { get; set; }
        }

        [DataContract]
        public class AddOrderMessage
        {
            [DataMember(Name = "order")]
            public string OrderId { get; set; }
        }

        [DataContract]
        public class FailMessage
        {
            [DataMember(Name = "message")]
            public string Message { get; set; }
        }

        [DataContract]
        public class MessageCalculation
        {
            [DataMember(Name = "delivery_company_name")]
            public string DeliveryCompanyName { get; set; }

            [DataMember(Name = "delivery_company")]
            public string DeliveryCompany { get; set; }

            [DataMember(Name = "delivery_company_driver_version")]
            public string DeliveryCompanyDriverVersion { get; set; }

            [DataMember(Name = "pickup_company_driver_version")]
            public string PickupCompanyDriverVersion { get; set; }

            [DataMember(Name = "pickup_price")]
            public string PickupPrice { get; set; }

            [DataMember(Name = "delivery_price")]
            public string DeliveryPrice { get; set; }

            [DataMember(Name = "delivery_price_fee")]
            public string DeliveryPriceFee { get; set; }

            [DataMember(Name = "declared_price_fee")]
            public string DeclaredPriceFee { get; set; }

            [DataMember(Name = "delivery_time_min")]
            public string DeliveryTimeMin { get; set; }

            [DataMember(Name = "delivery_time_max")]
            public string DeliveryTimeMax { get; set; }

            [DataMember(Name = "delivery_time_avg")]
            public string DeliveryTimeAvg { get; set; }

            [DataMember(Name = "return_price")]
            public string ReturnPrice { get; set; }

            [DataMember(Name = "return_client_price")]
            public string ReturnClientPrice { get; set; }

            [DataMember(Name = "return_partial_price")]
            public string ReturnPartialPrice { get; set; }

            [DataMember(Name = "total_price")]
            public string TotalPrice { get; set; }

            [DataMember(Name = "payment_price_fee")]
            public string Paymentpricefee { get; set; }

            [DataMember(Name = "delivery_date")]
            public string DeliveryDate { get; set; }

            [DataMember(Name = "confirm_date")]
            public string Confirmdate { get; set; }

            [DataMember(Name = "pickup_date")]
            public string PickupDate { get; set; }
        }

        [DataContract]
        public class MessageErrore
        {
            [DataMember(Name = "city")]
            public string City { get; set; }
        }

        [DataContract]
        public class MessageStatus
        {
            [DataMember(Name = "status")]
            public string Status { get; set; }

            [DataMember(Name = "status_description")]
            public string StatusDescription { get; set; }

            [DataMember(Name = "status_message")]
            public string StatusMessage { get; set; }

            [DataMember(Name = "pickup_date")]
            public string PickupDate { get; set; }

            [DataMember(Name = "delivery_date")]
            public string DeliveryDate { get; set; }
        }

        [DataContract]
        public class MessageOrderInfo
        {
            [DataMember(Name = "declared_price")]
            public string DeclaredPice { get; set; }

            [DataMember(Name = "payment_price")]
            public string PaymentPrice { get; set; }

            [DataMember(Name = "to_name")]
            public string ToName { get; set; }

            [DataMember(Name = "to_phone")]
            public string ToPhone { get; set; }

            [DataMember(Name = "to_email")]
            public string ToEmail { get; set; }

            [DataMember(Name = "to_street")]
            public string ToStreet { get; set; }

            [DataMember(Name = "to_house")]
            public string ToHouse { get; set; }

            [DataMember(Name = "to_flat")]
            public string ToFlat { get; set; }

            [DataMember(Name = "weight")]
            public string Weight { get; set; }

            [DataMember(Name = "dimension_side1")]
            public string DimensionSide1 { get; set; }

            [DataMember(Name = "dimension_side2")]
            public string DimensionSide2 { get; set; }

            [DataMember(Name = "dimension_side3")]
            public string DimensionSide3 { get; set; }
        }

        [DataContract]
        public class MessageTrueCancal
        {
            [DataMember(Name = "order_id")]
            public string OrderId { get; set; }

            [DataMember(Name = "ticket_id")]
            public string TicketId { get; set; }
        }

        [DataContract]
        public class FailOrderMessage
        {
            [DataMember(Name = "message")]
            public ErrorMessage Error { get; set; }
        }

        [DataContract]
        public class ErrorMessage
        {
        [DataMember(Name = "to_city")]
        public string ToCity { get; set; }

        [DataMember(Name = "delivery_point")]
        public string DeliveryPoint { get; set; }

        [DataMember(Name = "delivery_company")]
        public string DeliveryCompany { get; set; }

        [DataMember(Name = "dimension_side1")]
        public string DimensionSide1 { get; set; }

        [DataMember(Name = "calculate_order")]
        public string CalculateOrder { get; set; }

        [DataMember(Name = "weight")]
        public string Weight { get; set; }
        }
    }
}