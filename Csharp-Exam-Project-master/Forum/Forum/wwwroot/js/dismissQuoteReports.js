var elements = document.getElementsByClassName("dismissButton");

var Ids = $(".entityId");

for (var i = 0; i < elements.length; i++) {
    var test = Ids[i];

    elements[i].onclick = function () {
        $.ajax(
            {
                type: "GET",
                url: "/QuoteReport/DismissQuoteReport",
                success: function (test) {
                    $('#tableDiv').html(test);
                    $.getScript("../js/modal.js");
                    $.getScript("../js/dismissQuoteReports.js");
                    $.getScript("../js/quoteReportsPagging.js");
                },
                data: {
                    "id": test.value
                }
            });
    };
}