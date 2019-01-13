
/* Handle user interactions with the create/edit attachment set wizard view */

(($) => {

    // Handle addition of routing instances

    $('#addRoutingInstance').on('click', function (e) {

        const $grid             = $('#routing-instance-grid');
        const $routingInstance  = $('#RoutingInstance');

        if ($routingInstance.val() !== "") {
        
            const routingInstanceName = $routingInstance.find(":selected").data('name');

            const exists = $grid
                    .find('td > input[type="text"]')
                    .filter(function () {
                        return this.value === routingInstanceName;
                    })
                    .length > 0;

            if (exists) {

                Mind.Utilities.showDuplicateItemDialog('Duplicate Routing Instance','The routing instance already exists');
            }

            else {

                let arr = [];

                arr.push({ "RoutingInstanceName": routingInstanceName });

                /* Refresh the routing instances grid, then the BGP IP network inbound and outbound policy
                grids must be refreshed in order to refresh the BGP peer dropdown list options */

                refreshRoutingInstanceGrid(arr)
                    .then(() => { refreshBgpIpNetworkInboundPolicyGrid(); refreshBgpIpNetworkOutboundPolicyGrid() });
            }
        }
    });

    //Bind to click event of trash buttons in the routing instance grid rows

    $('#routing-instance-grid').on('click', '.mind-grid-delete-row', function (e) {

        const deleteRowId = $(this).data('row-id');
        const $row = $('#routing-instance-grid-row_' + deleteRowId);
       
        $row.remove();

        /* Refresh the routing instances grid, then the BGP IP network inbound and outbound policy
        grids must be refreshed in order to refresh the BGP peer dropdown list options */

        refreshRoutingInstanceGrid().then(() => { RefreshBgpIpNetworkInboundPolicyGrid(); RefreshBgpIpNetworkOutboundPolicyGrid(); });
    });

    // Handle add/delete from BGP IP Network Inbound Policy

    $('#addInboundIpNetwork').on('click', function (e) {

        const $grid                 = $('#bgp-ip-network-inbound-policy-grid');
        const $inboundIpNetwork     = $('#InboundIpNetwork');

        if ($inboundIpNetwork.val() !== "") {
        
            const cidrName = $inboundIpNetwork.find(":selected").data('cidr-name');

            const exists = $grid
                    .find('td > input[type="text"]')
                    .filter(function () {
                        return this.value === cidrName;
                    })
                    .length > 0;

            if (exists) {

                Mind.Utilities.showDuplicateItemDialog('Duplicate IP Network','The IP network already exists');
            }

            else {

                let arr = [];
               
                arr.push({
                    "TenantId"                      : $('#TenantId').val(),
                    "TenantIpNetworkCidrName"       : cidrName
                });

                refreshBgpIpNetworkInboundPolicyGrid(arr);
            }
        }
    });

    // Handle add/delete from BGP IP Network Outbound Policy

    $('#addOutboundIpNetwork').on('click', function (e) {

        const $grid                 = $('#bgp-ip-network-outbound-policy-grid');
        const $outboundIpNetwork    = $('#OutboundIpNetwork');

        if ($outboundIpNetwork.val() !== "") {

            const $selected         = $outboundIpNetwork.find(":selected");
            const cidrName          = $selected.data('cidr-name');
            const tenantName        = $selected.data('tenant-name');

            const exists = $grid
                    .find('td > input[type="text"]')
                    .filter(function () {
                        return this.value === cidrName;
                    })
                    .length > 0;

            if (exists) {

                // The IP network already exists in the table

                Mind.Utilities.showDuplicateItemDialog('Duplicate IP Network','The IP network already exists');
            }

            else {

                let arr = [];
                
                arr.push({

                    "TenantId"                  : $('#RemoteTenantId').val(),
                    "TenantName"                : tenantName,
                    "TenantIpNetworkCidrName"   : cidrName
                });

                refreshBgpIpNetworkOutboundPolicyGrid(arr);
            }
        }
    });

    //Bind to click event of trash buttons in the ip network inbound policy grid rows

    $('#bgp-ip-network-inbound-policy-grid').on('click', '.mind-grid-delete-row', function () {

        const deleteRowId   = $(this).data('row-id');
        const $row          = $('#bgp-ip-network-inbound-policy-grid-row_' + deleteRowId);

        $row.remove();
        RefreshBgpIpNetworkInboundPolicyGrid();
    });

    //Bind to click event of trash buttons in the ip network outbound policy grid rows

    $('#bgp-ip-network-outbound-policy-grid').on('click', '.mind-grid-delete-row', function () {

        const deleteRowId   = $(this).data('row-id');
        const $row          = $('#bgp-ip-network-outbound-policy-grid-row_' + deleteRowId);

        $row.remove();
        RefreshBgpIpNetworkOutboundPolicyGrid();
    });

    /* Bind to checkbox change event for all grids to set boolen value - this is needed to send correct boolean value
    to the controller on form submit */

    $('.mind-grid').on('change', '.mind-grid-checkbox', function (e) {

        this.value = this.checked;
    });

    /* Populate a list of tenant IP networks which can be added to the BGP IP network outbound policy when a 
    tenant is selected */

    const $remoteTenantId       = $('#RemoteTenantId'),
          $outboundIpNetwork    = $('#OutboundIpNetwork');

    $outboundIpNetwork[0].disabled  = true;

    $remoteTenantId.on('change', () => {

        if ($remoteTenantId.val() === "") {

            $outboundIpNetwork[0].selectedIndex     = 0;
            outboundIpNetwork[0].disabled          = true;
        }

        else {

            Mind.Utilities.populateElement($outboundIpNetwork, "TenantIpNetworks", 
            { 
                tenantId: $remoteTenantId.val()
            });
        }
    });

    // Helpers

    // Refresh the routing instance grid data

    function refreshRoutingInstanceGrid(arr) {
    
        const routingInstanceData   = getRoutingInstanceData(arr);
        const data                  = JSON.stringify(routingInstanceData);
        const deferred              = $.Deferred();

        Mind.Utilities.showSpinner('Getting data from the server....');

        $.post({
            contentType: 'application/json; charset=utf-8',
            url: "GetAttachmentSetRoutingInstancesGridData",
            data: data
        })
        .done((data) => {

            $('#routing-instance-grid').find('tbody').html(data);
            refreshValidation();

            deferred.resolve();
        })
        .always( () => Mind.Utilities.hideSpinner() );
         
        return deferred.promise();
    }

    // Refresh the BGP IP network inbound policy grid

    function refreshBgpIpNetworkInboundPolicyGrid(arr) {
    
        const bgpIpNetworkInboundPolicyData     = getBgpIpNetworkInboundPolicyData(arr);
        const data                              = JSON.stringify(bgpIpNetworkInboundPolicyData);
        const deferred                          = $.Deferred();

        Mind.Utilities.showSpinner('Getting data from the server....');

        $.post({
            url: "GetBgpIpNetworkInboundPolicyGridData",
            contentType: 'application/json; charset=utf-8',
            data: data
         })
         .done( (data) => {
            
                $('#bgp-ip-network-inbound-policy-grid').find('tbody').html(data);
                refreshValidation();

                deferred.resolve();
         })
         .always( () => Mind.Utilities.hideSpinner() );

        return deferred.promise();
    }

    // Refresh the BGP IP network outbound policy grid

    function refreshBgpIpNetworkOutboundPolicyGrid(arr) {
    
        const bgpIpNetworkOutboundPolicyData    = getBgpIpNetworkOutboundPolicyData(arr);
        const data                              = JSON.stringify(bgpIpNetworkOutboundPolicyData);
        const deferred                          = $.Deferred();

        Mind.Utilities.showSpinner('Getting data from the server....');

        $.post({
            url: "GetBgpIpNetworkOutboundPolicyGridData",
            contentType: 'application/json; charset=utf-8',
            data: data
        })
         .done( (data) => {

                $('#bgp-ip-network-outbound-policy-grid').find('tbody').html(data);
                refreshValidation();

                deferred.resolve();
        })
         .always( () => Mind.Utilities.hideSpinner() );

        return deferred.promise();
    }

    function getRoutingInstanceData(arr) {

        if (typeof (arr) === "undefined") arr = [];
        var $grid                             = $('#routing-instance-grid');
        var $rows                             = $grid.find('tbody tr');

        $rows.each(function () {

            const $row                            = $(this);

            arr.push({
                "RoutingInstanceName"               : $row.data('name'),
                "LocalIpRoutingPreference"          : $row.data('local-ip-routing-preference'),
                "AdvertisedIpRoutingPreference"     : $row.data('advertised-ip-routing-preference')
            });
        });

        return arr;
    }

    function getBgpIpNetworkInboundPolicyData(arr) {

        if (typeof (arr) === "undefined") arr = [];
        var $ipNetworkInboundPolicyGridRows   = $('#bgp-ip-network-inbound-policy-grid').find('tbody tr');

        $ipNetworkInboundPolicyGridRows.each(function () {

            const $row                              = $(this);
            
            arr.push({
                "TenantId"                          : $row.data('tenant-id'),
                "TenantIpNetworkCidrName"           : $row.data('cidr-name'),
                "AddToAllBgpPeersInAttachmentSet"   : $row.data('add-to-all-bgp-peers-in-attachment-set'),
                "Ipv4PeerAddress"                   : $row.data('ipv4-peer-address'),
                "LocalIpRoutingPreference"          : $row.data('local-ip-routing-preference')
            });
        });

        const routingInstanceData     = getRoutingInstanceData();
        const routingInstanceNames    = routingInstanceData.map((item) => { return item.RoutingInstanceName });

        return {

            "RoutingInstanceNames"          : routingInstanceNames,
            "VpnTenantIpNetworkInRequests"  : arr
        };
    }

    function getBgpIpNetworkOutboundPolicyData(arr) {

        if (typeof (arr) === "undefined") arr   = [];

        const $ipNetworkOutboundPolicyGridRows  = $('#bgp-ip-network-outbound-policy-grid').find('tbody tr');

        $ipNetworkOutboundPolicyGridRows.each(function () {

            const $row                              = $(this);
            
            arr.push({
                "TenantId"                          : $row.data('tenant-id'),
                "TenantName"                        : $row.data('tenant-name'),
                "TenantIpNetworkCidrName"           : $row.data('cidr-name'),
                "AddToAllBgpPeersInAttachmentSet"   : $row.data('add-to-all-bgp-peers-in-attachment-set'),
                "Ipv4PeerAddress"                   : $row.data('ipv4-peer-address'),
                "AdvertisdIpRoutingPreference"      : $row.data('advertised-ip-routing-preference')
            });
        });

        var routingInstanceData = getRoutingInstanceData();

        // We only need to send the routing instance names to the server

        var routingInstanceNames = routingInstanceData.map((item) => { return item.RoutingInstanceName });

        return {
            "RoutingInstanceNames"                  : routingInstanceNames,
            "VpnTenantIpNetworkOutRequests"         : arr
        };
    }

    // Re-apply validation to the form so that validation rules for the new inputs are created

    function refreshValidation() {

        var $form = $('#form');
        $form.removeData('validator');
        $form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('#form');
    }

})(jQuery);