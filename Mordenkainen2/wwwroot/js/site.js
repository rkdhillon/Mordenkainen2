// subverts submit button for ajax request.
$(function () {
    $("#log").submit(function (e) {
        e.preventDefault();  //prevent normal form submission

        var actionUrl = $(this).attr("Login");  // get the form action value
        $.post(actionUrl, $(this).serialize(), function (res) {
            //res is the response coming from our ajax call. Use this to update DOM
            $("#viewB").html(res);
        });
    });

});