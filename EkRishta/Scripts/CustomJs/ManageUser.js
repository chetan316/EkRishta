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