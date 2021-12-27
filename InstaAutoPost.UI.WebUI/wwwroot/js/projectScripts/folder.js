$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.Folder, "Zamanlayıcı");
    ChangeBreadComb("İçerik İçin Otomatik Klasör Oluştur ve Zamanla", "İçeriklerden sıralayarak(eskiden yeniye) belirlenen zaman periyodunda otomatik olarak kullanıcının belirlediği dizinde klasör oluşturur ve içerik için bir resim ve text dosyası oluşturur. İşlemin ardından giriş yapan kullanıcıya mail gönderilir. ", "folder.png");
    ChangeReportButton(typeEnum.Folder)
    AddFolderView();
});

//Zamanlayıcı Ekle View Getir
function AddFolderView() {
    var viewControl = $('#add_view');
    if (viewControl.length > 0) {
        toastr.warning('İşlem yapmadan önce açık olan formu kapatın..');
        return;
    }
    ClearAddView();
    var contentDiv = $(document.createElement('div')).attr("id", "add_view");
    $('#breadcomb_container').after(contentDiv);
    $('#add_view').hide().load("Folder/GetFolderPartial", function () {
        $('#insert_update_button').attr('onclick', "AddFolder()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
    });
    StopLoader();
}