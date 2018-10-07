$(document).ready(function () {

    var path = window.location.pathname;
    var pageName = path.split("/").pop();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(autoComplete);

    function autoComplete(sender, args) {
        $("#search_textBox").autocomplete({
            minLength: 2,
            delay: 0,
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: pageName + "/GetAutoCompleteData",
                    data: "{'value': '" + request.term + "'}", // 'value' in this line should match the name of the parameter in the webmethod
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        response(data.d);
                    },
                    error: function (result) {
                        console.error("An error occured while obtaining search terms from server.");
                    }
                });
            }
        });
    }
});
    