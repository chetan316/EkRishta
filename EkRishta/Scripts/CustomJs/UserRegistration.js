$(document).ready(function () {
    $(".onlynumeric").keypress(function (e) { if (e.which < 48 || e.which > 57) { return false; } });

    $("#acNextGender").on("click", function () {
        $("#dvGender").css("display", "none");
        $("#dvMobile").css("display", "block");
    });

    $("#txtMobileNo").on("keyup", function () {
        if ($("#txtMobileNo").val().length == 10) {
            var IsMobileNoExists = CheckMobileNo();
            if (IsMobileNoExists) {
                $("#dvValidationMessage").html("Mobile Number Already Registered");
                $("#acNextMobile").attr("disabled", "disabled");
            }
            else {
                $("#dvValidationMessage").html("");
                $("#acNextMobile").removeAttr("disabled");
            }
        }
        else {
            killAjax();
            $("#dvValidationMessage").html("");
        }
    });

    $("#acNextMobile").on("click", function () {
        if ($("#txtMobileNo").val() == "") {
            $("#dvValidationMessage").html("Please Enter Mobile Number");
        }
        else if ($("#txtMobileNo").val().length != 10) {
            $("#dvValidationMessage").html("Please Enter Valid Mobile Number");
        }
        else {
            var IsOTPSent = SendOTP($("#txtMobileNo").val());
            if (IsOTPSent) {
                $("#dvValidationMessage").html("");
                $("#dvMobile").css("display", "none");
                $("#dvOTP").css("display", "block");
            }
            else {
                $("#dvValidationMessage").html("Error occurred while sending OTP. Please try again.");
            }
        }
    });

    $("#acNextOTP").on("click", function () {
        ClearValidationDiv();
        if ($("#txtOTP").val() == "") {
            $("#dvValidationMessage").html("Please Enter OTP");
        }
        else {
            var IsOTPValid = CheckOTP($("#txtOTP").val(), $("#txtMobileNo").val());
            if (IsOTPValid) {
                $("#dvOTP").css("display", "none");
                $("#dvLoginDetails").css("display", "block");
            }
            else {
                $("#dvValidationMessage").html("Please Enter Valid OTP");
            }
        }
    });

    $("#acNextLoginDetails").on("click", function () {
        ClearValidationDiv();
        $("#dvLoginDetails").css("display", "none");
        $("#dvProfileCreatedFor").css("display", "block");
    });

    $("#acNextProfileCreatedDetails").on("click", function () {
        ClearValidationDiv();
        $("#dvProfileCreatedFor").css("display", "none");
        $("#dvContactDetails").css("display", "block");
        $("#txtContactEmailId").val($("#txtEmail").val());
        $("#txtContactMobileNo").val($("#txtMobileNo").val());
    });

    $("#acNextContactDetails").on("click", function () {
        ClearValidationDiv();
        $("#dvContactDetails").css("display", "none");
        $("#dvBasicDetails").css("display", "block");
    });

    $("#acNextBasicDetails").on("click", function () {
        ClearValidationDiv();
        var IsUserRegistered = RegisterUser();
        if (IsUserRegistered) {
            $("#dvBasicDetails").css("display", "none");
            $("#dvPhoto").css("display", "block");
        }
    })
});

$("#btnUpload").click(function () {
    // Checking whether FormData is available in browser
    if (window.FormData !== undefined) {

        var fileUpload = $("#fuPhoto").get(0);
        var files = fileUpload.files;

        if (fileUpload.files.length > 0) {
            $("#dvValidationMessage").html("");

            // Create FormData object
            var fileData = new FormData(this.form.UserId);

            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
            }

            // Adding one more key to FormData object
            fileData.append("UserId", $('#hdnUserId').val());

            $.ajax({
                url: "/UserRegistration/UploadFiles",
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fileData,
                success: function (result) {
                    console.log(result);
                    $("#dvSuccessMessage").html("<strong>Registration Successful</strong>");
                    $("#dvSuccessMessage").attr("class", "alert alert-success ac");
                    setTimeout(function () {
                        window.location.href = "/User/MyProfile";
                    }, 2000);
                },
                error: function (err) {
                    console.log(err.statusText);
                }
            });
        } else {
            //alert("FormData is not supported.");
            $("#dvValidationMessage").html("Please select photo");

        }

    }
});


function CheckOTP(OTP, MobileNo) {
    //if (OTP == "") {
    //    alert("Please Enter OTP");
    //    return false;
    //}
    var IsValidOTP = false;
    var formData = {
        MobileNo: MobileNo, OTP: OTP
    };
    $.ajax({
        url: "/Base/CheckOTP",
        data: JSON.stringify(formData),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var result = data;
            if (data == "SUCCESS") {
                IsValidOTP = true;
            }
            else {
                IsValidOTP = false;
            }
        },
        error: function (xhr) {
            alert(xhr.status);
        }
    });
    return IsValidOTP;
}

function RegisterUser() {
    var IsUserRegistered = false;
    var objUserRegistration = $("#frmRegister").serialize();
    $.ajax({
        url: "RegisterUser",
        type: "POST",
        data: objUserRegistration,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.Status == "SUCCESS") {
                IsUserRegistered = true;
                $("#hdnUserId").val(data.UserId);
            }
            else {
                $("#dvValidationMessage").html(data.Status);
            }
        },
        error: function (xhr) {
            alert(xhr.statusCode);
        }
    });
    return IsUserRegistered;
}

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
            alert(xhr.statusCode);
        }
    });
    return IsMobileNoExists;
}

function ClearValidationDiv() {
    $("#dvValidationMessage").html("");
}

function CalculateAge() {
    //if (dob == undefined)
    if ($('#ddlDOBMonth').val() != "0" && $('#ddlDOBDay').val() != "0" && $('#ddlDOBYear').val() != "0") {
        var dob = new Date($('#ddlDOBMonth').val() + "-" + $('#ddlDOBDay').val() + "-" + $('#ddlDOBYear').val());
        //else
        //    dob = new Date(dob);
        var today = new Date();
        var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));

        $('#hdnAge').val(age);
    }
}