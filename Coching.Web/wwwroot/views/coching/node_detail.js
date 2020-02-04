var menu = null;
var tool = {
    documentsInnerHtml: function (documents) {
        if (!documents || documents.length == 0) {
            return '';
        }
        var html = '<div class="documents">';
        for (var i = 0; i < documents.length; i++) {
            var doc = documents[i];
            html += '<a href="' + doc.Document.Src + '"><img src="' + doc.Document.Src + '" /></a>\n';
        }
        html += '</div>';
        return html;
    }
}

function show_hide(show, hide) {
    for (var i = 0; i < show.length; i++) {
        $(show[i]).show();
    }
    for (var i = 0; i < hide.length; i++) {
        $(hide[i]).hide();
    }
}