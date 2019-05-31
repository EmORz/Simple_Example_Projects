function typeEmoticonInTextarea(el, newText) {
    var start = el.prop("selectionStart");
    var end = el.prop("selectionEnd");
    var text = el.val();
    var before = text.substring(0, start);
    var after = text.substring(end, text.length);
    el.val(before + newText + after);
    el[0].selectionStart = el[0].selectionEnd = start + newText.length;
    el.focus();
}

$(".emote")
    .off('click')
    .on("click", function (e) {
        console.log("CLICKED!");
        typeEmoticonInTextarea($("#Description"), e.target.innerText);
        return false;
    });

function typeInTextarea(el, newText) {
    var start = el.prop("selectionStart");
    var end = el.prop("selectionEnd");
    var text = el.val();
    var before = text.substring(0, start);
    var after = text.substring(end, text.length);
    el.val(before + newText + after);
    el[0].selectionStart = el[0].selectionEnd = start + newText.length;
    el.focus();
}

$(".text")
    .off('click')
    .on("click", function () {
        typeInTextarea($("#Description"), $(this).val());
        return false;
    });

function myFunction() {
    if (document.getElementById("myDropdown").classList.contains("admin-dropdown-content")) {
        document.getElementById("myDropdown").classList.remove("admin-dropdown-content");
        document.getElementById("myDropdown").classList.toggle("show");
    }
    else {
        document.getElementById("myDropdown").classList.remove("show");
        document.getElementById("myDropdown").classList.toggle("admin-dropdown-content");
    }
}

window.onclick = function (event) {
    if (!event.target.classList.contains("fa-bars")) {
        document.getElementById("myDropdown").classList.remove("show");
        document.getElementById("myDropdown").classList.add("admin-dropdown-content");
    }
};


function myResponsiveFunction() {
    if (document.getElementById("myResponsiveDropdown").classList.contains("responsive-dropdown-content")) {
        document.getElementById("myResponsiveDropdown").classList.remove("responsive-dropdown-content");
        document.getElementById("myResponsiveDropdown").classList.toggle("show-responsive");
    }
    else {
        document.getElementById("myResponsiveDropdown").classList.remove("show-responsive");
        document.getElementById("myResponsiveDropdown").classList.toggle("responsive-dropdown-content");
    }
}

window.onclick = function (event) {
    if (!event.target.classList.contains("fa-bars")) {
        document.getElementById("myResponsiveDropdown").classList.remove("show-responsive");
        document.getElementById("myResponsiveDropdown").classList.add("responsive-dropdown-content");
    }
};