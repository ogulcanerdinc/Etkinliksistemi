var Category = {
    GetCategory: function (Id) {
        $.ajax({
            url: "/Category/CategoryEdit",
            type: "GET",
            data: { Id: Id },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                modalOperation.modalShowLarge("Kategori", '<i class="bx bx-plus label-icon align-middle fs-16 me-2"></i>' + "Kaydet", "Category.AddCategory()", response, "60%");

            }
        });
    },
    AddCategory: function () {
        var model = {
            Id: $("#Id").val(),
            CategoryName: $("#CategoryName").val()
        };
        $.ajax({
            url: "/Category/CategoryEdit",
            type: "POST",
            data: { model: model },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (!response.isValid) {

                    Swal.fire({
                        title: "Hata",
                        text: response.validErrors[0].message,
                        icon: 'error'
                    });
                }
                else if (response.isSuccess) {
                    Swal.fire({
                        title: "Başarılı",
                        text: response.message,
                        icon: 'success'
                    })
                    setTimeout(function () {
                        Category.GetCategoryList();
                        $(".btn-close").click();
                    }, 1000);
                }
                else {

                    Swal.fire({
                        title: "Hata",
                        text: response.message,
                        icon: 'error'
                    });
                }
            }
        });
    },
    DeleteCategory: function (Id) {
        Swal.fire({
            title: "Kayıt Silinecek",
            text: "Emin Misiniz?",
            showCancelButton: true,
            confirmButtonText: "Evet",
            cancelButtonText: "Hayır",
        }).then((result) => {
            if (result.isConfirmed) {
                ajaxMethods.post("/Category/CategoryDelete", { Id: Id }, function (response) {
                    if (response.isSuccess) {
                        Swal.fire({
                            title: "Başarılı",
                            text: response.message,
                            icon: 'success'
                        });
                        setTimeout(function () {
                            Category.GetCategoryList();
                            $(".btn-close").click();
                        }, 1200);
                    }
                    else {
                        Swal.fire({
                            title: "Hata",
                            text: response.message,
                            icon: 'error'
                        });
                    }
                });
            }
        });
    },
    GetCategoryList: function () {
        var table = null;
        if ($.fn.DataTable.isDataTable('#CategoryTable')) {
            $('#CategoryTable').DataTable().destroy();
        }
        var column = [];
        column.push({ "data": "categoryName" });
        column.push(
            {
                "data": "id",
                "render": function (data, type, row) {
                    var editButton = `<button type="button" onclick=Category.GetCategory('${data}')  style="cursor:pointer;" class="btn btn-primary btn-icon waves-effect waves-light"><i class="bx bxs-edit-alt"></i></button>`;
                    var deleteButton = `<button type="button" style="cursor:pointer;margin-left: 5px;" class="btn btn-danger btn-icon waves-effect waves-light" onclick=Category.DeleteCategory('${data}') ><i class="bx bxs-trash"></i></button>`;

                    return editButton + deleteButton;
                }
            });


        table = $('#CategoryTable').DataTable({
            "language": {
                "decimal": "",
                "processing": '<i class="fa fa-spinner fa-spin fa-2x fa-fw" style="border-style:none;border-width:0px;"></i><span class="sr-only">Yükleniyor...</span>',
                "emptyTable": 'Tabloda herhangi bir veri mevcut değil',
                "info": '_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor',
                "infoEmpty": 'Kayıt yok',
                "infoFiltered": '(_MAX_ kayıt içerisinden bulunan)',
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": 'Sayfada _MENU_ kayıt göster',
                "loadingRecords": "",
                "search": 'Ara:',
                "zeroRecords": "Eşleşen kayıt bulunamadı",
                "paginate": {
                    "first": "İlk",
                    "last": "Son",
                    "next": "Sonraki",
                    "previous": "Önceki",
                },
                "buttons": {
                    "collection": "Koleksiyon <span class=\"ui-button-icon-primary ui-icon ui-icon-triangle-1-s\"><\/span>",
                    "colvis": "Sütun görünürlüğü",
                    "colvisRestore": "Görünürlüğü eski haline getir",
                    "copySuccess": {
                        "1": "1 satır panoya kopyalandı",
                        "_": "%ds satır panoya kopyalandı"
                    },
                    "copyTitle": "Panoya kopyala",
                    "csv": "CSV",
                    "excel": "Excel",
                    "pageLength": {
                        "-1": "Bütün satırları göster",
                        "_": "%d satır göster"
                    },
                    "pdf": "PDF",
                    "print": "Yazdır",
                    "copy": "Kopyala",
                    "copyKeys": "Tablodaki veriyi kopyalamak için CTRL veya u2318 + C tuşlarına basınız. İptal etmek için bu mesaja tıklayın veya escape tuşuna basın."
                },
                "aria": {
                    "sortAscending": ": artan sütun sıralamasını aktifleştir",
                    "sortDescending": ": azalan sütun sıralamasını aktifleştir",
                }
            },
            "serverSide": true,
            "searching": true,
            "scrollX": true,
            "dom": 't,p,l,r,i,B', "responsive": true, "bAutoWidth": false,
            "buttons": ['excel', 'pdf',
            ],
            "retrieve": true,
            "pageLength": 50,
            "ajax": {
                url: '/Category/CategoryListQuery',
                type: 'POST',
                dataType: "json",
                contentType: "application/json",
                data: function (d) {

                    d.CategoryName = $("#CategoryNameSearch").val();
                    return JSON.stringify(d);
                },
            },
            "columns": column,
        });



    },
    CategoryFilter: function () {
        Category.GetCategoryList().draw();
    },
}