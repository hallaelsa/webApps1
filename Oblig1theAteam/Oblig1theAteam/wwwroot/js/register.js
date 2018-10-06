
// Registration validation. If it's not true the register button wont be triggered.
function validateSubmitForm() {
    let submit = true;
    let validation;
    validation = validateFirstName('#FirstName');
    submit = submit && validation;
    validation = validateLastName('#LastName');
    submit = submit && validation;
    validation = validateBirthday('#Birthday');
    submit = submit && validation;
    validation = validatePhoneNumber('#PhoneNumber');
    submit = submit && validation;
    validation = validateEmailForSubmit('#Email');
    submit = submit && validation;
    validation = validatePassword('#Password');
    submit = submit && validation;
    validation = validateConfirmPassword('#Password', '#ConfirmPassword');
    submit = submit && validation;
    return submit;
}

function validateFirstName(source) {
    let regEx = /^[A-ZÆØÅa-zæøå]+(([\'\,\.\-][a-z])?[a-zæøå]*)*$/;
    return isValid(regEx, source);
}

function validateLastName(source) {
    let regEx = /^[A-ZÆØÅa-zæøå]+(([\'\,\.\-][a-z])?[a-zæøå]*)*$/;
    return isValid(regEx, source);
}

function validateBirthday(source) {
    let regEx = /^(?:(?:31(\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\.)(?:0?2)\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
    return isValidDate(regEx, source);
}

function validatePhoneNumber(source) {
    let regEx = /^[1-9]{1}[0-9]{7}$/;
    return isValid(regEx, source);
}

function validateEmailForSubmit(source) {
    let regEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return isValid(regEx, source);
}

function validateEmail(source) {
    var inputEmail = $(source).val();

    if (inputEmail === "") {
        displayError(source);
        return false;
    }

    $.ajax({
        type: "POST",
        url: '/User/CheckUserExists',
        data: { email: inputEmail },
        success: function (res) {
            if (!res) {
                return validateEmailForSubmit(source);
            } else {
                $(source).val("");
                $(source).attr("placeholder", inputEmail + " already exists");
                return false;
            }
        }
    });
}

function validatePassword(source) {
    let regEx = /^(?=^.{8,15}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$/;
    return isValid(regEx, source);
}

function validateConfirmPassword(source, source2) {
    let match = compare(source, source2);
    if (match) {
        clearError(source2);
    } else {
        displayError(source2);
    }
    return match;
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

function isValidDate(regEx, source) {
    let OK = regEx.test($(source).val());

    if (!OK) {
        displayError(source);
        return false;
    } else {
        const birthday = $(source).val();
        const dayBirth = parseInt(birthday.substring(0, 2));
        const monthBirth = parseInt(birthday.substring(3, 5));
        const yearBirth = parseInt(birthday.substring(6));
        

        const dateNow = new Date();
        const dayNow = dateNow.getDate();
        const monthNow = dateNow.getMonth() + 1;
        const yearNow = dateNow.getFullYear();

        if (birthday.substring(6).length !== 4) {
            displayError(source);
            return false;
        }

        if (yearNow - yearBirth > 13 && yearNow - 110 <= yearBirth) {
            clearError(source);
            return true;
        }

        if (yearNow - yearBirth == 13 && monthNow - monthBirth > 0) {
            clearError(source);
            return true;
        }

        if (yearNow - yearBirth == 13 && monthNow - monthBirth == 0 && dayNow - dayBirth >= 0) {
            clearError(source);
            return true;
        }

        displayError(source);
        return false;
    }
}

function compare(password, comparePassword) {
    if ($(password).val() === $(comparePassword).val()) {
        return true;
    } else {
        return false;
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