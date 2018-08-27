function SendRequest(requestedUserId) {
    var IsSendRequestButtonVisible = $("#btnSendRequest_"+requestedUserId).is(":visible") == true ? "1" : "0";
    var requestStatus = IsSendRequestButtonVisible == "1" ? "Pending" : "Cancelled";
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

function CalculateAge() {
    if ($('#ddlDOBMonth').val() != "0" && $('#ddlDOBDay').val() != "0" && $('#ddlDOBYear').val() != "0") {
        var dob = new Date($('#ddlDOBMonth').val() + "-" + $('#ddlDOBDay').val() + "-" + $('#ddlDOBYear').val());
        var today = new Date();
        var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));

        $('#hdnAge').val(age);
        $('#pUserAge').html(age);
    }
}