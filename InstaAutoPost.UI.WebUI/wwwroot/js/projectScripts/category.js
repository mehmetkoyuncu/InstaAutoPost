//Kategorileri Getirir
function LoadCategories() {
    var contentDiv = $(document.createElement('div')).attr("id", "list_categories_container");
    $('#render_body').append(contentDiv);
    $('#list_categories_container').load('Category/GetAllCategories', function () {
        GetSelectContainer();
    });
};
//Filterı Getirir
function GetSelectContainer() {
    var contentDiv = $(document.createElement('div')).attr("id", "select_container");
    $('#list_categories_container').before(contentDiv);
    $('#select_container').load('Source/GetSourceSelectContainer', function () { });
}

//Sayfa yüklendiğinde
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.Category, "Kategori");
    ChangeBreadComb("Kategoriler", "Kategorileri buradan, kaynak adına göre listeleyebilir,yeni bir kategori ekleyebilir, silebilir ve güncelleyebilirsiniz.", "/images/BreadCombImages/RSS_button_1021.png");
    ChangeReportButton(typeEnum.Category)
    LoadCategories();
    StopLoader();
});


function GetAllCategories() {
    StartLoader();
    RenderBodyClear();
    RefreshGetCategoryLoad()

    ChangeButtonText("Kategori");
    $('#upsert_button').attr('onclick', "UpsertChange(true,'Category','')");
    var contentDiv = $(document.createElement('div')).attr("id", "select_container");
    $('#codeSource').before(contentDiv);
    $('#select_container').load("Source/GetSourceSelectContainer"), function () {
        StopLoader();
    };

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

function SelectSource() {
    StartLoader();
    var selectedItem = $("#select_source").val();

    if (selectedItem == -1)
        LoadCategories();
    else if (selectedItem == "")
        return;
    else {
        $('#list_categories_container').load('Category/GetCategoryBySourceId', { 'id': selectedItem }, function () {
        });
    }
    StopLoader();
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



