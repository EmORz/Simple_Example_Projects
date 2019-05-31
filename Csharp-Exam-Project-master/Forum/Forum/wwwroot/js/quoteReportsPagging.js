function SwitchPostReportsPage(index) {
    pageBtns[index].onclick = function () {
        var number = pageBtns[index].textContent;
        $.ajax(
            {
                type: "GET",
                url: "/QuoteReport/GetQuoteReports?start=" + (number - 1) * 5,
                success: function (test) {
                    $('#tableDiv').html(test);
                    $.getScript("../js/modal.js");
                    $.getScript("../js/dismissQuoteReports.js");
                    $.getScript("../js/quoteReportsPagging.js");
                }
            });
    };
}

var pageBtns = document.getElementsByClassName("quote-report-page-btn");

for (var i = 0; i < pageBtns.length; i++) {
    SwitchPostReportsPage(i);
}