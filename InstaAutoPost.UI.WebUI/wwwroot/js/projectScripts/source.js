$(document).ready(function () {
    RenderBodyClear();
    StartLoader();
    RefreshGetSourceLoad();
});
function RefreshGetSourceLoad() {
    $('#codeSource').load('Source/GetSources', function () {
        StopLoader()
    });
}

function GetAllSource() {
    RefreshGetSourceLoad();
}
function RemoveAddSourceView() {
    StartLoader();
    $('#add_source').remove();
    $('#add_view_source_button').show();
    StopLoader();
}

function AddSourceView(element) {
    StartLoader();
    var contentDiv = $(document.createElement('div')).attr("id", "add_source");
    $('#source_list_container').before(contentDiv);
    $('#add_source').load('Source/GetAddSourcesPartial', function () {
        $('#upsert_button').text('Ekle');
        $('#upsert_button').attr('onclick', "AddSource()");
        $('#add_view_source_button').hide();
        StopLoader();
    });
}

function AddSource() {
    StartLoader();
    $.ajax({
        type: "POST",
        url: "Source/AddSource",
        async: false,
        data: { 'name': $('#source_name').val(), 'image': $('#source_image').val() },
        success: function (data) {
            $('#codeSource').load('Source/GetSources', function () {
                $('#add_view_source_button').hide();
                RemoveAddSourceView();
                StopLoader();
            });
        }
    });

}
function GetEditSourceView(element, id) {
    StartLoader();
    if ($('#add_source').length == 0) {
        parseid = parseInt(id);
        $.ajax({
            type: "GET",
            url: "Source/GetSourceById",
            async: false,
            data: { 'id': parseid },
            success: function (data) {
                var contentDiv = $(document.createElement('div')).attr("id", "add_source");
                $('#source_list_container').before(contentDiv);
                $('#add_source').load('Source/GetAddSourcesPartial', function () {
                    $('#add_view_source_button').hide();
                    $('#source_image').val(data.image)
                    $('#source_name').val(data.name);
                    $('#upsert_button').text('Düzenle');
                    $('#upsert_button').attr('onclick', 'EditSource(' + id + ')');
                    StopLoader();
                });
            }
        });
    }
    else {
        alert("Seçili olanı kapat");
        StopLoader();
    };
}
function ClearSource() {
    $('#source_image').val("");
    $('#source_name').val("")
}
function EditSource(id) {
    StartLoader();
    parseid = parseInt(id);
    $.ajax({
        type: "PUT",
        url: "Source/EditSource",
        async: false,
        data: { 'id': parseid, 'name': $('#source_name').val(), 'image': $('#source_image').val() },
        success: function (data) {
            $('#codeSource').load('Source/GetSources', function () {
                RemoveAddSourceView();
                StopLoader();
            });
        }
    });
}
function RemoveSource(id) {
    StartLoader();
    parseid = parseInt(id);
    $.ajax({
        type: "DELETE",
        url: "Source/RemoveSource",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            $('#codeSource').load('Source/GetSources', StopLoader());
        }
    });
}

function DetailsSource(id) {
    StartLoader();
    parseid = parseInt(id);
    var contentDiv = $(document.createElement('div')).attr("id", "detailSource");
    $('#source_list_container').before(contentDiv);
    $('#detailSource').load('Source/DetailSource', { "id": parseid } , function () {
        $("#detail_source_modal").modal('show');
        StopLoader();
    });

}

function CloseModal() {
    $("#detail_source_modal").modal('hide');
}

function ViewDetail(id) {
    StartLoader();
    CloseModal();
    $('.modal-backdrop').remove();
    parseid = parseInt(id);
    GetCategories(id);
    StopLoader();
}