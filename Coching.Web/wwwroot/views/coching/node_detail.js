﻿
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

function modify_worker_success(result) {
    $('#worker_header').attr('src', header(result.Worker.Header));
    $('#worker_name').html(result.Worker.Name);

    if (init_data.is_admin() || !result.Coching && init_data.me_id() == result.WorkerGuid) {
        $('.node_time .node_time_clickable').addClass('clickable');
        $('.node_time .node_time_clickable').attr('onclick', "show_hide(['.node_time_edit'], ['.node_time'])");
    }
    else {
        $('.node_time .node_time_clickable').removeClass('clickable');
        $('.node_time .node_time_clickable').attr('onclick', "");
    }

    parent[init_data.notify()](result, true);
}

function modify_success(result) {
    $('#NodeName').html(result.Name);
    $('#NodeDescription').html(result.HtmlDescription);
    $('.node-documents-container').html(tool.documentsInnerHtml(result.Documents));
    $('.node-documents-container .documents').lightGallery({
        share: false
    });

    if (result.Coching) {
        $('.offer').removeClass('hidden');
        $('.node_time .node_time_label').html('考成工时');
        $('.node_time_edit label').html('考成工时');
        $('.node_time_edit input').attr('placeholder', '请确定考成业绩工时');
        $('.node_time_edit .help-info').html('最终确定的考成业绩工时（管理员）');
    }
    else {
        $('.offer').addClass('hidden');
        $('.node_time .node_time_label').html('预估工时');
        $('.node_time_edit label').html('预估工时');
        $('.node_time_edit input').attr('placeholder', '请预估工时');
        $('.node_time_edit .help-info').html('请预估工时（执行者）');
    }

    if (init_data.is_admin() || !result.Coching && init_data.me_id() == result.WorkerGuid) {
        $('.node_time .node_time_clickable').addClass('clickable');
        $('.node_time .node_time_clickable').attr('onclick', "show_hide(['.node_time_edit'], ['.node_time'])");
    }
    else {
        $('.node_time .node_time_clickable').removeClass('clickable');
        $('.node_time .node_time_clickable').attr('onclick', "");
    }

    if (init_data.is_admin() && result.Coching) {
        $('.node_time_edit a').removeClass('hidden');
    }
    else {
        $('.node_time_edit a').addClass('hidden');
    }

    $('.node_time .node_time_info').html(result.EstimatedTime);

    parent[init_data.notify()](result);
}

function calc_node_time() {
    $.post(init_data.node_time_url(), {
        id: init_data.node_id()
    }, function (result) {
        if (!result.Success) {
            top.layer.msg(result.Message);
        }
        else {
            $('.node_time_edit input').val(result.Body);
        }
    });
}