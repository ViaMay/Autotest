using System.Runtime.Serialization;

namespace Autotests.Utilities.ApiTestCore
{
    public class ApiResponse
    {
        [DataContract]
        public class AddMessage
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "key")]
            public string Key { get; set; }
        }

        [DataContract]
        public class InfoObjectMessage
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "warehouse")]
            public string Warehouse { get; set; }

            [DataMember(Name = "street")]
            public string Street { get; set; }

            [DataMember(Name = "house")]
            public string House { get; set; }

            [DataMember(Name = "flat")]
            public string Flat { get; set; }

            [DataMember(Name = "city")]
            public string City { get; set; }

            [DataMember(Name = "contact_person")]
            public string ContactPerson { get; set; }

            [DataMember(Name = "contact_phone")]
            public string ContactPhone { get; set; }

            [DataMember(Name = "contact_email")]
            public string ContactEmail { get; set; }

            [DataMember(Name = "schedule")]
            public string Schedule { get; set; }
        }

        [DataContract]
        public class AddOrderMessage
        {
            [DataMember(Name = "order")]
            public string OrderId { get; set; }
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

            [DataMember(Name = "to_email")]
            public string Email { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "warehouse")]
            public string Warehouse { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "street")]
            public string Street { get; set; }

            [DataMember(Name = "house")]
            public string House { get; set; }

            [DataMember(Name = "flat")]
            public string Flat { get; set; }

            [DataMember(Name = "contact_person")]
            public string ContactPerson { get; set; }

            [DataMember(Name = "contact_phone")]
            public string ContactPhone { get; set; }

            [DataMember(Name = "username")]
            public string Username { get; set; }
        }

        [DataContract]
        public class FailMessage
        {
            [DataMember(Name = "message")]
            public string ErrorText { get; set; }
        }

        [DataContract]
        public class FailOrderMessage
        {
            [DataMember(Name = "message")]
            public ErrorMessage Error { get; set; }
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
            public string ConfirmDate { get; set; }

            [DataMember(Name = "pickup_date")]
            public string PickupDate { get; set; }
        }

        [DataContract]
        public class MessageCompanyTerm
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "term")]
            public string Term { get; set; }

            [DataMember(Name = "prolongation")]
            public bool Prolongation { get; set; }
        }

        [DataContract]
        public class MessageDeliveryPoint
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "city")]
            public City City { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "schedule")]
            public string Schedule { get; set; }

            [DataMember(Name = "has_fitting_room")]
            public bool HasFittingRoom { get; set; }

            [DataMember(Name = "is_cash")]
            public bool IsCash { get; set; }

            [DataMember(Name = "is_card")]
            public bool IsCard { get; set; }

            [DataMember(Name = "longitude")]
            public string Longitude { get; set; }

            [DataMember(Name = "latitude")]
            public string Latitude { get; set; }

            [DataMember(Name = "metro")]
            public string Metro { get; set; }

            [DataMember(Name = "description_in")]
            public string DescriptionIn { get; set; }

            [DataMember(Name = "description_out")]
            public string DescriptionOut { get; set; }

            [DataMember(Name = "indoor_place")]
            public string IndoorPlace { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "company")]
            public string Company { get; set; }

            [DataMember(Name = "company_code")]
            public string CompanyCode { get; set; }
        }

        [DataContract]
        public class OptionsCity
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "name_index")]
            public string NameIndex { get; set; }

            [DataMember(Name = "city_id")]
            public string CityId { get; set; }

            [DataMember(Name = "country")]
            public string Country { get; set; }

            [DataMember(Name = "city")]
            public string City { get; set; }

            [DataMember(Name = "region")]
            public string Region { get; set; }

            [DataMember(Name = "region_id")]
            public string RegionId { get; set; }

            [DataMember(Name = "postal_code")]
            public string PostalCode { get; set; }

            [DataMember(Name = "area")]
            public string Area { get; set; }

            [DataMember(Name = "kladr")]
            public string Kladr { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "importance")]
            public string Importance { get; set; }
        }

        [DataContract]
        public class OptionsPoints
        { 
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "city_id")]
            public string CityId { get; set; }

            [DataMember(Name = "city")]
            public string City { get; set; }

            [DataMember(Name = "region")]
            public string Region { get; set; }

            [DataMember(Name = "region_id")]
            public string RegionId { get; set; }

            [DataMember(Name = "city_type")]
            public string CityType { get; set; }

            [DataMember(Name = "postal_code")]
            public string PostalCode { get; set; }

            [DataMember(Name = "area")]
            public string Area { get; set; }

            [DataMember(Name = "kladr")]
            public string Kladr { get; set; }

            [DataMember(Name = "company")]
            public string Company { get; set; }

            [DataMember(Name = "company_id")]
            public string CompanyId { get; set; }

            [DataMember(Name = "company_code")]
            public string CompanyCode { get; set; }

            [DataMember(Name = "metro")]
            public string Metro { get; set; }

            [DataMember(Name = "description_in")]
            public string DescriptionIn { get; set; }

            [DataMember(Name = "description_out")]
            public string DescriptionOut { get; set; }

            [DataMember(Name = "indoor_place")]
            public string IndoorPlace { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "schedule")]
            public string Schedule { get; set; }

            [DataMember(Name = "longitude")]
            public string Longitude { get; set; }

            [DataMember(Name = "latitude")]
            public string Latitude { get; set; }

            [DataMember(Name = "has_fitting_room")]
            public bool HasFittingRoom { get; set; }

            [DataMember(Name = "is_cash")]
            public bool IsCash { get; set; }

            [DataMember(Name = "is_card")]
            public bool IsCard { get; set; }
            
            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "status")]
            public string Status { get; set; }
        }

        [DataContract]
        public class City
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }
        }

        [DataContract]
        public class MessageErrore
        {
            [DataMember(Name = "city")]
            public string City { get; set; }
        }

        [DataContract]
        public class MessageOrderInfo
        {
            [DataMember(Name = "declared_price")]
            public string DeclaredPice { get; set; }

            [DataMember(Name = "payment_price")]
            public string PaymentPrice { get; set; }

            [DataMember(Name = "city_to")]
            public string ToCity { get; set; }

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

            [DataMember(Name = "delivery_company_order_number")]
            public string DeliveryCompanyOderNumber { get; set; }

            [DataMember(Name = "delivery_company")]
            public string DeliveryCompany { get; set; }

            [DataMember(Name = "post_track")]
            public string PostTrack { get; set; }

            [DataMember(Name = "order_status")]
            public OrderStatus[] OrderStatus { get; set; }
        }

        [DataContract]
        public class MessageStatusConfirm
        {
            [DataMember(Name = "status")]
            public string Status { get; set; }

            [DataMember(Name = "message")]
            public string Message { get; set; }
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
        public class MessagePaymentPriceFee
        {
            [DataMember(Name = "from")]
            public string From { get; set; }

            [DataMember(Name = "min")]
            public string Min { get; set; }

            [DataMember(Name = "percent")]
            public string Percent { get; set; }

            [DataMember(Name = "percent_card")]
            public string PercentCard { get; set; }
        }

        [DataContract]
        public class MessagePickupOrders
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "delivery_company_id")]
            public string DeliveryCompanyId { get; set; }
        }
        
        [DataContract]
        public class MessageDocumentDelivery
        {
            [DataMember(Name = "view")]
            public string View { get; set; }

            [DataMember(Name = "confirm")]
            public string Confirm { get; set; }
        }
        
        [DataContract]
        public class MessageDocumentsRequest
        {
            [DataMember(Name = "completed")]
            public bool Completed { get; set; }

            [DataMember(Name = "request_id")]
            public string RequestId { get; set; }

            [DataMember(Name = "documents")]
            public Documents[] Documents { get; set; }
        }
        
        [DataContract]
        public class MessageCompaniesOrShops
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }
        }
        
        [DataContract]
        public class OrderStatus
        {
            [DataMember(Name = "date")]
            public string Date { get; set; }

            [DataMember(Name = "company_status")]
            public string CompanStatus { get; set; }

            [DataMember(Name = "company_status_description")]
            public string CompanyStatusDescription { get; set; }
        }
        
        [DataContract]
        public class Documents
        {
            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "warehouse")]
            public string Warehouse { get; set; }

            [DataMember(Name = "pickup_company")]
            public string PickupCompany { get; set; }

            [DataMember(Name = "delivery_company")]
            public string DeliveryCompany { get; set; }

            [DataMember(Name = "url")]
            public string Url { get; set; }
        }

        [DataContract]
        public class ResponseDeamonPoints : TResponse
        {
            [DataMember(Name = "points")]
            public OptionsPoints[] Points { get; set; }
        }

        [DataContract]
        public class ResponseDeamonСities : TResponse
        {
            [DataMember(Name = "options")]
            public OptionsCity[] Options { get; set; }
        }

        [DataContract]
        public class ResponseDeamonСity : TResponse
        {
            [DataMember(Name = "result")]
            public OptionsCity Result { get; set; }
        }

        [DataContract]
        public class ResponseAddObject : TResponse
        {
            [DataMember(Name = "response")]
            public AddMessage Response { get; set; }
        }

        [DataContract]
        public class ResponseInfoObject : TResponse
        {
            [DataMember(Name = "response")]
            public InfoObjectMessage Response { get; set; }
        }

        [DataContract]
        public class ResponseAddOrder : TResponse
        {
            [DataMember(Name = "response")]
            public AddOrderMessage Response { get; set; }
        }

        [DataContract]
        public class ResponseCalculation : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCalculation[] Response { get; set; }
        }

        [DataContract]
        public class ResponseCompanyTerm : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCompanyTerm Response { get; set; }
        }

        [DataContract]
        public class ResponseCompaniesOrShops : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCompaniesOrShops[] Response { get; set; }
        }

        [DataContract]
        public class ResponsePickupCompany : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCompaniesOrShops Response { get; set; }
        }

        [DataContract]
        public class ResponseDeliveryPoints : TResponse
        {
            [DataMember(Name = "response")]
            public MessageDeliveryPoint[] Response { get; set; }
        }

        [DataContract]
        public class ResponseFail : TResponse
        {
            [DataMember(Name = "response")]
            public FailMessage Response { get; set; }

            [DataMember(Name = "error")]
            public string Error { get; set; }
        }

        [DataContract]
        public class ResponseFailObject : TResponse
        {
            [DataMember(Name = "response")]
            public FailOrderMessage Response { get; set; }
        }

        [DataContract]
        public class ResponseOrderInfo : TResponse
        {
            [DataMember(Name = "response")]
            public MessageOrderInfo Response { get; set; }
        }

        [DataContract]
        public class ResponseStatus : TResponse
        {
            [DataMember(Name = "response")]
            public MessageStatus Response { get; set; }
        }

        [DataContract]
        public class ResponseStatusConfirm : TResponse
        {
            [DataMember(Name = "response")]
            public MessageStatusConfirm Response { get; set; }
        }

        [DataContract]
        public class ResponsePickupOrders : TResponse
        {
            [DataMember(Name = "response")]
            public MessagePickupOrders[] Response { get; set; }
        }

        [DataContract]
        public class ResponseTrueCancel : TResponse
        {
            [DataMember(Name = "response")]
            public MessageTrueCancal Response { get; set; }
        }
        
        [DataContract]
        public class ResponseDocumentsRequest : TResponse
        {
            [DataMember(Name = "response")]
            public MessageDocumentsRequest Response { get; set; }
        }       
 
        [DataContract]
        public class ResponsePaymentPriceFee : TResponse
        {
            [DataMember(Name = "response")]
            public MessagePaymentPriceFee Response { get; set; }
        }

        [DataContract]
        public class ResponseDocumentPickup : TResponse
        {
            [DataMember(Name = "response")]
            public MessageDocumentDelivery Response { get; set; }
        }

        [DataContract]
        public class TResponse
        {
            [DataMember(Name = "success")]
            public bool Success { get; set; }
        }
    }
}