function RemoveSource(id) {
    StartLoader();
    $.ajax({
        type: "DELETE",
        url: "https://localhost:44338/source/DeleteSource",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ "Id": parseInt(id) }),
        success: function (data) {
            $("#source_list").load("/Home/_SourcePartial");
            StopLoader();
        }

    });
}
function EditSource(id) {

    name = $('#sourceHeader').val();
    image = $('#sourceLink').val();
    if (!name & !image) {
        alert("Boş olamaz");
        return;
    }
    StartLoader();
    $.ajax({
        type: "POST",
        url: "https://localhost:44338/source/UpdateSource",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'Image': image, 'Name': name, "Id": parseInt(id) }),
        success: function (data) {
            $("#source_list").load("/Home/_SourcePartial");
            StopLoader();
        }

    });

}
function GetAllCategoryPartial(id) {
    let sourceId = parseInt(id);
    $("#category_list").load("/Category/_CategoryPartial", { sourceId: sourceId });
    StopLoader();
}




