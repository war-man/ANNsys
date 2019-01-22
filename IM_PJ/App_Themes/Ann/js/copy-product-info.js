window.Clipboard = (function (window, document, navigator) {
    var textArea,
        copy;

    function isOS() {
        return navigator.userAgent.match(/ipad|iphone/i);
    }

    function createTextArea(text) {
        textArea = document.createElement('textArea');
        textArea.value = text;
        document.body.appendChild(textArea);
    }

    function selectText() {
        var range,
            selection;

        if (isOS()) {
            range = document.createRange();
            range.selectNodeContents(textArea);
            selection = window.getSelection();
            selection.removeAllRanges();
            selection.addRange(range);
            textArea.setSelectionRange(0, 999999);
        } else {
            textArea.select();
        }
    }

    function copyToClipboard() {
        document.execCommand('copy');
        document.body.removeChild(textArea);
    }

    copy = function (text) {
        createTextArea(text);
        selectText();
        copyToClipboard();
    };

    return {
        copy: copy
    };
})(window, document, navigator);


function copyProductInfo(id) {
    $('body').append($('<div>', {
        "class": 'copy-product-info hide'
    }));

    ajaxCopyInfo(id);

    Clipboard.copy($(".copy-product-info").text());

    $(".copy-product-info").remove();
}

function ajaxCopyInfo(id) {
    $.ajax({
        type: "POST",
        url: "/tat-ca-san-pham.aspx/copyProductInfo",
        data: "{id: " + id + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $(".copy-product-info").html(data.d);
        }
    });
}