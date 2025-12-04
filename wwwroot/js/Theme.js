function setTheme(mode) {
    document.cookie = "theme=" + mode + "; path=/; max-age=2592000";
}

function getTheme() {
    var cookies = document.cookie.split(";");

    for (var c of cookies) {
        var [key, value] = c.trim().split("=");

        if (key === "theme") return value;
    }
    return null;
}



function activateLightMode() {
    document.body.style.color = "black";
    document.body.style.background = "white";
    document.body.className = "";

    document.getElementsByTagName("footer")[0].style.color = "black";

    for (var input of document.getElementsByTagName("input")) {
        input.style.backgroundColor = "white";
        input.style.color = "black";
        input.style.borderColor = "#0d6efd";
        footer.style.color = "black";

    }
    // تغيّر كل عناصر الجدول إلى داكن
    for (var table of document.getElementsByTagName("table")) {
        table.style.backgroundColor = "#2b2b2b";
        table.style.color = "white";
    }

    for (var th of document.getElementsByTagName("th")) {
        th.style.backgroundColor = "#2b2b2b";
        th.style.color = "white";
    }

    for (var td of document.getElementsByTagName("td")) {
        td.style.backgroundColor = "#2b2b2b";
        td.style.color = "white";
    }


    setTheme("light");


}

function activateDarkMode() {
    document.body.style.color = "white";
    document.body.style.background = "#1e1e1e";
    document.body.className = "dark-mode";

    document.getElementsByTagName("footer")[0].style.color = "white";

    for (var input of document.getElementsByTagName("input")) {
        input.style.backgroundColor = "#1e1e1e";
        input.style.color = "white";
        input.style.borderColor = "#555";
        ;
    }
    for (var table of document.getElementsByTagName("table")) {
        table.style.backgroundColor = "#1e1e1e";
        table.style.color = "white";
    }

    for (var th of document.getElementsByTagName("th")) {
        th.style.backgroundColor = "#1e1e1e";
        th.style.color = "white";
    }

    for (var td of document.getElementsByTagName("td")) {
        td.style.backgroundColor = "#1e1e1e";
        td.style.color = "white";
    }


    setTheme("dark");


}

function traditional() {
    var traditional = document.getElementById("darkBtn");
    traditional.onclick = activateDarkMode;

    var saved = getTheme();
    if (saved === "dark") activateDarkMode();
}