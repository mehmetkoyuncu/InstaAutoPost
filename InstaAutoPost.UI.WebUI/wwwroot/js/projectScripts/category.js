
function GetCategories(id) {
    RenderBodyClear();
    StartLoader();
    parseid = parseInt(id);
    $('#codeSource').load('Category/GetCategoriesWithSource', { "sourceId": parseid }, function () {
        StopLoader();
    });
}