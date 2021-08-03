function AddViewAfter(idOfTheCreatedDiv, IdToBeAddedAfterDiv, urlSource) {
    var contentDiv = $(document.createElement('div')).attr("id", idOfTheCreatedDiv);
    $('#' + IdToBeAddedAfterDiv).after(contentDiv);
    $('#' + idOfTheCreatedDiv).load(urlSource);
}