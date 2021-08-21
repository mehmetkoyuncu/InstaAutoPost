
function RefreshGetCategoryLoad() {
    StartLoader();
    $('#codeSource').load('Category/GetAllCategories', function () {
        StopLoader();
    });
}

function GetAllCategories() {
    RefreshGetCategoryLoad();
    ChangeBreadComb("Kategoriler", "RSS kodunu buradan ekleyip otomatik olarak kaynağın ve içeriklerin eklenmesini sağlayabilir, kategorileri listeleyebilir, silebilir ve güncelleyebilirsiniz.", "/images/BreadCombImages/RSS_button_1021.png");
    ChangeButtonText("Kategori");
    $('#upsert_button').attr('onclick', "UpsertChange(true,'Category','')");
}

function AddCategory() {
    StartLoader();
    $.ajax({
        type: "POST",
        url: "Category/RunRssGenerator",
        async: false,
        data: { 'name': $('#category_name').val(), 'url': $('#category_url').val() },
        success: function (data) {
            $('#add_category').load('Category/GetAddCategoryPartial', function () {
                CloseUpsertView();
                StopLoader();
            });
        }
    });
}







function RemoveCategory(id) {
    StartLoader();
    parseid = parseInt(id);
    $.ajax({
        type: "DELETE",
        url: "Category/RemoveCategory",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            RefreshGetCategoryLoad();
            StopLoader();
        }
    });
}












function GetCategories(id) {
    RenderBodyClear();
    StartLoader();
    $("#breadcome_layout").hide();
    parseid = parseInt(id);
    $('#codeSource').load('Category/GetCategoriesWithSource', { "sourceId": parseid }, function () {
        StopLoader();
    });
}





function RemoveAddCategoryView() {
    StartLoader();
    $('#add_category').remove();
    $('#add_category_button').show();
    StopLoader();
};



