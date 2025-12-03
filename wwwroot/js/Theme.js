function activateLightMode() {
    document.body.style.color = "black";
    document.body.style.background = "white";
    document.body.className = "";

    for (let input of document.getElementsByTagName("input")) {
        input.style.backgroundColor = "white";
        input.style.color = "black";
        input.style.borderColor = "#0d6efd";
        
    }

}

function activateDarkMode() {
    document.body.style.color = "white";
    document.body.style.background = "#1e1e1e";
    document.body.className = "dark-mode";

    for (let input of document.getElementsByTagName("input")) {
        input.style.backgroundColor = "#1e1e1e";  // رمادي غامق
        input.style.color = "white";
        input.style.borderColor = "#555";
        ;
    }

}

function traditional() {
    var traditional = document.getElementById("darkBtn");
    traditional.onclick = activateDarkMode;
}