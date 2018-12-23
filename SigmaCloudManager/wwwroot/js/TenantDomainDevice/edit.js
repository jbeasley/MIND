(function ($) {

    var $form = $("#editForm"),
        $wizard = $("#editTenantDomainDeviceWizard");

    // Create the wizard
    Mind.Utilities.createWizard($wizard, $form, true);

    // Initialise all tool-tips
    $('[data-toggle="tooltip"]').tooltip();

}(jQuery));