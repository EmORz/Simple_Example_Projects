var elements = document.getElementsByClassName("dismissButton");

var Ids = $(".entityId");

for (var i = 0; i < elements.length; i++) {
    var test = Ids[i];

    elements[i].onclick = function () {
        $.ajax(
            {
                type: "GET",
                url: "/ReplyReport/DismissReplyReport",
                success: function (test) {
                    $('#tableDiv').html(test);
                    $.getScript("../js/modal.js");
                    $.getScript("../js/dismissReplyReports.js");
                    $.getScript("../js/replyReportsPagging.js");
                },
                data: {
                    "id": test.value
                }
            });
    };
}