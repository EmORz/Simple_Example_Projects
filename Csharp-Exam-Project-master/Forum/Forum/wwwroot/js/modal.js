//Getting report modal elements
var modals = document.getElementsByClassName('modalElement');

//Getting report modal buttons
var btns = document.getElementsByClassName("modalBtn");

//Getting close buttons
var closeButtons = document.getElementsByClassName("close");
var noButtons = document.getElementsByClassName("closeBtn");

//Creating the onclick actions for all the reportBtns
function testFunction(index) {
    btns[i].onclick = function () {
        modals[index].style.display = "block";
    };
}

for (var i = 0; i < btns.length; i++) {
    testFunction(i);
}

//Creating the onclick actions for all the close buttons
for (var closeIndex = 0; closeIndex < closeButtons.length; closeIndex++) {
    closeButtons[closeIndex].onclick = function () {
        for (var index = 0; index < modals.length; index++) {
            modals[index].style.display = "none";
        }
    };
}

for (var noIndex = 0; noIndex < noButtons.length; noIndex++) {
    noButtons[noIndex].onclick = function () {
        for (var index = 0; index < modals.length; index++) {
            modals[index].style.display = "none";
        }
    };
}