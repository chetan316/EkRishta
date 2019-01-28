function SendOTP(MobileNo) {
    var IsOTPSent = false;
    if (MobileNo == '' || MobileNo.length != 10) {
        alert('Please Enter Valid Mobile Number.');
        return false;
    }
    var formData = { MobileNo: MobileNo };
    $.ajax({
        url: "/Base/SendSMS",
        data: formData,
        type: "GET",
        //contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        async:false,
        success: function (data) {
            if (data == 'SUCCESS') {
                //alert('OTP Sent Successfully');
                IsOTPSent= true;
            }
            else {
                //alert('Please Enter Valid OTP');
                IsOTPSent= false;
            }
        },
        error: function (xhr) {
            alert(xhr.status);
        }
    });
    return IsOTPSent;
}

var xhr;
function killAjax() {
    try {
        if (xhr != null && xhr != undefined) {
            xhr.abort();
        }
    } catch (e) { }
}