function formatNumber(n) {
    n = n.toString()
    return n[1] ? n : '0' + n
}

function toTime(year, month, day, hour, minute, second) {
    return [year, month, day].map(formatNumber).join('-') + ' ' + [hour, minute, second].map(formatNumber).join(':')
}

function wcfToTime(date) {
    date = date.replace('/Date(', '');
    date = date.replace('+0800)', '');
    var timestamp = parseInt(date);

    var newDate = new Date();
    newDate.setTime(timestamp);
    return newDate;
}

function formatTime(date) {
    var year = date.getFullYear()
    var month = date.getMonth() + 1
    var day = date.getDate()

    var hour = date.getHours()
    var minute = date.getMinutes()
    var second = date.getSeconds()

    return this.toTime(year, month, day, hour, minute, second);
}

var YFUtils = {
    formatNumber: formatNumber,
    toTime: toTime,
    formatTime: formatTime,
    wcfToTime: wcfToTime,

    formatWcfTime: function (date) {
        return formatTime(wcfToTime(date));
    },

    timeFromString: function (str) {
        var time = new Date();
        time.setTime(Date.parse(str));
        return time;
    },

    startWith: function (str, start) {
        var reg = new RegExp("^" + start);
        return reg.test(str);
    },

    dealPic: function (image) {
        if (!startWith(image, "http")) {
            return "../images/" + image;
        }
        else {
            return image;
        }
    },

    indexOf: function (array, value) {
        for (var i = 0; i < array.length; i++) {
            if (typeof value == 'function') {
                if (value(array[i])) {
                    return i;
                }
            }
            else if (array[i] == value) {
                return i;
            }
        }

        return -1;
    },

    findIf: function (array, value) {
        for (var i = 0; i < array.length; i++) {
            if (typeof value == 'function') {
                if (value(array[i])) {
                    return array[i];
                }
            }
            else if (array[i] == value) {
                return array[i];
            }
        }

        return null;
    },

    findIfR: function (array, value) {
        for (var i = 0; i < array.length; i++) {
            var index = array.length - 1 - i;
            if (typeof value == 'function') {
                if (value(array[index])) {
                    return array[index];
                }
            }
            else if (array[index] == value) {
                return array[index];
            }
        }

        return null;
    },

    where: function (array, m) {
        var result = [];
        for (var i = 0; i < array.length; i++) {
            if (m(array[i])) {
                result.push(array[i]);
            }
        }

        return result;
    },

    select: function (array, m) {
        var result = [];
        for (var i = 0; i < array.length; i++) {
            result.push(m(array[i]));
        }

        return result;
    },

    groupBy: function (array, m) {
        var result = [];
        for (var i = 0; i < array.length; i++) {
            var key = m(array[i]);
            var item = this.findIfR(result, i => i.key == key);
            if (item != null) {
                item.list.push(array[i]);
            }
            else {
                result.push({
                    key: key,
                    list: [
                        array[i]
                    ]
                });
            }
        }

        return result;
    },

    sum: function (array, m) {
        var x = 0;
        for (var i = 0; i < array.length; i++) {
            x += m(array[i]);
        }
        return x;
    },

    getDaysInOneMonth: function (year, month) {
        month = parseInt(month, 10);
        var d = new Date(year, month, 0);
        return d.getDate();
    },

    deepClone: function (obj) {
        let objClone = Array.isArray(obj) ? [] : {};
        if (obj && typeof obj === "object") {
            for (key in obj) {
                if (obj.hasOwnProperty(key)) {
                    //判断ojb子元素是否为对象，如果是，递归复制
                    if (obj[key] && typeof obj[key] === "object") {
                        objClone[key] = deepClone(obj[key]);
                    } else {
                        //如果不是，简单复制
                        objClone[key] = obj[key];
                    }
                }
            }
        }
        return objClone;
    },

    showWarnToastr: function (message) {
        toastr.warning(message, "", { timeOut: 2000, positionClass: 'toast-bottom-center' });
    },

    showInfoToastr: function (message) {
        toastr.info(message, "", { timeOut: 2000, positionClass: 'toast-bottom-center' });
    },

    showSuccessToastr: function (message) {
        toastr.success(message, "", { timeOut: 2000, positionClass: 'toast-bottom-center' });
    },

    showErrorToastr: function (message) {
        toastr.error(message, "", { timeOut: 2000, positionClass: 'toast-bottom-center' });
    },

    view_back: function () {
        window.history.go(-1);
    },
    setCookie: function (name, value, days) {
        days = days || 30;
        var exp = new Date();
        exp.setTime(exp.getTime() + days * 24 * 60 * 60 * 1000);
        document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
    },
    getCookie: function (name) {
        var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
        if (arr = document.cookie.match(reg))
            return unescape(arr[2]);
        else
            return null;
    },
    delCookie: function (name) {
        var exp = new Date();
        exp.setTime(exp.getTime() - 1);
        var cval = getCookie(name);
        if (cval != null)
            document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString();
    },
    propertyCount: function (obj, m) {
        var count = 0;
        for (var p in obj) {
            if (m && !m(obj[p])) {
                continue;
            }
            count++;
        }
        return count;
    },
    property: function (obj, index, m) {
        for (var p in obj) {
            if (m && !m(obj[p])) {
                continue;
            }
            if (index-- == 0) {
                return obj[p];
            }
        }
        return null;
    },
    getHiddenProp: function () {
        var prefixes = ['webkit', 'moz', 'ms', 'o'];
        if ('hidden' in document) return 'hidden';
        for (var i = 0; i < prefixes.length; i++) {
            if ((prefixes[i] + 'Hidden') in document)
                return prefixes[i] + 'Hidden';
        }
        return null;
    },
    isHidden: function () {
        var prop = this.getHiddenProp();
        if (!prop) return false;
        return document[prop];
    },
    addToUrl: function (name, value) {
        var param = value ? name + '=' + value : '';
        var regex = new RegExp('[?&]' + name + '=[0-9a-zA-Z-,]+', 'i');
        var old = window.location.href.match(regex);

        var newUrl = null;
        if (old && old.length > 0) {
            var sign = old[0].substring(0, 1);
            if (param) {
                param = sign + param;
            }
            if (old[0].toLowerCase() == param.toLowerCase()) {
                return;
            }
            newUrl = window.location.href.replace(old[0], param);
        }
        else {
            if (window.location.href.indexOf('?') > 0) {
                newUrl = window.location.href + '&' + param;
            }
            else {
                newUrl = window.location.href + '?' + param;
            }
        }
        history.pushState(null, null, newUrl);
    }
}