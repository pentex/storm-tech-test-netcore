// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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

    $("#order-by-dropdown").change(function () {
        var selectedOption = $(this).children("option:selected").val();

        if (selectedOption === "Importance") {
            location.href = `/TodoList/Detail?todoListId=${$("#TodoListId").val()}&orderByRank=false`;
        }
        else if (selectedOption === "Rank") {
            location.href = `/TodoList/Detail?todoListId=${$("#TodoListId").val()}&orderByRank=true`;
        }
    });
});
