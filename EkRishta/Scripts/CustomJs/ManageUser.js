function SendRequest(requestedUserId, requestStatus) {
    var IsSendRequestButtonVisible = $("#btnSendRequest_" + requestedUserId).is(":visible") == true ? "1" : "0";
    if (requestStatus == undefined)
        requestStatus = IsSendRequestButtonVisible == "1" ? "Pending" : "Cancelled";

    var obj = {
        RequestedUserId: requestedUserId,
        RequestStatus: requestStatus
    }
    var formData = JSON.stringify(obj);
    $.ajax({
        url: "/User/SendRequest",
        data: formData,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if (data == "Request already Sent") {
                    $("#divMessage").html("Request already Sent");
                    $("#divMessage").attr("class", "alert alert-info fade in");
                }
                else if (requestStatus == "Pending") {
                    $("#divMessage").html("Request Sent Successfully");
                    $("#divMessage").attr("class", "alert alert-success fade in");
                    $("#btnCancelRequest_" + requestedUserId).css('display', 'block');
                    $("#btnSendRequest_" + requestedUserId).css('display', 'none');
                }
                else if (requestStatus == "Cancelled") {
                    $("#divMessage").html("Request Cancelled Successfully");
                    $("#divMessage").attr("class", "alert alert-success fade in");
                    $("#btnCancelRequest_" + requestedUserId).css('display', 'none');
                    $("#btnSendRequest_" + requestedUserId).css('display', 'block');
                }
                else if (requestStatus == "Blocked") {
                    $("#divMessage").html("Profile Blocked Successfully");
                    $("#divMessage").attr("class", "alert alert-success fade in");
                    $("#btnBlockRequest_" + requestedUserId).val('Blocked');
                    $("#btnBlockRequest_" + requestedUserId).removeAttr('onclick');
                }
            }
            else {
                $("#divMessage").html("Error occurred. Please Try again");
                $("#divMessage").attr("class", "alert alert-danger fade in");
            }
        },
        error: function (xhr) {
            console.log(xhr.status + "-" + xhr.responseText);
        }
    });
}

function ShortlistProfile(shortlistedUserId, requestStatus, cntrl, source) {
    requestStatus = (requestStatus == "" || requestStatus == "NS") ? "S" : "NS";
    var obj = {
        RequestedUserId: shortlistedUserId,
        RequestStatus: requestStatus
    }
    var formData = JSON.stringify(obj);
    $.ajax({
        url: "/User/ManageShortlistedProfile",
        data: formData,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != "") {
                $(cntrl).attr('onclick', 'ShortlistProfile(' + shortlistedUserId + ',\'' + requestStatus + '\',this' + ')')
                if (source != undefined && source == "ShortlistedProfiles") {
                    $("#user_sec_" + shortlistedUserId).css("display", "none");
                }
                else if (source != undefined && source == "ViewProfile") {
                    $("#pLike").html("Liked");
                }
            }
            else {
                $("#divMessage").html("Something went wrong. Please Try again");
                $("#divMessage").attr("class", "alert alert-danger fade in");
            }
        },
        error: function (xhr) {
            console.log(xhr.status + "-" + xhr.responseText);
        }
    });
}

function AcceptRejectRequest(requestedUserId, requestStatus) {
    var obj = {
        RequestedUserId: requestedUserId,
        RequestStatus: requestStatus
    }
    var formData = JSON.stringify(obj);
    $.ajax({
        url: "/User/AccceptRejectRequest",
        data: formData,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (data != "") {
                if (requestStatus == "Accepted") {
                    $("#divMessage").html("Request Updated Successfully");
                    $("#divMessage").attr("class", "alert alert-success fade in");
                    $("#btnAcceptRequest_" + requestedUserId).attr("class", "btn btn-info disabled");
                    $("#btnAcceptRequest_" + requestedUserId).val("Accepted");
                    $("#btnRejectRequest_" + requestedUserId).attr("class", "btn btn-info");
                    $("#btnRejectRequest_" + requestedUserId).val("Reject");
                }
                else if (requestStatus == "Rejected") {
                    $("#divMessage").html("Request Updated Successfully");
                    $("#divMessage").attr("class", "alert alert-success fade in");
                    $("#btnRejectRequest_" + requestedUserId).attr("class", "btn btn-info disabled");
                    $("#btnRejectRequest_" + requestedUserId).val("Rejected");
                    $("#btnAcceptRequest_" + requestedUserId).attr("class", "btn btn-info");
                    $("#btnAcceptRequest_" + requestedUserId).val("Accept");
                }
            }
            else {
                $("#divMessage").html("Error occurred. Please Try again");
                $("#divMessage").attr("class", "alert alert-danger fade in");
            }
        },
        error: function (xhr) {
            console.log(xhr.status + "-" + xhr.responseText);
        }
    });
}

