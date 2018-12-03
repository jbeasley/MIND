 (function ($) {

    var $form = $("#createForm"),
        $wizard = $("#createVpnWizard");

    // Create the wizard
    Mind.Utilities.createWizardWithNetworkStageOrSyncModal($wizard, $form, true);

    var $protocolType = $('#ProtocolType'),
        protocolType = $protocolType[0],
        $addressFamily = $('#AddressFamily'),
        addressFamily = $addressFamily[0],
        $topologyType = $('#TopologyType'),
        topologyType = $topologyType[0];

    // Handle changes to the protocol type dropdown list selection
    $protocolType.on('change', function (e) {

        if (this.value === null || this.value === "") {

            addressFamily.selectedIndex = 0;
            addressFamily.disabled = true;
            topologyType.selectedIndex = 0;
            topologyType.disabled = true;
        }
        else {

            Mind.Utilities.populateElement($addressFamily, "AddressFamilies", { protocolType: this.value });
            Mind.Utilities.populateElement($topologyType, "TopologyTypes", { protocolType: this.value });
        }
    });

    if (protocolType.value === null || protocolType.value === "") {

        addressFamily.selectedIndex = 0;
        addressFamily.disabled = true;
        topologyType.selectedIndex = 0;
        topologyType.disabled = true;
    }

    // Initialise all tool-tips
    $('[data-toggle="tooltip"]').tooltip();

}(jQuery));

