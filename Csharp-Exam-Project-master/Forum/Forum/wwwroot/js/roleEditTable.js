var x = window.matchMedia("(max-width: 700px)")
x.addListener(function myFunction(x) {
    if (x.matches) { // If media query matches

        var header = document.getElementById('role-edit-actions-header');
        header.textContent = "Ac...";

        var tables = document.getElementsByClassName("posts-table");
        tables[0].style.fontSize = "12px";

        var usernames = document.getElementsByClassName("username");
        for (var i = 0; i < usernames.length; i++) {
            var username = usernames[i];
            if (username.textContent.length >= 4) {
                usernames[i].textContent = usernames[i].textContent.substring(0, 2) + '...';
            }
        }

        var roleNames = document.getElementsByClassName("roleName");
        for (var roleIndex = 0; roleIndex < roleNames.length; roleIndex++) {
            var role = roleNames[roleIndex];
            if (role.textContent.length >= 4) {
                roleNames[roleIndex].textContent = roleNames[roleIndex].textContent.substring(0, 2) + '...';
            }
        }

    } else {
        var header = document.getElementById('role-edit-actions-header');
        header.textContent = "Actions";

        var oldUsernames = document.getElementsByClassName("oldUsername");
        var usernames = document.getElementsByClassName("username");
        for (var i = 0; i < usernames.length; i++) {
            var username = usernames[i];
            usernames[i].textContent = oldUsernames[i].value;
        }

        var oldRoleNames = document.getElementsByClassName("oldRoleName");
        var roleNames = document.getElementsByClassName("roleName");
        for (var roleIndex = 0; roleIndex < roleNames.length; roleIndex++) {
            var role = roleNames[roleIndex];
            roleNames[roleIndex].textContent = oldRoleNames[roleIndex].value;
        }

        var tables = document.getElementsByClassName("posts-table");
        tables[0].style.fontSize = "18px";
        tables[0].style.display = "table";
    }
})