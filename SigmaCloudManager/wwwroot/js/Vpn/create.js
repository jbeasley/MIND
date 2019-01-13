
/* Setup the 'create vpn' wizard view */

(($) => {

    var $form       = $("#form"),
        $wizard     = $("#createVpnWizard");

    // Create the wizard

    Mind.Utilities.createWizardWithNetworkStageOrSyncModal($wizard, $form, true);
   
    // Initialise all tool-tips

    $('[data-toggle="tooltip"]').tooltip();

})(jQuery);

