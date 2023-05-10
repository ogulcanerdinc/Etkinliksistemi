var Auth = {
    Login: function () {
        //$('body').loading('start');
        var model = {
            Email: $("#Email").val(),
            Password: $("#Password").val()
        };
        $.ajax({
            url: "/Auth/Login",
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
                    setTimeout(function () {
                        window.location = "/";

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
}