(function ($) {

    var $form = $("#form"),
        $wizard = $("#editVpnWizard");

    // Create the wizard
    Mind.Utilities.createWizardWithNetworkStageOrSyncModal($wizard, $form, true);

    // Initialise all tool-tips
    $('[data-toggle="tooltip"]').tooltip();

}(jQuery));