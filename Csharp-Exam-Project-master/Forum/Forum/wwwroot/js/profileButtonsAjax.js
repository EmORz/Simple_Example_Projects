$("#myProfileButton")
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/AccountPanel/Profile/MyProfile",
                success: function (test) {
                    $('#profile-target-div').html(test);
                }
            });

        $.ajax(
            {
                type: "GET",
                url: "",
                success: function (test) {
                    $('#chat-conversations-div').html("");
                }
            });

        $("#myProfileButton").removeClass("text-white");
        $("#myProfileButton").removeClass("menu-bg-forum");

        $("#myProfileButton").addClass("active");

        if ($("#settingsButton").hasClass("active")) {
            $("#settingsButton").removeClass("active");

            $("#settingsButton").addClass("menu-bg-forum");
            $("#settingsButton").addClass("text-white");
        }
        else if ($("#messagesPanelButton").hasClass("active")) {
            $("#messagesPanelButton").removeClass("active");

            $("#messagesPanelButton").addClass("menu-bg-forum");
            $("#messagesPanelButton").addClass("text-white");
        }

    });

$("#settingsButton")
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/AccountPanel/Settings/Settings",
                success: function (test) {
                    $('#profile-target-div').html(test);
                    $.getScript("../js/accountPanelSettings.js");
                }
            });

        $.ajax(
                {
                    type: "GET",
                    url: "",
                    success: function (test) {
                        $('#chat-conversations-div').html("");
                    }
                });

        $("#settingsButton").removeClass("text-white");
        $("#settingsButton").removeClass("menu-bg-forum");

        $("#settingsButton").addClass("active");


        if ($("#myProfileButton").hasClass("active")) {
            $("#myProfileButton").removeClass("active");

            $("#myProfileButton").addClass("menu-bg-forum");
            $("#myProfileButton").addClass("text-white");
        }
        else if ($("#messagesPanelButton").hasClass("active")) {
            $("#messagesPanelButton").removeClass("active");

            $("#messagesPanelButton").addClass("menu-bg-forum");
            $("#messagesPanelButton").addClass("text-white");
        }
    });

$("#messagesPanelButton")
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/AccountPanel/Chat/MessagesPanel",
                success: function (test) {
                    $('#profile-target-div').html(test);
                    $.getScript("../js/messagesPanel.js");
                }
            });

        $("#messagesPanelButton").removeClass("text-white");
        $("#messagesPanelButton").removeClass("menu-bg-forum");

        $("#messagesPanelButton").addClass("active");


        if ($("#myProfileButton").hasClass("active")) {
            $("#myProfileButton").removeClass("active");

            $("#myProfileButton").addClass("menu-bg-forum");
            $("#myProfileButton").addClass("text-white");
        }
        else if ($("#settingsButton").hasClass("active")) {
            $("#settingsButton").removeClass("active");

            $("#settingsButton").addClass("menu-bg-forum");
            $("#settingsButton").addClass("text-white");
        }

    });