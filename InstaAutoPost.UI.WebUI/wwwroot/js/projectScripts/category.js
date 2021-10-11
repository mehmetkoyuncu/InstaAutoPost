

//Kategorileri Getirir
function LoadCategories() {
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "list_categories_container");
    $('#render_body').append(contentDiv);
    $('#list_categories_container').load('Category/GetAllCategories', function () {
        var options = $('#category_options');
        if (options.length > 0)
            return
        else {
            var contentDiv = $(document.createElement('div')).attr("id", "list_options_container");
            $('#list_categories_container').before(contentDiv);
            $('#list_options_container').load('Category/GetSourcesIdAndNameList', function () {
            });
        }
        StopLoader();
    });
};

//Sayfa yüklendiğinde
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.Category, "Kategori");
    ChangeBreadComb("Kategoriler", "Kategorileri buradan, kaynak adına göre listeleyebilir,yeni bir kategori ekleyebilir, silebilir ve güncelleyebilirsiniz. Sosyal medya uygulamalarında kullanılacak etiket isimleri de burada kullanılır.", "category.jpg");
    ChangeReportButton(typeEnum.Category)
    LoadCategories();
});



//Kategori Ekle formu getir
function AddCategoryView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("Category/GetAddCategoryPartial", function () {
        $('#insert_update_button').attr('onclick', "AddCategory()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
    });
}

//Kategori Düzenle formu getir
function EditCategoryView(id) {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("Category/GetAddCategoryPartial", function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        $('#insert_update_button').attr('onclick', 'EditCategory(' + id + ')');
        var category = GetCategoryById(id);
        var name = category.value.name.trim();
        var sourceId = category.value.sourceId;
        var tags = category.value.tags;
        debugger;
        $('#upsert_category_name').val(name);
        $("#upsert_category_tags").val(tags);
        $("#source_select_list option[value=" + sourceId + "]").attr('selected', 'selected');
        $('#insert_update_button').text('Düzenle');
        $('#insert_button').hide();
        $("html").animate({ "scrollTop": $("#add_view").scrollTop() +100 });
        $('#add_view').fadeIn(1000);
    });
}



//Kategori Ekle
function AddCategory() {
    StartLoader();
    var name = $('#upsert_category_name').val()
    var sourceId = parseInt($("#source_select_list").val());
    var tags = $("#upsert_category_tags").val();
    $.ajax({
        type: "POST",
        url: "Category/AddCategory",
        async: false,
        data: { 'name': name, 'sourceId': sourceId, 'tags':tags },
        success: function (data) {
            if (data > 0)
                toastr.success('Kayıt başarıyla eklendi..');
            else
                toastr.error('Kategori eklenirken hata oluştu !');
            LoadCategories();
            CloseAddView();
            ClearFilter();
        },
        error: function () {
            SetRequestError();
            LoadCategories();
            CloseAddView();
            ClearFilter();
        }
    });
}


//Kategori Güncelle
function EditCategory(id) {
    var name = $('#upsert_category_name').val();
    var sourceId = parseInt($("#source_select_list").val());
    var tags =$("#upsert_category_tags").val();
    StartLoader();;
    $.ajax({
        type: "PUT",
        url: "Category/EditCategory",
        async: false,
        data: { 'id': parseInt(id), 'name': name, 'sourceId': parseInt(sourceId), 'tags': tags },
        success: function (data) {
            if (data > 0)
                toastr.success('Kayıt başarıyla güncellendi..');
            else
                toastr.error('Kategori güncellenirken hata oluştu !');
            LoadCategories();
            CloseAddView();
            ClearFilter();
        },
        error: function () {
            SetRequestError();
            LoadCategories();
            CloseAddView();
            ClearFilter();
        }
    });
}


//Kategori Sil
function RemoveCategory(id) {
    StartLoader();
    parseid = parseInt(id);
    $.ajax({
        type: "DELETE",
        url: "Category/RemoveCategory",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            LoadCategories();
            ClearFilter();
        }
    });
}

//Selectbox ile seçim yapıldığında kaynak id'sine göre listenin yenilenmesi
function SelectSource() {
    StartLoader();
    var sourceId = parseInt($("#select_source").val());
    var orderId = parseInt($('#select_order').val());
    var searchText = $('#search_box').val();
    if (sourceId == "")
        return;
    else {
        $('#list_categories_container').load('Category/Filter', { 'sourceId': sourceId, 'orderId': orderId, 'searchText': searchText }, function () {
            StopLoader();
        });
    }
}


//Sırala
function ApplyOrder() {
    StartLoader();
    var sourceId = parseInt($("#select_source").val());
    var orderId = parseInt($('#select_order').val());
    var searchText = $('#search_box').val();
    if (orderId == -1) {
        StopLoader();
        return;
    }
    else {
        $('#list_categories_container').load('Category/Filter', { 'sourceId': sourceId, 'orderId': orderId, 'searchText': searchText }, function () {
            StopLoader();
        });
    }
}


//Arama
function SearchCategory() {
    var sourceId = parseInt($("#select_source").val());
    var orderId = parseInt($('#select_order').val());
    var searchText = $('#search_box').val()
    var sourceId=$("#select_source").val();
    $('#list_categories_container').load('Category/Filter', { 'sourceId': sourceId, 'orderId': orderId, 'searchText': searchText }, function () {
    });
}

//Filtreyi Temizle
function ClearFilter() {
    StartLoader();
$("#select_source").val(-1);
    $("#select_order").val(-1);
    $('#search_box').val('');
    StopLoader();
}


//Kategori Detay Modalı Getir
function DetailsCategoryView(id) {
    var contentDiv = $(document.createElement('div')).attr("id", "detail_categories_container");
    $('#list_categories_container').append(contentDiv);
    $('#detail_categories_container').load('Category/GetCategoryDetail', { 'id': parseInt(id) }, function () {
        $('#category_detail').modal('show');
    });
}

//Kategori Detay Modalı Kaldır
function CloseCategoryModal() {
    $('#category_detail').modal('hide');
    $('#detail_categories_container').remove();
    $('.modal-backdrop').remove();
}

function GetCategoryById(id) {
    parseid = parseInt(id);
    var result = 0;
    $.ajax({
        type: "GET",
        url: "Category/GetCategoryById",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            result= data;
        }
    });
    return result;
}



























