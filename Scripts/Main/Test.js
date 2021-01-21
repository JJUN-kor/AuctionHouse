$(document).ready(function () {

    $.ajax({
        type: 'POST',
        url: "/Home/Test",
        dataType: "json",

        success: function (data) {
            console.log("성공"+data);
            alert("성공");

        },

        error: function (xhr, status, error) {
            console.log(error);
        }
    });
});