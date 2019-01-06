
(($) => {

    const $form = $("#createForm"),
          $wizard = $("#createVifWizard");

    // Create the wizard
    Mind.Utilities.createWizardWithNetworkStageOrSyncModal($wizard, $form, true);

    const $attachmentId = $('#AttachmentId'),
          attachmentId = $attachmentId[0],
          $vifRole = $('#VifRoleName'),
          vifRole = $vifRole[0],
          $ipAddressingComponent = $("#ipAddressingComponent"),
          $contractBandwidthPoolComponent = $("#contractBandwidthPoolComponent"),
          $bgpPeersComponent = $('#bgpPeersComponent');
        
    const getIpAddressingComponent = () => {
    
        Mind.Utilities.populateElement($ipAddressingComponent, "GetIpAddressingComponent", {
            attachmentId: attachmentId.value,
            vifRoleName: vifRole.value
        },
        (data) => {

            $ipAddressingComponent.html(data).fadeIn();
            initToolTipsAndValidation();
        });
    }

    const getContractBandwidthPoolComponent = () => {

        Mind.Utilities.populateElemen($contractBandwidthPoolComponent, "GetContractBandwidthPoolComponent", {
            attachmentId: attachmentId.value,
            vifRoleName: vifRole.value
        },
        (data) => {

            $contractBandwidthPoolComponent.html(data);
            initToolTipsAndValidation();
        });
    }

    const getBgpPeersComponent = () => {
    
        Mind.Utilities.populateElement($bgpPeersComponent, "GetBgpPeersComponent", {
            attachmentId: attachmentId.value,
            vifRoleName: vifRole.value,
        },
        (data) => {

             $bgpPeersComponent.html(data);
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