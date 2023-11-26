// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    $(function () {
        $("#sendMessageToPublic").submit(function (e) {
            e.preventDefault();
            $.ajax({
                // Hier kan je kiezen tussen GET, POST, PUT, DELETE....
                // Elk van deze keywords heeft een andere nut,
                // zie de les frontend.
                type: "POST",
                // Hier geven we de action naam en controler naam mee.
                url: '/Home/MessagePublic',
                // Hier gaan we data meegeven in een JSON formaat.
                data: { "id": $("#idInput").val(), "message": $("#messageInput").val() },
                dataType: "json",
                // Hier gaan we jQuery/javascript code schrijven indien onze
                // request successvol is.
                success: function (response) {
                    console.log(response);
                    // Hier gaan we de span met id "statusSpan" ophalen
                    // en de text vervangen met de status.
                    // Status krijgen we van de action Message mee.
                    /* $("#chatbox").append( response.status);*/
                    $("#messageInput").val("")
                },
                error: function (response) {
                    // Indien onze request faalt, kunnen we hier een mooie
                    // foutmelding tonen.
                    // In ons geval gaan we een alert tonen op het scherm
                    // (in praktijk moet je dit beter afhandelen!).
                    console.log(error);
                    
                }
            });
        });
     })


$(function () {
    $("#sendMessageToPrivate").submit(function (e) {
        e.preventDefault();
        $.ajax({
            // Hier kan je kiezen tussen GET, POST, PUT, DELETE....
            // Elk van deze keywords heeft een andere nut,
            // zie de les frontend.
            type: "POST",
            // Hier geven we de action naam en controler naam mee.
            url: '/Home/MessagePrivate',
            // Hier gaan we data meegeven in een JSON formaat.
            data: { "senderId": $("#senderId").val(), "receiverId": $("#receiverId").val(), "message": $("#messageInput").val() },
            dataType: "json",
            // Hier gaan we jQuery/javascript code schrijven indien onze
            // request successvol is.
            success: function (response) {
                console.log(response);
                // Hier gaan we de span met id "statusSpan" ophalen
                // en de text vervangen met de status.
                // Status krijgen we van de action Message mee.
                /* $("#chatbox").append( response.status);*/
                $("#messageInput").val("")
            },
            error: function (response) {
                // Indien onze request faalt, kunnen we hier een mooie
                // foutmelding tonen.
                // In ons geval gaan we een alert tonen op het scherm
                // (in praktijk moet je dit beter afhandelen!).
                console.log(error);

            }
        });
    });
})
