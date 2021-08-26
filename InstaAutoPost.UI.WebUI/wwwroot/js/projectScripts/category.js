

//Kategorileri Getirir
function LoadCategories() {
    var selectedItem = $("#select_source").val();
    var contentDiv = $(document.createElement('div')).attr("id", "list_categories_container");
    $('#render_body').append(contentDiv);
    $('#list_categories_container').load('Category/GetAllCategories', function () {
        ChangeSelectedItem(selectedItem);
    });
};

//Sayfa yüklendiğinde
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.Category, "Kategori");
    ChangeBreadComb("Kategoriler", "Kategorileri buradan, kaynak adına göre listeleyebilir,yeni bir kategori ekleyebilir, silebilir ve güncelleyebilirsiniz.", "category.jpg");
    ChangeReportButton(typeEnum.Category)
    LoadCategories();
    StopLoader();
});



//Kategori Ekle formu getir
function AddCategoryView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        window.alert("Açık olan formu kapatınız");
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').load("Category/GetAddCategoryPartial", function () {
        $('#insert_update_button').attr('onclick', "AddCategory()");
    });
    $('#insert_button').hide();
}

//Kategori Düzenle formu getir
function EditCategoryView(id, name, sourceId) {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        window.alert("Açık olan formu kapatınız");
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').load("Category/GetAddCategoryPartial", function () {
        $('#insert_update_button').attr('onclick', 'EditCategory(' + id + ')');
        $('#upsert_category_name').val(name);
        $("#source_select_list option[value=" + sourceId + "]").attr('selected', 'selected');
        $('#insert_update_button').text('Düzenle');
    });
    $('#insert_button').hide();
}



//Kategori Ekle
function AddCategory() {
    StartLoader();
    var sourceId = parseInt($("#source_select_list").val());
    $.ajax({
        type: "POST",
        url: "Category/AddCategory",
        async: false,
        data: { 'name': $('#upsert_category_name').val(), 'sourceId': $("#source_select_list").val() },
        success: function (data) {
            $('#list_categories_container').load('Category/GetCategoryBySourceId', { 'id': sourceId }, function () {
                ChangeSelectedItem(sourceId);
            });
            CloseAddView();
            StopLoader();
        }
    });
}
function EditCategory(id) {
    var name = $('#upsert_category_name').val();
    var sourceId = parseInt($("#source_select_list").val());
    StartLoader();;
    $.ajax({
        type: "PUT",
        url: "Category/EditCategory",
        async: false,
        data: { 'id': parseInt(id), 'name': name, 'sourceId': parseInt(sourceId) },
        success: function (data) {
            $('#list_categories_container').load('Category/GetCategoryBySourceId', { 'id': sourceId }, function () {
                ChangeSelectedItem(sourceId);
            });
            CloseAddView();
            StopLoader();
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
            var selectedItem = $("#select_source").val();
            $('#list_categories_container').load('Category/GetCategoryBySourceId', { 'id': selectedItem }, function () {
                ChangeSelectedItem(selectedItem);
            });
            StopLoader();
        }
    });
}

//Selectbox ile seçim yapıldığında kaynak id'sine göre listenin yenilenmesi
function SelectSource() {
    StartLoader();
    var selectedItem = $("#select_source").val();
    if (selectedItem == -1)
        LoadCategories();
    else if (selectedItem == "")
        return;
    else {
        $('#list_categories_container').load('Category/GetCategoryBySourceId', { 'id': selectedItem }, function () {
            ChangeSelectedItem(selectedItem);
        });
    }
    StopLoader();
}

//Selectbox value idnin load sırasında tutulması
function ChangeSelectedItem(id) {
    $("#select_source option[value=" + parseInt(id) + "]").attr('selected', 'selected');
}
//Order idnin load esnasında tutlması
function ChangeSelectedOrder(id) {
    $("#select_order option[value=" + parseInt(id) + "]").attr('selected', 'selected');
}


//Sırala
function ApplyOrder() {
    StartLoader();
    var sourceId = parseInt($("#select_source").val());
    var orderId = parseInt($('#select_order').val());
    if (orderId == -1) {
        StopLoader();
        return;
    }
    else {
        $('#list_categories_container').load('Category/ApplyOrder', { 'sourceId': sourceId, 'orderId': orderId }, function () {
            ChangeSelectedItem(sourceId);
            ChangeSelectedFilter(orderId);
        });
    }
    StopLoader();
}




























