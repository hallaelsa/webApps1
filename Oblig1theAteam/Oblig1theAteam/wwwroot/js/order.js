function validateCardNumber(source) {
    var regEx = /^[0-9]{16}$/;
    return isValid(regEx, source);
}

function validateCVV(source) {
    var regEx = /^[0-9]{3}$/;
    return isValid(regEx, source);
}

function validateExpirationMonth() {
    var regEx = /^1[012]$|^0?[1-9]$/;
    return regEx.test($("#ExpirationMonth").val());
}

function validateExpirationYear() {
    var regEx = /^[1][8-9]$|^[2][0-9]$/;
    return regEx.test($("#ExpirationYear").val());
}

function validateExpirationDate(source) {
    let OKMonth = validateExpirationMonth();
    let OKYear = validateExpirationYear();
    
    if (!OKMonth || !OKYear) {
        $(source).css("color", "red");
        $('#ExpirationDate').show();

        if (OKMonth) {
            $("#ExpirationMonth").css("color", "black");
        }
        if (OKYear) {
            $("#ExpirationYear").css("color", "black");
        }
        return false;
    } else {
        $(source).css("color", "black");
        $('#ExpirationDate').hide();
        return true;
    }
}

function validateAll() {
    let submit = true;
    let validation;
    validation = validateCardNumber('#CardNumber');
    submit = submit && validation;
    validation = validateCVV('#CVV');
    submit = submit && validation;
    validation = validateExpirationDate("#ExpirationMonth");
    submit = submit && validation;
    validation = validateExpirationDate("#ExpirationYear");
    submit = submit && validation;
    return submit;
}

function isValid(regEx, source) {
    let OK = regEx.test($(source).val());
    if (!OK) {
        displayError(source);
        return false;
    } else {
        clearError(source);
        return true;
    }
}

function displayError(target) {
    $(target).css("color", "red");
    $(target + "Error").show();
}

function clearError(target) {
    $(target).css("color", "black");
    $(target + "Error").hide();
}