function LoadMailMenu(callback) {
    StartLoader();
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "list_mail_container");
    $('#render_body').append(contentDiv);
    $('#list_mail_container').load('Mail/GetMailMenu', function () {
        StopLoader();
        if (callback) {
            callback();
        }
    });
};
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.Mail, "Mail");
    ChangeBreadComb("Mail", "Maillerinizi okuyabilir, bir mail şablonu oluşturabilir, mailin konusunu ve içeriğini oluşturabilir ve yeni bir mail gönderebilirsiniz.","mail.jpg");
    ChangeReportButton(typeEnum.Mail)
    LoadMailMenu(LoadSentMails);
    $('#insert_button span').text("Yeni Mail");
    StopLoader();
});

function LoadAccountSetting() {
    StartLoader();
    $('#mailContent').load('Mail/GetAccountSetting', function () {
        ApplyDisabled();
        StopLoader();
    });
}
function ApplyDisabled() {
    $('#account_mail').attr("disabled", "disabled");
    $('#account_password').attr("disabled", "disabled");
    $('#account_mail').css("color", "black");
    $('#account_password').css("color", "black");
    $('#mail_account_savebutton').attr("disabled", "disabled");
}
function ApplyEnabled() {
    $('#account_mail').removeAttr("disabled");
    $('#account_password').removeAttr("disabled");
    $('#account_mail').css("color", "white");
    $('#account_password').css("color", "white");
    $('#mail_account_savebutton').removeAttr("disabled");
}

function EnableCheck() {
    if ($('#edit_check').is(':checked'))
        ApplyEnabled();
    else
        ApplyDisabled();
};
function CreateAuthenticate() {
    var mail = $("#account_mail").val();
    var password = $("#account_password").val();
    if (!mail || !password) {
        toastr.error('Lütfen zorunlu (*) alanları doldurunuz..');
        return;
    }
    StartLoader();
    $.ajax({
        type: "POST",
        url: "Mail/CreateAuthenticate",
        async: false,
        data: { 'AccountMailAddress': mail, 'AccountMailPassword': password },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla eklendi..');
                LoadAccountSetting();
                StopLoader();
            }
            else {
                toastr.error('Kayıt eklenirken hata oluştu !');
                StopLoader();
            }

        },
        error: function () {
            SetRequestError();
            StopLoader();
        }
    });
};
function LoadMailOptions() {
    StartLoader();
    $('#mailContent').load('Mail/GetMailOptions', function () {
        $('#summerNote').summernote();
        $('#mail_options_sendbutton').hide();
        $('#panel_head').html(" <h3><i class='fas fa-cogs' style='margin-right:3px;'> Mail Ayarları</h3>");
        StopLoader();
    });
}


function CreateMailOptions() {
    var to = $("#mail_to").val();
    var subject = $("#mail_subject").val();
    if (!to || !subject) {
        toastr.error('Lütfen zorunlu (*) alanları doldurunuz..');
        return;
    }
    StartLoader();
    var htmlContent = $("#summerNote").val();
    $.ajax({
        type: "POST",
        url: "Mail/CreateMailOptions",
        async: false,
        data: { 'MailDefaultHTMLContent': htmlContent, 'MailDefaultSubject': subject, 'MailDefaultTo': to },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla eklendi..');
                LoadMailOptions();
                StopLoader();
            }
            else {
                toastr.error('Kayıt eklenirken hata oluştu !');
                StopLoader();
            }
        },
        error: function () {
            SetRequestError();
            StopLoader();
        }
    });
};

function AddMailView() {
    StartLoader();
    $('#mailContent').load('Mail/GetSendMailView', function () {
        $('#summerNote').summernote();
        $('#mail_options_savebutton').hide();
        $('#panel_head').html(" <h3><i class='fas fa-plus-square' style='margin-right:3px;'> Yeni Mail</h3>");
        StopLoader();
    });
}

function SendMailDefault() {
    var to = $("#mail_to").val();
    var subject = $("#mail_subject").val();
    if (!to || !subject) {
        toastr.error('Lütfen zorunlu (*) alanları doldurunuz..');
        return;
    }
    StartLoader();
    var htmlContent = $("#summerNote").val();
    $.ajax({
        type: "POST",
        url: "Mail/SendMailDefault",
        async: false,
        data: { 'MailDefaultHTMLContent': htmlContent, 'MailDefaultSubject': subject, 'MailDefaultTo': to },
        success: function (data) {
            if (data > 0) {
                toastr.success('Mail başarıyla gönderildi.');
                LoadMailOptions();
                StopLoader();
            }
            else {
                toastr.error('Mail gönderilirken hata oluştu! Arşive kaydedildi.');
                StopLoader();
            }

        },
        error: function () {
            SetRequestError();
            StopLoader();
        }
    });
}

function LoadSentMails() {
    StartLoader();
    $('#mailContent').load('Mail/GetSentMails', function () {
        StopLoader();
    });
}