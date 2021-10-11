
//Type Enum'ı
const typeEnum = {
    Category: "Category",
    Source: "Source",
    SourceContent: "SourceContent",
    RssGenerator: "RssGenerator"
};
const alertType = {
    Success: "success",
    Error:"error"
}
function RenderBodyClear() {
    $('#render_body').empty();
}
//Loader Başlatır
function StartLoader() {
    /*  $("#loader").delay(50).fadeIn();*/
    $("#loader").delay(50).fadeIn();

}
//Loader bitirir.
function StopLoader() {
    $("#loader").delay(50).fadeOut();
}
//BreadComb değiştirir
function ChangeBreadComb(title, text, image) {
    $('#breadcomb_image').attr("src", "/img/breadcomb/" + image);
    $('#breadcomb_header').text(title);
    $('#breadcomb_text').text(text);
}
//Insert button değiştirir
function ChangeInsertButton(type, text) {
    $('#insert_button').html("<i class='far fa-plus-square mr-2'></i><span>" + text + " Ekle</span > ");
    $('#insert_button').attr('onclick', "Add" + type+"View()");
}
//Report buton değiştirir
function ChangeReportButton(type) {
    $('#report').attr('onclick', "Report" + type+"()");
}


//Ekle formu kapat
function CloseAddView() {
    $('#add_view').fadeOut(1500, function () {
        $(this).remove();
    })
    $('#insert_button').show();
}
//Ekle formu inputları temizle
function ClearAddView() {
    $('#add_view input').val('');
    $('#add_view select').val(-1);
}

function SetAlertDialog(type,message,showOkButton=false,time=1500) {
    if (type = alertType.Success) {
        Swal.fire({
            position: 'top-end',
            icon: type,
            title: message.toString(),
            showConfirmButton: showOkButton,
            timer: parseInt(time)
        })
    }
    else {
        Swal.fire({
            position: 'top-end',
            icon: type,
            title: message.toString(),
            showConfirmButton: showOkButton,
            timer: parseInt(time)
        })

    }
}

function SetRequestError() {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Beklenmeyen bir hata oluştu !',
        confirmButtonText:'Tamam',
    });
}

$(document).ready(function () {
    toastr.options = {
        "closeButton": false,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
});


