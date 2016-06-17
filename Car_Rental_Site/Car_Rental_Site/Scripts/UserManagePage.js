var loadInfo = false;       //Check if the user already load is order info information to prevent reload

//Set the tabs view to the user manage page
$(function () {
    $("#Tabs").tabs({
        beforeLoad: function (event, ui) {
            ui.jqXHR.error(function () {
                ui.panel.html("Couldn't load this tab. We'll try to fix this as soon as possible.");
            });
        }
    });
});

//When the user click on the History of rent tab - if the loadInfo = false
//He send request to the server by ajax to get back all user orders
$(function () {
    $("#LoadInfo").click(function () {
        if (!loadInfo) {
            $.ajax({
                type: "GET",
                url: $(location).attr('origin') + "/Account/UserOrders",
                success: function (data) {
                    if (data.length < 1) {
                        $("#OrdersDetails").html("<h3 class='NoMatch'>You dont rental any vehicle yet!</h3>");
                    }
                    for (var i = 0; i < data.length; i++) {
                        temp = "<tr>";
                        for (var key in data[i]) {
                            if (data[i][key] != null) {
                                temp += "<td>" + data[i][key] + "</td>";
                            }
                            else {
                                temp += "<td>--/--/----</td>";
                            }
                        }
                        temp += "</tr>";
                        $(temp).appendTo($("#OrdersDetailsResults"));
                        $("#OrdersDetailsResults").css("visibility", "visible");
                        loadInfo = true;
                    }
                },
                error: function (err) {
                    openErrorsDialog(err.statusText, false);
                },
                cache: false
            });
        }
    });
});

//Set the all actions to change user password by jquery ui dialogs
//$(function () {
//    //Set the main dialog for change user password
//    $("#resetPasswordDialog").dialog({
//        autoOpen: false,
//        resizable: false,
//        height: 350,
//        width: 350,
//        modal: true,
//        show: {
//            effect: "scale",
//            duration: 500
//        },
//        hide: {
//            effect: "scale",
//            duration: 500
//        },
//        close: function (event, ui) {
//            $("#OldPassword").val("");
//            $("#NewPassword").val("");
//            $("#ConfirmPassword").val("");
//        }
//    });

//    //Open the dialog
//    $("#resetPasswordLink").click(function () {
//        $("#resetPasswordDialog").dialog("open");
//        return false;
//    });

//    //Send request to the server by ajax to try change the password
//    //Only if the old password correct
//    $("#btnChange").click(function () {

//        var form = $(this).parents("form");

//        $.ajax({
//            type: "PUT",
//            url: $(location).attr('origin') + "/Account/ResetPassword",
//            data: form.serialize(),
//            success: function () {
//                $("#resetPasswordDialog").dialog("close");
//                $("#ConfirmMessage").html("Your password was successfully changed!");
//                $("#resetConfirm").dialog("open");
//            },
//            error: function (err) {
//                $("#resetPasswordDialog").dialog("close");
//                $("#ConfirmMessage").html("Sorry, we could not change your password! </br> The reason is " + err.statusText);
//                $("#resetConfirm").dialog("open");
//            },
//            cache: false
//        });
//        return false;
//    });

//    //Close the dialog
//    $("#btnCancel").click(function () {
//        $("#resetPasswordDialog").dialog("close");
//    });

//    //Set another dialog to show if the change password succeed or not
//    $("#resetConfirm").dialog({
//        autoOpen: false,
//        resizable: false,
//        modal: true,
//        show: {
//            effect: "scale",
//            duration: 500
//        },
//        hide: {
//            effect: "scale",
//            duration: 500
//        },
//        buttons: {
//            "OK": function () {
//                $(this).dialog("close");
//            }
//        }
//    });
//});