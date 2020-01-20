var layui_menu = null;

function LayuiMenu() {
    layui.define(["layer"],
        function (i) {
        }
    );

    this.show = function (elem, datas, callback, pos) {
        var n = '';
        for (var i = 0; i < datas.length; i++)
        {
            var data = datas[i];
            if (data.line) {
                n += '<li class="line"/>';
            }
            else {
                n += '<li ' + (data.active ? 'class="layui-this"' : '') + ' cmd-event="' + data.value + '">' + data.title + '</li>';
            }
        }
        n = '<ul>' + n + '</ul>';
        var that = this;
        layui_menu = layui.layer.tips(n, elem, {
            tips: 3,
            time: 0,
            fixed: !0,
            skin: "layui-box layui-menu",
            success: function (i) {
                i.find("li").on("mousedown",
                    function (i) {
                        layui.stope(i);
                    }).on("click", function () {
                        callback($(this).attr('cmd-event'));
                        that.close();
                    })
            }
        });
        $(document).off("mousedown", that.close).on("mousedown", that.close);
        $(window).off("resize", that.close).on("resize", that.close);
    }

    this.close = function () {
        if (layui_menu) {
            layui.layer.close(layui_menu);
            layui_menu = null;
        }
    }

    this.visible = function () {
        return !!layui_menu;
    }
}