namespace Coza_Ecommerce_Shop.Extentions
{
    public enum Permission
    { 
        // Truy cập trang Admin
        AccessAdminPage,
        

        // Quản lý vai trò
        ManageRoles,
        ViewRoles,

        // Quản lý quyền của vai trò
        EditRolePermission,
        ViewPermissions,

        // Quản lý tài khoản
        ViewUsers,
        CreateUser,
        EditUser,
        DeleteUser,
        ViewUserDetail,

        // profile 
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
        DeleteProductCategory
    }

}
