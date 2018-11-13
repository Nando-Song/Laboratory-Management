verifyCode = new GVerify("v_container"); //验证码
var f7 = 1;

$(".rShort").blur(function () {
    var res = verifyCode.validate($(".rShort").val());
    if (res) {
        $(".error").html("");
        f7 = 1;
    }
    else {
        $(".error").html("验证码错误");
        $(".r7").html("*");
        f7 = 0;
    }
})

function CheckForm() {
    var res = verifyCode.validate($(".rShort").val());
    if (res) {
        $(".error").html("");
        f7 = 1;
    }
    else {
        $(".error").html("验证码错误");
        $(".r7").html("*");
        f7 = 0;
    }
     
    if (f7 == 1) { return true; }
    else { return false; }
}

