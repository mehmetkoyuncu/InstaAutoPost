$(document).ready(function () {
    StartLoader();
    ChangeInsertButton(typeEnum.AutoJob, "Zamanlayıcı Ayarları");
    ChangeBreadComb("İçerik için otomatik çalıştırılan işlemlerin zamanını ve ayarlarını buradan güncelleyebilirsiniz.", "folder.png");
    ChangeReportButton(typeEnum.AutoJob)
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