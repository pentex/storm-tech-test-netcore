﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(() => {
    $("#hide-completed-items-checkbox").change(function () {
        if ($(this).is(":checked")) {
            $(".is-done").hide();
        }
        else {
            $(".is-done").show();
        }
    });
});
