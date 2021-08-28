
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
    $('#breadcomb_image').attr("src", "/img/breadcomb/" + image);
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


//Ekle formu kapat
function CloseAddView() {
    StartLoader();
    $('#add_view').remove();
    $('#insert_button').show();
    StopLoader();
}
//Ekle formu inputları temizle
function ClearAddView() {
    $('#add_view input').val('');
}