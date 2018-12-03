(function ($) {

    // Initialise all tool-tips
    $('[data-toggle="tooltip"]').tooltip();

    // Set the checked state of the checkbox for an item in the table from stored value
    // in the cookie (if present)
    var attachmentId = Mind.Utilities.getCookie("attachmentId");
    if (attachmentId !== "") {

        var $input = $('.form-check-input[data-query-parameter = "attachmentId=' + attachmentId + '"]');
        if ($input.length === 1) {
            $input[0].checked = true;
        }
    }

    // Handle click event for the card and using the current attachment item context
    $(".custom-card").click(function (e) {

        e.preventDefault();

        var $inputs = $(".form-check-input:checkbox:checked");
        if ((/false/i).test($inputs.data('is-tagged'))) {

            // An untagged attachment item is selected
            $("#untaggedItemSelectedModal").modal();
        }
    });

}(jQuery));
