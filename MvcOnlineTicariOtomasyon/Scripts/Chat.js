$(function () {
    if (!$.connection || !$.connection.chatHub) {
        console.error("SignalR hub yüklenemedi. Startup.cs ve ChatHub.cs kontrol et.");
        return;
    }

    var chat = $.connection.chatHub;

    chat.client.addNewMessageToPage = function (name, message) {
        $('#discussion').append('<li><strong>' + $('<div/>').text(name).html() + '</strong>: ' + $('<div/>').text(message).html() + '</li>');
    };

    var userName = prompt('Enter your name:', '');
    if (!userName) userName = "Guest";
    $('#displayname').val(userName);
    $('#message').focus();

    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            var msg = $('#message').val();
            if (msg && msg.trim() !== '') {
                chat.server.send($('#displayname').val(), msg);
                $('#message').val('').focus();
            }
        });

        $('#message').keypress(function (e) {
            if (e.which === 13) $('#sendmessage').click();
        });
    });
});
