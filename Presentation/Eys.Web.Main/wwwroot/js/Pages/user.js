var User = {
    PasswordUpdate: function () {
        //$('body').loading('start');
        var model = {
            Id: $("#Id").val(),
            Password: $("#Password").val(),
            NewPassword: $("#NewPassword").val()
        };
        $.ajax({
            url: "/User/ChangePassword",
            type: "POST",
            data: {
                model: model
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (!response.isValid) {
                    var message = "";
                    $.each(response.validErrors, function (i, item) {
                        message += item.message + "<br>";
                    });
                    Swal.fire({
                        title: 'Hata!',
                        html: message,
                        icon: 'error',

                    });
                }
                else if (response.isSuccess) {
                    Swal.fire({
                        title: "Başarılı",
                        text: response.message,
                        icon: 'success'
                    })
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
    ProfileUpdate: function () {
        //$('body').loading('start');
        var model = {
            Id: $("#Id").val(),
            Name: $("#Name").val(),
            SurName: $("#Surname").val(),
            Email: $("#Email").val(),
            Password: "a"
        };
        $.ajax({
            url: "/User/UserUpdate",
            type: "POST",
            data: {
                model: model
            },
            headers: {
                RequestVerificationToken:
                    $('input:hidden[name="__RequestVerificationToken"]').val()
            },
            success: function (response) {

                if (!response.isValid) {
                    var message = "";
                    $.each(response.validErrors, function (i, item) {
                        message += item.message + "<br>";
                    });
                    Swal.fire({
                        title: 'Hata!',
                        html: message,
                        icon: 'error',

                    });
                }
                else if (response.isSuccess) {
                    Swal.fire({
                        title: "Başarılı",
                        html: response.message + "</br>Giriş sayfasına yönlendiriliyorsunuz.",
                        icon: 'success'
                    })
                    setTimeout(function () {
                        window.location = "/Auth/Login";

                    }, 3000);
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
    GetEventsList: function () {
        var table = null;
        if ($.fn.DataTable.isDataTable('#EventsTable')) {
            $('#EventsTable').DataTable().destroy();
        }
        var column = [];
        column.push({
            "data": "eventImage",
            "render": function (data, type, row) {
                var image = "";
                if (data != null) {

                    var fileLink = apis + "/image/public/" + data.uploadedImage.fileName;
                    image = `<img src=${fileLink} style=" height: 100px; width: 200px; ">`;
                }

                return image;
            }
        });
        column.push({ "data": "eventName" });
        column.push({ "data": "eventAdress" });
        column.push({
            "data": "eventStartDate",
            "render": function (data, type, row) {


                return row.eventStartDate + "-" + row.eventEndDate;
            }
        });


        table = $('#EventsUserTable').DataTable({
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
                url: '/Events/EventsUserListQuery',
                type: 'POST',
                dataType: "json",
                contentType: "application/json",
                data: function (d) {
                    return JSON.stringify(d);
                },
            },
            "columns": column,
        });


        $('#EventsTable  tbody').on('click', 'tr', function () {
            var getId = table.row(this).data();
            window.location.href = "/Events/EventDetail/" + getId.id;
        });

    },
    EventsFilter: function () {
        Events.GetEventsList().draw();
    },
}