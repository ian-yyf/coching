(function(g, f){
    typeof define === 'function' && define.amd ? define(f) :
    g.lozadImg = f();
}(this, function(){
    var win = {
        height: window.screen.availHeight,
        wight: window.screen.availWidth
    };

    function markAsLoaded(element) {
        element.setAttribute('data-loaded', true);
    }

    function isLoaded(element) {
        return element.getAttribute('data-loaded') === 'true';
    }

    function loaded(element){
        if (element.nodeName.toLowerCase() === 'picture') {
            var img = document.createElement('img');
            if (isIE && element.getAttribute('data-iesrc')) {
                img.src = element.getAttribute('data-iesrc');
            }
            if (element.getAttribute('data-alt')) {
                img.alt = element.getAttribute('data-alt');
            }
            element.appendChild(img);
        }
        if (element.getAttribute('data-src')) {
            element.src = element.getAttribute('data-src');
        }
        if (element.getAttribute('data-srcset')) {
            element.srcset = element.getAttribute('data-srcset');
        }
        if (element.getAttribute('data-background-image')) {
            element.style.backgroundImage = 'url(\'' + element.getAttribute('data-background-image') + '\')';
        }
        if (element.getAttribute('data-toggle-class')) {
            element.classList.toggle(element.getAttribute('data-toggle-class'));
        }
    }

    function getScrollTop(){
        var scrollTop=0;
        if(document.documentElement && document.documentElement.scrollTop){
            scrollTop=document.documentElement.scrollTop;
        }else if(document.body){
            scrollTop=document.body.scrollTop;
        }
        return scrollTop;
    }

    function lozadImg(selector) {
        window.onscroll = function () {
            load();
        };

        var load = function () {
            var winBottom = $(window).height() + $(window).scrollTop();

            $(selector).each(function () {
                var element = $(this)[0];
                if (isLoaded(element)) {
                    return;
                }

                var eleTop = $(this).offset().top;

                if (winBottom > eleTop) {
                    loaded(element);
                    markAsLoaded(element);
                }
            });
        };

        return {
            fastLoad: function () {
                load();
            }
        }
    }
    return lozadImg;
}));