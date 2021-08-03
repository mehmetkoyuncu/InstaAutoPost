function GetAllSource() {
    $('#codeSource').load('Source/GetSources');
}
function RemoveAddSourceView() {
    $('#add_source_view_content').remove();
    $('#add_view_source_button').show();
}

function AddSourceView(element) {
    $(element).hide();
    AddViewAfter('add_source', 'header_adv_area', 'Source/GetAddSourcesPartial');
}
