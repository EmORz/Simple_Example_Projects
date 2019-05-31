$('#postReportsBtn')
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/PostReport/GetPostReports",
                success: function (test) {
                    $('#tableDiv').html(test);
                    $.getScript("../js/modal.js");
                    $.getScript("../js/postReportsPagging.js");
                    $.getScript("../js/dismissPostReports.js");
                }
            });
        $("#postReportsBtn").removeClass("text-white");
        $("#postReportsBtn").removeClass("menu-bg-forum");

        $("#postReportsBtn").addClass("active");

        if ($("#quoteReportsBtn").hasClass("active")) {
            $("#quoteReportsBtn").removeClass("active");

            $("#quoteReportsBtn").addClass("text-white");
            $("#quoteReportsBtn").addClass("menu-bg-forum");
        }
        if ($("#replyReportsBtn").hasClass("active")) {
            $("#replyReportsBtn").removeClass("active");

            $("#replyReportsBtn").addClass("text-white");
            $("#replyReportsBtn").addClass("menu-bg-forum");
        }
    });

$('#replyReportsBtn')
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/ReplyReport/GetReplyReports",
                success: function (test) {
                    $('#tableDiv').html(test);
                    $.getScript("../js/modal.js");
                    $.getScript("../js/dismissReplyReports.js");
                    $.getScript("../js/replyReportsPagging.js");
                }
            });
        
        $("#replyReportsBtn").removeClass("text-white");
        $("#replyReportsBtn").removeClass("menu-bg-forum");

        $("#replyReportsBtn").addClass("active");

        if ($("#quoteReportsBtn").hasClass("active")) {
            $("#quoteReportsBtn").removeClass("active");

            $("#quoteReportsBtn").addClass("text-white");
            $("#quoteReportsBtn").addClass("menu-bg-forum");
        }
        if ($("#postReportsBtn").hasClass("active")) {
            $("#postReportsBtn").removeClass("active");

            $("#postReportsBtn").addClass("text-white");
            $("#postReportsBtn").addClass("menu-bg-forum");
        }
    });

$('#quoteReportsBtn')
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/QuoteReport/GetQuoteReports",
                success: function (test) {
                    $('#tableDiv').html(test);
                    $.getScript("../js/modal.js");
                    $.getScript("../js/dismissQuoteReports.js");
                    $.getScript("../js/quoteReportsPagging.js");
                }
            });

        $("#quoteReportsBtn").removeClass("text-white");
        $("#quoteReportsBtn").removeClass("menu-bg-forum");

        $("#quoteReportsBtn").addClass("active");

        if ($("#replyReportsBtn").hasClass("active")) {
            $("#replyReportsBtn").removeClass("active");

            $("#replyReportsBtn").addClass("text-white");
            $("#replyReportsBtn").addClass("menu-bg-forum");
        }
        if ($("#postReportsBtn").hasClass("active")) {
            $("#postReportsBtn").removeClass("active");

            $("#postReportsBtn").addClass("text-white");
            $("#postReportsBtn").addClass("menu-bg-forum");
        }
    });