
// Handle user interactions for the 'create attachment set' view

(($) => {

    var $form               = $("#form"),
        $wizard             = $("#createAttachmentSetWizard");

    Mind.Utilities.createWizard($wizard, $form);

    var $region             = $('#Region'),
        region              = $region[0],
        $subregion          = $('#SubRegion'),
        subregion           = $subregion[0],
        $routingInstance    = $('#RoutingInstance'),
        routingInstance     = $routingInstance[0],
        $tenantId           = $('#TenantId'),
        tenantId            = $tenantId[0];

    // Initialise all tool-tips

    $('[data-toggle="tooltip"]').tooltip();

    if (region.value === null || region.value === "") {

        subregion.disabled          = true;
        routingInstance.disabled    = true;
    }

    // Handle changes to the region drop-down list

    $region.on('change', (e) => {

        if (region.value === null || region.value === "") {

            subregion.selectedIndex         = 0;
            subregion.disabled              = true;
            routingInstance.selectedIndex   = 0;
            $routingInstance.empty();
            routingInstance.disabled        = true;
        }
        else {

            routingInstance.selectedIndex   = 0;
            routingInstance.disabled        = true;

            Mind.Utilities.populateElement($subregion, "SubRegions", 
            { 
                region: region.value 
            });

            Mind.Utilities.populateElement($routingInstance, "RoutingInstances", 
            { 
                tenantId: tenantId.value, 
                region: region.value 
            });
        }
    });

    // Handle changes to the sub-region drop-down list

    $subregion.on('change', (e) => {

        if (subregion.value === null || subregion.value === "") {

            Mind.Utilities.populateElement($routingInstance, "RoutingInstances", 
            { 
                tenantId: tenantId.value, 
                region: region.value 
            });
        }
        else {

            Mind.Utilities.populateElement($routingInstance, "RoutingInstances", 
            {
                tenantId: tenantId.value, 
                region: region.value, 
                subRegion: subregion.value 
            });
        }
    });

})(jQuery);
