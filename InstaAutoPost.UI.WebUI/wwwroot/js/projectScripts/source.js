

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



//Kaynak Ekle formu getir
function AddSourceView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("Source/GetAddSourcePartial", function () {
        $('#insert_update_button').attr('onclick', "AddSource()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
        $('#form_controls label').hide();
    });
}

//Kaynak Düzenle formu getir
function EditSourceView(id) {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("Source/GetAddSourcePartial", function () {
        $("html, body").animate({ scrollTop: 0 }, "slow");
        $('#insert_update_button').attr('onclick', 'EditSource(' + id + ')');
        var source = GetSourceById(id);
        var name = source.value.name.trim();
        var image = source.value.image;
        $('#upsert_source_name').val(name);
        $("#upsert_source_imageLink").val(image);
        $('#insert_update_button').text('Düzenle');
        $('#insert_button').hide();
        $("html").animate({ "scrollTop": $("#add_view").scrollTop() + 100 });
        $('#add_view').fadeIn(1000);
    });
}



//Kaynak Ekle
function AddSource() {
    var name = $('#upsert_source_name').val()
    if (!name) {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    var imageLink = $("#upsert_source_imageLink").val();
    StartLoader();
    $.ajax({
        type: "POST",
        url: "Source/AddSource",
        async: false,
        data: { 'name': name, 'image': imageLink },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla eklendi..');
                LoadSources();
                CloseAddView();
                ClearFilter();
                StopLoader();
            }
            else {
                toastr.error('Kayıt eklenirken hata oluştu !');
                StopLoader();
            }
        },
        error: function () {
            SetRequestError();
            LoadSources();
            CloseAddView();
            ClearFilter();
        }
    });
}

//Kaynak Düzenle
function EditSource(id) {
    var name = $('#upsert_source_name').val()
    if (!name) {
        toastr.error('Lütfen zorunlu alanları (*) doldurunuz.');
        return;
    }
    var imageLink = $("#upsert_source_imageLink").val();
    StartLoader();
    $.ajax({
        type: "PUT",
        url: "Source/EditSource",
        async: false,
        data: { 'id': parseInt(id), 'name': name, 'image': imageLink },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla güncellendi..');
                LoadSources();
                CloseAddView();
                ClearFilter();
                StopLoader();
            }
            else {
                toastr.error('Kayıt güncellenirken hata oluştu !');
                StopLoader();
            }
        },
        error: function () {
            SetRequestError();
            LoadSources();
            CloseAddView();
            ClearFilter();
        }
    });
}


//Kaynak Sil
function RemoveSource(id) {
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
                url: "Source/RemoveSource",
                async: false,
                data: { 'id': parseid },
                success: function (data) {
                    if (data > 0)
                        toastr.success('Kayıt başarıyla silindi..');
                    else
                        toastr.error('Kayıt silinirken hata oluştu !');
                    LoadSources();
                    ClearFilter();
                },
                error: function () {
                    SetRequestError();
                    LoadCategories();
                    ClearFilter();
                }
            });
        }
    })



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
var timer = null;
function SearchSource() {
    RunSearchSpinner();
    clearTimeout(timer);
    timer = setTimeout(
        function () {
            var orderId = parseInt($('#select_order').val());
            var searchText = $('#search_box').val()
            $('#list_sources_container').load('Source/Filter', { 'orderId': orderId, 'searchText': searchText }, function () {
            });
            RunSearchSpinner('fa fa-search');
        }, 1500);
}

//Filtreyi Temizle
function ClearFilter() {
    StartLoader();
    $("#select_order").val(-1);
    $('#search_box').val('');
    StopLoader();
}


//Düzenleme için id'ye göre kategoriyi getir
function GetSourceById(id) {
    parseid = parseInt(id);
    var result = 0;
    $.ajax({
        type: "GET",
        url: "Source/GetSourceById",
        contentType: "application/json; charset=utf-8",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            result = data;
        }
    });
    return result;
}


























