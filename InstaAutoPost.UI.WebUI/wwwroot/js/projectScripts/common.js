function StartLoader() {
    $("#loader").delay(50).fadeIn();
}
function StopLoader() {
    $("#loader").delay(50).fadeOut();
}
function RenderBodyClear() {
    $("#codeSource").empty();
}

function ChangeBreadComb(title, text, image) {
    $('#breadcomb_image').attr("src", image);
    $('#breadcomb_header').text(title);
    $('#breadcomb_text').text(text);
}

function ChangeButtonText(type) {
    $('#upsert_button').html("<i class='far fa-plus-square mr-2'></i><span>" + type + " Ekle</span > ");
}

function UpsertChange(isInsert,type,parameter="",) {
    StartLoader();
    var contentDiv = $(document.createElement('div')).attr("id", "upsert_container");
    $('#codeSource').before(contentDiv);
    $('#upsert_container').load('Home/GetUpsertPartial', function () {
        $('#upsert_button').hide();
        if (isInsert) {
            $('#upsert_title').html('<h3 style="display:inline-block">Ekle</h3>');
            $('#upsert_save_button').text('Ekle');
            $('#upsert_save_button').attr('onclick', "Add" + type + "(" + parameter + ")");
            $('#upsert_title_icon').append("<h3 style='display:inline-block;'><i class='fas fa-plus-square'></i></h3>")
        }
        else {
            $('#upsert_title').text("<h3 style='display:inline-block'>Düzenle</h3>");
            $('#upsert_save_button').text('Düzenle');
            $('#upsert_save_button').attr('onclick', "Edit" + type + "(" + parameter + ")");
            $('#upsert_title_icon').append("<h3 style='display:inline-block;'><i class='fas fa - edit'></i></i></h3>")
            
        }
        if (type === "Category") {
            rssNameDiv = document.createElement('div');
            $(rssNameDiv).addClass('input-group mg-b-pro-edt');
            categoryNameSpan = document.createElement('span');
            $(categoryNameSpan).addClass('input-group-addon');
            $(rssNameDiv).append(categoryNameSpan);
            $(categoryNameSpan).append("<i class='icon nalika-edit' aria-hidden='true'></i>");
            $(rssNameDiv).append("<input type='text' class='form-control' id='category_name' placeholder='Kategori Adı'>")
            $('#upsert_input_section').append(rssNameDiv);

            rssURLDiv = document.createElement('div');
            $(rssURLDiv).addClass('input-group mg-b-pro-edt');
            categoryURLSpan = document.createElement('span');
            $(categoryURLSpan).addClass('input-group-addon');
            $(rssURLDiv).append(categoryURLSpan);
            $(categoryURLSpan).append("<i class='icon nalika-favorites' aria-hidden='true'></i>");
            $(rssURLDiv).append("<input type='text' id='category_url' class='form-control' placeholder='Rss Linki'>")
            $('#upsert_input_section').append(rssURLDiv);
        }
        else if (type === "RSS") {
            rssNameDiv = document.createElement('div');
            $(rssNameDiv).addClass('input-group mg-b-pro-edt');
            categoryNameSpan = document.createElement('span');
            $(categoryNameSpan).addClass('input-group-addon');
            $(rssNameDiv).append(categoryNameSpan);
            $(categoryNameSpan).append("<i class='icon nalika-edit' aria-hidden='true'></i>");
            $(rssNameDiv).append("<input type='text' class='form-control' id='category_name' placeholder='Kategori Adı'>")
            $('#upsert_input_section').append(rssNameDiv);

            rssURLDiv = document.createElement('div');
            $(rssURLDiv).addClass('input-group mg-b-pro-edt');
            categoryURLSpan = document.createElement('span');
            $(categoryURLSpan).addClass('input-group-addon');
            $(rssURLDiv).append(categoryURLSpan);
            $(categoryURLSpan).append("<i class='icon nalika-favorites' aria-hidden='true'></i>");
            $(rssURLDiv).append("<input type='text' id='category_url' class='form-control' placeholder='Rss Linki'>")
            $('#upsert_input_section').append(rssURLDiv);
        }
        StopLoader(); 
    });
}
function CloseUpsertView() {
    StartLoader();
    $('#upsert_container').remove();
    $('#upsert_button').show();
    StopLoader();
}
function ClearInput() {
    $('#upsert_input_section input').val("");
}