const pageType = ".aspx";
/*
Author: Andy Tang
*/
$(document).ready(function ($) {
    var selectInput = $('query-selector');
    $(document).on('change', selectInput, function (e) {
        redirect($('#query-selector').find(':selected').val());
    });
});

/*
Author: Andy Tang
*/
function redirect(value) {
    if (value !== '') {
        document.location.href = value + pageType;
    }
}
