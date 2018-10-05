$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(autoComplete);
    function autoComplete(sender, args) {
        //var data = [
        //    "Dog",
        //    "Cat",
        //    "Bird",
        //    "Giraffe",
        //    "Bowl",
        //    "Big"
        //];

        //$("#search_textBox").autocomplete({
        //    source: data
        //});


        $("#search_textBox").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "ProteinInfo.aspx/GetAutoCompleteData",
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
    