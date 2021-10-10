

//İçerikleri Getirir
function LoadContents() {
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "list_content_container");
    $('#render_body').append(contentDiv);
    $('#list_content_container').load('SourceContent/GetAllContent', function () {
        var options = $('#sourceContent_options');
        if (options.length > 0)
            return;
        else {
            var contentDiv = $(document.createElement('div')).attr("id", "list_options_container");
            $('#list_content_container').before(contentDiv);
            $('#list_options_container').load('SourceContent/GetSourcesIdAndNameList', function () {
            });
        }
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
    $('#add_view').hide().load("SourceContent/GetAddSourceContentPartial", function () {
        $('#insert_update_button').attr('onclick', "AddSourceContent()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
    });
}

//İçerik Düzenle formu getir
function EditSourceContentView(id, title, imageURL, tags, description, categoryId, sourceId) {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        window.alert("Açık olan formu kapatınız");
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("SourceContent/GetAddSourceContentPartial", function () {

        $("html, body").animate({ scrollTop: 0 }, "slow");
        $('#insert_update_button').attr('onclick', 'EditSourceContent(' + id + ')');
        $('#upsert_sourceContent_title').val(title);
        $("#upsert_sourceContent_imageLink").val(imageURL);
        $("#upsert_sourceContent_tags").val(tags);
        $("#upsert_sourceContent_description").val(description);
        $("#source_select_list").val(sourceId);
        $("#category_select_list").val(categoryId);

        AddSourceSelect();
        $('#category_select_list').removeAttr("disabled");

        $('#insert_update_button').text('Düzenle');
        $('#insert_button').hide();
        $("html").animate({ "scrollTop": $("#add_view").scrollTop() + 100 });
        $('#add_view').fadeIn(1000);
    });
}


function SelectSource() {
    var sourceId = parseInt($("#select_source").val());
        if (sourceId == -1) {
            $("#category_select_list").empty();
            $('#category_select_list').append("<option value=" + -1 + ">ÖNCE BİR KAYNAK SEÇİNİZ..</option>");
            $('#category_select_list').attr("disabled", true);
            return;
        }
        $.ajax({
            type: "GET",
            url: "SourceContent/GetCategoryIdAndName",
            async: false,
            data: { 'sourceId': sourceId },
            success: function (data) {
                $("#category_select_list").empty();
                $(function () {
                    $('#category_select_list').append("<option value=" + -1 + ">Bir kategori seçiniz..</option>");
                    $.each(data, function (index, value) {
                        $('#category_select_list').append("<option value=" + value.id + ">" + value.name + "</option>");
                        $('#category_select_list').removeAttr("disabled");
                    });
                });
            }
        });
}




//İçerik Ekle
function AddSourceContent() {
    StartLoader();
    var sourceId = parseInt($("#source_select_list").val());
    var categoryId = parseInt($("#category_select_list").val());
    if (categoryId <= -1 || !categoryId) {
        alert("Kategori alanı boş olamaz..");
        return;
    }
    if (!sourceId || sourceId <= -1) {
        alert("Kaynak alanı boş olamaz..");
        return;
    }
    var title = $('#upsert_sourceContent_title').val();
    var imageURL = $("#upsert_sourceContent_imageLink").val();
    var tags = $("#upsert_sourceContent_tags").val();
    var description = $("#upsert_sourceContent_description").val();

    $.ajax({
        type: "POST",
        url: "SourceContent/AddSourceContent",
        async: false,
        data: { 'title': title, 'categoryId': categoryId, 'imageURL': imageURL, 'tags': tags, 'description': description },
        success: function (data) {
            LoadContents();
            CloseAddView();
            ClearFilter();
        }
    });
}

function SelectCategory() {

    var categoryId = parseInt($("#category_select_list").val());
    var orderId = parseInt($('#select_order').val());
    var searchText = $('#search_box').val();
    if (categoryId == "" || categoryId==-1)
        return;
    else {
        StartLoader();
        $('#list_content_container').load('SourceContent/Filter', { 'categoryId': categoryId, 'orderId': orderId, 'searchText': searchText }, function () {
            StopLoader();
        });
    }
}



//İçerik Düzenle
function EditSourceContent(id) {
    StartLoader();
    var sourceId = parseInt($("#source_select_list").val());
    var categoryId = parseInt($("#category_select_list").val());
    if (categoryId <= -1 || !categoryId) {
        alert("Kategori alanı boş olamaz..");
        return;
    }
    if (!sourceId || sourceId <= -1) {
        alert("Kaynak alanı boş olamaz..");
        return;
    }
    var title = $('#upsert_sourceContent_title').val();
    var imageURL = $("#upsert_sourceContent_imageLink").val();
    var tags = $("#upsert_sourceContent_tags").val();
    var description = $("#upsert_sourceContent_description").val();

    $.ajax({
        type: "PUT",
        url: "SourceContent/EditSourceContent",
        async: false,
        data: { 'id': parseInt(id), 'title': title, 'categoryId': categoryId, 'imageURL': imageURL, 'tags': tags, 'description': description },
        success: function (data) {
            LoadContents();
            CloseAddView();
            ClearFilter();
        }
    });
}


//İçerik Sil
function RemoveSourceContent(id) {
    StartLoader();
    parseid = parseInt(id);
    $.ajax({
        type: "DELETE",
        url: "SourceContent/RemoveSourceContent",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            LoadContents();
            ClearFilter();
        }
    });
}

//Sırala
function ApplyOrder() {
    StartLoader();
    var categoryId = parseInt($("#category_select_list").val());
    var orderId = parseInt($('#select_order').val());
    var searchText = $('#search_box').val();
    if (categoryId == "")
        return;
    else {
        $('#list_content_container').load('SourceContent/Filter', { 'categoryId': categoryId, 'orderId': orderId, 'searchText': searchText }, function () {
            StopLoader();
        });
    }
}


//Arama
function SearchSourceContent() {
    StartLoader();
    var categoryId = parseInt($("#category_select_list").val());
    var orderId = parseInt($('#select_order').val());
    var searchText = $('#search_box').val();
    if (categoryId == "")
        return;
    else {
        $('#list_content_container').load('SourceContent/Filter', { 'categoryId': categoryId, 'orderId': orderId, 'searchText': searchText }, function () {
            StopLoader();
        });
    }
}

//Filtreyi Temizle
function ClearFilter() {
    StartLoader();
    $("#select_source").val(-1);
    $("#select_order").val(-1);
    $('#search_box').val('');
    StopLoader();
}

//Kategori Detay Modalı Getir
function DetailsSourceContentView(id) {
    var contentDiv = $(document.createElement('div')).attr("id", "detail_sourceContent_container");
    $('#source_list_container').append(contentDiv);
    $('#detail_sourceContent_container').load('SourceContent/GetSourceContent', { 'id': parseInt(id) }, function () {
        $('#sourceContent_detail').modal('show');
    });
}

//Kategori Detay Modalı Kaldır
function CloseSourceContentModal() {
    $('#sourceContent_detail').modal('hide');
    $('#detail_sourceContent_container').remove();
    $('.modal-backdrop').remove();
}


function AddSourceSelect() {
    var sourceId = parseInt($("#source_select_list").val());
    if (sourceId == -1) {
        $("#category_select_list").empty();
        $('#category_select_list').append("<option value=" + -1 + ">Bir kategori seçiniz..</option>");
        $('#category_select_list').attr("disabled", true);
        return;
    }
    $.ajax({
        type: "GET",
        url: "SourceContent/GetCategoryIdAndName",
        async: false,
        data: { 'sourceId': sourceId },
        success: function (data) {
            $("#category_select_list").empty();
            $(function () {
                $.each(data, function (index, value) {
                    $('#category_select_list').append("<option value=" + value.id + ">" + value.name + "</option>");
                    $('#category_select_list').removeAttr("disabled");
                });
            
            });
        }
    });
}
























