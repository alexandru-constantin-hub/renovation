// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function getCookie(cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}





$(document).ready(function () {
    if (getCookie('active') == "") {
        window.location.href = '/login';
    }


    if (getCookie("active") != "") {
        if (getCookie("role") == "Membre") {
            if (window.location.href.indexOf("fourniseurs") > -1 || window.location.href.indexOf("administrators") > -1) {
                window.location.href = '/membres';
                alert("Vous n'avez pas le droit d'afficher cette page");
            }

        };

        if (getCookie("role") == "Furniseur") {
            if (window.location.href.indexOf("membres") > -1 || window.location.href.indexOf("administrators") > -1) {
                window.location.href = '/furniseurs';
                alert("Vous n'avez pas le droit d'afficher cette page");

            }

        };

        if (getCookie("role") == "Administrator") {
            if (window.location.href.indexOf("membres") > -1 || window.location.href.indexOf("furniseurs") > -1) {
                window.location.href = '/administrators';
                alert("Vous n'avez pas le droit d'afficher cette page");
            }

        };

    }






});