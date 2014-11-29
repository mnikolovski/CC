var cc = cc || {};
cc.controllers = cc.controllers || {};
cc.controllers.ChannelFactory = function ChannelFactory() {
    var me = {};

    var dataTypeJson = 'json';

    function bypassCacheFor(url) {
        var date = new Date();
        var timestamp = date.getTime();
        var key = '_ts';
        var re = new RegExp("([?|&])" + key + "=.*?(&|$)", "i");
        var separator = url.indexOf('?') !== -1 ? "&" : "?";
        url = url.toLowerCase();
        if (url.match(re)) {
            return url.replace(re, '$1' + key + "=" + timestamp + '$2');
        } else {
            return url + separator + key + "=" + timestamp;
        }
    }

    function internalChannel(data, type, dataType, url, isAsync, success, error) {
        url = bypassCacheFor(url);
        return $.ajax({
            async: isAsync,
            type: type,
            cache: false,
            dataType: dataType,
            contentType: 'application/json; charset=utf-8',
            data: data,
            url: url,
            success: function(result, textStatus, jqXHR) {
                if (success) {
                    success(result);
                }
            },
            error: function(jqXHR, textStatus, aError) {
                if (error) {
                    var result = jqXHR.responseJSON;
                    if (!result) {
                        try {
                            result = $.parseJSON(jqXHR.responseText);
                        } catch(e) {
                            result = jqXHR.responseText;
                        }
                    }

                    error(result);
                }
            }
        });
    }

    me.sendData = function(data, url, success, error) {
        return internalChannel(data, 'POST', dataTypeJson, url, true, success, error);
    };

    return me;
}();