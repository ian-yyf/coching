using System;
using System.Collections.Generic;
using System.Text;

namespace Coching.Model.Data
{
    public enum ActionLogKind
    {
        创建项目,
        修改项目,
        删除项目,
        添加分支,
        修改分支,
        删除分支,
        改变分支执行人,
        修改分支状态,
        预报预估工时,
        确定预估工时,
        添加批注,
        修改批注,
        删除批注,
        修改实际工时
    }
}
