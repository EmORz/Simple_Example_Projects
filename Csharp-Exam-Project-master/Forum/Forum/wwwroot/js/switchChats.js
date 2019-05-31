var recentConversationButtons = document.getElementsByClassName("recentConversationButton");
for (var index = 0; index < recentConversationButtons.length; index++) {

    recentConversationButtons[index].onclick = function TEST(e, index) {

        $.ajax(
            {
                type: "POST",
                url: "/AccountPanel/Chat/ChatWithSomebody",
                dataType: "text",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    recieverName: e.target.value
                }),
                success: function (test) {
                    $('#messages-panel').html(test);
                    $.getScript("../js/site.js");

                    var counter = 0;

                    var scripts = document.getElementsByName("script");
                    for (var i = 0; i < scripts.length; i++) {
                        if (scripts[i].src == '../js/sendMessages.js') {
                            counter++;
                        }
                    }

                    if (counter == 0) {
                        $.getScript("../js/sendMessages.js");
                    }

                    var element = document.getElementById("chat-box");
                    scrollToBottom(element);
                }
            });
    };
}