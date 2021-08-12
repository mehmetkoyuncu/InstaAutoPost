
function RefreshGetCategoryLoad() {
    StartLoader();
    $('#codeSource').load('Category/GetAllCategories', function () {
        StopLoader()
    });
}

function GetAllCategories() {
    RefreshGetCategoryLoad();
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

function RemoveCategory(id,sourceId) {
    StartLoader();
    parseid = parseInt(id);
    $.ajax({
        type: "DELETE",
        url: "Category/RemoveCategory",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            $('#codeSource').load('Category/GetCategoriesWithSource', { "sourceId": sourceId }, function () {
                StopLoader();
            });
        }
    });
}
function AddCategoryView(element,id) {
    StartLoader();
    var contentDiv = $(document.createElement('div')).attr("id", "add_category");
    $('#category_list_container').before(contentDiv);
    $('#add_category').load('Category/GetAddCategoryPartial', function () {
        $('#upsert_category_button').text('Ekle');
        $('#upsert_category_button').attr('onclick', "AddCategory("+id+")");
        $('#add_category_button').hide();
        StopLoader();
    });
}


function RemoveAddCategoryView() {
    StartLoader();
    $('#add_category').remove();
    $('#add_category_button').show();
    StopLoader();
};


function AddCategory(id) {
    StartLoader();
    parseid = parseInt(id);
    $.ajax({
        type: "POST",
        url: "Category/RunRssGenerator",
        async: false,
        data: { 'name': $('#category_name').val(), 'url': $('#category_url').val()},
        success: function (data) {
            $('#add_category').load('Category/GetAddCategoryPartial', function () {
                $('#add_category_button').hide();
                this.RemoveAddCategoryView();
                StopLoader();
            });
        }
    });
}
