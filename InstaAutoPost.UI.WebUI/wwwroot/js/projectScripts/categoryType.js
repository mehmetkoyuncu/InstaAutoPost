

//Kaynakları Getirir
function LoadCategoryTypes() {
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "list_categoryType_container");
    $('#render_body').append(contentDiv);
    $('#list_categoryType_container').load('CategoryType/GetAllCategoryTypes', function () {
        StopLoader();
    });
};

//Sayfa yüklendiğinde
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.CategoryType, "Kategori Tipi");
    ChangeBreadComb("Kategori Tipi", "Kategori Tiplerini buradan, listeleyebilir,yeni bir kaynak ekleyebilir, silebilir ve güncelleyebilirsiniz.", "categoryType.jpg");
    ChangeReportButton(typeEnum.CategoryType)
    LoadCategoryTypes();
});



//Kaynak Ekle formu getir
function AddCategoryTypeView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("CategoryType/GetAddCategoryTypePartial", function () {
        $('#insert_update_button').attr('onclick', "AddCategoryType()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
        $('#form_controls label').hide();
    });
}

//Kaynak Düzenle formu getir
function EditCategoryTypeView(id) {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("CategoryType/GetAddCategoryTypePartial", function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        $('#insert_update_button').attr('onclick', 'EditCategoryType(' + id + ')');
        var categoryType = GetCategoryTypeById(id);
        var name = categoryType.value.name.trim();
        var template = categoryType.value.template;
        $('#upsert_categoryType_name').val(name);
        $('#template_select').val(template);
        $('#insert_update_button').text('Düzenle');
        $('#insert_button').hide();
        $("html").animate({ "scrollTop": $("#add_view").scrollTop() + 100 });
        $('#add_view').fadeIn(1000);
    });
}



//Kaynak Ekle
function AddCategoryType() {
    var name = $('#upsert_categoryType_name').val()
    var template = $('#template_select').val()

    if (!name || !template || template == '-1') {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    StartLoader();
    $.ajax({
        type: "POST",
        url: "CategoryType/AddCategoryType",
        async: false,
        data: { 'name': name, 'template': template },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla eklendi..');
                LoadCategoryTypes();
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
            LoadCategoryTypes();
            CloseAddView();
        }
    });
}

//Kaynak Düzenle
function EditCategoryType(id) {
    var name = $('#upsert_categoryType_name').val()
    var template = $('#template_select').val()

    if (!name || !template || template=='-1') {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    StartLoader();
    $.ajax({
        type: "PUT",
        url: "CategoryType/EditCategoryType",
        async: false,
        data: { 'id': parseInt(id), 'name': name, 'template': template},
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla güncellendi..');
                LoadCategoryTypes();
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
            LoadCategoryTypes();
            CloseAddView();
            ClearFilter();
        }
    });
}


//Kaynak Sil
function RemoveCategoryType(id) {
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
                url: "CategoryType/RemoveCategoryType",
                async: false,
                data: { 'id': parseid },
                success: function (data) {
                    if (data > 0)
                        toastr.success('Kayıt başarıyla silindi..');
                    else
                        toastr.error('Kayıt silinirken hata oluştu !');
                    LoadCategoryTypes();
                },
                error: function () {
                    SetRequestError();
                    LoadCategoryTypes();
                }
            });
        }
    })



}

//Düzenleme için id'ye göre kategoriyi getir
function GetCategoryTypeById(id) {
    parseid = parseInt(id);
    var result = 0;
    $.ajax({
        type: "GET",
        url: "CategoryType/GetCategoryTypeById",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            result = data;
        }
    });
    return result;
}


























