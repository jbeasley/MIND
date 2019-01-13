﻿

// Handle user interactions for generic 'create vif' views

(($) => {

    const $form = $('#form');

    // Create the wizard
   
    Mind.Utilities.createWizardWithNetworkStageOrSyncModal( $('#vifWizard'), $form, true);
    
    const $attachmentRole                   = $('#AttachmentRoleName');
    const $vifRole                          = $('#VifRoleName');
        
    const getIpAddressingComponent = () => {
    
        Mind.Utilities.populateElement( $('#ipAddressingComponent'), 'GetIpAddressingComponent', {

            attachmentId    : $('#AttachmentId').val(),
            vifRoleName     : $vifRole.val()
        },
        () => {
            
            initToolTipsAndValidation();
        });
    }

    const getContractBandwidthPoolComponent = () => {

        Mind.Utilities.populateElement( $('#contractBandwidthPoolComponent'), 'GetContractBandwidthPoolComponent', {

            attachmentRoleName  : $attachmentRole.val(),
            vifRoleName         : $vifRole.val()
        },
        () => {
            
            initToolTipsAndValidation();
        });
    }

    const getBgpPeersComponent = () => {
    
        Mind.Utilities.populateElement( $('#bgpPeersComponent'), 'GetBgpPeersComponent', {

            attachmentRoleName  : $attachmentRole.val(),
            vifRoleName         : $vifRole.val(),
        },
        () => {
          
             initToolTipsAndValidation();
        });
    }

    const initToolTipsAndValidation = () => {

        // Initialise new tool-tips

        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialise unobtrusive validation on the form

        $form.removeData("validator")
             .removeData("unobtrusiveValidation");

        $.validator.unobtrusive.parse($form);
    }

    // Main

    $vifRole.on('change', function (e) {
    
        if (this.value !== null) {

            getIpAddressingComponent();
            getContractBandwidthPoolComponent();
            getBgpPeersComponent();
        }
    });

})(jQuery);