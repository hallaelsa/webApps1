
function validateFirstName(source) {
    var regEx = /^[A-ZÆØÅa-zæøå]+(([\'\,\.\-][a-z])?[a-zæøå]*)*$/;
    return isValid(regEx, source);
}

function validateLastName(source) {
    var regEx = /^[A-ZÆØÅa-zæøå]+(([\'\,\.\-][a-z])?[a-zæøå]*)*$/;
    return isValid(regEx, source);
}

function validateBirthday(source) {
    var regEx = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]|(?:Jan|Mar|May|Jul|Aug|Oct|Dec)))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2]|(?:Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)(?:0?2|(?:Feb))\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9]|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep))|(?:1[0-2]|(?:Oct|Nov|Dec)))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
    return isValidDate(regEx, source);
}

function validatePhoneNumber(source) {
    var regEx = /^[1-9]{1}[0-9]{7}$/;
    return isValid(regEx, source);
}

function validateEmail(source) {
    var regEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return isValid(regEx, source);
}

function validatePassword(source) {
    var regEx = /^(?=^.{8,15}$)(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$/;
    return isValid(regEx, source);
}

function validateConfirmPassword(source, source2) {
    var match = compareStrings(source, source2);
    if (match) {
        clearError(source2);
    } else {
        displayError(source2);
    }
    return match;
}

function isValid(regEx, source) {
    var OK = regEx.test($(source).val());

    if (!OK) {
        displayError(source);
        return false;
    } else {     
        clearError(source);
        return true;
    }
}

function isValidDate(regEx, source) {
    var OK = regEx.test($(source).val());

    if (!OK) {
        displayError(source);
        return false;
    } else {
        var sourceBirthYear = $(source).val().substring(6);
        var yearNow = new Date().getFullYear();

        if (sourceBirthYear.length !== 4 || (yearNow - 13) < sourceBirthYear || (yearNow - 100) > sourceBirthYear) {
            displayError(source);
            return false;
        }

        clearError(source);
        return true;
    }
}

function compareStrings(firstString, secondString) {
    if ($(firstString).val() === $(secondString).val()) {
        return true;
    } else {
        return false;
    }
}

/**
 * Changes the styling of an HTML element to indicate an error.
 * @param target the HTML element to style
 */
function displayError(target) {
    console.log(target + ": no match");
    $(target).css("color", "red");
    $(target + "Error").show();
}

/**
 * Changes the styling of an HTML element to indicate normal state. Used to clear previous errors.
 * @param target the HTML element to style
 */
function clearError(target) {
    console.log(target + ": match");
    $(target).css("color", "black");
    $(target + "Error").hide();
}