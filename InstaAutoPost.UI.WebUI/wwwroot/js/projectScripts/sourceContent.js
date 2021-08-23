function LoadSourceContentList(categoryId) {
    $('#codeSource').load('SourceContent/GetSourceContent', { 'categoryId': categoryId }, function () {
    });
};

function RemoveSourceContent(id, categoryId) {
    StartLoader();
    parseid = parseInt(id);
    $.ajax({
        type: "DELETE",
        url: "SourceContent/RemoveSourceContent",
        async: false,
        data: { 'id': parseid },
        success: function (data) {
            var a = 5;
            LoadSourceContentList(categoryId);
            StopLoader();
        }
    });
}
function EditSourceContentView(int categoryId) {

}
