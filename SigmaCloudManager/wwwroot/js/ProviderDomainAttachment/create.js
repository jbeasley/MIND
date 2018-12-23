
(function ($) {

    var $form = $("#createForm"),
        $wizard = $("#createAttachmentWizard");

    // Create the wizard
    Mind.Utilities.createWizardWithNetworkStageOrSyncModal($wizard, $form, true);

    var $bundleRequired = $('#BundleRequired'),
        bundleRequired = $bundleRequired[0],
        $multiportRequired = $('#MultiportRequired'),
        multiportRequired = $multiportRequired[0],
        $portPool = $('#PortPoolName'),
        portPool = $portPool[0],
        $attachmentRole = $('#AttachmentRoleName'),
        attachmentRole = $attachmentRole[0],
        $attachmentBandwidthGbps = $('#AttachmentBandwidthGbps'),
        attachmentBandwidthGbps = $attachmentBandwidthGbps[0],
        $ipAddressingComponent = $("#ipAddressingComponent"),
        $contractBandwidthPoolComponent = $("#contractBandwidthPoolComponent"),
        $bgpPeersComponent = $('#bgpPeersComponent'),
        $locationSelector = $('#locationSelector');

    $attachmentBandwidthGbps.on('change', function (e) {

        attachmentRole.selectedIndex = 0;
        attachmentRole.disabled = true;
        portPool.selectedIndex = 0;
        $ipAddressingComponent.fadeOut().empty();
        $contractBandwidthPoolComponent.fadeOut().empty();

        if (attachmentBandwidthGbps.value === null || attachmentBandwidthGbps.value === "") {

            portPool.disabled = true;
        }
        else {

            portPool.disabled = false;
        }
    });

    $portPool.on('change', function (e) {

        $ipAddressingComponent.fadeOut().empty();
        $contractBandwidthPoolComponent.fadeOut().empty();
        var portPoolVal = this.value;
        if (portPoolVal === null || portPoolVal === "") {

            attachmentRole.selectedIndex = 0;
            attachmentRole.disabled = true;
        }
        else {

            Mind.Utilities.populateElement($attachmentRole, "AttachmentRoles", {
                portPoolName: portPoolVal
            });
        }
    });

    $attachmentRole.on('change', function (e) {

        var attachmentRoleName = this.value;
        if (attachmentRoleName !== null) {

            GetIpAddressingComponent();
            GetContractBandwidthPoolComponent();
            GetBgpPeersComponent();
        }
    });

    if (attachmentBandwidthGbps.value === null || attachmentBandwidthGbps.value === "") {

        portPool.disabled = true;
    }

    if (portPool.value === null || portPool.value === "") {

        attachmentRole.disabled = true;
    }

    $locationSelector.on('change', '#RegionId', function (e) {

        var regionId = this.value;
        if (regionId !== null) {
       
            Mind.Utilities.populateElement($locationSelector, "GetLocationSelectorComponent", { regionId: regionId }, InitToolTipsAndValidation);
        }
    });

    $locationSelector.on('change', '#SubRegionId', function (e) {

        var subRegionId = this.value;
        var region = $('#RegionId');
        Mind.Utilities.populateElement($locationSelector, "GetLocationSelectorComponent", { regionId: region[0].value, subRegionId: subRegionId }, InitToolTipsAndValidation);
    });

    $multiportRequired.on('click', function (e) {

        bundleRequired.checked = false;
        Mind.Utilities.populateElement($attachmentBandwidthGbps, "AttachmentBandwidths", { multiportRequired: multiportRequired.checked });
    });

    $bundleRequired.on('click', function (e) {

        multiportRequired.checked = false;
        Mind.Utilities.populateElement($attachmentBandwidthGbps, "AttachmentBandwidths", { bundleRequired: bundleRequired.checked });
    });

    function GetIpAddressingComponent() {

        $ipAddressingComponent.fadeOut().empty();

        $.get("GetIpAddressingComponent", {
            portPoolName: portPool.value,
            attachmentRoleName: attachmentRole.value,
            attachmentBandwidthGbps: attachmentBandwidthGbps.value,
            isMultiport: multiportRequired.checked
        })
            .done(function (data) {

                $ipAddressingComponent.html(data).fadeIn();
                InitToolTipsAndValidation();
            });
    }

    function GetContractBandwidthPoolComponent() {

        $contractBandwidthPoolComponent.fadeOut().empty();

        $.get("GetContractBandwidthPoolComponent", {
            portPoolName: portPool.value,
            attachmentRoleName: attachmentRole.value,
            attachmentBandwidthGbps: attachmentBandwidthGbps.value
        })
            .done(function (data) {

                $contractBandwidthPoolComponent.html(data).fadeIn();
                InitToolTipsAndValidation();
            });
    }

    function GetBgpPeersComponent() {

        $bgpPeersComponent.fadeOut().empty();

        $.get("GetBgpPeersComponent", {
            portPoolName: portPool.value,
            attachmentRoleName: attachmentRole.value,
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
