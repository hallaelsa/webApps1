function addToShoppingCart(movie_id, button=null) {
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

$(document).ready(function () {
    // Predictive search for movie titles.
    $(document).ready(function () {
        $('.typeahead').typeahead({
            autoSelect: true,
            minLength: 1,
            delay: 400,
            source: function (query, process) {
                return $.get('/Home/GetMoviesByTitleJson?title=' + $('#title').val(), { query: query }, function (data) {
                    console.log(data);
                    data = JSON.parse(data);
                    return process(data);
                });
            }
        });
    });


    // When modal is clicked, populate it with the relevant movies information.
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
        
        const trailerLink = $(this).data('movie-trailer');
        setTrailer(trailerLink);
        
        const movieId = $(this).data('movie-id');
        setModalButton(movieId)
    })

    // Set the trailer for the movie, if one exists.
    function setTrailer(trailerLink) {
        if (trailerLink != "") {
            $('#modal-movie-trailer').css('display', 'block');
            $("#trailer").attr('src', trailerLink);
        } else {
            $('#modal-movie-trailer').css('display', 'none');
        }
    }

    // Following code determines which button to show in the modal: Add to Cart; In Cart and Owned.
    function setModalButton(id) {
        const inner = $('#' + id + '-buy-button').text().trim();
        const button = $('#modal-movie-buy-button');

        if (inner == "Owned") {
            button.replaceWith('<a id="modal-movie-buy-button" class="btn btn-default btn-lg disabled">Owned</a>');
        } else if (inner == "In Cart") {
            button.replaceWith('<a id="modal-movie-buy-button" class="btn btn-default btn-lg disabled">In Cart</a>');
        } else {
            button.replaceWith('<a id="modal-movie-buy-button" class="btn btn-primary btn-lg" onclick="addToShoppingCart(' + id + ')">Add to cart</a>');
        }
    }

    // code for closing the modal
    $('#movieModal').on('hide.bs.modal', function () {
        $('#trailer').attr('src', '');
    })
})