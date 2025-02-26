using Coza_Ecommerce_Shop.Models.Entities;
using Coza_Ecommerce_Shop.Models.Helper;
using Coza_Ecommerce_Shop.Repositories.Interfaces;
using Coza_Ecommerce_Shop.ViewModels.VNPay;
using Microsoft.AspNetCore.Http;

namespace Coza_Ecommerce_Shop.Services
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionRepository _transactionRepository;
		private static readonly Random _random = new Random();

		public VnPayService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ITransactionRepository transactionRepository)
		{
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
			_transactionRepository = transactionRepository;
		}

		public static string GenerateTransactionId()
		{
			lock (_random) // Đảm bảo Random không bị trùng khi chạy đa luồng
			{
				string tick = DateTime.UtcNow.Ticks.ToString();
				string randomSuffix = _random.Next(1000, 9999).ToString();
				return tick + randomSuffix;
			}
		}

		public async Task<string> CreatePaymentUrl(Order order)
        {
            var context = _httpContextAccessor.HttpContext;
            var timeNow = DateTime.UtcNow;
            var tranSactionId = GenerateTransactionId();

            Transaction transaction = new Transaction
            {
                TransactionId = tranSactionId,
                OrderId = order.Id
            };

            await _transactionRepository.CreateTransaction(transaction);


            var BaseUrl = _configuration["VnPay:BaseUrl"];
            var HashSecret = _configuration["VnPay:HashSecret"];


            var vnpayUrl = _configuration["VnPay:Url"];
            var returnUrl = _configuration["VnPay:PaymentBackReturnUrl"];
            var tmnCode = _configuration["VnPay:TmnCode"];
            var hashSecret = _configuration["VnPay:HashSecret"];
            var version = _configuration["VnPay:Version"];
            var command = _configuration["VnPay:Command"];
            var locale = _configuration["VnPay:Locale"];
            var currCode = _configuration["VnPay:CurrCode"];

            var vpay = new VnPayLibrary();
            vpay.AddRequestData("vnp_Version", version);
            vpay.AddRequestData("vnp_Command", command);
            vpay.AddRequestData("vnp_TmnCode", tmnCode);
            vpay.AddRequestData("vnp_Amount", ((int)order.TotalAmount * 100).ToString());
            vpay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            vpay.AddRequestData("vnp_CurrCode", currCode);
            vpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
            vpay.AddRequestData("vnp_Locale", locale);
            vpay.AddRequestData("vnp_OrderInfo", $"Thanh toán đơn hàng {order.Code}");
            vpay.AddRequestData("vnp_OrderType", "billpayment");
            vpay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vpay.AddRequestData("vnp_TxnRef", tranSactionId);


            string paymentUrl = vpay.CreateRequestUrl(BaseUrl, hashSecret);
            return await Task.FromResult(paymentUrl);
        }

        public VnPaymentResponseModel PaymentExecute(IQueryCollection collections)
        {

            if (collections.Count == 0)
            {
                return new VnPaymentResponseModel { Success = false, VnPayResponseCode = "NO_DATA" };
            }
            VnPayLibrary vnpay = new VnPayLibrary();
            var response = new VnPaymentResponseModel();

            // Lấy toàn bộ dữ liệu từ query string (chỉ lấy các key bắt đầu bằng "vnp_")
            foreach (var key in collections.Keys)
            {
                if (!string.IsNullOrEmpty(collections[key]) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, collections[key]);
                }
            }

           
            var vnp_TxnRef = vnpay.GetResponseData("vnp_TxnRef");
            var vnp_TransactionNo = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
			var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
			var bankCode = collections["vnp_BankCode"];
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            
            var vnp_HashSecret = _configuration["VnPay:HashSecret"];


			// Kiểm tra chữ ký hợp lệ hay không
			bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);


			return new VnPaymentResponseModel
            {
                Success = checkSignature && vnp_ResponseCode == "00" && vnp_TransactionStatus == "00",
                PaymentMethod = bankCode,
                OrderDescription = vnp_OrderInfo.ToString(),
                OrderTransactionId = vnp_TxnRef.ToString(),
                PaymentId = vnp_TransactionNo.ToString(),
                TransactionId = vnp_TransactionNo.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode
            };

        }

    }
}
