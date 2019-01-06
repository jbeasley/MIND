
// Script for the 'Create' view for Infrastructure Devices
(($) => {

   const $form = $("#createForm"),
         $wizard = $("#createInfrastructureDeviceWizard");

    // Create the wizard
    Mind.Utilities.createWizard($wizard, $form, true);

    const $locationSelector = $('#locationSelector');

    // Handle change of region selection
    $locationSelector.on('change', '#RegionId', function (e) {

        // Load the list of sub-regions
        if (this.value !== null) {
        
            Mind.Utilities.populateElement($locationSelector, "GetLocationSelectorComponent", 
            { 
                regionId: regionId 
            }, 
            initToolTipsAndValidation);
        }
    });

    // Handle change of sub-region selection
    $locationSelector.on('change', '#SubRegionId', function (e) {

        // Load the list of locations
        var $region = $('#RegionId');

        Mind.Utilities.populateElement($locationSelector, "GetLocationSelectorComponent", 
        { 
            regionId: $region[0].value, 
            subRegionId: this.value 
        }, 
        initToolTipsAndValidation);
    });

    function initToolTipsAndValidation() {

        // Initialise new tool-tips
        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialise unobtrusive validation on the form
        $form.removeData("validator")
             .removeData("unobtrusiveValidation");

        $.validator.unobtrusive.parse($form);
    }

})(jQuery);

