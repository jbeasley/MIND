
// Handle user interactions for generic 'edit vif' views

(($) => {

    // Create the wizard

    Mind.Utilities.createWizardWithNetworkStageOrSyncModal( $('#vifWizard'), $('#form'), true);

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