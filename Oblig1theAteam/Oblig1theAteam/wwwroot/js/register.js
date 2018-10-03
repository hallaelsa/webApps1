
function validateFirstName(source) {
    let regEx = /^[A-ZÆØÅa-zæøå]+(([\'\,\.\-][a-z])?[a-zæøå]*)*$/;
    return isValid(regEx, source);
}

function validateLastName(source) {
    let regEx = /^[A-ZÆØÅa-zæøå]+(([\'\,\.\-][a-z])?[a-zæøå]*)*$/;
    return isValid(regEx, source);
}

function validateBirthday(source) {
    let regEx = /^(?:(?:31(\.)(?:0?[13578]|1[02]|(?:Jan|Mar|May|Jul|Aug|Oct|Dec)))\1|(?:(?:29|30)(\.)(?:0?[1,3-9]|1[0-2]|(?:Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\.)(?:0?2|(?:Feb))\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\.)(?:(?:0?[1-9]|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep))|(?:1[0-2]|(?:Oct|Nov|Dec)))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;
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
    console.log("input = " + inputEmail);

    if (inputEmail === "") {
        displayError(source);
        return false;
    }

    $.ajax({
        url: '/User/CheckUserExists',
        data: { email: inputEmail },
        success: function (res) {
            console.log("res " + res);
            if (!res) {
                let regEx = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return isValid(regEx, source);
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
        let sourceBirthYear = $(source).val().substring(6);
        let yearNow = new Date().getFullYear();

        if (sourceBirthYear.length !== 4 || (yearNow - 13) < sourceBirthYear || (yearNow - 100) > sourceBirthYear) {
            displayError(source);
            return false;
        }


        clearError(source);
        return true;

        //let sourceArray = $(source).val().split(".").map(Number);

        //console.log("Array day =" + sourceArray[0]);
        //console.log("Array month =" + sourceArray[1]);
        //console.log("Array year =" + sourceArray[2]);

        //let dayNow = parseInt(new Date().getDate());
        //let monthNow = parseInt(new Date().getMonth());
        //let yearNow = parseInt(new Date().getFullYear());

        //console.log("2018-" + sourceArray[2] + "=" + (yearNow - sourceArray[2]));
        //console.log("10-" + sourceArray[1] + " =" + (monthNow + 1 - sourceArray[1]));
        //console.log("3-" + sourceArray[0] + "=" + (dayNow - sourceArray[0]));

        //if ((yearNow - sourceArray[2]) > 13 && (yearNow - 100) > sourceArray[2]) {
        //    if ((monthNow + 1 - sourceArray[1]) >= 0 && (monthNow - sourceArray[1]) != NaN) {
        //        if ((dayNow - sourceArray[0]) > 0 && (dayNow - sourceArray[0]) != NaN) {
        //            clearError(source);
        //            return true;
        //        }
        //    }
        //}
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
    console.log(target + ": no match");
    $(target).css("color", "red");
    $(target + "Error").show();
}

function clearError(target) {
    console.log(target + ": match");
    $(target).css("color", "black");
    $(target + "Error").hide();
}