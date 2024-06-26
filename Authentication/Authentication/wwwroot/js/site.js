// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var timer = null;
//type:alert-info, alert-warning, alert-success, alert-danger
function alert_show(type, title, content) {
    /* $("#alert").removeClass().addClass("alert " + type);*/
    $("#alert").toggleClass("visually-hidden")
    $("#alert-title").html(title);
    $("#alert-content").html(content);
    timer = setTimeout(function () {
        alert_hide();
    }, 2000);
}
function alert_hide() {
    clearTimeout(timer);
    $("#alert").toggleClass("visually-hidden")
}