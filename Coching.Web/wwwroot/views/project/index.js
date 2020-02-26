layui.define(["layer"],
    function (i) {
    }
);

var tool = {
    partnerHtml: function (p) {
        return '<img id="' + p.ID + '" class="partner-image" src="' + header(p.User.Header) + '" title="' + p.User.Name + '[' + p.RoleTitle + ']" />';
    },
    projectInnerHtml: function (project) {
        var html = '<div class="layout-row title">'
            + '<div class="ellipsis-1 text-color-important text-size-common-mid flex">' + (project.Name || '&nbsp;') + '</div>'
            + '<img src="/res/workers.png" title="成员管理" onclick="workers(this)" />'
            + '<img src="/res/edit.png" title="编辑" onclick="edit(this)" />'
            + '</div>'
            + '<div class="layout-row descrip">'
            + '<div class="ellipsis-2 flex text-color-descrip text-size-descrip">' + (project.Description || '&nbsp;') + '</div>'
            + '<img class="logo" src="' + projectHeader(project.Header) + '" />'
            + '</div>'
            + '<div class="layout-row layout-center-h partners">';

        for (var i = 0; i < project.Partners.length; i++) {
            html += this.partnerHtml(project.Partners[i]);
        }
        return html + '</div>';
    },
    projectHtml: function (project) {
        return '<div id="' + project.ID + '" class="item-container" onclick="go_coching(this)">'
            + this.projectInnerHtml(project)
            + '</div>';
    }
}

function partners_notify(project, del, add) {
    if (del) {
        $('#' + project + ' #' + del).remove();
    }
    if (add) {
        if ($('#' + project + ' #' + add.ID).length == 0) {
            $('#' + project + ' .partners').append(tool.partnerHtml(add));
        }
        else {
            $('#' + project + ' #' + add.ID).replaceWith(tool.partnerHtml(add));
        }
    }
}
