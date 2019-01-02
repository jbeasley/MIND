(function ($) {

    var $form = $("#editForm"),
        $wizard = $("#editTenantDomainDeviceWizard"),
        $portFormContainer = $('#portFormContainer');

    // Create the wizard
    Mind.Utilities.createWizard($wizard, $form, true);

    // Handle change of port role selection
    $portFormContainer.on('change', '#PortRole', function (e) {

        var $portProfileSelector = $('#portProfileSelector');
        // Load the list of port pools related to the selected port role
        Mind.Utilities.populateElement($portProfileSelector, "GetPortProfileComponent", { portRole: this.value }, InitToolTipsAndValidation);
    });

    function InitToolTipsAndValidation() {

        // Initialise new tool-tips
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialise unobtrusive validation on the form
        var $form = $('#createForm')
            .removeData("validator")
            .removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($form);
    }

}(jQuery));