var showBtns = document.getElementsByClassName("show-quotes");
var wrappers = document.getElementsByClassName("quotes-div");
var icons = document.getElementsByClassName("icon-arrow");

function ToggleQuotes(i) {
    showBtns[i].onclick = function () {
        if (wrappers[i].classList.contains("display-none")) {
            wrappers[i].classList.remove("display-none");
            wrappers[i].classList.add("display-block");

            showBtns[i].textContent = "Dont show Quotes ";

            icons[i].classList.remove("fa-arrow-down");
            icons[i].classList.add("fa-arrow-up");
        }
        else {
            wrappers[i].classList.remove("display-block");
            wrappers[i].classList.add("display-none");

            showBtns[i].textContent = "Show Quotes ";

            icons[i].classList.remove("fa-arrow-up");
            icons[i].classList.add("fa-arrow-down");
        }
    }
}

for (var i = 0; i < showBtns.length; i++) {
    ToggleQuotes(i);
}