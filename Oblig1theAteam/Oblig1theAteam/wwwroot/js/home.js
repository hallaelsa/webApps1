﻿function addToShoppingCart(movie_id, button=null) {
    $.ajax({
        url: '/Home/AddToShoppingCart',
        data: { id: movie_id },
        success: function (res) {
            if (res) {
                let cartCount = $('#cartCount').html();
                cartCount++;
                $('#cartCount').html(cartCount);
                $('#movieModal').modal('hide');

                if (button != null) {
                    $(button).replaceWith('<a id = "' + movie_id + '-buy-button" class= "btn btn-default btn-buy-movie disabled" >In Cart</a >');
                } else {
                    $('#' + movie_id + '-buy-button').replaceWith('<a id = "buy-button" class= "btn btn-default btn-buy-movie disabled" >In Cart</a >');
                }
            } else {
                $('#movieModal').modal('hide');
            }
        }
    });
}

function getMovieList(title) {
    $.ajax({
        url: '/Home/GetMoviesByTitleJson',
        data: { title: title },
        success: function (res) {
            if (res) {
                console.log(res);
            }
        }
    })
}

$(document).ready(function () {
    const bloodhoundSuggestions = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('Value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        sufficient: 3,
        remote: {
            wildcard: '%QUERY',
            url: '/Home/GetMoviesByTitleJson?title=%QUERY',
        }
    });

    $("#title").typeahead({
        hint: false,
        highlight: true,
        minLength: 1
    }, {
        name: 'suggestions',
        displayKey: 'Value',
        source: bloodhoundSuggestions
    });

    $('a[data-movieModal=true]').click(function () {
        const movie_title = $(this).data('movie-title');
        $('#modal-movie-title').html(movie_title);

        const movie_poster = $(this).data('movie-poster');
        $('#modal-movie-poster-container').html('<img id="modal-movie-poster" src="../images/posters/main/' + movie_poster + '" />');

        const movie_description = '<span class="modal-movie-heading">Synopsis</span><br/>' + $(this).data('movie-description');
        $('#modal-movie-description').html(movie_description);

        const movie_year = '<span class="modal-movie-heading">Year: </span>' + $(this).data('movie-year');
        $('#modal-movie-year').html(movie_year);

        const movie_rating = '<span class="modal-movie-heading">Rating: </span>' + $(this).data('movie-rating');
        $('#modal-movie-rating').html(movie_rating + " years");

        const movie_runtime = '<span class="modal-movie-heading">Runtime: </span>' + $(this).data('movie-runtime');
        $('#modal-movie-runtime').html(movie_runtime + " min");

        const movie_price = '<span class="modal-movie-heading">Price: </span>' + $(this).data('movie-price');
        $('#modal-movie-price').html(movie_price + ",-");

        let trailer_link = $(this).data('movie-trailer');
        if (trailer_link != "") {
            $('#modal-movie-trailer').css('display', 'block');
            $("#trailer").attr('src', trailer_link);
        } else {
            $('#modal-movie-trailer').css('display', 'none');
        }

        const movie_id = $(this).data('movie-id');

        const inner = $('#' + movie_id + '-buy-button').text().trim();
        console.log(inner);
        const button = $('#modal-movie-buy-button');
        console.log(inner);

        if (inner == "Owned") {
            button.replaceWith('<a id="modal-movie-buy-button" class="btn btn-default btn-lg disabled">Owned</a>');
        } else if (inner == "In Cart") {
            button.replaceWith('<a id="modal-movie-buy-button" class="btn btn-default btn-lg disabled">In Cart</a>');
        } else {
            button.replaceWith('<a id="modal-movie-buy-button" class="btn btn-primary btn-lg" onclick="addToShoppingCart(' + movie_id + ')">Add to cart</a>');
        }
    })

    $('#movieModal').on('hide.bs.modal', function () {
        $('#trailer').attr('src', '');
    })
})