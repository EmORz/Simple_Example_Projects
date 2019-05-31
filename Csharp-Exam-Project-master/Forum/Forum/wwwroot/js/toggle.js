var toggleButtons = document.getElementsByClassName("toggleBtn");
console.log("toggleButtons count: " + toggleButtons.length);

var toggleDivs = document.getElementsByClassName("toggle-div");
console.log("toggleDivs count: " + toggleDivs.length);

var toggleIcons = document.getElementsByClassName("toggleIcon");
console.log("toggleIcons count: " + toggleIcons.length);

function toggle(i) {

    toggleButtons[i].onclick = function () {
        if (toggleDivs[i].style.display === "none")
        {
            toggleDivs[i].style.display = "block"; toggleIcons[i].classList.remove("fa-caret-down");
            toggleIcons[i].classList.add("fa-caret-up");

        }
        else {
            toggleDivs[i].style.display = "none";
            
            toggleIcons[i].classList.remove("fa-caret-up");
            toggleIcons[i].classList.add("fa-caret-down");
        }
    };
}

for (var index = 0; index < toggleButtons.length; index++)
{
    toggle(index);
}