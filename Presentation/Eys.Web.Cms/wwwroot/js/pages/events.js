var Events = {
    GetEvents: function (Id) {
        $.ajax({
            url: "/Events/EventsEdit",
            type: "GET",
            data: { Id: Id },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {
                modalOperation.modalShowLarge("Etkinlik", '<i class="bx bx-plus label-icon align-middle fs-16 me-2"></i>' + "Kaydet", "Events.AddEvents()", response, "60%");

            }
        });
    },
    AddEvents: function () {
        var formData = new FormData();
        $('body').loading('start');
        var fileupload = $("#formFile").get(0);

        $("#frmEvent :input").each(function (x, y) {
            var inputs = $(y);
            if (inputs[0].type == "file" && inputs[0].id == "formFile") {
                var files = fileupload.files;
                for (var i = 0; i < files.length; i++) {
                    formData.append('Images', files[i]);
                }
            }
            else {
                var dd = $(y).attr("name");
                formData.append(dd, $(y).val());
            }

        });
        formData.append("EventDescription", $('#EventDescription').summernote("code"));
        formData.append("EventRules", $('#EventRules').summernote("code"));
        $.ajax({
            url: "/Events/EventsEdit",
            type: "POST",
            data: formData,
            contentType: false,
            processData: false,
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
                        Events.GetEventsList();
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
    DeleteEvents: function (Id) {
        Swal.fire({
            title: "Kayıt Silinecek",
            text: "Emin Misiniz?",
            showCancelButton: true,
            confirmButtonText: "Evet",
            cancelButtonText: "Hayır",
        }).then((result) => {
            if (result.isConfirmed) {
                ajaxMethods.post("/Events/EventsDelete", { Id: Id }, function (response) {
                    if (response.isSuccess) {
                        Swal.fire({
                            title: "Başarılı",
                            text: response.message,
                            icon: 'success'
                        });
                        setTimeout(function () {
                            Events.GetEventsList();
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
    GetEventsList: function () {
        var table = null;
        if ($.fn.DataTable.isDataTable('#EventsTable')) {
            $('#EventsTable').DataTable().destroy();
        }
        var column = [];
        column.push({ "data": "eventName" });
        column.push({ "data": "category.categoryName" });
        column.push({ "data": "eventShortDescription" });
        column.push({ "data": "eventStartDate" });
        column.push({ "data": "eventEndDate" });
        column.push({ "data": "city.name" });
        column.push({ "data": "eventAdress" });
        column.push(
            {
                "data": "id",
                "render": function (data, type, row) {
                    var editButton = `<a type="button" href=/Events/EventsEdit?Id=${data}  style="cursor:pointer;" class="btn btn-primary btn-icon waves-effect waves-light"><i class="bx bxs-edit-alt"></i></a>`;
                    var deleteButton = `<button type="button" style="cursor:pointer;margin-left: 5px;" class="btn btn-danger btn-icon waves-effect waves-light" onclick=Events.DeleteEvents('${data}') ><i class="bx bxs-trash"></i></button>`;

                    return editButton + deleteButton;
                }
            });


        table = $('#EventsTable').DataTable({
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
                url: '/Events/EventsListQuery',
                type: 'POST',
                dataType: "json",
                contentType: "application/json",
                data: function (d) {

                    d.EventsName = $("#EventsNameSearch").val();
                    return JSON.stringify(d);
                },
            },
            "columns": column,
        });



    },
    EventsFilter: function () {
        Events.GetEventsList().draw();
    },
    DeleteImage: function (Id) {
        Swal.fire({
            title: "Kayıt Silinecek",
            text: "Emin Misiniz?",
            showCancelButton: true,
            confirmButtonText: "Evet",
            cancelButtonText: "Hayır",
        }).then((result) => {
            if (result.isConfirmed) {
                ajaxMethods.post("/Events/EventsImage", { Id: Id }, function (response) {
                    if (response.isSuccess) {
                        Swal.fire({
                            title: "Başarılı",
                            text: response.message,
                            icon: 'success'
                        });
                        setTimeout(function () {
                            $("#" + Id + "_area").attr("style", "display:none");
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
}