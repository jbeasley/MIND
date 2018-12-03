(function ($) {

    // Initialise all tool-tips
    $('[data-toggle="tooltip"]').tooltip();

    // Set the checked state of the checkbox for an item in the table from stored value
    // in the cookie (if present)
    var vpnId = Mind.Utilities.getCookie("vpnId");
    if (vpnId !== "") {

        var $input = $('.form-check-input[data-query-parameter = "vpnId=' + vpnId + '"]');
        if ($input.length === 1) {
            $input[0].checked = true;
        }
    }

}(jQuery));
