


//Sosyal Medyaları Getirir
function LoadSocialMedias() {
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "socialMedia_list_container");
    $('#render_body').append(contentDiv);
    $('#socialMedia_list_container').load('SocialMedia/GetAllSocialMedias', function () {
        StopLoader();
    });
};

//Sayfa yüklendiğinde
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.SocialMedia, "Sosyal Medya");
    ChangeBreadComb("Sosyal Medya Hesapları", "Sosyal Medya Hesaplarınızı buradan, listeleyebilir,yeni bir kaynak ekleyebilir, silebilir ve güncelleyebilirsiniz.", "socialMedia.png");
    ChangeReportButton(typeEnum.SocialMedia)
    LoadSocialMedias();
});



//Sosyal Medya Ekle formu getir
function AddSocialMediaView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("SocialMedia/GetAddSocialMediaPartial", function () {
        $('#insert_update_button').attr('onclick', "AddSocialMedia()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
        $('#form_controls label').hide();
    });
}

//Sosyal Medya Düzenle formu getir
function EditSocialMediaView(id) {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("SocialMedia/GetAddSocialMediaPartial", function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        $('#insert_update_button').attr('onclick', 'EditSocialMedia(' + id + ')');
        var sMedia = GetSocialMediaById(id);
        var name = sMedia.name;
        var media = sMedia.icon;
        var userName = sMedia.accountNameOrMail
        var password = sMedia.password
        $('#upsert_socialMedia_name').val(name);
        $("#socialMediaIcon_select_list").val(media);
        $('#upsert_socialMedia_userName').val(userName);
        $('#upsert_socialMedia_password').val(password);
        $('#insert_update_button').text('Düzenle');
        $('#insert_button').hide();
        $("html").animate({ "scrollTop": $("#add_view").scrollTop() + 100 });
        $('#add_view').fadeIn(1000);
    });
}



//Sosyal Medya Ekle
function AddSocialMedia() {
    var name = $('#upsert_socialMedia_name').val();
    var media = $("#socialMediaIcon_select_list").val();
    var userName = $('#upsert_socialMedia_userName').val();
    var password = $('#upsert_socialMedia_password').val();
    if (!name || !media || !userName || !password || media==-1) {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    StartLoader();
    $.ajax({
        type: "POST",
        url: "SocialMedia/AddSocialMedia",
        async: false,
        data: { 'name': name, 'Icon': media, 'accountNameOrMail': userName, 'password': password },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla eklendi..');
                LoadSocialMedias();
                CloseAddView();
                StopLoader();
            }
            else {
                toastr.error('Kayıt eklenirken hata oluştu !');
                StopLoader();
            }
        },
        error: function () {
            SetRequestError();
            LoadSocialMedias();
            CloseAddView();
        }
    });
}

//Sosyal Medya Düzenle
function EditSocialMedia(id) {
    var name = $('#upsert_socialMedia_name').val();
    var media = $("#socialMediaIcon_select_list").val();
    var userName = $('#upsert_socialMedia_userName').val();
    var password = $('#upsert_socialMedia_password').val();
    if (!name || !media || !userName || !password || media == -1) {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    StartLoader();
    $.ajax({
        type: "PUT",
        url: "SocialMedia/EditSocialMedia",
        async: false,
        data: { 'id': parseInt(id), 'name': name, 'Icon': media, 'accountNameOrMail': userName, 'password': password },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla güncellendi..');
                LoadSocialMedias();
                CloseAddView();
                StopLoader();
            }
            else {
                toastr.error('Kayıt güncellenirken hata oluştu !');
                StopLoader();
            }
        },
        error: function () {
            SetRequestError();
            LoadSocialMedias();
            CloseAddView();
        }
    });
}


//Sosyal Medya Sil
function RemoveSocialMedia(id) {
    Swal.fire({
        title: 'Bu kaydı silmek istediğinizden emin misiniz ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sil',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            StartLoader();
            parseid = parseInt(id);
            $.ajax({
                type: "DELETE",
                url: "SocialMedia/RemoveSocialMedia",
                async: false,
                data: { 'id': parseid },
                success: function (data) {
                    if (data > 0)
                        toastr.success('Kayıt başarıyla silindi..');
                    else
                        toastr.error('Kayıt silinirken hata oluştu !');
                    LoadSocialMedias();
                    ClearFilter();
                },
                error: function () {
                    SetRequestError();
                    LoadCategories();
                    ClearFilter();
                }
            });
        }
    })



}

//Düzenleme için id'ye göre kategoriyi getir
function GetSocialMediaById(id) {
    parseid = parseInt(id);
    var result = 0;
    $.ajax({
        type: "GET",
        url: "SocialMedia/GetSocialMediaById",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            result = data;
        }
    });
    return result;
}


























