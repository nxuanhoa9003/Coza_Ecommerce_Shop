namespace Coza_Ecommerce_Shop.Extentions
{
    public enum Permission
    {
        // Truy cập trang Admin
        AccessAdminPage,

        // Quản lý vai trò
        ManageRoles,
        ViewRoles,
        CreateRoles,
        EditRoles,
        DeleteRoles,

        // Quản lý quyền của vai trò
        EditRolePermission,
        ViewPermissions,
        UpdateRoleClaims,

        // Quản lý tài khoản
        ViewUsers,
        CreateUser,
        EditUser,
        DeleteUser,
        ViewUserDetail,

        // Khách hàng
        ViewListCustomer,
        ViewDetailListCustomer,

        // Profile 
        ViewProfile,
        UpdateProfile,

        // Quản lý tin tức (News)
        ViewNews,
        ViewNewsDetail,
        CreateNews,
        EditNews,
        DeleteNews,

        // Quản lý bài viết (Posts)
        ViewPost,
        ViewPostDetail,
        CreatePost,
        EditPost,
        DeletePost,

        // Quản lý sản phẩm (Products)
        ViewProduct,
        ViewProductDetail,
        CreateProduct,
        EditProduct,
        DeleteProduct,

        // Quản lý danh mục sản phẩm (Product Category)
        ViewProductCategory,
        ViewProductCategoryDetail,
        CreateProductCategory,
        EditProductCategory,
        DeleteProductCategory,

        // Quản lý banner
        CreateBanner,
        EditBanner,
        DeleteBanner,

        // Quản lý danh mục (Category)
        CreateCategory,
        EditCategory,
        DeleteCategory,

        // Quản lý đơn hàng (Order)
        UpdateOrder,

        // Quản lý cài đặt (Setting)
        UpdateSetting
    }

}
