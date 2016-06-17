//Set dialog to all expected error that happened in the server
$(function () {
    $("#ServerErrorMessages").dialog({
        autoOpen: false,
        resizable: false,
        modal: true,
        show: {
            effect: "bounce",
            duration: 500
        },
        hide: {
            effect: "explode",
            duration: 500
        }
    });
});

//The function that open the Error Messages dialog - called by razor if viewBag.Error not null
//If viewBag.CriticalError is not null the user redirect to the home page
//More details and all the arrangement to this in the "_Latout.cshtml"
function openErrorsDialog(errorMessage, critical) {
    $("#ErrorMessage").html(errorMessage);          //add the error message to the dialog

    if (critical) {                                 //if critical error happened - on close the user redirect to the home page
        $("#ServerErrorMessages").dialog({
            buttons: [{
                text: "OK", click: function () {
                    $(this).dialog("close");
                }
            }],
            close: function (event, ui) {
                location.href = $(location).attr('origin');
            }
        });
        $("<p>This Error is critical error!</br>You will redirect to the home page!</p>").appendTo($("#ServerErrorMessages"));
    }
    else {
        $("#ServerErrorMessages").dialog({          //if regular error happened
            buttons: [{
                text: "OK", click: function () {
                    $(this).dialog("close");
                }
            }]
        });
    }

    $("#ServerErrorMessages").dialog("open");       //open the dialog
}