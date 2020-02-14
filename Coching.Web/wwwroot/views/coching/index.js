layui.define(["layer"],
    function (i) {
    }
);

var tree = null;

var NodeStatus = {
    未进行: 0,
    进行中: 1,
    完成: 2,
    取消: 3,
    暂停: 4
}
var NodeColors = [
    '#62b7f4',
    '#FF4500',
    '#006400',
    '#888888',
    '#080808',
]
var tool = {
    findNode: function (id, array) {
        for (var i = 0; i < array.length; i++) {
            if (array[i].id == id) {
                return array[i];
            }
            if (array[i].children) {
                var node = this.findNode(id, array[i].children);
                if (node) {
                    return node;
                }
            }
        }
        return null;
    },
    removeNode: function (id, array) {
        for (var i = 0; i < array.length; i++) {
            if (array[i].id == id) {
                array.splice(i, 1);
                break;
            }

            if (array[i].children) {
                array[i].children = this.removeNode(id, array[i].children);
            }
        }
        return array;
    },
    minSize: function (array) {
        var x = 1, y = 0;
        for (var i = 0; i < array.length; i++) {
            if (array[i].collapsed || !array[i].children || array[i].children.length == 0) {
                y += 1;
                continue;
            }

            var childrenSize = this.minSize(array[i].children);
            x = Math.max(x, 1 + childrenSize.x);
            y += childrenSize.y;
        }
        return {
            x: x,
            y: y
        }
    },
    toTreeData: function (array) {
        return YFUtils.select(array, i => {
            return {
                id: i.ID,
                root: i.RootGuid,
                parent: i.ParentGuid,
                name: i.Name,
                label: i.Label,
                description: i.Description,
                coching: i.Coching,
                creator: {
                    id: i.Creator.ID,
                    name: i.Creator.Name,
                    header: i.Creator.Header
                },
                worker: i.Worker == null ? null : {
                    id: i.Worker.ID,
                    name: i.Worker.Name,
                    header: i.Worker.Header
                },
                status: {
                    value: i.Status,
                    title: i.StatusTitle,
                    time: i.TimeInfo
                },
                children: this.toTreeData(i.Children || []),
                collapsed: false,
                itemStyle: {
                    color: NodeColors[i.Status],
                    borderWidth: i.Coching && i.EstimatedManHour == 0 && i.Status != NodeStatus.未进行 && i.Status != NodeStatus.取消 ? 1 : 0,
                    borderColor: '#FF0000',
                    shadowColor: '#000000',
                    shadowBlur: 0
                },
            }
        });
    },
    statusHtml: function (result, workers) {
        var status = '<div class="layout-row layout-center-h root-status">';
        status += '<div class="workers layout-row layout-center-h flex">';
        if (workers) {
            var partners = init_data.partners();
            partners = YFUtils.where(partners, p => {
                return YFUtils.findIf(workers, w => p.UserGuid == w) != null;
            });
            for (var i = 0; i < partners.length; i++) {
                status += '<img worker="' + partners[i].User.ID + '" src="' + header(partners[i].User.Header) + '" title="' + partners[i].User.Name + '" />';
            }
        }
        status += '</div>';
        status += '<div class="status-value text-size-descrip text-color-descrip status-color' + result.Status + '">' + result.StatusTitle + '</div>';
        status += '</div>';
        return status;
    },
    innerHtml: function (result, workers) {
        return '<div class="ellipsis-1 text-size-descrip text-color-common">' + result.Name + '</div>'
            + '<div class="ellipsis-2 text-size-min text-color-descrip">' + (result.Description || '') + '</div>'
            + this.statusHtml(result, workers);
    },
    itemHtml: function (result, workers) {
        return '<div id="' + result.ID + '" class="catalogue-item" onclick="root(this)">'
            + this.innerHtml(result, workers)
            + '</div>';
    },
    updateHtml: function (result) {
        $('#' + result.ID + ' .ellipsis-1').html(result.Name);
        $('#' + result.ID + ' .ellipsis-2').html(result.Description || '');
        for (var status in NodeStatus) {
            $('#' + result.ID + ' .status-value').removeClass('status-color' + NodeStatus[status]);
        }
        $('#' + result.ID + ' .status-value').addClass('status-color' + result.Status);
        $('#' + result.ID + ' .status-value').html(result.StatusTitle);
    },
    addWorker: function (id, worker) {
        if ($('#' + id + '[worker=' + worker.ID + ']').length == 0) {
            $('#' + id + ' .workers').append('<img worker="' + worker.ID + '" src="' + header(worker.Header) + '" />');
        }
    },
    expand: function (node) {
        node.collapsed = false;
        node.itemStyle.shadowBlur = 0;
    },
    collapse: function (node) {
        node.collapsed = true;
        node.itemStyle.shadowBlur = 10;
    },
    collapseOther: function (nodes, id) {
        var cancel = false;
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].id != id) {
                if (nodes[i].children && nodes[i].children.length > 0) {
                    var c_cancel = this.collapseOther(nodes[i].children, id);
                    if (!c_cancel) {
                        this.collapse(nodes[i]);
                    }
                    cancel = cancel || c_cancel;
                }
            }
            else {
                cancel = true;
            }
        }
        return cancel;
    }
}

