$(document).ready(function () {
    $(document).on("click", ".quick-order-btn", function (e) {
        e.preventDefault();
        var productId = $(this).data("id");

        $.get("/quick-view-product/" + productId, function (data) {
            $("#quickProductModal").html(data);
            $('.js-modal1').addClass('show-modal1');

            reinitializeSlick();
            reinitMagnificPopup();
            reinitSelect2();
        });
    });

    $(document).on("click", ".js-hide-modal1", function () {
        $('.js-modal1').removeClass('show-modal1');
    });
});;


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
reinitMagnificPopup();
reinitSelect2();
function reinitSelect2() {
    $(".js-select2").each(function () {
        $(this).select2({
            minimumResultsForSearch: 20,
            dropdownParent: $(this).next('.dropDownSelect2')
        });
    })

    $(".js-select2").on("change", function () {
        var selectedSku = $(this).val();
        if (selectedSku) {
            fetchVariantDetails(selectedSku);
        }
    });

}

function fetchVariantDetails(sku) {
    $.ajax({
        url: "/GetVariantDetails", // Đường dẫn API
        type: "GET",
        data: { sku: sku },
        dataType: "json",
        success: function (response) {
            console.log(response);
            if (response.success) {
                $("#productPrice").text(response.data.price.toLocaleString() + "đ");
                $("#productStock").text("Stock: " + response.data.stock);
            }
        },
        error: function () {
            alert("Có lỗi xảy ra khi lấy dữ liệu.");
        }
    });
}



