setInterval(function () {

    $.ajax(
        {
            type: "GET",
            url: "/AccountPanel/Chat/RecentConversations",
            success: function (test) {
                var oldSearchValue = document.getElementById("myInput").value;

                var activeElement = document.activeElement;

                if ($('#chat-conversations-div').html !== test.html) {

                    $('#chat-conversations-div').html(test);

                    document.getElementById("myInput").value = oldSearchValue;

                    if (activeElement.id == "myInput") {
                        document.getElementById("myInput").focus();
                    }
                    activeElement.focus();

                }
            }
        });
},
    3000);