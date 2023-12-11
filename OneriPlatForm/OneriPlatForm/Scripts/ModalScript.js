$(document).ready(function () {
    $("#moviepost").click(function (event) {     
        event.preventDefault();
        var formData = {
            title: $('#title_id').val(),
            year: $('#year_text').val(),
            genre: $('#genre_text').val(),
            imdb: $('#imdb_text').val(),
            director: $('#director_text').val()
        };
        $.ajax({
            type: 'POST',
            url: '/Movie/CreateList',
            data: JSON.stringify(formData),
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response.success) { 
                    var row = "";
                    $.each(response.data, function (i, movie) { // movie olarak düzeltildi
                        row += '<tr>';
                        row += '<td>' + movie.Title + '</td>';
                        row += '<td>' + movie.Year + '</td>';
                        row += '<td>' + movie.Genre + '</td>';
                        row += '<td>' + movie.Imdb + '</td>';
                        row += '<td>' + movie.Director + '</td>';
                        row += '</tr>';


                    });
                    $('#tablemovie').html(row);
                } else {
                    alert('İşlem başarısız: ' + response.message);

                }

            }
        });
    });
    function showSuccessToast(message) {
        toastr.success(message);
    }

    function showErrorToast(message) {
        toastr.error(message);
    }

    function ClearModalForm() {
        $("#myForm input").each(function () {
            $(this).val('');
        });
    }

    function parseJsonDate(jsonDate) {

        var match = /\/Date\((\d+)\)\//.exec(jsonDate);

        if (match) {
            var timestamp = parseInt(match[1]);
            var date = new Date(timestamp);
            return date;
        } else {

            return null;
        }
    }

    function formatDateToYYYYMMDD(date) {
        var year = date.getFullYear();
        var month = (date.getMonth() + 1).toString().padStart(2, '0');
        var day = date.getDate().toString().padStart(2, '0');
        return year + '-' + month + '-' + day;
    }

    $('.searchablecontrol').on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#myTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

});

