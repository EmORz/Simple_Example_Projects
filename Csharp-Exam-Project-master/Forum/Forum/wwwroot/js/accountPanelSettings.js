$("#editProfileButton")
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/AccountPanel/Settings/EditProfile",
                success: function (test) {
                    $('#account-panel').html(test);
                }
            });

        $("#editProfileButton").removeClass("text-white");
        $("#editProfileButton").removeClass("menu-bg-forum");

        $("#editProfileButton").addClass("active");

        if ($("#changePasswordButton").hasClass("active")) {
            $("#changePasswordButton").removeClass("active");

            $("#changePasswordButton").addClass("menu-bg-forum");
            $("#changePasswordButton").addClass("text-white");
        }
        if ($("#deleteAccButton").hasClass("active")) {
            $("#deleteAccButton").removeClass("active");

            $("#deleteAccButton").addClass("menu-bg-forum");
            $("#deleteAccButton").addClass("text-white");
        }
    });

$("#changePasswordButton")
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/AccountPanel/Settings/ChangePassword",
                success: function (test) {
                    $('#account-panel').html(test);
                }
            });

        $("#changePasswordButton").removeClass("text-white");
        $("#changePasswordButton").removeClass("menu-bg-forum");

        $("#changePasswordButton").addClass("active");

        if ($("#editProfileButton").hasClass("active")) {
            $("#editProfileButton").removeClass("active");

            $("#editProfileButton").addClass("menu-bg-forum");
            $("#editProfileButton").addClass("text-white");
        }
        if ($("#deleteAccButton").hasClass("active")) {
            $("#deleteAccButton").removeClass("active");

            $("#deleteAccButton").addClass("menu-bg-forum");
            $("#deleteAccButton").addClass("text-white");
        }
    });

$("#deleteAccButton")
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/AccountPanel/Settings/DeleteAccount",
                success: function (test) {
                    $('#account-panel').html(test);
                }
            });

        $("#deleteAccButton").removeClass("text-white");
        $("#deleteAccButton").removeClass("menu-bg-forum");

        $("#deleteAccButton").addClass("active");

        if ($("#editProfileButton").hasClass("active")) {
            $("#editProfileButton").removeClass("active");

            $("#editProfileButton").addClass("menu-bg-forum");
            $("#editProfileButton").addClass("text-white");
        }
        if ($("#changePasswordButton").hasClass("active")) {
            $("#changePasswordButton").removeClass("active");

            $("#changePasswordButton").addClass("menu-bg-forum");
            $("#changePasswordButton").addClass("text-white");
        }
    });
