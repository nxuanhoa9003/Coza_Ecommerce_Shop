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
        });
    });

    $(document).on("click", ".js-hide-modal1", function () {
        $('.js-modal1').removeClass('show-modal1');
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

            if (minVal > maxVal - 50000) {
                minPrice.value = maxVal - 50000;
                minVal = maxVal - 50000;
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
                        <div class="block2-txt flex-w flex-t p-t-14">
                            <div class="block2-txt-child1 flex-col-l">
                                <a href="/product/product-detail/${product.slug}" class="stext-104 cl4 hov-cl1 trans-04 js-name-b2 p-b-6">
                                    ${product.Title}
                                </a>
                                <span class="stext-105 cl3">${basePrice}</span>
                            </div>
                            <div class="block2-txt-child2 flex-r p-t-3">
                                <a href="#" class="btn-addwish-b2 dis-block pos-relative js-addwish-b2">
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


    /*add to cart*/
    $(document).on("click", ".btn-add-to-cart", function () {
        let Id = $("#p-sku").val();
        let size = $("#selected-size").val();
        let color = $("#selected-color").val();
        let quantity = $("#quantity").val();
        let variantSku = $("#v-sku").val();
        alert(`Id: ${Id}\nSize: ${size}\nColor: ${color}\nQuantity: ${quantity}\nSKU: ${variantSku}`);

    });

    /*add to cart*/


    fetchProductVariants();

    reinitMagnificPopup();

    reinitializeSlick();

});











