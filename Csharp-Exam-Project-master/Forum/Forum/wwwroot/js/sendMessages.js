function scrollToBottom(el) {
    console.log(el);
    el.scrollTop = el.scrollHeight;
    el.scrollTop;
}

$("#sendButton")
    .click(function () {
        $.ajax(
            {
                type: "POST",
                url: "/Message/Send",
                dataType: "text",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    RecieverId: $("#RecieverId").val(),
                    Description: $("#Description").val()
                }),
                success: function (test) {
                    $.getScript("../js/site.js");
                    document.getElementById("Description").value = "";
                }
            });
    });