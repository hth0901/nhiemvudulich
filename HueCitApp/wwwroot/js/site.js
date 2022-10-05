// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    var CURRENT_URL = window.location.href.split("#")[0].split("?")[0];
    var CURRENT_PATH = window.location.pathname.split("#")[0].split("?")[0];
    const main_menu = document.getElementById('main-menu-navigation');


    const arr_a = Array.from(main_menu.querySelectorAll('a'));

    arr_a.forEach((el, idx) => {
        if (el.pathname === CURRENT_PATH) {
            el.parentNode.classList.add("active");
        }
        else {
            el.parentNode.classList.remove("active");
        }
    })
})