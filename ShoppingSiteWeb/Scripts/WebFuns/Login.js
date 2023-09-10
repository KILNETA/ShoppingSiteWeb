function inputBoxIsNullOrEmpty(obj, error) {
    if (obj.value == "") {
        error.innerText = "請填寫此欄";
        obj.style.borderColor = "#FF0000";
    }
    else {
        error.innerText = "　";
        obj.style = "TextBox";
    }
}

jQuery(function () {
    var time = new Date();
    var text = "Welcome Einkaufen. Current time:" + time.toUTCString();
    jQuery('#LoginQRcode').qrcode({
        width: 144, height: 144, text: text
    });
})