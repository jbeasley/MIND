﻿
// Script for the 'Create' view for Tenant Domain Devices
(function ($) {

    var $form = $("#createForm"),
        $wizard = $("#createTenantDomainDeviceWizard");

    // Create the wizard
    Mind.Utilities.createWizard($wizard, $form, true);

    var $locationSelector = $('#locationSelector');

    // Handle change of region selection
    $locationSelector.on('change', '#RegionId', function (e) {

        // Load the list of sub-regions
        var regionId = this.value;

        if (regionId !== null) {
        
            Mind.Utilities.populateElement($locationSelector, "GetLocationSelectorComponent", { regionId: regionId }, InitToolTipsAndValidation);
        }
    });

    // Handle change of sub-region selection
    $locationSelector.on('change', '#SubRegionId', function (e) {

        // Load the list of locations
        var $region = $('#RegionId');
        var subRegionId = this.value;

        Mind.Utilities.populateElement($locationSelector, "GetLocationSelectorComponent", { regionId: $region[0].value, subRegionId: subRegionId }, InitToolTipsAndValidation);
    });
    
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

