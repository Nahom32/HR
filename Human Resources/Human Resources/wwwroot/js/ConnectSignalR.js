
"use strict";

var connection = new signalR.HubConnectionBuilder()
                            .configureLogging(signalR.LogLevel.Information)
                            .withUrl("/messagehub").build();
connection.start();
connection.on("ReceiveMessage", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    alert(msg);
});
