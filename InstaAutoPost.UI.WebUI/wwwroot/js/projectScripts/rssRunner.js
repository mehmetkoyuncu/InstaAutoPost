

//Kaynakları Getirir
//function LoadSources() {
//    RenderBodyClear();
//    var contentDiv = $(document.createElement('div')).attr("id", "list_sources_container");
//    $('#render_body').append(contentDiv);
//    $('#list_sources_container').load('Source/GetAllSources', function () {
//        var options = $('#source_options');
//        if (options.length > 0)
//            return
//        else {
//            var contentDiv = $(document.createElement('div')).attr("id", "list_options_container");
//            $('#list_sources_container').before(contentDiv);
//            $('#list_options_container').load('Source/GetSourceFilterView', function () {
//            });
//        }
//        StopLoader();
//    });
//};

//Sayfa yüklendiğinde
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.RssGenerator, "RSS Generator");
    ChangeBreadComb("RSS Generator", "Bir medya web sitesinin RSS linki kopyalanarak kaynak, kategori ve içerik kaydının otomatik olarak yüklenmesi sağlanır. Örneğin : https://www.haberturk.com/rss/manset.xml", "rss.png");
    ChangeReportButton(typeEnum.RssGenerator)
    AddRssGeneratorView();
});



//Kategori Ekle formu getir
function AddRssGeneratorView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("RssRunner/GetRSSGeneratorPartial", function () {
        $('#insert_update_button').attr('onclick', "AddRSSGenerator()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
    });
    StopLoader();
}
//Kaynak Ekle
function AddRSSGenerator() {
    var url = $('#upsert_rssGenerator_link').val();
    var categoryTypeId = parseInt($("#categoryType_select_list").val());
    if (!url || !categoryTypeId || categoryTypeId == -1)
    {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    $.ajax({
        type: "POST",
        url: "RssRunner/RunRssGenerator",
        async: false,
        data: { 'url': url, 'categoryTypeId': categoryTypeId },
        success: function (data) {
            if (data) {
                var count = data.value.rssAddedCount;
                toastr.success(count+' adet kayıt başarıyla eklendi..');
            }
            else {
                toastr.error('Kayıt eklenirken hata oluştu !');
            }
            StopLoader();
        },
        error: function () {
            SetRequestError();
        }
    });
}

//////Kategori Düzenle formu getir
////function EditSourceView(id, name, imageUrl) {
////    var viewControl = $('#add_view');
////    if (viewControl.length > 0) {
////        window.alert("Açık olan formu kapatınız");
////        return;
////    }
////    ClearAddView();
////    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
////    $('#breadcomb_container').after(contentDiv);
////    $('#add_view').hide().load("Source/GetAddSourcePartial", function () {
////        $("html, body").animate({ scrollTop: 0 }, "slow");
////        $('#insert_update_button').attr('onclick', 'EditSource(' + id + ')');
////        $('#upsert_source_name').val(name);
////        $("#upsert_source_imageLink").val(imageUrl);
////        $('#insert_update_button').text('Düzenle');
////        $('#insert_button').hide();
////        $("html").animate({ "scrollTop": $("#add_view").scrollTop() + 100 });
////        $('#add_view').fadeIn(1000);
////    });
////}





//////Kaynak Düzenle
////function EditSource(id) {
////    var name = $('#upsert_source_name').val();
////    var imageLink = $("#upsert_source_imageLink").val();
////    StartLoader();;
////    $.ajax({
////        type: "PUT",
////        url: "Source/EditSource",
////        async: false,
////        data: { 'id': parseInt(id), 'name': name, 'image': imageLink },
////        success: function (data) {
////            LoadSources();
////            CloseAddView();
////            ClearFilter();
////        }
////    });
////}


//////Kaynak Sil
////function RemoveSource(id) {
////    StartLoader();
////    parseid = parseInt(id);
////    $.ajax({
////        type: "DELETE",
////        url: "Source/RemoveSource",
////        async: false,
////        data: { 'id': parseid },
////        success: function (data) {
////            LoadSources();
////            ClearFilter();
////        }
////    });
////}

//////Sırala
////function ApplyOrder() {
////    StartLoader();
////    var orderId = parseInt($('#select_order').val());
////    var searchText = $('#search_box').val();
////    if (orderId == -1) {
////        StopLoader();
////        return;
////    }
////    else {
////        $('#list_sources_container').load('Source/Filter', { 'orderId': orderId, 'searchText': searchText }, function () {
////            StopLoader();
////        });
////    }
////}


//////Arama
////function SearchSource() {
////    var orderId = parseInt($('#select_order').val());
////    var searchText = $('#search_box').val()
////    $('#list_sources_container').load('Source/Filter', { 'orderId': orderId, 'searchText': searchText }, function () {
////    });
////}

//////Filtreyi Temizle
////function ClearFilter() {
////    StartLoader();
////    $("#select_order").val(-1);
////    $('#search_box').val('');
////    StopLoader();
////}



























