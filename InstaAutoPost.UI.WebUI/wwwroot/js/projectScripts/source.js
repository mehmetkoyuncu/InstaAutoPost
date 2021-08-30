﻿

//Kaynakları Getirir
function LoadSources() {
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "list_sources_container");
    $('#render_body').append(contentDiv);
    $('#list_sources_container').load('Source/GetAllSources', function () {
        var options = $('#source_options');
        if (options.length > 0)
            return
        else {
            var contentDiv = $(document.createElement('div')).attr("id", "list_options_container");
            $('#list_sources_container').before(contentDiv);
            $('#list_options_container').load('Source/GetSourceFilterView', function () {
            });
        }
        StopLoader();
    });
};

//Sayfa yüklendiğinde
$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.Source, "Kaynak");
    ChangeBreadComb("Kaynaklar", "Kaynakları buradan, listeleyebilir,yeni bir kaynak ekleyebilir, silebilir ve güncelleyebilirsiniz.", "source.jpg");
    ChangeReportButton(typeEnum.Source)
    LoadSources();
});



//Kategori Ekle formu getir
function AddSourceView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        window.alert("Açık olan formu kapatınız");
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("Source/GetAddSourcePartial", function () {
        $('#insert_update_button').attr('onclick', "AddSource()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
    });
}

//Kategori Düzenle formu getir
function EditSourceView(id, name,imageUrl) {
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
   var name= $('#upsert_source_name').val();
   var imageLink= $("#upsert_source_imageLink").val();
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
        $('#list_sources_container').load('Source/Filter', {'orderId': orderId, 'searchText': searchText }, function () {
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



























