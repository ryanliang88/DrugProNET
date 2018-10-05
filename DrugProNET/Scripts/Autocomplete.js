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
        // id is modified by ASP NET
        $("#search_textBox").autocomplete({
            source: data
        });

        // See if you can get someting called a static id on the aspx page
    }
});
    