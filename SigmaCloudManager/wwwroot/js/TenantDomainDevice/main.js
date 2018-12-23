(function ($) {

    // Initialise all tool-tips
    $('[data-toggle="tooltip"]').tooltip();

    // Set the checked state of the checkbox for an item in the table from stored value
    // in the cookie (if present)
    var tenantDomainDeviceId = Mind.Utilities.getCookie("tenantDomainDeviceId");
    if (tenantDomainDeviceId !== "") {

        var $input = $('.form-check-input[data-query-parameter = "tenantDomainDeviceId=' + tenantDomainDeviceId + '"]');
        if ($input.length === 1) {
            $input[0].checked = true;
        }
    }

}(jQuery));
