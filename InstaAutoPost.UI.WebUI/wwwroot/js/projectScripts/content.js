

//İçerikleri Getirir
function LoadContents() {
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "list_content_container");
    $('#render_body').append(contentDiv);
    $('#list_content_container').load('SourceContent/GetAllContent', function () {
        StopLoader();
    });
};

//Sayfa yüklendiğinde
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.SourceContent, "İçerik");
    ChangeBreadComb("İçerikler", "Sosyal medyada paylaşacağınız içerikleri buradan listeleyebilir, yeni bir içerik ekleyebilir, düzenleyebilir ve silebilirsiniz.", "content.jpg");
    ChangeReportButton(typeEnum.SourceContent)
    LoadContents();
});



//İçerik Ekle formu getir
function AddSourceContentView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        window.alert("Açık olan formu kapatınız");
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("SourceContent/GetAddContentPartial", function () {
        $('#insert_update_button').attr('onclick', "AddSourceContent()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
    });
}

//Kategori Düzenle formu getir
function EditSourceView(id, name, imageUrl) {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        window.alert("Açık olan formu kapatınız");
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("Source/GetAddSourcePartial", function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        $('#insert_update_button').attr('onclick', 'EditSource(' + id + ')');
        $('#upsert_source_name').val(name);
        $("#upsert_source_imageLink").val(imageUrl);
        $('#insert_update_button').text('Düzenle');
        $('#insert_button').hide();
        $("html").animate({ "scrollTop": $("#add_view").scrollTop() + 100 });
        $('#add_view').fadeIn(1000);
    });
}



//Kaynak Ekle
function AddSource() {
    StartLoader();
    var name = $('#upsert_source_name').val();
    var imageLink = $("#upsert_source_imageLink").val();
    $.ajax({
        type: "POST",
        url: "Source/AddSource",
        async: false,
        data: { 'name': name, 'image': imageLink },
        success: function (data) {
            LoadSources();
            CloseAddView();
            ClearFilter();
        }
    });
}

//Kaynak Düzenle
function EditSource(id) {
    var name = $('#upsert_source_name').val();
    var imageLink = $("#upsert_source_imageLink").val();
    StartLoader();;
    $.ajax({
        type: "PUT",
        url: "Source/EditSource",
        async: false,
        data: { 'id': parseInt(id), 'name': name, 'image': imageLink },
        success: function (data) {
            LoadSources();
            CloseAddView();
            ClearFilter();
        }
    });
}


//Kaynak Sil
function RemoveSource(id) {
    StartLoader();
    parseid = parseInt(id);
    $.ajax({
        type: "DELETE",
        url: "Source/RemoveSource",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            LoadSources();
            ClearFilter();
        }
    });
}

//Sırala
function ApplyOrder() {
    StartLoader();
    var orderId = parseInt($('#select_order').val());
    var searchText = $('#search_box').val();
    if (orderId == -1) {
        StopLoader();
        return;
    }
    else {
        $('#list_sources_container').load('Source/Filter', { 'orderId': orderId, 'searchText': searchText }, function () {
            StopLoader();
        });
    }
}


//Arama
function SearchSource() {
    var orderId = parseInt($('#select_order').val());
    var searchText = $('#search_box').val()
    $('#list_sources_container').load('Source/Filter', { 'orderId': orderId, 'searchText': searchText }, function () {
    });
}

//Filtreyi Temizle
function ClearFilter() {
    StartLoader();
    $("#select_order").val(-1);
    $('#search_box').val('');
    StopLoader();
}



























