$('#searchButton')
    .click(function () {
        $.ajax(
            {
                type: "GET",
                url: "/Owner/Role/Search",
                dataType: "text",
                data: 'key=' + document.getElementById("myInput").value,
                success: function (test) {
                    console.log(test);

                    $.getScript("../js/site.js");

                    var searchBar = document.getElementById("myInput");

                    searchBar.value = "";

                    var targetDiv = $("#edit-roles-table-div");

                    targetDiv.html(test);
                }
            });
    });