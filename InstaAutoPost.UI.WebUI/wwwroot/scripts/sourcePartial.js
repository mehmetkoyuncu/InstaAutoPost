function RemoveSource(id) {
    StartLoader();
    $.ajax({
        type: "DELETE",
        url: "/Home/RemoveSource",
        data: { "Id": parseInt(id) },
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
        url: "Home/EditSource",
        data: { 'image': image, 'name': name, "Id": parseInt(id) },
        success: function (data) {
            $("#source_list").load("/Home/_SourcePartial");
            StopLoader();
        }
    });

}
function GetAllSourcePartial() {
    $("#source_list").load("/Home/_SourcePartial");
    StopLoader();
}




