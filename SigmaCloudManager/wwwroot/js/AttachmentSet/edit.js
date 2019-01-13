
// Handle user interactions for the 'edit attachment set' view

(($) => {

    var $form       = $("#form"),
        $wizard     = $("#editAttachmentSetWizard");

    // Initialise the wizard

    Mind.Utilities.createWizard($wizard, $form);

    var $region     = $('#Region'),
        region      = $region[0],
        $subregion  = $('#SubRegion'),
        subregion   = $subregion[0],
        $tenantId   = $('#TenantId'),
        tenantId    = $tenantId[0];
        
    // Initialise all tool-tips

    $('[data-toggle="tooltip"]').tooltip();

    // Handle changes to the sub-region drop-down list

    $subregion.on('change', () => {

        if (subregion.value !== null) {

            Mind.Utilities.populateElement($routingInstance, "RoutingInstances", 
            { 
                tenantId: tenantId.value, 
                region: region.value, 
                subRegion: subregion.value 
            });
        }
    });

})(jQuery);
