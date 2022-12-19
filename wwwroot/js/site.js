// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

setTimeout(function () {

    // Closing the alert
    $('.alert').alert('close');
}, 2000);


$(document).ready(function () {

    $("#myInput").on("keyup", function () {

        var value = $(this).val().toLowerCase();

        $("#myTable tr").filter(function () {

            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)

        });

    });


    $("#piece").on("change", function () {

        var value = $(this).val().toLowerCase();

        $("#myTable tr").filter(function () {

            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)

        });

    });

    $("#renovation").on("change", function () {

        var value = $(this).val();

        $("#myTable tr").filter(function () {

            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)

        });

    });

});