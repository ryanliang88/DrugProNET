const pageType = ".aspx";

$(document).ready(function ($) {
    var selectInput = $('query-selector');
    $(document).on('change', selectInput, function (e) {
        redirect($('#query-selector').find(':selected').val());
    });
});

function redirect(value) {
    if (value !== '') {
        document.location.href = value + pageType;
    }
}
