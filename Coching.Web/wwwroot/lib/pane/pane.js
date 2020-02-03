function close_yui_pane(id) {
    $('#' + id).remove();
}

var pane = {
    open: function (options) {
        options.id = options.id || 'yui-pane';
        options.height = options.height || '100%';
        options.shadeClose = !!options.shadeClose;

        var html = '<div id="' + options.id + '" class="yui-pane-container layout-column" style="display: none">';
        if (options.shadeClose) {
            html += '<div class="flex" onclick="close_yui_pane(\'' + options.id + '\')">';
        }
        else {
            html += '<div class="flex">';
        }

        html += '</div>' + '<div class="yui-pane layout-column" style="height: ' + options.height + ';">';
        if (options.title) {
            html += '<div class="yui-pane-title">'
                + '<i class="layui-icon layui-icon-close" onclick="close_yui_pane(\'' + options.id + '\')"></i>'
                + '<span>' + options.title + '</span>'
                + '</div>';
        }

        html += '<iframe scrolling="auto" allowtransparency="true" onload="" class="flex" frameborder="0" name="' + options.id + '" src="' + options.src + '"></iframe>'
            + '</div>'
            + '</div >';

        $(document.body).append(html);
        $('#' + options.id).show();
        $('#' + options.id + ' .yui-pane').addClass('yui-pane_toggle');
        return options.id;
    },
    close: function (id) {
        $('#' + id).remove();
    }
}