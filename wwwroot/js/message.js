"use strict"
var connection = new signalR.HubConnectionBuilder()
    .withUrl('messages')
    .build();

connection.on("ReceiveMessage", function (message) {
    var div = document.createElement("div");
    var clientUserName = document.getElementById("username").value;
    var userName = message.split(":")[0]
    var messageToShow = message.split(":")[1]
    div.innerHTML = `<p class="mb-2"><span style="color:#7571f9">` + userName + `:</span>` + `` + messageToShow + `</p>` + "<hr/>";
    if (clientUserName != userName) {
        div.style.cssText ="background-color:#d5d4ff;"
    }
    div.style.cssText += "border-radius:10px;"
    document.getElementById("messages").appendChild(div);
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("sendButton").addEventListener("click", function (e) {
    var userName = document.getElementById("username").value;
    var message = document.getElementById("message").value;
    document.getElementById("message").value = "";
    var data = {}
    data["UserName"] = userName;
    data["Message"] = message;
    connection.invoke("SendMessageToAll", data).catch(function (err) {
        return console.error(err.toString());
    });
    e.preventDefault();

});