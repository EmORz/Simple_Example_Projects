function SwitchPostReportsPage(index) {
    pageBtns[index].onclick = function () {
        var number = pageBtns[index].textContent;
        $.ajax(
            {
                type: "GET",
                url: "/ReplyReport/GetReplyReports?start=" + (number - 1) * 5,
                success: function (test) {
                    $('#tableDiv').html(test);
                    $.getScript("../js/modal.js");
                    $.getScript("../js/dismissReplyReports.js");
                    $.getScript("../js/replyReportsPagging.js");
                }
            });
    };
}

var pageBtns = document.getElementsByClassName("reply-report-page-btn");

for (var i = 0; i < pageBtns.length; i++) {
    SwitchPostReportsPage(i);
}