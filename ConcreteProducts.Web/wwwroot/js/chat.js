var connection = new signalR
    .HubConnectionBuilder()
    .withUrl("/chat")
    .build();

connection.on("ReceiveMessage",
    function (message) {
        var chatInfo = `<div>[${(message.username)}] ${escapeSpecialCharacters(message.text)}</div>`;
        $('#messagesList').append(chatInfo);
    });

$('#sendButton').click(function () {
    var message = $('#messageInput').val();
    if (message === null || message === '') {
        alert('Empty messages are not alowed!.');
        return;
    }

    connection.invoke('Send', message);
    $('#messageInput').val('');
});

connection
    .start()
    .catch(function (err) {
        return console.error(err.toString());
    });

function escapeSpecialCharacters(char) {
    return char
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}