function ViewProfile(UserId) {
    var obj = {
        UserId: UserId
    }
    var formData = JSON.stringify(obj);
    //window.location.href = "/User/ViewProfile/"+UserId;
    $.ajax({
        url: "/User/MyProfile",
        data: formData,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            //if (data != "") {
            //    if (data == "Request already Sent") {
            //        $("#divMessage").html("Request already Sent");
            //        $("#divMessage").attr("class", "alert alert-info fade in");
            //    }
            //    else if (requestStatus == "Pending") {
            //        $("#divMessage").html("Request Sent Successfully");
            //        $("#divMessage").attr("class", "alert alert-success fade in");
            //        $("#btnCancelRequest_" + requestedUserId).css('display', 'block');
            //        $("#btnSendRequest_" + requestedUserId).css('display', 'none');
            //    }
            //    else if (requestStatus == "Cancelled") {
            //        $("#divMessage").html("Request Cancelled Successfully");
            //        $("#divMessage").attr("class", "alert alert-success fade in");
            //        $("#btnCancelRequest_" + requestedUserId).css('display', 'none');
            //        $("#btnSendRequest_" + requestedUserId).css('display', 'block');
            //    }
            //}
            //else {
            //    $("#divMessage").html("Error occurred. Please Try again");
            //    $("#divMessage").attr("class", "alert alert-danger fade in");
            //}
            window.location.href = "/User/ViewProfile";
        },
        error: function (xhr) {
            console.log(xhr.status + "-" + xhr.responseText);
        }
    });
}

/*----------------------------------------------------Update Profile Block---------------------------------------------------------------------------------*/
function EditBasicDetails(cntrl) {
    if (cntrl.innerHTML.toLowerCase() == "edit") {
        $("#ulBasicLabels").css("display", "none");
        $("#ulBasicEdit").css("display", "block");
        cntrl.innerHTML = "Update";
    }
    else {
        UpdateBasicDetails(cntrl);
    }
}

