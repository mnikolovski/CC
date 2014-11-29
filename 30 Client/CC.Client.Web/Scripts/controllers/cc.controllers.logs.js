var cc = cc || {};
cc.controllers = cc.controllers || {};
cc.controllers.Logs = function Logs() {
    var me = {};

    me.log = function(data, success, error) {
        cc.controllers.ChannelFactory.sendData(JSON.stringify({ logModel: data }), '/Logs/Log', success, error);
    };

    return me;
}();