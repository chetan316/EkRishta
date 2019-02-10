$(document).ready(function () {
    $("#txtMobileNo").on("keyup", function () {
        if ($("#txtMobileNo").val().length == 10) {
            var IsMobileNoExists = CheckMobileNo();
            if (!IsMobileNoExists) {
                $("#dvValidationMessage").html("Please Enter Registered Mobile Number");
            }
            else {
                $("#dvValidationMessage").html("");
            }
        }
        else {
            killAjax();
            $("#dvValidationMessage").html("");
        }
    });
});


function CheckMobileNo() {
    var IsMobileNoExists = false;
    xhr = $.ajax({
        url: "ValidateMobileNo",
        type: "POST",
        data: {
            "MobileNo": $("#txtMobileNo").val()
        },
        dataType: "json",
        async: false,
        success: function (data) {
            if (data != "") {
                IsMobileNoExists = true;
            }
        },
        error: function (xhr) {
            //alert(xhr.statusCode);
        }
    });
    return IsMobileNoExists;
}

function SendForgotPasswordOTP() {
    var IsOTPSent = false;
    var MobileNo = $("#txtMobileNo").val();

    if (MobileNo == '' || MobileNo.length != 10) {
        alert('Please Enter Valid Mobile Number.');
        return false;
    }
    var formData = { MobileNo: MobileNo };
    $.ajax({
        url: "SendForgotPasswordOTP",
        data: formData,
        type: "POST",
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data == 'SUCCESS') {
                IsOTPSent = true;
                $("#dvValidationMessage").html("");
                $("#dvOTP").css("display", "none");
                $("#dvForgotPwd").css("display", "block");
            }
            else {
                IsOTPSent = false;
            }
        },
        error: function (xhr) {
            alert(xhr.status);
        }
    });
    return IsOTPSent;
}

function ChangePassword() {
    var MobileNo = $("#txtMobileNo").val();
    var Password = $("#txtPassword").val();
    var ConfirmPassword = $("#txtConfirmPassword").val();
    var OTP = $("#txtOTP").val();

    if (Password == "") {
        $("#dvValidationMessage").html("Please Enter Password");
        return false;
    }
    if (ConfirmPassword == "") {
        $("#dvValidationMessage").html("Please Enter Confirm Password");
        return false;
    }
    if (Password != "" && ConfirmPassword != "" && (Password != ConfirmPassword)) {
        $("#dvValidationMessage").html("Password and Confirm Password do not match");
        return false;
    }
    
    if (OTP == "") {
        $("#dvValidationMessage").html("Please Enter OTP");
        return false;
    }

    var formData = { MobileNo: MobileNo, Password: Password, OTP: OTP };
    $.ajax({
        url: "ChangePassword",
        data: formData,
        type: "POST",
        dataType: 'json',
        async: false,
        success: function (data) {
            if (data == 'SUCCESS') {
                $("#dvValidationMessage").html("");
                alert("Password changed successfully");
                window.location.href = window.origin + "/MobileApp/Login/MobileIndex";
            }
            else {
                $("#dvValidationMessage").html(data);
            }
        },
        error: function (xhr) {
            alert(xhr.status);
        }
    });
}

