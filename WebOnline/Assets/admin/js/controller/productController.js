var product = {
        init: function () {
            product.registerEvents();
        },
    registerEvents: function () {
            $('.btn-active').off('click').on('click', function (e) {
                e.preventDefault();
                var btn = $(this);
                var id = btn.data('id');// lấy thuột tính trước dât sau là id vd data-id trong class
                $.ajax({
                    url: "/Admin/Product/ChangeStatus",
                    data: { id: id },//id truyền vào
                    dataType: "json",
                    type: "POST",
                    success: function (response) {
                        console.log(response);
                        if (response.status == true) {//trạng thái trả về true
                            btn.text('Kích hoạt');
                        }
                        else {
                            btn.text('Khoá');
                        }
                    }
                });
            });
            $('.btn-images').off('click').on('click', function (e) 
            {
                e.preventDefault();
                $('#imagesManage').modal('show');
                $('#hiProductID').val($(this).data('id'));
                product.loadImages();
            });

            $('#btnChooImages').off('click').on('click', function (e)
            {
                e.preventDefault();
                var finder = new CKFinder();
                finder.selectActionFunction = function (url)
                {
                    $('#imageList').append('<div style="float:left"><img src="' + url + '"width="100"/><a href="#"><i class="fa fa-times" class="btn-delImage"></i></a></div>');
                    $('.btn-delImage').off('click').on('click', function (e)
                    {
                        e.preventDefault();
                        $(this).parent().remove();
                    });
                };
            finder.popup(); 
            });

            $('#btnSaveImages').off('click').on('click', function () {
                var images = [];
                var id = $('#hiProductID').val();
                $.each($('#imageList img'), function (i, item)
                {
                    images.push($(item).prop('src'));
                })
                console.log(id);
                $.ajax
                ({
                    url: '/Admin/Product/SaveImages',
                    type: 'POST',
            
                    data: {
                        id:id,
                        images: JSON.stringify(images)
                    },
                    dataType: 'json',
                    success: function (response) {
                        if (response.status) {
                            $('#imagesManage').modal('hide');
                            $('#imageList').html('');
                            alert('Lưu thành công');
                        }
                        
                    }
                });
            });
    },
    loadImages: function () {
        $.ajax
            ({
                url: '/Admin/Product/LoadImages',
                type: 'GET',
                data: {
                    id: $('#hiProductID').val()
                },
                dataType: 'json',
                success: function (response) {
                        var data = response.data;
                        var html = '';
                        $.each(data, function (i, item) {
                            html += '<div style="float:left"><img src="' + item + '"width="100"/><a href="#"><i class="fa fa-times" class="btn-delImage"></i></a></div>'
                        });
                    $('#imageList').html(html);
                    product.registerEvents();
                }
            });
    }
}
product.init();
