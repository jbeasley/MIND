
(function ($) {

    var $form = $("#createForm"),
        $wizard = $("#createVifWizard");

    // Create the wizard
    Mind.Utilities.createWizardWithNetworkStageOrSyncModal($wizard, $form, true);

    var $attachmentId = $('#AttachmentId'),
        attachmentId = $attachmentId[0],
        $vifRole = $('#VifRoleName'),
        vifRole = $vifRole[0],
        $ipAddressingComponent = $("#ipAddressingComponent"),
        $contractBandwidthPoolComponent = $("#contractBandwidthPoolComponent"),
        $bgpPeersComponent = $('#bgpPeersComponent');

    $vifRole.on('change', function (e) {

        var vifRoleName = this.value;
        if (vifRoleName !== null) {

            GetIpAddressingComponent();
            GetContractBandwidthPoolComponent();
            GetBgpPeersComponent();
        }
    });

    function GetIpAddressingComponent() {

        $ipAddressingComponent.fadeOut().empty();

        $.get("GetIpAddressingComponent", {
            attachmentId: attachmentId.value,
            vifRoleName: vifRole.value
        })
            .done(function (data) {

                $ipAddressingComponent.html(data).fadeIn();
                InitToolTipsAndValidation();
            });
    }

    function GetContractBandwidthPoolComponent() {

        $contractBandwidthPoolComponent.fadeOut().empty();

        $.get("GetContractBandwidthPoolComponent", {
            attachmentId: attachmentId.value,
            vifRoleName: vifRole.value
        })
            .done(function (data) {

                $contractBandwidthPoolComponent.html(data).fadeIn();
                InitToolTipsAndValidation();
            });
    }

    function GetBgpPeersComponent() {

        $bgpPeersComponent.fadeOut().empty();

        $.get("GetBgpPeersComponent", {
            attachmentId: attachmentId.value,
            vifRoleName: vifRole.value,
        })
            .done(function (data) {

                $bgpPeersComponent.html(data).fadeIn();
                InitToolTipsAndValidation();
            });
    }

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