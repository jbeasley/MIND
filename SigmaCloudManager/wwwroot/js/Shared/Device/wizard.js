
// Handle user interactions for generic create and edit 'Device' views.

(($) => {

    const $form       = $('#form');
    const $wizard     = $('#deviceWizard');

    // Create the wizard

    Mind.Utilities.createWizard($wizard, $form, true);

    var $locationSelector       = $('#locationSelector');
    var $portFormContainer      = $('#portFormContainer');

    // Handle change of region selection

    $locationSelector.on('change', '#RegionId', function (e) {

        // Load the list of sub-regions

        var regionId = this.value;

        if (regionId !== null) {
        
            Mind.Utilities.populateElement($locationSelector, 'GetLocationSelectorComponent', 
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
        var subRegionId = this.value;

        Mind.Utilities.populateElement($locationSelector, 'GetLocationSelectorComponent', 
        { 
            regionId: $region[0].value, 
            subRegionId: subRegionId 
        }, 
        initToolTipsAndValidation);
    });

    // Handle change of port role selection

    $portFormContainer.on('change', '#PortRole', function (e) {

        var $portProfileSelector = $('#portProfileSelector');

        // Load the list of port pools related to the selected port role

        Mind.Utilities.populateElement($portProfileSelector, 'GetPortProfileComponent', 
        { 
            portRole: this.value
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

