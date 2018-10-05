$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(autoComplete);
    function autoComplete(sender, args) {
        var data = [
            "Dog",
            "Cat",
            "Bird",
            "Giraffe",
            "Bowl",
            "Big"
        ];

        $("#search_textBox").autocomplete({
            source: data
        });
    }
});
    