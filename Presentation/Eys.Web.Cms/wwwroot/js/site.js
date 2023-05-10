$(document).ready(function () {
    // $.fn.dataTable.moment( 'HH:mm:ss DD/MM/YYYY' );

    window.onerror = function (error, url, line) {
        $('body').loading('stop');
        $("body").fadeTo("slow", 1);
    };
});
// $(document).on('click', '.ActionSpinnerButton', function () {
//     $(this).attr("disabled", true);
// });
$(document).on({
    ajaxStart: function () {
        $('body').loading('start');
        $("body").fadeTo("slow", 0.90);
    },
    ajaxStop: function () {
        $('body').loading('stop');
        $("body").fadeTo("slow", 1);
    }
});
var JqueryDataTable = {
    getResarchDataTable(tableName, length = 10) {
        var table = $('#' + tableName).DataTable({
            "order": [],
            "dom": '<"pull-right"f><"pull-right"i>tip',
            "pageLength": length,
            "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
            "contentType": "application/json; charset=utf-8",
            "bFilter": true,
            "responsive": true,
            "language": {
                "decimal": "",
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
                "aria": {
                    "sortAscending": ": artan sütun sıralamasını aktifleştir",
                    "sortDescending": ": azalan sütun sıralamasını aktifleştir",
                }
            }
        });
        return table;
    },
    getPermissionDataTable(tableName, length = 10) {
        var table = $('#' + tableName).DataTable({
            "order": [],
            "dom": '<"pull-left"f><"pull-right"l>tip',
            "pageLength": length,
            "scrollX": true,
            "scrollCollapse": true,
            "fixedColumns": {
                rightColumns: 1,
                scrollX: true,
            },
            "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
            "contentType": "application/json; charset=utf-8",
            "language": {
                "decimal": "",
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
                "aria": {
                    "sortAscending": ": artan sütun sıralamasını aktifleştir",
                    "sortDescending": ": azalan sütun sıralamasını aktifleştir",
                }
            }
        });
        return table;
    },
    getNoOrderDataTable(tableName, length = 10) {
        var table = $('#' + tableName).DataTable({
            "order": [],
            "dom": '<"pull-left"f><"pull-right"l>tip',
            "pageLength": length,
            "columnDefs": [{ orderable: false, targets: [1, 2, 3] }],
            "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
            "contentType": "application/json; charset=utf-8",
            "language": {
                "decimal": "",
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
                "aria": {
                    "sortAscending": ": artan sütun sıralamasını aktifleştir",
                    "sortDescending": ": azalan sütun sıralamasını aktifleştir",
                }
            }
        });
        return table;
    },
    getServerSideDataTable(tableName, length = 10) {
        var table = $('#' + tableName).DataTable({
            "order": [],
            "dom": '<"pull-left"f><"pull-right"l>tip',
            "pageLength": length,
            "columnDefs": [{ orderable: false, targets: [1, 2, 3] }],
            "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "All"]],
            "contentType": "application/json; charset=utf-8",
            "language": {
                "decimal": "",
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
                "aria": {
                    "sortAscending": ": artan sütun sıralamasını aktifleştir",
                    "sortDescending": ": azalan sütun sıralamasını aktifleştir",
                }
            }
        });
        return table;
    },

}
var Select2Settings = {
    AllSelect2: function () {
        $('.js-states').each(function (index, value) {
            $("#" + value.id).select2();
        });
    }
}
var ajaxMethods = {
    post: function (url, parameters, callback) {
        var roleNumber = "";
        if (document.getElementById("roleForAlertPopup") != null) {
            roleNumber = document.getElementById("roleForAlertPopup").value;
        }
        $.post(url, parameters, callback).fail(function (xhr, status, error) {
            if (status == "error") {

                if (xhr.status == 403 && (roleNumber == "4" || roleNumber == "6")) {
                    swal({
                        title: "",
                        text: Lang.Clinical("AccessDeniedMsg"),
                    });
                }
                else if (xhr.status == 403) {
                    alert("Error : Bu kaynağa erişiminiz yoktur. Lütfen sistem yöneticisi ile görüşün.");
                }
                else {
                    //toastrNotifications.showError();
                    console.log(error)
                }
            }
        });


    },
    get: function (url, parameters, callback) {
        return $.get(url, parameters, callback).fail(function (xhr, status, error) {
            if (status == "error") {
                if (xhr.status == 403) {
                    alert("Error : Bu kaynağa erişiminiz yoktur. Lütfen sistem yöneticisi ile görüşün.");
                }

                else {
                    toastrNotifications.showError();
                }
            }
        });
    },
    load: function (element, url, parameters, callback) {
        $(element).load(url, parameters, callback);
    },
    postWithJson: function (url, data, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            traditional: true,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: successCallback,
            error: errorCallback
        });
    },
    postSerialize: function (url, data, successCallback, errorCallback) {
        $.ajax({
            type: "POST",
            url: url,
            data: data,
            traditional: true,
            success: successCallback,
            error: errorCallback
        });
    },
    dialogLoad: function (element, url, options) {
        $(element).load(url).dialog(options).dialog('open');
    },
    setTimezoneName: function () {
        var tmZone = ajaxMethods.getTimezoneOffset();
        var tmZoneName = ajaxMethods.getTimezoneName();
        var ctime = ajaxMethods.getCookie("jstimeoffset");
        if (tmZone != ctime) {
            ajaxMethods.setCookie("jstimeoffset", tmZone, 7);
            ajaxMethods.setCookie("jstimezone", btoa(tmZoneName), 7);
        }
    },
    getTimezoneName: function () {
        const today = new Date();
        const short = today.toLocaleDateString(undefined);
        const full = today.toLocaleDateString(undefined, { timeZoneName: 'long' });
        const shortIndex = full.indexOf(short);
        if (shortIndex >= 0) {
            const trimmed = full.substring(0, shortIndex) + full.substring(shortIndex + short.length);
            return trimmed.replace(/^[\s,.\-:;]+|[\s,.\-:;]+$/g, '');
        } else {
            return full;
        }
    },
    getTimezoneOffset: function () {
        return new Date().getTimezoneOffset();
    },
    setCookie: function (name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    },
    getCookie: function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) == ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
        }
        return null;
    }
};
var modalOperation = {
    title: function (titleName) {
        return $("#modalTitle").html("<center>" + titleName + "</center>");
    },
    multipletitle: function (titleName) {
        return $("#multiplemodalTitle").html("<center>" + titleName + "</center>");
    },
    buttonName: function (btnName) {
        return $("#btnSaveModal").html(btnName);
    },
    multiplebuttonName: function (btnName) {
        return $("#btnMultipleSaveModal").html(btnName);
    },
    xlbuttonName: function (btnName) {
        return $("#btnXlSaveModal").html(btnName);
    },
    btnSaveHide: function () {
        return $("#btnSaveModal").css("display", "none");
    },
    btnSaveShow: function () {
        return $("#btnSaveModal").css("display", "");
    },
    btnMultipleSaveShow: function () {
        return $("#btnMultipleSaveModal").css("display", "");
    },
    btnXlSaveShow: function () {
        return $("#btnXlSaveModal").css("display", "");
    },
    btnSaveDisabled: function () {
        return $("#btnSaveModal").attr("disabled", true);
    },
    btnSaveActive: function () {
        return $("#btnSaveModal").removeAttr("disabled");
    },
    buttonClickEvent: function (functionName) {
        return $('#btnSaveModal').attr('onClick', functionName);
    },
    multiplebuttonClickEvent: function (functionName) {
        return $('#btnMultipleSaveModal').attr('onClick', functionName);
    },
    xlbuttonClickEvent: function (functionName) {
        return $('#btnXlSaveModal').attr('onClick', functionName);
    },
    body: function (result) {
        var resultHtml = '<div class="row"><div class="col-md-12 zeroSidePadding">' + result + '</div></div>';
        return $("#modalBody").html(resultHtml);
    },
    multiplebody: function (result) {
        var resultHtml = '<div class="row"><div class="col-md-12 zeroSidePadding">' + result + '</div></div>';
        return $("#multimodalBody").html(resultHtml);
    },
    extraBigbody: function (result) {
        var resultHtml = '<div class="row"><div class="col-md-12 zeroSidePadding">' + result + '</div></div>';
        return $("#extraLargeModalBody").html(resultHtml);
    },
    show: function () {

        return $("#appModal").modal({ backdrop: 'static', keyboard: false });
    },
    multipleshow: function () {

        return $("#multipleappModal").modal({ backdrop: 'static', keyboard: false });
    },
    extraBigshow: function () {

        return $("#extraBigModal").modal({ backdrop: 'static', keyboard: false });
    },
    hide: function () {
        return $("#appModal").modal("hide");
    },
    closeRemoveModal: function (elm) {
        const theModal = $(elm).closest('.modal');
        var test = theModal[0].id;

        $("#" + theModal[0].id + "-dialog").html("");
        $("#" + theModal[0].id).css("display", "none");
    },
    largeModal: function () {
        $("#modalDialog").removeClass('modal-md');
        $("#modalDialog").removeClass('modal-lg');
        return $("#modalDialog").addClass("modal-dialog modal-lg");
    },
    xlargeModal: function () {
        $("#modalDialog").removeClass('modal-md');
        $("#modalDialog").removeClass('modal-lg');
        return $("#modalDialog").addClass("modal-dialog modal-xl");
    },
    smallModal: function () {
        $("#modalDialog").removeClass('modal-lg');
        $("#modalDialog").removeClass('modal-md');
        return $("#modalDialog").addClass("modal-dialog modal-md");
    },
    mediumModal: function () {
        $("#modalDialog").removeClass('modal-lg');
        return $("#modalDialog").addClass("modal-dialog modal-md");
    },
    // modalSize: function (width, heigth) {
    // $("#modalDialog").css({ "width": width, "height": heigth });
    // $("#modalBody").css({ "width": width - 20, "height": heigth });
    // },
    btnCloseEvent: function () {
        $(".tempElement").remove();
    },
    footerHide: function () {
        $("#modalFooter").hide();
    },
    footerShow: function () {
        $("#modalFooter").show();
    },
    heliosModal: function (title, body) {
        const customModal = new Modal(title, body, 'lg');
        customModal.show();
    },
    close: function (elm) {
        const theModal = $(elm).closest('.modal');
        theModal.hide();
    },
    modalShowSmall: function (title, btnName, btnEvent, body, callback) {
        this.title(title);
        if ((btnName != null && btnEvent != null) || (btnName != '')) {
            this.buttonName(btnName);
            this.buttonClickEvent(btnEvent);
            this.btnSaveShow();

        } else {
            this.btnSaveHide();
        }
        this.smallModal();
        this.body(body);
        this.show();
        if (callback) {
            callback();
        }
        //this.modalSize("50%", "40%");
    },

    modalShowLarge: function (title, btnName, btnEvent, body, width, heigth, callback, showScroll = false,) {

        if (showScroll) {
            $("#modalBody").addClass("scrollStyle");
            $("#modalBody").css("overflow-y", "scroll");
            $("#modalBody").css("background-color", "white");
            $("#modalBody").css("max-height", "490px");
        }
        this.title(title);
        if (btnName != null) {
            this.buttonName(btnName);
            this.buttonClickEvent(btnEvent);
            this.btnSaveShow();
            this.footerShow();
        } else {

            this.btnSaveHide();
        }
        if (heigth == null) {
            this.heigth = "40%";
        }
        this.body(body);
        this.largeModal();
        this.show();
        $('#appModal').modal('show');
        if (callback) {
            callback();
        }
        // this.modalSize(width, heigth);
    },
    multiModalShowLarge: function (title, btnName, btnEvent, body, width, heigth, callback, showScroll = false,) {

        if (showScroll) {
            $("#multimodalBody").addClass("scrollStyle");
            $("#multimodalBody").css("overflow-y", "scroll");
            $("#multimodalBody").css("background-color", "white");
            $("#multimodalBody").css("max-height", "490px");
        }
        this.multipletitle(title);
        if (btnName != null) {
            this.multiplebuttonName(btnName);
            this.multiplebuttonClickEvent(btnEvent);
            this.btnXlSaveShow();
            this.footerShow();
        } else {

            this.btnSaveHide();
        }
        if (heigth == null) {
            this.heigth = "40%";
        }
        this.multiplebody(body);
        this.largeModal();

        if (callback) {
            callback();
        }
        // this.modalSize(width, heigth);
    },
    modalShowXLarge: function (title, btnName, btnEvent, body, width, heigth, callback, showScroll = false) {

        if (showScroll) {
            $("#extraLargeModalBody").addClass("scrollStyle");
            $("#extraLargeModalBody").css("overflow-y", "scroll");
            $("#extraLargeModalBody").css("background-color", "white");
            $("#extraLargeModalBody").css("max-height", "490px");
        }
        this.title(title);
        if (btnName != null) {
            this.xlbuttonName(btnName);
            this.xlbuttonClickEvent(btnEvent);
            this.btnSaveShow();
            this.footerShow();
        } else {

            this.btnSaveHide();
        }
        if (heigth == null) {
            this.heigth = "40%";
        }
        this.extraBigbody(body);
        this.extraBigshow();
        $('#extraBigModal').modal('show');

        if (callback) {
            callback();
        }
        // this.modalSize(width, heigth);
    },
    modalShowMedium: function (title, btnName, btnEvent, body, width, heigth, callback) {
        this.title(title);
        if (btnName != null) {
            this.buttonName(btnName);
            this.buttonClickEvent(btnEvent);
            this.btnSaveShow();
        } else {

            this.btnSaveHide();
        }

        this.body(body);
        this.mediumModal();
        this.show();
        if (heigth == null) {
            heigth = "40%";
        }
        if (callback) {
            callback();
        }
    },
};
var Lang = {
    Base: function (name) {
        if ($("#_resBase").val()) {
            var jsonParse = JSON.parse($("#_resBase").val());
            return jsonParse[name];
        } else {
            return "";
        }
    },
    ChangeLang: function (code) {
        $.ajax({
            url: "/Home/ChangeLangue",
            type: "GET",
            data: { code: code },
            success: function () {
                window.location.reload();
            }
        });
    }

};
var Mode = {
    GetTemplate: function () {
        var getNowTemplate = $("#html").attr("data-layout-mode");
        var getCookieTemplate = "";
        if (getNowTemplate == "light")
            getCookieTemplate = "dark";
        else
            getCookieTemplate="light"
       
        Cookies.set('template', getCookieTemplate)

    },
    ChangeTemplate: function () {
        $("#html").attr("data-layout-mode", Cookies.get('template'))
    }
}
