

//İçerikleri Getirir
function LoadContents(next = 0, quantity = 10, callback) {
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "list_content_container");
    $('#render_body').append(contentDiv);
    $('#list_content_container').load('SourceContent/GetAllContent', { 'next': next, 'quantity': quantity }, function () {
        var options = $('#sourceContent_options');
        if (options.length > 0)
            return;
        else {
            var contentDiv = $(document.createElement('div')).attr("id", "list_options_container");
            $('#list_content_container').before(contentDiv);
            $('#list_options_container').load('SourceContent/GetSourcesIdAndNameList', function () {
                if (callback)
                    callback();
                else
                    $('#page-1').addClass('page-selected');
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
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("SourceContent/GetAddSourceContentPartial", function () {
        $('#insert_update_button').attr('onclick', "AddSourceContent()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
        $('#form_controls label').hide();
    });
}

//İçerik Düzenle formu getir
function EditSourceContentView(id) {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("SourceContent/GetAddSourceContentPartial", function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        $('#insert_update_button').attr('onclick', 'EditSourceContent(' + id + ')');
        var sourceContent = GetSourceContentById(id);
        var sourceId = sourceContent.value.sourceId;
        var title = sourceContent.value.title;
        var imageURL = sourceContent.value.imageURL;
        var tags = sourceContent.value.tags;
        var description = sourceContent.value.description; $('#upsert_sourceContent_title').val(title);
        $("#upsert_sourceContent_imageLink").val(imageURL);
        $("#upsert_sourceContent_tags").val(tags);
        $("#upsert_sourceContent_description").val(description);
        $("#source_select_list").val(sourceId);
        AddSourceSelect(function () {
            var categoryId = sourceContent.value.categoryId;
            $("#category_select_list").val(categoryId);
        });
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
        StartLoader();
        LoadContents(callback = function () {
            $("#category_select_list").empty();
            $('#category_select_list').append("<option value=" + -1 + ">ÖNCE BİR KAYNAK SEÇİNİZ..</option>");
            $('#category_select_list').attr("disabled", true);
        });
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
    var sourceId = parseInt($("#source_select_list").val());
    var categoryId = parseInt($("#category_select_list").val());
    var title = $('#upsert_sourceContent_title').val();
    if (categoryId <= -1 || !categoryId || !sourceId || sourceId <= -1 || !title) {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    var imageURL = $("#upsert_sourceContent_imageLink").val();
    var tags = $("#upsert_sourceContent_tags").val();
    var description = $("#upsert_sourceContent_description").val();
    StartLoader();
    $.ajax({
        type: "POST",
        url: "SourceContent/AddSourceContent",
        async: false,
        data: { 'title': title, 'categoryId': categoryId, 'imageURL': imageURL, 'tags': tags, 'description': description },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla eklendi..');
                LoadContents();
                CloseAddView();
                ClearFilter();
            }
            else if (data == -1) {
                toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
                StopLoader();
            }
            else {
                toastr.error('Kayıt eklenirken hata oluştu !');
                StopLoader();
            }
        },
        error: function () {
            SetRequestError();
            LoadContents();
            CloseAddView();
            ClearFilter();
        }
    });
}

//İçerik Düzenle
function EditSourceContent(id) {
    var sourceId = parseInt($("#source_select_list").val());
    var categoryId = parseInt($("#category_select_list").val());
    var title = $('#upsert_sourceContent_title').val();
    if (categoryId <= -1 || !categoryId || !sourceId || sourceId <= -1 || !title) {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    var title = $('#upsert_sourceContent_title').val();
    var imageURL = $("#upsert_sourceContent_imageLink").val();
    var tags = $("#upsert_sourceContent_tags").val();
    var description = $("#upsert_sourceContent_description").val();
    StartLoader();
    $.ajax({
        type: "PUT",
        url: "SourceContent/EditSourceContent",
        async: false,
        data: { 'id': parseInt(id), 'title': title, 'categoryId': categoryId, 'imageURL': imageURL, 'tags': tags, 'description': description },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla güncellendi..');
                LoadContents();
                CloseAddView();
                ClearFilter();
            }
            else if (data == -1) {
                toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
                StopLoader();
            }
            else {
                toastr.error('Kayıt güncellenirken hata oluştu !');
                StopLoader();
            }
        },
        error: function () {
            SetRequestError();
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
    if (categoryId == "" || categoryId == -1)
        return;
    else {
        StartLoader();
        $('#list_content_container').load('SourceContent/Filter', { 'categoryId': categoryId, 'orderId': orderId, 'searchText': searchText }, function () {
            $('#page-1').addClass('page-selected');
            StopLoader();
        });
    }
}





//İçerik Sil
function RemoveSourceContent(id) {
    Swal.fire({
        title: 'Bu kaydı silmek istediğinizden emin misiniz ?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sil',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            StartLoader();
            parseid = parseInt(id);
            $.ajax({
                type: "DELETE",
                url: "SourceContent/RemoveSourceContent",
                async: false,
                data: { 'id': parseid },
                success: function (data) {
                    if (data > 0)
                        toastr.success('Kayıt başarıyla silindi..');
                    else
                        toastr.error('Kayıt silinirken hata oluştu !');
                    LoadContents();
                    ClearFilter();
                },
                error: function () {
                    SetRequestError();
                    LoadContents();
                    ClearFilter();
                }
            });
        }
    })
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
            $('#page-1').addClass('page-selected');
            StopLoader();
        });
    }
}


//Arama
var timer = null;
function SearchSourceContent() {
    RunSearchSpinner();
    clearTimeout(timer);
    timer = setTimeout(
        function () {
            var categoryId = parseInt($("#category_select_list").val());
            var orderId = parseInt($('#select_order').val());
            var searchText = $('#search_box').val();
            $('#list_content_container').load('SourceContent/Filter', { 'categoryId': categoryId, 'orderId': orderId, 'searchText': searchText }, function () {
                $('#page-1').addClass('page-selected');
            });

            RunSearchSpinner('fa fa-search');
        }, 1500);
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


function AddSourceSelect(callback) {
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
                if (callback)
                    callback();

            });
        }
    });
}
function GetSourceContentById(id) {
    parseid = parseInt(id);
    var result = 0;
    $.ajax({
        type: "GET",
        url: "SourceContent/GetSourceContentById",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            result = data;
        }
    });
    return result;
}

function Paging(number) {
    StartLoader();
    var categoryId = parseInt($("#category_select_list").val());
    var orderId = parseInt($('#select_order').val());
    var searchText = $('#search_box').val();
    if (categoryId != -1 || orderId != -1 || searchText != "") {
        var next = ((number - 1) * 10);
        var quantity = 10;
        $('#list_content_container').load('SourceContent/Filter', { 'categoryId': categoryId, 'orderId': orderId, 'searchText': searchText, 'next': next, 'quantity': quantity }, function () {
            $('#page-' + number).addClass('page-selected');
        });
        StopLoader();
    }
    else {
        LoadContents(next = ((number - 1) * 10), quantity = 10, function () {
            $('#page-' + number).addClass('page-selected');
        });
    }


}
function Previous() {
    var number = $('#page_content .page-selected')[0].innerHTML;
    var IntNumber = parseInt(number);
    if (IntNumber === 1)
        Paging(IntNumber);
    else
        Paging(IntNumber - 1);
}

function After() {
    var number = $('#page_content .page-selected')[0].innerHTML;
    var IntNumber = parseInt(number);
    var numDiv = $('#page-' + (IntNumber+1));
    if (numDiv.length > 0)
        Paging(IntNumber + 1);
    else
        Paging(IntNumber);
}





















