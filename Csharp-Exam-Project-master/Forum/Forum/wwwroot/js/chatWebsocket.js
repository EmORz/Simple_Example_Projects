var socket = new WebSocket('wss://localhost:44305/Message/UpdateChat');

setInterval(function () {
    var lastDates = document.getElementsByClassName("sent-on");
    if (document.getElementById("RecieverId") != null) {

        var otherUserId = document.getElementById("RecieverId");
        if (lastDates.length == 0) {

            socket.send("No Messages Found" + " - " + otherUserId.value);
        }

        var lastDateSentOn = lastDates[lastDates.length - 1];
        if (lastDateSentOn != null) {

            var onlyDate = lastDateSentOn.textContent.substring(8, lastDateSentOn.length);

            socket.send(onlyDate + " - " + otherUserId.value);
        }
    }
},
    500);

socket.onmessage = (message) => {

    var targetDiv = $("#chat-box");

    var messagesArr = JSON.parse(message.data);

    if (messagesArr.length > 0) {
        for (var index = 0; index < messagesArr.length; index++) {

            console.log(messagesArr[index].LoggedInUser);

            if (messagesArr[index].LoggedInUser == messagesArr[index].AuthorName) {

                targetDiv.append('<div class="bg-burly text-forum p-10 m-10px half-width float-left border-5">'
                    + '<p class="bold m-0">' + messagesArr[index].AuthorName + ' said:</p>'
                    + '<p class="p-10">' + messagesArr[index].Description + '</p>'
                    + '<p class="m-0 font-14 text-end sent-on"><b>Sent on</b> ' + messagesArr[index].CreatedOn + '</p>'
                    + '</div>');
            }
            else {

                targetDiv.append('<div class="bg-burly text-forum p-10 m-10px half-width float-right border-5">'
                    + '<p class="bold m-0">' + messagesArr[index].AuthorName + ' said:</p>'
                    + '<p class="p-10">' + messagesArr[index].Description + '</p>'
                    + '<p class="m-0 font-14 text-end sent-on"><b>Sent on</b> ' + messagesArr[index].CreatedOn + '</p>'
                    + '</div>');
            }
        }
    }
    var element = document.getElementById("chat-box");

    if (messagesArr.length > 0) {
        scrollToBottom(element);
    }
};
