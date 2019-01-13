(($) => {

    // Set the checked state of the checkbox for an item in the table from stored value
    // in the cookie (if present)

    var attachmentId = Mind.Utilities.getCookie("provider-domain-attachment-id");
    if (attachmentId !== "") {

        var $input = $('.form-check-input[data-query-parameter = "attachmentId=' + attachmentId + '"]');
        if ($input.length === 1) {
            $input[0].checked = true;
        }
    }

    // Handle click event for the card and using the current attachment item context

    $(".custom-card").click(function (e) {

        e.preventDefault();

        var $inputs = $(".form-check-input:checkbox:checked"),
            $this = $(this),
            action = $this.data('action');

        // Ignore the card click if an action is defined - the action is processed by the 'cardSelectionHelper.js' script

        if (action === undefined) 
        {
            var isTagged = $inputs.data('is-tagged');
            if ((/false/i).test(isTagged)) {

                // Prevent any other events handlers defined in 'cardSelectionHelper.js' from being processed 

                e.stopImmediatePropagation();

                // An untagged attachment item is selected

                $("#untaggedItemSelectedModal").modal();
            }
        }
    });

})(jQuery);
