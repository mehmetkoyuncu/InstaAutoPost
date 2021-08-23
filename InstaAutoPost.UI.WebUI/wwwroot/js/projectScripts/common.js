
//Type Enum'ı
var typeEnum = {
    Category: "Category",
    Source: "Source",
    SourceContent: "SourceContent"
};
//Loader Başlatır
function StartLoader() {
    /*  $("#loader").delay(50).fadeIn();*/
    $("#loader").show();
}
//Loader bitirir.
function StopLoader() {
    $("#loader").delay(50).fadeOut();
}
//BreadComb değiştirir
function ChangeBreadComb(title, text, image) {
    $('#breadcomb_image').attr("src", image);
    $('#breadcomb_header').text(title);
    $('#breadcomb_text').text(text);
}
//Insert button değiştirir
function ChangeInsertButton(type, text) {
    $('#insert_button').html("<i class='far fa-plus-square mr-2'></i><span>" + text + " Ekle</span > ");
    $('#insert_button').attr('onclick', "Add" + type+"View()");
}
//Report buton değiştirir
function ChangeReportButton(type) {
    $('#report').attr('onclick', "Report" + type+"()");
}









function CloseUpsertView() {
    StartLoader();
    $('#upsert_container').remove();
    $('#insert_button').show();
    StopLoader();
}
function ClearInput() {
    $('#upsert_input_section input').val("");
}