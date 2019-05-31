function scrollToBottom(el) {
    el.scrollTop = el.scrollHeight;
    el.scrollTop;
}

$('#searchButton')
    .click(function () {
        $.ajax(
            {
                type: "POST",
                url: "/AccountPanel/Chat/ChatWithSomebody",
                dataType: "text",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    recieverName: document.getElementById("myInput").value,
                    showAll: false
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

                    var searchBar = document.getElementById("myInput");

                    searchBar.value = "";
                }
            });
    });