function UpdateBasicDetails(cntrl) {
    var formData = {
        UserFirstName: $("#txtFirstName").val(),
        UserLastName: $("#txtLastName").val(),
        DOBDay: $("#ddlDOBDay").val(),
        DOBMonth: $("#ddlDOBMonth").val(),
        DOBYear: $("#ddlDOBYear").val(),
        UserAge: $("#hdnAge").val(),
        UserGender: $("#ddlGender").val(),
        UserMaritialStatus: $("#ddlMaritialStatus").val(),
        UserEmailId: $("#txtEmail").val(),
        LanguageId: $("#ddlMotherTounge").val()
    }
    $.ajax({
        url: "/User/UpdateBasicDetails",
        data: formData,
        type: "POST",
        success: function (htmlText) {
            $('#divBasicDetails').html(htmlText);
            $("#ulBasicLabels").css("display", "block");
            $("#ulBasicEdit").css("display", "none");
            cntrl.innerHTML = "Edit";
        },
        error: function (HttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    })
}


function EditProfessionalDetails(cntrl) {
    if (cntrl.innerHTML.toLowerCase() == "edit") {
        $("#ulProfessionalLabels").css("display", "none");
        $("#ulProfessionalEdit").css("display", "block");
        cntrl.innerHTML = "Update";
    }
    else {
        UpdateProfessionalDetails(cntrl);
    }
}

function UpdateProfessionalDetails(cntrl) {
    var formData = {
        Degree: $("#txtDegree").val(),
        Field: $("#txtField").val(),
        CollegeName: $("#txtCollegeName").val(),
        CompanyName: $("#txtCompanyName").val(),
        Designation: $("#txtDesignation").val(),
        Income: $("#ddlIncome").val()
    }
    $.ajax({
        url: "/User/UpdateProfessionalDetails",
        data: formData,
        type: "POST",
        success: function (htmlText) {
            $('#divProfessionalDetails').html(htmlText);
            $("#ulProfessionalLabels").css("display", "block");
            $("#ulProfessionalEdit").css("display", "none");
            cntrl.innerHTML = "Edit";
        },
        error: function (HttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    })
}

function EditAddressDetails(cntrl) {
    if (cntrl.innerHTML.toLowerCase() == "edit") {
        $("#ulAddressLabels").css("display", "none");
        $("#ulAddressEdit").css("display", "block");
        cntrl.innerHTML = "Update";
    }
    else {
        UpdateAddressDetails(cntrl);
    }
}

function UpdateAddressDetails(cntrl) {
    var formData = {
        Address1: $("#txtAddress1").val(),
        Address2: $("#txtAddress2").val(),
        CityId: $("#ddlCity").val(),
        StateId: $("#ddlState").val(),
        CountryId: $("#ddlCountry").val(),
        Pincode: $("#txtPincode").val()
    }
    $.ajax({
        url: "/User/UpdateAddressDetails",
        data: formData,
        type: "POST",
        success: function (htmlText) {
            $('#divAddressDetails').html(htmlText);
            $("#ulAddressLabels").css("display", "block");
            $("#ulAddressEdit").css("display", "none");
            cntrl.innerHTML = "Edit";
        },
        error: function (HttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    })
}

function EditFamilyDetails(cntrl) {
    if (cntrl.innerHTML.toLowerCase() == "edit") {
        $("#ulFamilyLabels").css("display", "none");
        $("#ulFamilyEdit").css("display", "block");
        cntrl.innerHTML = "Update";
    }
    else {
        UpdateFamilyDetails(cntrl);
    }
}

function UpdateFamilyDetails(cntrl) {
    var formData = {
        FatherName: $("#txtFatherName").val(),
        FatherProfession: $("#txtFatherProfession").val(),
        MotherName: $("#txtMotherName").val(),
        MotherProfession: $("#txtMotherProfession").val(),
        FamilyDescription: $("#txtFamilyDescription").val()
    }
    $.ajax({
        url: "/User/UpdateFamilyDetails",
        data: formData,
        type: "POST",
        success: function (htmlText) {
            $('#divFamilyDetails').html(htmlText);
            $("#ulFamilyLabels").css("display", "block");
            $("#ulFamilyEdit").css("display", "none");
            cntrl.innerHTML = "Edit";
        },
        error: function (HttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    })
}

function EditReligionDetails(cntrl) {
    if (cntrl.innerHTML.toLowerCase() == "edit") {
        $("#ulReligionLabels").css("display", "none");
        $("#ulReligionEdit").css("display", "block");
        cntrl.innerHTML = "Update";
    }
    else {
        UpdateReligionDetails(cntrl);
    }
}

function UpdateReligionDetails(cntrl) {
    var formData = {
        ReligionId: $("#ddlReligion").val(),
        CastId: $("#ddlCast").val(),
        MoonSign: $("#txtMoonsign").val(),
        Star: $("#txtStar").val(),
        Gotra: $("#txtGotra").val()
    }
    $.ajax({
        url: "/User/UpdateReligionDetails",
        data: formData,
        type: "POST",
        success: function (htmlText) {
            $('#divReligionDetails').html(htmlText);
            $("#ulReligionLabels").css("display", "block");
            $("#ulReligionEdit").css("display", "none");
            cntrl.innerHTML = "Edit";
        },
        error: function (HttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    })
}

function EditOtherDetails(cntrl) {
    if (cntrl.innerHTML.toLowerCase() == "edit") {
        $("#ulOtherLabels").css("display", "none");
        $("#ulOtherEdit").css("display", "block");
        cntrl.innerHTML = "Update";
    }
    else {
        UpdateOtherDetails(cntrl);
    }
}

function UpdateOtherDetails(cntrl) {
    var formData = {
        Height: $("#txtHeight").val(),
        BodyType: $("#txtBodyType").val(),
        SkinTone: $("#txtSkinTone").val(),
        BloodGroup: $("#ddlBloodGroup").val(),
        BirthPlace: $("#txtBirthPlace").val(),
        BirthTime: $("#txtBirthTime").val(),
        IsSmoke: $("#ddlSmoke").val(),
        IsDrink: $("#ddlDrink").val(),
        IsPhysicalDisable: $("#ddlPhysicalDisable").val(),
        IdealpartnerDescription: $("#txtIdealpartnerDescription").val()
    }
    $.ajax({
        url: "/User/UpdateOtherDetails",
        data: formData,
        type: "POST",
        success: function (htmlText) {
            $('#divOtherDetails').html(htmlText);
            $("#ulOtherLabels").css("display", "block");
            $("#ulOtherEdit").css("display", "none");
            cntrl.innerHTML = "Edit";
        },
        error: function (HttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    })
}

function EditUserPreference(cntrl) {
    if (cntrl.innerHTML.toLowerCase() == "edit") {
        $("#ulPreferenceLabels").css("display", "none");
        $("#ulPreferenceEdit").css("display", "block");
        cntrl.innerHTML = "Update";
    }
    else {
        UpdateUserPreference(cntrl);
    }
}

function UpdateUserPreference(cntrl) {
    var formData = {
        FromAge: $("#txtFromAgePref").val(),
        ToAge: $("#txtToAgePref").val(),
        FromHeight: $("#txtFromHeightPref").val(),
        ToHeight: $("#txtToHeightPref").val(),
        MaritialStatus: $("#ddlMaritialStatusPref").val(),
        CityId: $("#ddlCityPref").val(),
        //CountryId:$("#").val(),
        ReligionId: $("#ddlReligionPref").val(),
        CasteId: $("#ddlCastePref").val(),
        MotherToungeId: $("#ddlMotherToungePref").val(),
        Income: $("#ddlIncomePref").val(),
        IsSmoke: $("#ddlSmokePref").val(),
        IsDrink: $("#ddlDrinkPref").val(),
        IsPhysicalDisable: $("#ddlPhysicalDisablePref").val(),
        SkinTone: $("#txtSkinTonePref").val(),
        BodyType: $("#txtBodyTypePref").val(),
        ActionType: "U"
    };

    $.ajax({
        url: "/UserPreferences/ManageUserPreferences",
        data: formData,
        type: "POST",
        success: function (htmlText) {
            $('#divUserPreference').html(htmlText);
            $("#ulPreferenceLabels").css("display", "block");
            $("#ulPreferenceEdit").css("display", "none");
            cntrl.innerHTML = "Edit";
        },
        error: function (HttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    })
}

function CalculateAge() {
    if ($('#ddlDOBMonth').val() != "0" && $('#ddlDOBDay').val() != "0" && $('#ddlDOBYear').val() != "0") {
        var dob = new Date($('#ddlDOBMonth').val() + "-" + $('#ddlDOBDay').val() + "-" + $('#ddlDOBYear').val());
        var today = new Date();
        var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));

        $('#hdnAge').val(age);
        $('#pUserAge').html(age);
    }
}

$(document).ready(function () {
    $('#txtHeight').on('keypress', function (e) {
        var ingnore_key_codes = [34, 39];
        if ($.inArray(e.which, ingnore_key_codes) >= 0) {
            e.preventDefault();
        }
    });

    $("#fuCoverPhoto").on('change', function () {
        // Checking whether FormData is available in browser
        if (window.FormData !== undefined) {

            var fileUpload = $("#fuCoverPhoto").get(0);
            var files = fileUpload.files;

            if (fileUpload.files.length > 0) {
                // Create FormData object
                var fileData = new FormData(this.form.UserId);

                // Looping over all files and add it to FormData object
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                // Adding one more key to FormData object
                fileData.append("ImageType", "Cover");

                $.ajax({
                    url: "/User/UpdatePhoto",
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: fileData,
                    success: function (result) {
                        $(".user-bg").css({
                            'background-image': 'url(' + result + ')',
                            'background-repeat': 'no-repeat'
                        });
                    },
                    error: function (err) {
                        console.log(err.statusText);
                    }
                });
            } else {
                alert("Please select photo");
            }
        } else {
            alert("Please select valid file");
        }
    });

    $("#fuProfilePhoto").on('change', function () {
        if (window.FormData !== undefined) {

            var fileUpload = $("#fuProfilePhoto").get(0);
            var files = fileUpload.files;

            if (fileUpload.files.length > 0) {
                // Create FormData object
                var fileData = new FormData(this.form.UserId);

                // Looping over all files and add it to FormData object
                for (var i = 0; i < files.length; i++) {
                    fileData.append(files[i].name, files[i]);
                }

                // Adding one more key to FormData object
                fileData.append("ImageType", "Profile");

                $.ajax({
                    url: "/User/UpdatePhoto",
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: fileData,
                    success: function (result) {
                        $("#imgProfilePic").attr("src", result);
                    },
                    error: function (err) {
                        console.log(err.statusText);
                    }
                });
            } else {
                alert("Please select photo");

            }

        } else {
            alert("Please select valid file");
        }
    });
});

function OpenFileDialog() {
    $("#fuCoverPhoto").click();
}

function OpenFileDialogForProfilePic() {
    $("#fuProfilePhoto").click();
}
