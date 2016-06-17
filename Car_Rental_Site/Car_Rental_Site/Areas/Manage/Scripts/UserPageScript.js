//Set datepicker to choose/change birth date
//$(function () {
//    $("#BirthDate").datepicker({
//        altFormat: "yy-mm-dd",
//        autoSize: true, maxDate: new Date(new Date().getFullYear() - 18, 0, 0, 0),
//        changeMonth: true,
//        changeYear: true,
//        dateFormat: 'dd/mm/yy'
//    });
//    $("#BirthDate").datepicker("setDate", $("#BirthDate").attr("value"));
//});

////Set the all actions to reset user password by jquery ui
//$(function () {
//    //Set the main dialog for reset password
//    $("#resetPasswordDialog").dialog({
//        autoOpen: false,
//        resizable: false,
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
//            $("#NewPassword").val("");
//            $("#ConfirmPassword").val("");
//        }
//    });

//    //Open the dialog
//    $("#resetPasswordLink").click(function () {
//        $("#resetPasswordDialog").dialog("open");
//        return false;
//    });

//    //Send request to the server by ajax to save the new password
//    $("#btnChange").click(function () {

//        var form = $(this).parents("form");

//        $.ajax({
//            type: "PUT",
//            url: $(location).attr('origin') + "/Users/ResetPassword",
//            data: form.serialize(),
//            success: function () {
//                $("#resetPasswordDialog").dialog("close");
//                $("#ConfirmMessage").html("User password was successfully changed!");
//                $("#resetConfirm").dialog("open");
//            },
//            error: function (err) {
//                $("#resetPasswordDialog").dialog("close");
//                $("#ConfirmMessage").html("Sorry, we could not change user password! </br> The reason is " + err.statusText);
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

//    //Set another dialog to show if the reset password succeed or not
//    //refresh the page in the end to no save accidently the old password
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
//        close: function (event, ui) {
//            location.reload();
//        },
//        buttons: {
//            "OK": function () {
//                $(this).dialog("close");
//            }
//        }
//    });
//});

//Set the all actions to change user role by jquery ui
//$(function () {
//    //Get the current user role
//    $.ajax({
//        type: "GET",
//        url: $(location).attr('origin') + "/Users/GetUserRole",
//        data: { userName: $("#ChangeRoleLink").attr("href") },
//        success: function (data) {
//            $("#currentRole").html(data);
//        },
//        error: function (err) {
//            $("#currentRole").html(err);
//        },
//        cache: false
//    });

//    //Set the main dialog for change user role
//    $("#ChangeUserRole").dialog({
//        autoOpen: false,
//        resizable: false,
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
//    });

//    //Open the dialog
//    $("#ChangeRoleLink").click(function () {
//        $("#ChangeUserRole").dialog("open");
//        return false;
//    });

//    //Send request to the server by ajax to save the new user rule
//    $("#btnSaveRole").click(function () {
//        $.ajax({
//            type: "PUT",
//            url: $(location).attr('origin') + "/Users/ChangeUserRole",
//            data: { userName: $("#ChangeRoleLink").attr("href"), userRole: $("#NewRole").val() },
//            success: function () {
//                $("#ChangeUserRole").dialog("close");
//                $("#ChangeConfirmMessage").html("User role was successfully changed!");
//                $("#changeConfirm").dialog("open");
//            },
//            error: function (err) {
//                $("#ChangeUserRole").dialog("close");
//                $("#ChangeConfirmMessage").html("Sorry, we could not change user role! </br> The reason is " + err.statusText);
//                $("#changeConfirm").dialog("open");
//            },
//            cache: false
//        });
//        return false;
//    });

//    //Close the dialog
//    $("#btnCancelRole").click(function () {
//        $("#ChangeUserRole").dialog("close");
//    });

//    //Set another dialog to show if the change role succeed or not
//    //refresh the page in the end to no save accidently the old role
//    $("#changeConfirm").dialog({
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
//        close: function (event, ui) {
//            location.reload();
//        },
//        buttons: {
//            "OK": function () {
//                $(this).dialog("close");
//            }
//        }
//    });
//});