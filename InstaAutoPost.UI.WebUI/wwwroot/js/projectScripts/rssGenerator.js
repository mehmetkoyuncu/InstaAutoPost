function AddRSS() {
    StartLoader();
    $.ajax({
        type: "POST",
        url: "RssRunner/RunRssGenerator",
        async: false,
        data: { 'name': $('#category_name').val(), 'url': $('#category_url').val() },
        success: function (data) {
            $('#codeSource').load('SourceContent/GetSourceContent', { "categoryId": data.CategoryId}, function () {
                CloseUpsertView();
                StopLoader();
            });
        }
    });
}
function GetRssAddView() {
    StartLoader();
    RenderBodyClear();
    UpsertChange(true, 'RSS', '');
    StopLoader();
}