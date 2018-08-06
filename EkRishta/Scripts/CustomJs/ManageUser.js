function SendRequest(requestedUserId) {
    var IsSendRequestButtonVisible = $("#btnSendRequest").css('display') != 'none' ? "1" : "0";
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
                }
                else if (requestStatus == "Cancelled") {
                    $("#divMessage").html("Request Cancelled Successfully");
                    $("#divMessage").attr("class", "alert alert-success fade in");
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
        RequestStatus: status
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
                }
                else if (requestStatus == "Rejected") {
                    $("#divMessage").html("Request Updated Successfully");
                    $("#divMessage").attr("class", "alert alert-success fade in");
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