
// Handle user interactions for generic 'edit vif' views

(($) => {

    // Determine whether the sync or stage modal form should be shown when the wizard finsihes

    const showStageOrSyncModal      = (/true/i).test($('showStageOrSyncModal').val());

    // Create the wizard

    Mind.Utilities.createWizardWithNetworkStageOrSyncModal( $('#vifWizard'), $('#form'), showStageOrSyncModal);

    const $routingInstance              = $('#ExistingRoutingInstanceName');
    const $createNewRoutingInstance     = $('#CreateNewRoutingInstance');

    if ($createNewRoutingInstance.length > 0) {
    
        if ($createNewRoutingInstance[0].checked) {

            if ($routinginstance.length > 0) {

                const routingInstance           = $routingInstance[0];
                routingInstance.selectedIndex   = 0;
                routingInstance.disabled        = true;
            }
    
        }

        $createNewRoutingInstance.on('click', function () {

            if ($routinginstance.length > 0) {

                const routingInstance = $routingInstance[0];

                if (this.checked) {

                    routingInstance.selectedIndex   = 0;
                    routingInstance.disabled        = true;
                }
                else {

                    routingInstance.disabled = false;
                }
            }
        });
    }

})(jQuery);