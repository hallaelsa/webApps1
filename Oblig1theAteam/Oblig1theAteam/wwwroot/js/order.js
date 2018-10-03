function validateCardNumber(source) {
    var regEx = /^[0-9]{16}$/;
    return isValid(regEx, source);
}

function validateCVV(source) {
    var regEx = /^[0-9]{3}$/;
    return isValid(regEx, source);
}

function validateExpirationMonth(source) {
    var regEx = /^1[012]|0?[1-9]$/;
    return isExpirationDateValid(regEx, source);
}

function validateExpirationYear(source) {
    var regEx = /^[1][8-9]|[2][0-9]$/;
    return isExpirationDateValid(regEx, source);
}

function isExpirationDateValid(regEx, source) {
    let OK = regEx.test($(source).val());
    if (!OK) {
        $(source).css("color", "red");
        $('#ExpirationDate').show();
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
    validation = validateExpirationMonth('#ExpirationMonth');
    submit = submit && validation;
    validation = validateExpirationYear('#ExpirationYear');
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
    console.log(target + ": no match");
    $(target).css("color", "red");
    $(target + "Error").show();
}

function clearError(target) {
    console.log(target + ": match");
    $(target).css("color", "black");
    $(target + "Error").hide();
}