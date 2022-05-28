


//Sosyal Medyaları Getirir
function LoadSocialMediaAccountCategorys() {
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "socialMediaAccountCategory_list_container");
    $('#render_body').append(contentDiv);
    $('#socialMediaAccountCategory_list_container').load('SocialMediaAccountsCategoryType/GetAllSocialMediaAccountCategories', function () {
        StopLoader();
    });
};

//Sayfa yüklendiğinde
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.SocialMediaAccountCategory, "Sosyal Medya-Kategori");
    ChangeBreadComb("Sosyal Medya-Kategori", "Sosyal Medya ile kategoriler buradan ilişkilendirilebilir. İlişkiler silinebilir ve düzenlenebilir.", "socialMediaAccountCategory.jpg");
    ChangeReportButton(typeEnum.SocialMediaAccountCategory)
    LoadSocialMediaAccountCategorys();
});



//Sosyal Medya Ekle formu getir
function AddSocialMediaAccountCategoryView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("SocialMediaAccountsCategoryType/GetAddSocialMediaAccountCategoryPartial", function () {
        $('#insert_update_button').attr('onclick', "AddSocialMediaAccountCategory()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
        $('#form_controls label').hide();
    });
}

//Sosyal Medya Düzenle formu getir
function EditSocialMediaAccountCategoryView(id) {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("SocialMediaAccountsCategoryType/GetAddSocialMediaAccountCategoryPartial", function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        $('#insert_update_button').attr('onclick', 'EditSocialMediaAccountCategoryType(' + id + ')');
        var sMedia = GetSocialMediaAccountCategoryById(id);
        var mediaId = sMedia.socialMediaAccountId;
        var categoryId = sMedia.categoryTypeId;
        $('#category_select_list').val(categoryId);
        $("#socialMedia_select_list").val(mediaId);
        $('#insert_update_button').text('Düzenle');
        $('#insert_button').hide();
        $("html").animate({ "scrollTop": $("#add_view").scrollTop() + 100 });
        $('#add_view').fadeIn(1000);
    });
}



//Sosyal Medya Ekle
function AddSocialMediaAccountCategory() {
    var categoryId = $('#category_select_list').val();
    var mediaId = $("#socialMedia_select_list").val();
    if (!categoryId || !mediaId || mediaId == -1 || categoryId==-1) {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    StartLoader();
    $.ajax({
        type: "POST",
        url: "SocialMediaAccountsCategoryType/AddSocialMediaAccountCategory",
        async: false,
        data: { 'socialMediaAccountId': mediaId, 'categoryTypeId': categoryId},
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla eklendi..');
                LoadSocialMediaAccountCategorys();
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
            LoadSocialMediaAccountCategorys();
            CloseAddView();
        }
    });
}

//Sosyal Medya Düzenle
function EditSocialMediaAccountCategoryType(id) {
    var categoryId =$('#category_select_list').val();
    var mediaId =$("#socialMedia_select_list").val();
    if (!mediaId || !categoryId || mediaId == -1 || categoryId == -1) {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    StartLoader();
    $.ajax({
        type: "PUT",
        url: "SocialMediaAccountsCategoryType/EditSocialMediaAccountCategoryType",
        async: false,
        data: { 'id': parseInt(id), 'categoryTypeId': categoryId, 'socialMediaAccountId': mediaId },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla güncellendi..');
                LoadSocialMediaAccountCategorys();
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
            LoadSocialMediaAccountCategorys();
            CloseAddView();
        }
    });
}


//Sosyal Medya Sil
function RemoveSocialMediaAccountCategory(id) {
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
                url: "SocialMediaAccountsCategoryType/RemoveSocialMediaAccountCategoryType",
                async: false,
                data: { 'id': parseid },
                success: function (data) {
                    if (data > 0)
                        toastr.success('Kayıt başarıyla silindi..');
                    else
                        toastr.error('Kayıt silinirken hata oluştu !');
                    LoadSocialMediaAccountCategorys();
                },
                error: function () {
                    SetRequestError();
                    LoadCategories();
                }
            });
        }
    })



}

//Düzenleme için id'ye göre kategoriyi getir
function GetSocialMediaAccountCategoryById(id) {
    parseid = parseInt(id);
    var result = 0;
    $.ajax({
        type: "GET",
        url: "SocialMediaAccountsCategoryType/GetSocialMediaAccountCategoryById",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            result = data;
        }
    });
    return result;
}


























