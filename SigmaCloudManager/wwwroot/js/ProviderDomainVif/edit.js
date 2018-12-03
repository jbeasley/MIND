
(function ($) {

    var $form = $("#editForm"),
        $wizard = $("#editVifWizard");

    // Create the wizard
    Mind.Utilities.createWizardWithNetworkStageOrSyncModal($wizard, $form, true);

    var $routingInstance = $('#ExistingRoutingInstanceName'),
        $createNewRoutingInstance = $('#CreateNewRoutingInstance');

    if ($createNewRoutingInstance.length > 0) {
    
        if ($createNewRoutingInstance[0].checked) {

            if ($routinginstance.length > 0) {

                var routingInstance = $routingInstance[0];
                routingInstance.selectedIndex = 0;
                routingInstance.disabled = true;
            }
    
        }

        $createNewRoutingInstance.on('click', function () {

            if ($routinginstance.length > 0) {

                var routingInstance = $routingInstance[0];

                if (this.checked) {

                    routingInstance.selectedIndex = 0;
                    routingInstance.disabled = true;
                }
                else {

                    routingInstance.disabled = false;
                }
            }
        });
    }

}(jQuery));