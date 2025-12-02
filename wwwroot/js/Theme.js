function activateLightMode() {
    document.body.style.color = "black";
    document.body.style.background = "white";
    document.body.className = "";

}

function activateDarkMode() {
    document.body.style.color = "white";
    document.body.style.background = "black";
    document.body.className = "dark-mode";

}

function traditional() {
    var traditional = document.getElementById("darkBtn");
    traditional.onclick = activateDarkMode;
}