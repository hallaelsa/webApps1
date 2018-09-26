function showVisaForm() {
    jQuery("#visa-form").css("visibility", "visible");
}

$(function () {
    $("#completingPurchaseForm").submit(function (e) {
        e.preventDefault();

        var formAction = $(this).attr("action");
        var formData = new FormData();

        var cardNumber = $('#CardNumber');
        var cvv = $('#CVV');
        var expMonth = $('#ExpirationMonth');
        var expYear = $('#ExpirationYear');
        formData.append("CardNumber", cardNumber);
        formData.append("CVV", cvv);
        formData.append("ExpirationMonth", expMonth);
        formData.append("ExpirationYear", expYear);

        $.ajax({
            type: 'post',
            url: formAction,
            data: formData,
            processData: false,
            contentType: false
        }).done(function (success) {
            if (success) {
                $("#orderModal").modal('show');
            }
        });
    });
});

$('#modal-home-button').click(function () {
    window.location.replace("/");
});

$('#modal-order-page-button').click(function () {
    window.location.replace("/Order/MyOrders");
});