
/* Helper script for first displaying a wizard and then showing a modal form allowing the user to stage or 
   sync a resource with the network when the finish button of the wizard is clicked. */

(($) => {

    const createWizardWithNetworkStageOrSyncModal = ($wizard, $form, showStageOrSyncModalOnFinish) => {

        /* Show the wizard, optionally instructing the wizard plugin to show the stage or sync form when the
           user finishes the wizard */

        Mind.Utilities.createWizard($wizard, $form, showStageOrSyncModalOnFinish ? () => { showStageOrSyncModal($form) } : null);
    }  

    /// Show the stage or sync modal form

    const showStageOrSyncModal = ($form) => {

        const $stageOrSyncModal = $('#stageOrSyncModal');

         if ($stageOrSyncModal.length > 0) {

            const $stageResource          = $('#stageResource'),
                  $syncResourceToNetwork  = $('#syncResourceToNetwork');

            $stageOrSyncModal.modal('show');

            /* Allow stage OR sync to network checkboxes to be checked */

            if ($stageResource.length > 0 && $syncResourceToNetwork.length > 0) {

                $stageResource.on('click',         () => { $syncResourceToNetwork[0].checked = false; });                             
                $syncResourceToNetwork.on('click', () => { $stageResource[0].checked = false; });
            }

            /*  When the save button of the form is clicked, save checkbox values of the form
                to elements on the page if they exist. This allows the values of the checkboxes to 
                be posted to the server */

            $('#stageOrSyncModalSaveBtn').one('click', () => {
        
                const $stage          = $('#Stage'),
                      $syncToNetwork  = $('#SyncToNetwork');

                if ($stageResource.length > 0 && $stage.length > 0) {
    
                    $stage[0].value = $stageResource[0].checked;
                }

                if ($syncResourceToNetwork.length > 0 && $syncToNetwork.length > 0) {

                    $syncToNetwork[0].value = $syncResourceToNetwork[0].checked;
                }
        
                $stageOrSyncModal.modal('hide');

                Mind.Utilities.showSpinner();

                $form.submit();
    
            });
        }
    }

    $.extend(Mind.Utilities, {

        createWizardWithNetworkStageOrSyncModal: createWizardWithNetworkStageOrSyncModal
    });  

})(jQuery);