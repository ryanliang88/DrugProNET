$(document).ready(function () {

    var path = window.location.pathname;
    var pageName = path.split("/").pop();

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(autoComplete);
    function autoComplete(sender, args) {
        $("#search_textBox").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: pageName + "/GetAutoCompleteData",
                    data: "{'value': '" + document.getElementById('search_textBox').value + "'}",
                    dataType: "json",
                    success: function (data) {
                        response(data.d);
                    },
                    error: function (result) {
                        alert("error");
                    }
                });
            }
        });
    }
});
    