$(document).ready(function () {
    StartLoader();
    GetAllSourcePartial();
});


function AddSource() {
   
    let name = $('#sourceHeader').val();
    let image = $('#sourceLink').val();
    name = $('#sourceHeader').val();
    image = $('#sourceLink').val();
    if (!name & !image) {
        alert("Boş olamaz");
        return;
    }
    StartLoader();
    $.ajax({
        type: "POST",
        url: "https://localhost:44338/source/AddSource",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'Image': image, 'Name': name }),
        success: function (data) {
            $("#source_list").load("/Home/_SourcePartial");
            StopLoader();
        }
    });

}
function UpdateSource(id,name) {

    $('#InsSource').remove();
    $("html, body").animate({ scrollTop: 0 }, "slow");
    $('#sourceHeader').val("");
    $('#sourceLink').val("");
    $('#flush-collapseOne').collapse("show");
    $('#sourceTextSpan').text("Kaynak Düzenle");
    $('#sourceHeader').val(name);
    /*    '<a onclick"InsertSource()"> (Ekle) </a>'*/
    $('#accordionFlushExample').append('<a onclick="InsertSource()" id="InsSource"> Ekle </a>');
    $("#submitSource").attr("onclick", "EditSource(" + id + ")");
    //StartLoader();
    //$.ajax({
    //    type: "POST",
    //    url: "https://localhost:44338/source/AddSource",
    //    datatype: "json",
    //    contentType: "application/json; charset=utf-8",
    //    data: JSON.stringify({ 'Image': image, 'Name': name }),
    //    success: function (data) {
    //        $("#source_list").load("/Home/_SourcePartial");
    //        StopLoader();
    //    }

    //});
}
function InsertSource() {
    $("#submitSource").attr("onclick", "AddSource()");
    $('#sourceTextSpan').text("Kaynak Ekle");
    $('#sourceHeader').val("");
    $('#sourceLink').val("");
    $('#InsSource').remove();
   

}
function UpdateSourceWithImage(id, name, image) {
    $('#InsSource').remove();
    $("html, body").animate({ scrollTop: 0 }, "slow");
    $('#sourceHeader').val("");
    $('#sourceLink').val("");
    $('#flush-collapseOne').collapse("show");
    $('#sourceTextSpan').text("Kaynak Düzenle");
    $('#sourceHeader').val(name);
    $('#sourceLink').val(image);
    $('#accordionFlushExample').append('<a onclick="InsertSource()" id="InsSource"> Ekle </a>');
    $("#submitSource").attr("onclick", "EditSource("+id+")");
}


$('#submitSource').click(function () {
    $('#sourceHeader').val("");
    $('#sourceLink').val("");
    $('#sourceLink').val("");
    $('#sourceHeader').val("");

})