var menu = {
    commands: function (node) {
        var cmds = [
            {
                name: '添加分支',
                command: 'addChild'
            }
        ];
        if (!init_data.right_menu) {
            cmds.push({
                name: '查看详情',
                command: 'detail'
            })
        }
        if (node.data.collapsed) {
            cmds.push({
                name: '展开',
                command: 'expand'
            })
        }
        else if (node.data.children && node.data.children.length > 0) {
            cmds.push({
                name: '收缩',
                command: 'collapse'
            })
        }
        cmds.push({
            name: '收缩其他',
            command: 'collapseOther'
        });
        cmds.push({
            line: true
        });
        cmds.push({
            name: '删除',
            command: 'delete',
            warn: true
        });
        return cmds;
    },
    execute: function (command, node) {
        this[command].call(this, node);
    },
    addChild: function (node) {
        add_child(node.data.id, node.data.root);
    },
    detail: function (node) {
        init_data.detail(node);
    },
    delete: function (node) {
        layer.confirm('确定要删除么', function (index) {
            $.post(init_data.del(), {
                id: node.data.id
            }, function (result) {
                if (!result.Success) {
                    top.layer.msg(result.Message);
                }
                else {
                    if (node.data.id == node.data.root) {
                        $('#' + node.data.id).remove();
                        tree.refresh([]);
                    }
                    else {
                        var data = tool.removeNode(node.data.id, tree.getOption().series[0].data);
                        tree.refresh(data);
                        reload_root(node.data.root);
                    }
                }
            });

            layer.close(index);
        });
    },
    expand: function (node) {
        tool.expand(node.data);
        tree.refresh(null, true);
    },
    collapse: function (node) {
        tool.collapse(node.data);
        tree.refresh(null, true);
    },
    collapseOther: function (node) {
        var data = tree.getOption().series[0].data;
        tool.collapseOther(data, node.data.id);
        tree.refresh(data, true);
    }
}

function root_id(id) {
    $.post(init_data.tree(), {
        id: id
    }, function (result) {
        if (!result.Success) {
            top.layer.msg(result.Message);
        }
        else {
            if (window.location.href.toLowerCase().indexOf(('rootGuid=' + id).toLowerCase()) < 0) {
                var regex = /rootGuid=[0-9a-zA-Z-]+/i;
                var newUrl = null;
                if (window.location.href.match(regex)) {
                    newUrl = window.location.href.replace(regex, 'rootGuid=' + id);
                }
                else {
                    newUrl = window.location.href + '&rootGuid=' + id;
                }
                history.pushState(null, result.Body.Name, newUrl);
            }
            $('.catalogue-item').removeClass('layui-this');
            $('#' + id).addClass('layui-this');

            if (init_data.root_id) {
                init_data.root_id(result.Body);
            }

            init_tree(tool.toTreeData([result.Body]));
        }
    });
}

function root(e) {
    root_id($(e).attr('id'));
}

function reload_root(id) {
    $.post(init_data.root(), {
        id: id
    }, function (result) {
        if (!result.Success) {
            top.layer.msg(result.Message);
        }
        else {
            $('#' + result.Body.Node.ID).html(tool.innerHtml(result.Body.Node, result.Body.Workers));
        }
    });
}

