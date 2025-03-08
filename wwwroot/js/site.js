$(function () {

    document.addEventListener("DOMContentLoaded", function () {
        let megaMenu = $(".has-mega-menu");

        megaMenu.addEventListener("mouseover", function () {
            let menu = megaMenu.$(".mega-menu");
            menu.style.visibility = "visible";
            menu.style.opacity = "1";
        });

        megaMenu.addEventListener("mouseleave", function () {
            let menu = megaMenu.$(".mega-menu");
            menu.style.visibility = "hidden";
            menu.style.opacity = "0";
        });
    });

    $(document).on("click", ".quick-order-btn", function (e) {
        e.preventDefault();
        var productId = $(this).data("id");
        $.get("/quick-view-product/" + encodeURIComponent(productId), function (data) {
            $("#quickProductModal").html(data);
            $('.js-modal1').addClass('show-modal1');
            reinitMagnificPopup();
            reinitializeSlick();
            AddToWishlist();
            LoadWishlist();
        });
    });

    $(document).on("click", ".js-hide-modal1", function () {
        $('.js-modal1').removeClass('show-modal1');
        LoadWishlist();
    });


    // <input range>
    let minPrice = document.getElementById("min-price");
    let maxPrice = document.getElementById("max-price");
    let minValue = document.getElementById("min-value");
    let maxValue = document.getElementById("max-value");
    let sliderTrack = document.querySelector(".slider-track");

    if (minPrice && maxPrice && minValue && maxValue && sliderTrack) {
        function updateValues() {
            let minVal = parseInt(minPrice.value);
            let maxVal = parseInt(maxPrice.value);

            // Kiểm tra nếu maxVal nhỏ hơn hoặc bằng minPrice.min + 50000
            if (maxVal <= parseInt(minPrice.min) + 50000) {
                maxVal = parseInt(minPrice.min) + 50000;
                maxPrice.value = maxVal;
            }

            if (minVal > maxVal - 50000) {
                minVal = Math.max(parseInt(minPrice.min), maxVal - 50000);
                minPrice.value = minVal;
            }


            minValue.textContent = minVal.toLocaleString() + "đ";
            maxValue.textContent = maxVal.toLocaleString() + "đ";

            let minPercent = ((minVal - minPrice.min) / (minPrice.max - minPrice.min)) * 100;
            let maxPercent = ((maxVal - maxPrice.min) / (maxPrice.max - maxPrice.min)) * 100;

            sliderTrack.style.background = `linear-gradient(to right, 
            #b3b3b3 0%, #b3b3b3 ${minPercent}%, 
            #717fe0 ${minPercent}%, #717fe0 ${maxPercent}%, 
            #b3b3b3 ${maxPercent}%, #b3b3b3 100%)`;
        }

        minPrice.addEventListener("input", updateValues);
        maxPrice.addEventListener("input", updateValues);

        updateValues();
    }

    // </input range>


    // load more product
    $(document).on("click", "#btn-load-more", function (e) {
        var page = $(this).data("page");
        $.ajax({
            url: "/product/load-more-product",
            method: "GET",
            data: { page: page },
            success: function (response) {
                if (response) {
                    renderProducts(response);
                    $("#btn-load-more").data("page", response.nextPage);
                   
                }
            },
            error: function (xhr, status, error) {
                console.error("Lỗi khi lấy biến thể:", error);
            }
        });

    });

    function renderProducts(data) {
        let html = "";
        let products = data.products;
        if (products) {
            LoadWishlist();
        }
        products.forEach(product => {
            let productDefault = product.Variants.find(p => p.IsDefault);
            if (productDefault == null) {
                productDefault = product.Variants[0];
            }
            let basePrice = productDefault.BasePrice ? productDefault.BasePrice.toLocaleString("vi-VN") + "đ" : "0đ";

            html += `
                <div class="col-sm-6 col-md-4 col-lg-3 p-b-35 isotope-item">
                    <div class="block2">
                        <div class="block2-pic hov-img0">
                            <img src="${product.Image}" alt="IMG-PRODUCT">
                            <a href="#" data-id="${product.Id}" class="quick-order-btn block2-btn flex-c-m stext-103 cl2 size-102 bg0 bor2 hov-btn1 p-lr-15 trans-04 js-show-modal1">
                                Quick View
                            </a>
                        </div>
                        <div class="block2-txt flex-w flex-t p-t-14 product-detail">
                            <div class="block2-txt-child1 flex-col-l">
                                <a href="/product/product-detail/${product.Slug}" class="stext-104 cl4 hov-cl1 trans-04 js-name-b2 js-name-detail p-b-6">
                                    ${product.Title}
                                </a>
                                <span class="stext-105 cl3">${basePrice}</span>
                            </div>
                            <div class="block2-txt-child2 flex-r p-t-3">
                                <a href="#" data-id="${product.Id}" class="btn-addwish dis-block pos-relative">
                                    <img class="icon-heart1 dis-block trans-04" src="/assets/images/icons/icon-heart-01.png" alt="ICON">
                                    <img class="icon-heart2 dis-block trans-04 ab-t-l" src="/assets/images/icons/icon-heart-02.png" alt="ICON">
                                </a>
                            </div>
                        </div>
                    </div>
                </div>`;
        });

        var $newItems = $(html);
        $(".product-container").append($newItems);

        if (products) {
            AddToWishlist();
        }

        // Cập nhật Isotope
        $(".product-container").isotope("appended", $newItems).isotope("layout");
        setTimeout(function () {
            $(".isotope-grid").isotope({ filter: "*" });
        }, 50);

        
    }


    let productVariants = [];
    function fetchProductVariants() {

        let psku = $("#p-sku").val();
        if (psku) {
            $.ajax({
                url: "/product/get-variants",
                method: "GET",
                data: { sku: psku },
                success: function (response) {
                    productVariants = Array.from(response.variants);
                    updateAvailableColors($("#selected-size").val());
                    updateAvailableSizes($("#selected-color").val());

                },
                error: function (xhr, status, error) {
                    console.error("Lỗi khi lấy biến thể:", error);
                }
            });

        }
    }


    function updateVariantInfo() {
        let selectedSize = $('.size-btn.active').data('size');
        let selectedColor = $('.color-btn.active').data('color');
        const psku = $("#p-sku").val();

        if (selectedSize && selectedColor && psku) {
            $.ajax({
                url: "/product/GetVariant",
                type: "GET",
                data: { sku: psku, size: selectedSize, color: selectedColor },
                success: function (response) {
                    if (response && response.variant) {
                        let variant = response.variant;
                        $('#v-sku').val(variant.sku);
                        $('#base-price').text(variant.basePrice.toLocaleString('vi-VN') + 'đ');

                    }
                },
                error: function () {
                    alert("có lỗi");
                }
            });
        }
    }




    $(document).on("click", ".size-btn", function () {
        if ($(this).hasClass("disabled")) return;

        $('.size-btn').removeClass('active');
        $(this).addClass('active');

        let selectedSize = $(this).data("size");
        $("#selected-size").val(selectedSize);

        fetchProductVariants();

        updateAvailableColors(selectedSize);
        updateVariantInfo();
    });

    $(document).on("click", ".color-btn", function () {
        if ($(this).hasClass("disabled")) return;

        $('.color-btn').removeClass('active');
        $(this).addClass('active');

        let selectedColor = $(this).data("color");
        $("#selected-color").val(selectedColor);

        fetchProductVariants();

        updateAvailableSizes(selectedColor);
        updateVariantInfo();
    });


    function updateAvailableColors(selectedSize) {
        $(".color-btn").each(function () {
            let color = $(this).data("color");

            let isAvailable = productVariants.some(v => v.size === selectedSize && v.color === color && v.stock > 0);

            $(this).toggleClass("disabled", !isAvailable).prop("disabled", !isAvailable);
        });
    }

    function updateAvailableSizes(selectedColor) {
        $(".size-btn").each(function () {
            let size = $(this).data("size");
            let isAvailable = productVariants.some(v => v.color === selectedColor && v.size === size && v.stock > 0);

            $(this).toggleClass("disabled", !isAvailable).prop("disabled", !isAvailable);
        });
    }


    function reinitializeSlick() {
        $('.wrap-slick3').each(function () {
            $(this).find('.slick3').slick({
                slidesToShow: 1,
                slidesToScroll: 1,
                fade: true,
                infinite: true,
                autoplay: false,
                autoplaySpeed: 6000,

                arrows: true,
                appendArrows: $(this).find('.wrap-slick3-arrows'),
                prevArrow: '<button class="arrow-slick3 prev-slick3"><i class="fa fa-angle-left" aria-hidden="true"></i></button>',
                nextArrow: '<button class="arrow-slick3 next-slick3"><i class="fa fa-angle-right" aria-hidden="true"></i></button>',

                dots: true,
                appendDots: $(this).find('.wrap-slick3-dots'),
                dotsClass: 'slick3-dots',
                customPaging: function (slick, index) {
                    var portrait = $(slick.$slides[index]).data('thumb');
                    return '<img src=" ' + portrait + ' "/><div class="slick3-dot-overlay"></div>';
                },
            });
        });
    }

    function reinitMagnificPopup() {
        $('.gallery-lb').each(function () {
            $(this).magnificPopup({
                delegate: 'a', // the selector for gallery item
                type: 'image',
                gallery: {
                    enabled: true
                },
                mainClass: 'mfp-fade'
            });
        });
    }



    $("#checkAll").on("change", function () {
        $(".item-check").prop("checked", $(this).prop("checked"));
    });
    $(".item-check").on("change", function () {
        let allChecked = $(".item-check").length === $(".item-check:checked").length;
        $("#checkAll").prop("checked", allChecked);
    });

    /*add to cart*/
    $(document).on("click", ".btn-add-to-cart", function () {
        let productSku = $("#p-sku").val();
        let size = $("#selected-size").val();
        let color = $("#selected-color").val();
        let quantity = $("#quantity").val();
        let variantSku = $("#v-sku").val();
        addToCart(productSku, variantSku, quantity, $(this));
    });
    function addToCart(productSku, variantSku, quantity, button) {
        $.ajax({
            url: "/cart/add-to-cart",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                productSku: productSku,
                variantSku: variantSku,
                quantity: quantity
            }),
            success: function (response) {
                if (response.susscess) {

                    var nameProduct = button.closest(".product-detail").find(".js-name-detail").html();
                    swal(nameProduct, "is added to cart !", "success");
                    getCartItemCount();
                }


            },
            error: function (xhr) {
                if (xhr.status === 401) {  // Kiểm tra nếu API trả về 401
                    alert("Bạn chưa đăng nhập. Vui lòng đăng nhập để tiếp tục.");
                    window.location.href = "/account/login?returnUrl=" + encodeURIComponent(window.location.pathname + window.location.search);
                } else {
                    alert(xhr.responseJSON?.message || "Lỗi khi thêm sản phẩm vào giỏ hàng!");
                }
            }
        });
    }
    /*add to cart*/

    //update quantity cart

    $(".btn-num-product-decrease").on("click", function () {
        var row = $(this).closest("tr");
        var numProduct = Number($(this).next().val());
        if (numProduct > 1) $(this).next().val(numProduct - 1);
        updateCart(row);
    });

    $(".btn-num-product-increase").on("click", function () {
        var row = $(this).closest("tr");
        var numProduct = Number($(this).prev().val());
        $(this).prev().val(numProduct + 1);
        updateCart(row);
    });

    $(".quantity-input").on("keydown", function (e) {
        if (e.key === "Enter") {
            e.preventDefault(); // Ngăn form submit
            var row = $(this).closest("tr");
            updateCart(row);
        }
    });

    //update quantity cart

    // remove product from cart
    $(".remove-item").on("click", function () {
        var row = $(this).closest("tr");
        var productSku = $(this).data("product-sku");
        var variantSku = $(this).data("variant-sku");

        $.ajax({
            url: "/cart/remove-from-cart",
            type: "POST",
            data: { productSku: productSku, variantSku: variantSku },
            success: function (response) {
                if (response.success) {
                    row.fadeOut(300, function () {
                        $(this).remove();
                        getCartItemCount();
                        updateCartTotal();
                    });

                } else {
                    alert("Lỗi khi xóa sản phẩm");
                }
            }
        });
    });

    // Hàm cập nhật số lượng sản phẩm
    function updateCart(row) {
        var productSku = row.data("product-sku");
        var variantSku = row.data("variant-sku");
        var quantity = row.find(".quantity-input").val();
        var price = parseFloat(row.find(".column-3").data("price"));

        $.ajax({
            url: "/cart/update-cart",
            type: "POST",
            data: { productSku: productSku, variantSku: variantSku, quantity: quantity },
            success: function (response) {
                if (response.success) {
                    // Tính toán tổng giá mới
                    var newTotal = price * quantity;
                    var formattedTotal = newTotal.toLocaleString("vi-VN") + "đ"; // Format tiền VNĐ

                    // Cập nhật tổng giá trong cột "Total"
                    row.find(".column-5").data("total", newTotal); // Cập nhật data-total
                    row.find(".column-5").text(formattedTotal); // Cập nhật hiển thị
                    updateCartTotal();
                } else {
                    alert("Cập nhật số lượng thất bại");
                }
            }
        });
    }
    function updateCartTotal() {
        let totalCartPrice = 0;

        $(".totalItem").each(function () {
            totalCartPrice += parseFloat($(this).data("total")); // Lấy giá trị từ data-total
        });

        // Format giá tiền theo VNĐ
        var formattedTotal = totalCartPrice.toLocaleString("vi-VN") + "đ";

        // Cập nhật giá trị trong SubCartTotal
        $(".SubCartTotal").text(formattedTotal);
    }

    //update quantity cart

    function getCartItemCount() {
        $.ajax({
            url: "/cart/count-cart-items",
            type: "GET",
            contentType: "application/json",
            success: function (data) {
                $(".js-show-cart").attr("data-notify", data.count); // Gán số lượng vào phần tử có id="cart-count"
            },
            error: function (xhr) {
                console.error("Lỗi:", xhr.responseJSON?.message || "Có lỗi xảy ra");
                $(".js-show-cart").attr("notify", 0); // Gán số lượng vào phần tử có id="cart-count"

            }
        });
    }


    // add to wish list
    function LoadWishlist() {
        $.ajax({
            url: "/product/load-wishlist",
            type: "POST",
            success: function (response) {
                if (response && response.issuccess === true) {
                    var normalizedWishlists = response.data.map(id => id.toLowerCase());
                    let wishlistCount = normalizedWishlists.length;
                    $(".icon-header-wishlish").attr("data-notify", wishlistCount);
                    $(".btn-addwish").each(function () {
                        var productId = $(this).data("id").toString().toLowerCase();
                        if (normalizedWishlists.includes(productId)) {
                            $(this).addClass("active");
                        } else {
                            $(this).removeClass("active");
                        }
                    });
                }
            },
            error: function (xhr) {
                console.error("Lỗi khi tải danh sách wishlist");
            }
        });
    }

    LoadWishlist();


    function AddToWishlist() {
        $(".btn-addwish").on('click', function (e) {
            e.preventDefault();
            let $this = $(this);
            let nameProduct = $this.closest(".product-detail").find('.js-name-detail').html();
            let productid = $this.data("id");
            if (!productid) return;
            $.ajax({
                url: "/product/add-to-wishlist",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({ productid: productid }),
                success: function (response) {
                    // Kiểm tra nếu phần tử đã được thêm vào wishlist
                    if ($this.hasClass("active")) {
                        // Nếu đã tym, hủy tym
                        $this.removeClass("active");
                        swal(nameProduct, "has been removed from wishlist!", "warning");
                    } else {
                        // Nếu chưa tym, thêm vào wishlist
                        $(this).addClass("active");
                        swal(nameProduct, "is added to wishlist!", "success");
                    }
                    LoadWishlist();
                },
                error: function (xhr) {
                    let response = JSON.parse(xhr.responseText);
                    if (response.isLogin && !response.isLogin) {
                        window.location.href = "/account/login"; // Chuyển hướng đến trang đăng nhập
                    } else {
                        alert(response.message);
                    }
                }
            });


        });

    }

    AddToWishlist();
    // add to wish list

    // remove item wish lish
    $(".btn-remove-item-wishlist").on('click', function (e) {
        let $this = $(this);
        let productid = $this.data("id");
        if (!productid) return;
        $.ajax({
            url: "/product/add-to-wishlist",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({ productid: productid }),
            success: function (response) {
                
                // Xóa dòng sản phẩm khỏi bảng Wishlist
                $this.closest("tr").remove();

                // Cập nhật lại wishlist icon
                LoadWishlist();
            },
            error: function (xhr) {
                let response = JSON.parse(xhr.responseText);
                if (response.isLogin && !response.isLogin) {
                    window.location.href = "/account/login"; // Chuyển hướng đến trang đăng nhập
                } else {
                    alert(response.message);
                }
            }
        });
    });
    // remove item wish lish



    // Gọi API khi trang tải xong
    getCartItemCount();


    // show modal cart
    /*==================================================================
    [ Cart ]*/
    $('.js-show-cart').on('click', function () {
        getModalCart();
    });

    $('.js-hide-cart').on('click', function () {
        $('.js-panel-cart').removeClass('show-header-cart');
    });

    function getModalCart() {
        $.ajax({
            url: "/cart/cart-items",
            type: "GET",
            success: function (response) {
                if (response && response.data) {
                    let cartitems = response.data;

                    let modalCartWrapItems = $(".header-cart-wrapitem");
                    let totalCartPrice = 0;

                    modalCartWrapItems.empty();
                    $.each(cartitems, function (index, item) {
                        console.log(item);
                        let itemc = `
                            <li class="header-cart-item flex-w flex-t m-b-12">
					            <div class="header-cart-item-img"
                                    data-productsku="${item.productSku}" data-variantsku="${item.variantSku}" 
                                    data-price="${item.price}" data-quantity="${item.quantity}"    
                                >

                                    <img src="${item.productImage}" alt="IMG">
					            </div>

					            <div class="header-cart-item-txt p-t-8">
						            <a href="/product/product-detail/${item.productSlug}" class="header-cart-item-name m-b-10 hov-cl1 trans-04">
							            ${item.productName}
						            </a>
                                    <span class="fs-12">
								        Color: ${item.color}
								        <br />
								        Size: ${item.size}
							        </span>

						            <span class="header-cart-item-info">
							            ${item.quantity + " x " + item.price.toLocaleString("vi-VN") + "đ"}
						            </span>
					            </div>
				            </li>
                        `
                        modalCartWrapItems.append(itemc);
                        totalCartPrice += (parseFloat(item.price) * parseInt(item.quantity));
                    });

                    $(".header-cart-total").text(`Total: ${totalCartPrice.toLocaleString("vi-VN") + "đ"}`);
                    $(".header-cart-total").data("total", totalCartPrice).attr("data-total", totalCartPrice);

                    $('.js-panel-cart').addClass('show-header-cart');
                }
            },
            error: function (xhr) {
                if (xhr.status === 401) {  // Kiểm tra nếu API trả về 401
                    alert("Bạn chưa đăng nhập. Vui lòng đăng nhập để tiếp tục.");
                    window.location.href = "/account/login?returnUrl=" + encodeURIComponent(window.location.pathname + window.location.search);
                } else {
                    alert(xhr.responseJSON?.message || "Lỗi khi xem giỏ hàng!");
                }
            }
        });
    }

    // remove item modal cart
    $(document).on("click", ".header-cart-item-img", function () {
        let productSku = $(this).data("productsku");
        let variantSku = $(this).data("variantsku");
        let price = parseFloat($(this).data("price"));
        let quantity = parseInt($(this).data("quantity"));
        removeItemModalCart($(this), productSku, variantSku, price, quantity);
    });
    function removeItemModalCart(button, productSku, variantSku, price, quantity) {
        $.ajax({
            url: "/cart/remove-from-cart",
            type: "POST",
            data: { productSku: productSku, variantSku: variantSku },
            success: function (response) {
                if (response.success) {

                    let li = button.closest(".header-cart-item");
                    li.fadeOut(300, function () {
                        $(this).remove();
                        updateTotalModalCartPrice(price, quantity);
                    });

                } else {
                    alert("Lỗi khi xóa sản phẩm");
                }
            }
        });
    }
    // Cập nhật tổng tiền sử dụng data-total
    function updateTotalModalCartPrice(price, quantity) {
        let totalElement = $(".header-cart-total");
        let currentTotal = parseFloat(totalElement.data("total")) || 0;
        let newTotal = currentTotal - (price * quantity);

        // Cập nhật lại data-total
        totalElement.data("total", newTotal);

        // Cập nhật nội dung hiển thị
        if (newTotal <= 0) {
            totalElement.text("Total: 0đ");
        } else {
            totalElement.text(`Total: ${newTotal.toLocaleString("vi-VN")}đ`);
        }
    }

    // show modal cart


    fetchProductVariants();

    reinitMagnificPopup();

    reinitializeSlick();


    // send message contact
    function sendMessageContact() {

        const notyf = new Notyf({
            position: { x: 'right', y: 'top' }, // Đặt thông báo ở góc trên bên phải
            duration: 3000,
            dismissible: true
        });


        let emailInput = $("#email-contact");
        let fullnameInput = $("#fullname-contact");
        let messageInput = $("#message-contact");

        // Lấy dữ liệu từ form
        let email = emailInput.length ? emailInput.val().trim() : null;
        let fullname = fullnameInput.length ? fullnameInput.val().trim() : null;
        let message = messageInput.val().trim(); // Tin nhắn luôn có
        // Nếu input tồn tại và có giá trị thì mới kiểm tra
        if (email !== null && email === "") {
            alert("Please enter your email address.");
            return;
        }

        if (fullname !== null && fullname === "") {
            alert("Please enter your full name.");
            return;
        }

        // Kiểm tra tin nhắn bắt buộc nhập
        if (!message) {
            alert("Please enter your message.");
            return;
        }


        // Gửi AJAX
        $.ajax({
            url: "/contact/send-message",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                email: email,
                fullname: fullname,
                message: message
            }),
            success: function (response) {
                //alert("Message sent successfully: " + response);
                // Reset form sau khi gửi thành công
                $("#email-contact").val('');
                $("#fullname-contact").val('');
                $("#message-contact").val('');
                notyf.success(response);
            },
            error: function (xhr, status, error) {
                notyf.error(xhr.responseText);
            }
        });
    }
    $("#btn-sendmessage-contact").on('click', function (e) {
        e.preventDefault();
        sendMessageContact();
    });
    // send message contact




    // api address

    $.getJSON('https://esgoo.net/api-tinhthanh/1/0.htm', function (data_tinh) {
        if (data_tinh.error == 0) {
            $.each(data_tinh.data, function (key_tinh, val_tinh) {
                $("#province").append('<option value="' + val_tinh.id + '">' + val_tinh.full_name + '</option>');
            });
            $("#province").change(function (e) {
                var idtinh = $(this).val();
                //Lấy quận huyện
                $.getJSON('https://esgoo.net/api-tinhthanh/2/' + idtinh + '.htm', function (data_quan) {
                    if (data_quan.error == 0) {
                        $("#district").html('<option value="0">Quận Huyện</option>');
                        $("#ward").html('<option value="0">Phường Xã</option>');
                        $.each(data_quan.data, function (key_quan, val_quan) {
                            $("#district").append('<option value="' + val_quan.id + '">' + val_quan.full_name + '</option>');
                        });
                        //Lấy phường xã
                        $("#district").change(function (e) {
                            var idquan = $(this).val();
                            $.getJSON('https://esgoo.net/api-tinhthanh/3/' + idquan + '.htm', function (data_phuong) {
                                if (data_phuong.error == 0) {
                                    $("#ward").html('<option value="0">Phường Xã</option>');
                                    $.each(data_phuong.data, function (key_phuong, val_phuong) {
                                        $("#ward").append('<option value="' + val_phuong.id + '">' + val_phuong.full_name + '</option>');
                                    });
                                }
                            });
                        });

                    }
                });
            });

        }
    });

    function updateFullAddress() {
        let province = $("#province option:selected").text();
        let district = $("#district option:selected").text();
        let ward = $("#ward option:selected").text();
        let detailAddress = $("input[name='DetailAddress']").val().trim();

        // Loại bỏ giá trị mặc định không hợp lệ
        if (province === "Chọn tỉnh/thành...") province = "";
        if (district === "Chọn quận/huyện...") district = "";
        if (ward === "Chọn phường/xã...") ward = "";

        // Ghép các phần có giá trị thành địa chỉ đầy đủ
        let fullAddress = [detailAddress, ward, district, province]
            .filter(part => part !== "")
            .join(", ");

        $("#fullAddress").val(fullAddress);
    }

    // Gán sự kiện thay đổi cho dropdown và input
    $("#province, #district, #ward, input[name='DetailAddress']").on("change keyup", updateFullAddress);
});














