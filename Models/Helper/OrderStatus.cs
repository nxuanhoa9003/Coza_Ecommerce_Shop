namespace Coza_Ecommerce_Shop.Models.Helper
{

    public enum OrderStatus
    {
        Pending,        // Chờ xác nhận (chưa thanh toán)
        WaitingForPayment, // Đang chờ thanh toán (đã chuyển hướng sang VNPAY)
        Paid,           // Đã thanh toán thành công
        Confirmed,      // Đã xác nhận đơn hàng (sau khi thanh toán thành công)
        Shipping,       // Đang giao hàng
        Completed,      // Đã hoàn thành
        Canceled,       // Đã hủy
        PaymentFailed   // Thanh toán thất bại
    }

    public static class OrderStatusHelper
    {

        public static readonly Dictionary<OrderStatus, string> OrderStatusDisplayNames = new()
        {
            { OrderStatus.Pending, "Chờ xác nhận" },
            { OrderStatus.WaitingForPayment, "Đang chờ thanh toán" },
            { OrderStatus.Paid, "Đã thanh toán" },
            { OrderStatus.Confirmed, "Đã xác nhận" },
            { OrderStatus.Shipping, "Đang giao hàng" },
            { OrderStatus.Completed, "Đã hoàn thành" },
            { OrderStatus.Canceled, "Đã hủy" },
            { OrderStatus.PaymentFailed, "Thanh toán thất bại" }
        };


        public static string GetBadgeClass(string status)
        {
            return status switch
            {
                "Pending" => "badge bg-secondary",           // Chờ xác nhận - Xám
                "WaitingForPayment" => "badge bg-warning",  // Đang chờ thanh toán - Vàng
                "Paid" => "badge bg-primary",               // Đã thanh toán - Xanh dương
                "Confirmed" => "badge bg-info",             // Đã xác nhận - Xanh nhạt
                "Shipping" => "badge bg-success",           // Đang giao hàng - Xanh lá
                "Completed" => "badge bg-dark",             // Đã hoàn thành - Đen
                "Canceled" => "badge bg-danger",            // Đã hủy - Đỏ
                "PaymentFailed" => "badge bg-danger",       // Thanh toán thất bại - Đỏ
                _ => "badge bg-secondary"
            };
        }


        public static readonly Dictionary<string, OrderStatus> OrderStatusMap = new(StringComparer.OrdinalIgnoreCase)
        {
            { "Pending", OrderStatus.Pending },
            { "WaitingForPayment", OrderStatus.WaitingForPayment },
            { "Paid", OrderStatus.Paid },
            { "Confirmed", OrderStatus.Confirmed },
            { "Shipping", OrderStatus.Shipping },
            { "Completed", OrderStatus.Completed },
            { "Canceled", OrderStatus.Canceled },
            { "PaymentFailed", OrderStatus.PaymentFailed }
        };




        public static readonly Dictionary<OrderStatus, List<OrderStatus>> ValidTransitions = new()
        {
            // Đơn hàng mới, có thể chờ thanh toán (VNPAY) hoặc xác nhận luôn (COD)
            { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.WaitingForPayment, OrderStatus.Confirmed, OrderStatus.Canceled } },

            // Nếu đang chờ thanh toán qua VNPAY
            { OrderStatus.WaitingForPayment, new List<OrderStatus> { OrderStatus.Paid, OrderStatus.PaymentFailed } },

            // Nếu thanh toán thành công, đơn hàng sẽ được xác nhận
            { OrderStatus.Paid, new List<OrderStatus> { OrderStatus.Confirmed } },

            // Nếu đơn hàng được xác nhận, tiến hành giao hàng
            { OrderStatus.Confirmed, new List<OrderStatus> { OrderStatus.Shipping } },

            // Nếu đơn hàng đang giao, sau khi hoàn thành sẽ chuyển sang Completed
            { OrderStatus.Shipping, new List<OrderStatus> { OrderStatus.Completed } },

            // Nếu thanh toán thất bại (VNPAY), đơn hàng sẽ bị hủy
            { OrderStatus.PaymentFailed, new List<OrderStatus> { OrderStatus.Canceled } }
        };


        public static List<OrderStatus> GetValidNextStatuses(string currentStatus, string paymentMethod)
        {
            if (!OrderStatusMap.TryGetValue(currentStatus, out var currentStatusEnum))
            {
                return new List<OrderStatus>(); // Trạng thái không hợp lệ
            }

            if (!ValidTransitions.TryGetValue(currentStatusEnum, out var nextStatuses))
            {
                return new List<OrderStatus>(); // Không có trạng thái tiếp theo hợp lệ
            }

            // Lọc trạng thái theo quy tắc của từng phương thức thanh toán
            return nextStatuses
                .Where(newStatus => CanTransition(currentStatusEnum, newStatus, paymentMethod))
                .ToList();
        }


        public static bool CanTransition(OrderStatus currentStatus, OrderStatus newStatus, string paymentMethod)
        {
            if (!ValidTransitions.TryGetValue(currentStatus, out var validNextStatuses)) return false;

            if (!validNextStatuses.Contains(newStatus)) return false;

            // Quy tắc đặc biệt theo phương thức thanh toán
            if (paymentMethod == PaymentType.COD && newStatus == OrderStatus.WaitingForPayment)
            {
                return false; // COD không cần trạng thái "Chờ thanh toán"
            }

            if (paymentMethod == PaymentType.VNPAY && currentStatus == OrderStatus.Pending && newStatus == OrderStatus.Confirmed)
            {
                return false; // VNPAY không thể từ Pending -> Confirmed nếu chưa thanh toán
            }

            return true;
        }



    }


}
