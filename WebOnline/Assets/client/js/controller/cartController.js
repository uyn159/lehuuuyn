var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });
        $('#btnPayment').off('click').on('click', function () {
            window.location.href = "/thanh-toan";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.txtQuantity');//lấy giá trị từ lớp
            var cartList = [];//chứ danh sách
            $.each(listProduct, function (i, item) {
                cartList.push({//push từng đối tươngj
                    Quantity: $(item).val(),//models
                    Product: {
                        ID: $(item).data('id')
                    }
                });
            });

            $.ajax({//đẩy lên controller
                url: '/Cart/Update',
                data: { cartModel: JSON.stringify(cartList) },//thành 1 chuỗi
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {//cập nhật thành công
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $('#btnDeleteAll').off('click').on('click', function () {


            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });

        $('.btn-delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
    }
}
cart.init();