function changed_notify(result, reload) {
    if (result.ID == result.RootGuid) {
        tool.updateHtml(result);
    }
    if (reload) {
        reload_root(result.RootGuid);
    }

    var array = tree.getOption().series[0].data;
    var node = tool.findNode(result.ID, array);

    if (node) {
        var newNode = tool.toTreeData([result])[0];
        for (var p in newNode) {
            if (p == 'id' || p == 'children' || p == 'collapsed') {
                continue;
            }
            node[p] = newNode[p];
        }
        tree.refresh(array);
    }
}

function optimizeSize(data) {
    var size = tool.minSize(data);
    $('.tree').css('min-width', size.x * 300);
    $('.tree').css('min-height', size.y * 100);
}

function optimizeScroll() {
    if ($('.tree').width() > $('.tree-container').width()) {
        $('.tree-container').scrollLeft($('.tree').width() / 2 - $('.tree-container').width() / 2);
    }
    if ($('.tree').height() > $('.tree-container').height()) {
        $('.tree-container').scrollTop($('.tree').height() / 2 - $('.tree-container').height() / 2);
    }
}

function init_tree(data) {
    data = data || [];
    optimizeSize(data);
    if (tree) {
        tree.dispose();
        tree = null;
    }
    tree = echarts.init($('.tree')[0]);
    optimizeScroll();
    tree.showLoading();

    if (init_data.right_menu) {
        $('.tree-container').bind("contextmenu", function (x) {
            return !init_data.right_menu.visible();
        });

        tree.on('contextmenu', function (params) {
            var commands = menu.commands(params);
            var datas = YFUtils.select(commands, cmd => {
                if (cmd.line) {
                    return {
                        line: true
                    }
                }
                return {
                    value: cmd.command,
                    title: cmd.name
                }
            });

            $('.right-click-menu-pos').css({
                'left': params.event.offsetX,
                'top': params.event.offsetY
            });

            init_data.right_menu.show($('.right-click-menu-pos'), datas, function (value) {
                menu.execute(value, params);
            });
        });

        $('.tree-container').click(function () {
            init_data.right_menu.close();
        })
    }

    if (init_data.click) {
        tree.on('click', function (params) {
            init_data.click(params);
        });
    }

    var option = {
        tooltip: {
            trigger: 'item',
            triggerOn: 'mousemove'
        },
        series: [
            {
                type: 'tree',

                data: data,

                left: '20%',
                right: '20%',
                top: '20%',
                bottom: '20%',
                roam: true,
                symbol: function (node, data) {
                    return data.data.coching ? 'roundRect' : 'rect';
                },
                symbolSize: [160, 30],
                initialTreeDepth: -1,

                expandAndCollapse: false,
                label: {
                    position: 'inside',
                    rotate: 0,
                    verticalAlign: 'middle',
                    align: 'center',
                    fontSize: 9,
                    formatter: function (node) {
                        if (node.data.worker && node.data.worker.id == init_data.me_id()) {
                            return ['{mine|' + node.data.label + '}'].join('\n');
                        }
                        else {
                            return ['{name|' + node.data.label + '}'].join('\n');
                        }
                    },
                    rich: {
                        name: {
                            color: 'white'
                        },
                        mine: {
                            color: 'yellow'
                        }
                    }
                },
                tooltip: {
                    formatter: function (node) {
                        var status = null;
                        if (node.data.worker) {
                            status = '<img src="' + header(node.data.worker.header) + '">';
                        }
                        else {
                            status = '<img src="' + header(node.data.creator.header) + '">';
                        }

                        var title = node.data.status.title;
                        if (node.data.status.time) {
                            title += ' ▪ ' + node.data.status.time;
                        }
                        status += '<div class="flex">' + title + '</div>';

                        return '<div class="tooltip-container">'
                            + '<div class="tooltip-title">' + node.data.name + '</div>'
                            + '<div class="tooltip-content">' + (node.data.description || '无详细内容') + '</div>'
                            + '<div class="tooltip-status layout-row layout-center-h">'
                            + status
                            + '</div>';
                        + '</div>';
                    },
                    trigger: init_data.tooltip ? 'item' : 'none'
                },

                animationDurationUpdate: 750
            }
        ]
    }

    tree.setOption(option);

    tree.refresh = function (data, scroll) {
        option.series[0].data = data || tree.getOption().series[0].data;
        optimizeSize(option.series[0].data);
        tree.resize();
        tree.setOption(option);
        if (scroll) {
            optimizeScroll();
        }
    }

    tree.hideLoading();
}
