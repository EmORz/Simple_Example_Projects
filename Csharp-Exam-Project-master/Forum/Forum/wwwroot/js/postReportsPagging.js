function SwitchPostReportsPage(index) {
    pageBtns[index].onclick = function () {
        var number = pageBtns[index].textContent;
        $.ajax(
            {
                type: "GET",
                url: "/PostReport/GetPostReports?start=" + (number - 1) * 5,
                success: function (test) {
                    $('#tableDiv').html(test);
                    $.getScript("../js/modal.js");
                    $.getScript("../js/dismissPostReports.js");
                    $.getScript("../js/postReportsPagging.js");
                }
            });
    };
}

var pageBtns = document.getElementsByClassName("post-report-page-btn");

for (var i = 0; i < pageBtns.length; i++) {
    SwitchPostReportsPage(i);
}