function AddRSS() {
    StartLoader();
    $.ajax({
        type: "POST",
        url: "RssRunner/RunRssGenerator",
        async: false,
        data: { 'name': $('#category_name').val(), 'url': $('#category_url').val() },
        success: function (data) {
            $('#codeSource').load('SourceContent/GetSourceContent', { "categoryId": data.value.categoryId }, function () {
                CloseUpsertView();
                $('#run_rss').attr('onclick', 'GetRssAddView()');
                $('#source_content_header').text("Eklenen İçerikler");
                $('#source_content_result').show();
                $('#source_content_result').append('<strong>' + data.value.rssAddedCount + ' adet içerik eklendi.</strong>');
                StopLoader();
            });
        }
    });
}
function GetRssAddView() {
    StartLoader();
    RenderBodyClear();
    ChangeBreadComb("RSS Generator", "RSS kodunu buradan ekleyip otomatik olarak kaynağın ve içeriklerin eklenmesini sağlayabilir, eklenen kategorileri listeleyebilir, silebilir ve güncelleyebilirsiniz.", "/images/BreadCombImages/RSS_button_1021.png");
    ChangeButtonText("Rss Kodu");
    $('#upsert_button').attr('onclick', "UpsertChange(true,'RSS','')");
    UpsertChange(true, 'RSS', '');
    StopLoader();
}

