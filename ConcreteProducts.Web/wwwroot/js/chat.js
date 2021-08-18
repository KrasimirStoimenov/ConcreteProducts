var connection = new signalR
    .HubConnectionBuilder()
    .withUrl("/chat")
    .build();

$('.scroll').scrollTop($('.scroll')[0].scrollHeight);

connection.on("ReceiveMessage",
    function (message) {
        var date = new Date(message.publishedOn);
        var chatInfo = `<div id="messagesList" style="font-size: 20px;" class="chatContainer">
                    <p><span class="font-weight-bold">[${message.username}]</span> - ${message.text}</p>
                    <span class="time-left">${date.toLocaleString()}</span>
                </div>`;
        $('#messagesList').append(chatInfo);
        $('.scroll').scrollTop($('.scroll')[0].scrollHeight);
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
