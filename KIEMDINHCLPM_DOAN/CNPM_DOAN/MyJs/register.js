function showDangKy() {
    document.getElementById("register").classList.toggle("active");
    document.getElementById("overlayRe").classList.toggle("active");
}
function invalidNameAccount(textbox) {
    if (textbox.value == "") {
        texbox.setCustomValidity('Tên tài khoản không được để trống');
    }
    else if (textbox.tooLong()) {
        textbox.setCustomValidity('Họ tên tối đa 30 kí tự');
    }
}