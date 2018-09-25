function showVisaForm() {
    jQuery("#visa-form").css("visibility", "visible");

}

$(function () {
    $('#editCourseModal').on("click", function (e) {
        e.preventDefault();
        console.log("halla")
        //perform the url load  then
        $('#orderModal').modal({
            keyboard: true
        }, 'show');
        return false;
    })
})

$(document).ready(function () {
    $('a[data-movieModal=true]').click(function () {
        const movie_id = $(this).data('movie-id');
        $('#modal-home-button').click(function () {
            window.location.replace("/");
        });
        const movie_id = $(this).data('movie-id');
        $('#modal-order-page-button').click(function () {
            window.location.replace("/Order/MyOrders");
        });
    })
});