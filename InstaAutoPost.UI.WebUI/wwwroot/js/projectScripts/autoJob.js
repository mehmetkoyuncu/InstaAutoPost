$(document).ready(function () {
    ChangeInsertButton(typeEnum.AutoJob, "Zamanlayıcı Ayarları");
    ChangeBreadComb("Zamanlayıcı","İçerik için otomatik çalıştırılan işlemlerin zamanını ve ayarlarını buradan güncelleyebilirsiniz. İş sürecinin başlatılması için üzerindeki seçim kutusunun seçili olması gerekmektedir. Seçili olmadığı durumda güncelleme işlemi yapıldığında iş süreci durdurulur.", "timer.jpg");
    ChangeReportButton(typeEnum.AutoJob)
    LoadAutoJobView();
});

//Zamanlayıcı Ekle View Getir
function LoadAutoJobView() {
    StartLoader();
    RenderBodyClear();
    var contentDiv = $(document.createElement('div')).attr("id", "autoJob_list_container");
    $('#render_body').append(contentDiv);
    $('#autoJob_list_container').load("AutoJobOptions/GetAutoJobOptionsPartial", function () {
        $('#insert_update_button').attr('onclick', "AddAutoJob()");
        $('#insert_button').hide();
        $('#add_view').fadeIn(1000);
        StopLoader();
    });
}
function UpdateJob(id) {
    var IsWork = $('#chckJobDate-' + id).is(':checked')
    var Cron = $('#selectJobDate-' + id).val();
    var CronDescription = $('#selectJobDate-' + id + " :selected").text();
    if (IsWork == true && Cron == "-1") {
        toastr.error('Güncellenen otomatik işlem için zaman dilimi giriniz.');
        return;
    }
    if (IsWork == false && Cron != "-1") {
        toastr.error('Güncellenen otomatik işlem için işlemi seçiniz..');
        return;
    }
    if (Cron == -1) {
        CronDescription = null;
        Cron = null;
    }

    StartLoader();
    $.ajax({
        type: "PUT",
        url: "AutoJobOptions/EditAutoJob",
        async: false,
        data: { 'id': parseInt(id), 'IsWork': IsWork, 'Cron': Cron, 'CronDescription': CronDescription },
        success: function (data) {
            if (data > 0) {
                toastr.success('Kayıt başarıyla güncellendi..');
                LoadAutoJobView();
                StopLoader();
            }
            else {
                toastr.error('Kayıt güncellenirken hata oluştu !');
                StopLoader();
            }
        },
        error: function () {
            SetRequestError();
            LoadAutoJobView();
        }
    });
}