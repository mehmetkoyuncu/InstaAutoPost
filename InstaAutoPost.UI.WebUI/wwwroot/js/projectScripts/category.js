
function GetCategories(id) {
    RenderBodyClear();
    StartLoader();
    $("#breadcome_layout").hide();
    parseid = parseInt(id);
    $('#codeSource').load('Category/GetCategoriesWithSource', { "sourceId": parseid }, function () {
        StopLoader();
    });
}