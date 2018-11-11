
(function ($) {

    var $routingInstance = $('#RoutingInstance'),
        routingInstance = $routingInstance[0];

    // Handle addition of routing instances
    $('#addRoutingInstance').on('click', function (e) {

        var $grid = $('#routing-instance-grid');

        if (routingInstance.value !== null && routingInstance.value !== "") {

            var $selected = $routingInstance.find(":selected");
            var routingInstanceName = $selected.data('name');

            var exists = $grid
                .find('td > input[type="text"]')
                .filter(function () {
                    return this.value === routingInstanceName;
                })
                .length > 0;

            if (exists) {

                // The routing instance already exists in the table
                $("#duplicateItemModal").modal();
            }
            else {

                var arr = [];
                arr.push({
                    "RoutingInstanceName": routingInstanceName
                });

                // Refresh the routing instances grid, then the BGP IP network inbound policy
                // grid must be refreshed in order to refresh the BGP peer dropdown list options
                RefreshRoutingInstanceGrid(arr)
                    .then(function () {
                        RefreshBgpIpNetworkInboundPolicyGrid();
                });
            }
        }
    });

    //Bind to click event of trash buttons in the routing instance grid rows
    $('#routing-instance-grid').on('click', '.mind-grid-delete-row', function (e) {

        var deleteRowId = $(this).data('row-id');
        var $row = $('#routing-instance-grid-row_' + deleteRowId);
        $row.remove();

        // Refresh the routing instances grid, then the BGP IP network inbound and outbound policy
        // grids must be refreshed in order to refresh the BGP peer dropdown list options
        RefreshRoutingInstanceGrid()
            .then(function () {
                RefreshBgpIpNetworkInboundPolicyGrid();
                RefreshBgpIpNetworkOutboundPolicyGrid();
            });
    });

    // Handle add/delete from BGP IP Network Inbound Policy
    $('#addInboundIpNetwork').on('click', function (e) {

        var $grid = $('#bgp-ip-network-inbound-policy-grid');
        var $tenantId = $('#TenantId'),
            tenantId = $tenantId[0],
            $inboundIpNetwork = $('#InboundIpNetwork'),
            inboundIpNetwork = $inboundIpNetwork[0];

        if (inboundIpNetwork.value !== null && inboundIpNetwork.value !== "") {

            var $selected = $inboundIpNetwork.find(":selected");
            var cidrName = $selected.data('cidr-name');

            var exists = $grid
                .find('td > input[type="text"]')
                .filter(function () {
                    return this.value === cidrName;
                })
                .length > 0;

            if (exists) {

                // The IP network already exists in the table
                $("#duplicateItemModal").modal();
            }
            else {

                var arr = [];
                arr.push({
                    "TenantId": tenantId.value,
                    "TenantIpNetworkCidrName": cidrName
                });

                RefreshBgpIpNetworkInboundPolicyGrid(arr);
            }
        }
    });

    // Handle add/delete from BGP IP Network Outbound Policy
    $('#addOutboundIpNetwork').on('click', function (e) {

        var $grid = $('#bgp-ip-network-outbound-policy-grid');
        var $remoteTenantId = $('#RemoteTenantId'),
            remoteTenantId = $remoteTenantId[0],
            $outboundIpNetwork = $('#OutboundIpNetwork'),
            outboundIpNetwork = $outboundIpNetwork[0];

        if (outboundIpNetwork.value !== null && outboundIpNetwork.value !== "") {

            var $selected = $outboundIpNetwork.find(":selected");
            var cidrName = $selected.data('cidr-name');
            var tenantName = $selected.data('tenant-name');

            var exists = $grid
                .find('td > input[type="text"]')
                .filter(function () {
                    return this.value === cidrName;
                })
                .length > 0;

            if (exists) {

                // The IP network already exists in the table
                $("#duplicateItemModal").modal();
            }
            else {

                var arr = [];
                arr.push({
                    "TenantId": remoteTenantId.value,
                    "TenantName": tenantName,
                    "TenantIpNetworkCidrName": cidrName
                });

                RefreshBgpIpNetworkOutboundPolicyGrid(arr);
            }
        }
    });

    //Bind to click event of trash buttons in the ip network inbound policy grid rows
    $('#bgp-ip-network-inbound-policy-grid').on('click', '.mind-grid-delete-row', function (e) {

        var deleteRowId = $(this).data('row-id');
        var $row = $('#bgp-ip-network-inbound-policy-grid-row_' + deleteRowId);
        $row.remove();
        RefreshBgpIpNetworkInboundPolicyGrid();
    });

    //Bind to click event of trash buttons in the ip network outbound policy grid rows
    $('#bgp-ip-network-outbound-policy-grid').on('click', '.mind-grid-delete-row', function (e) {

        var deleteRowId = $(this).data('row-id');
        var $row = $('#bgp-ip-network-outbound-policy-grid-row_' + deleteRowId);
        $row.remove();
        RefreshBgpIpNetworkOutboundPolicyGrid();
    });

    // Bind to checkbox change event for all grids to set boolen value - this is needed to send correct boolean value
    // to the controller on form submit
    $('.mind-grid').on('change', '.mind-grid-checkbox', function (e) {

        this.value = this.checked;
    });

    // Populate a list of tenant IP networks which can be added to the BGP IP network outbound policy when a 
    // tenant is selected
    var $remoteTenantId = $('#RemoteTenantId'),
        remoteTenantId = $remoteTenantId[0],
        $outboundIpNetwork = $('#OutboundIpNetwork'),
        outboundIpNetwork = $outboundIpNetwork[0];

    outboundIpNetwork.disabled = true;

    $remoteTenantId.on('change', function (e) {

        if (remoteTenantId.value === null || remoteTenantId.value === "") {

            outboundIpNetwork.selectedIndex = 0;
            outboundIpNetwork.disabled = true;
        }
        else {

            Mind.Utilities.populateElement($outboundIpNetwork, "TenantIpNetworks", { tenantId: remoteTenantId.value });
        }
    });

    // Helpers functions

    // Refresh the routing instance grid data
    function RefreshRoutingInstanceGrid(arr) {

        var $grid = $('#routing-instance-grid');
        var routingInstanceData = GetRoutingInstanceData(arr);

        var $tbody = $grid.find('tbody');
        var data = JSON.stringify(routingInstanceData);
        var deferred = $.Deferred();

        $.ajax({
            url: "GetAttachmentSetRoutingInstancesGridData",
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            data: data,
            success: function (data) {

                $tbody.html(data);
                RefreshValidation();

                deferred.resolve();
            }
        });

        return deferred.promise();
    }

    // Refresh the BGP IP network inbound policy grid
    function RefreshBgpIpNetworkInboundPolicyGrid(arr) {

        var $grid = $('#bgp-ip-network-inbound-policy-grid');
        var bgpIpNetworkInboundPolicyData = GetBgpIpNetworkInboundPolicyData(arr);
        var data = JSON.stringify(bgpIpNetworkInboundPolicyData);
        var deferred = $.Deferred();

        $.ajax({
            url: "GetBgpIpNetworkInboundPolicyGridData",
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            data: data,
            success: function (data) {

                var $tbody = $grid.find('tbody');
                $tbody.html(data);
                RefreshValidation();

                deferred.resolve();
            }
        });

        return deferred.promise();
    }

    // Refresh the BGP IP network outbound policy grid
    function RefreshBgpIpNetworkOutboundPolicyGrid(arr) {

        var $grid = $('#bgp-ip-network-outbound-policy-grid');
        var bgpIpNetworkOutboundPolicyData = GetBgpIpNetworkOutboundPolicyData(arr);
        var data = JSON.stringify(bgpIpNetworkOutboundPolicyData);
        var deferred = $.Deferred();

        $.ajax({
            url: "GetBgpIpNetworkOutboundPolicyGridData",
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            data: data,
            success: function (data) {

                var $tbody = $grid.find('tbody');
                $tbody.html(data);
                RefreshValidation();

                deferred.resolve();
            }
        });

        return deferred.promise();
    }

    function GetRoutingInstanceData(arr) {

        if (typeof (arr) === "undefined") arr = [];
        var $grid = $('#routing-instance-grid');
        var $rows = $grid.find('tbody tr');

        $rows.each(function () {

            var $row = $(this);
            var routingInstanceName = $row.data('name');
            var localIpRoutingPreference = $row.data('local-ip-routing-preference');
            var advertisedIpRoutingPreference = $row.data('advertised-ip-routing-preference');
            arr.push({
                "RoutingInstanceName": routingInstanceName,
                "LocalIpRoutingPreference": localIpRoutingPreference,
                "AdvertisedIpRoutingPreference": advertisedIpRoutingPreference
            });
        });

        return arr;
    }

    function GetBgpIpNetworkInboundPolicyData(arr) {

        if (typeof (arr) === "undefined") arr = [];
        var $ipNetworkInboundPolicyGridRows = $('#bgp-ip-network-inbound-policy-grid').find('tbody tr');

        $ipNetworkInboundPolicyGridRows.each(function () {

            var $row = $(this);
            var tenantId = $row.data('tenant-id');
            var cidrName = $row.data('cidr-name');
            var localIpRoutingPreference = $row.data('local-ip-routing-preference');
            var addToAllBgpPeersInAttachmentSet = $row.data('add-to-all-bgp-peers-in-attachment-set');
            var ipv4PeerAddress = $row.data('ipv4-peer-address');

            arr.push({
                "TenantId": tenantId,
                "TenantIpNetworkCidrName": cidrName,
                "AddToAllBgpPeersInAttachmentSet": addToAllBgpPeersInAttachmentSet,
                "Ipv4PeerAddress": ipv4PeerAddress,
                "LocalIpRoutingPreference": localIpRoutingPreference
            });
        });

        var routingInstanceData = GetRoutingInstanceData();
        var routingInstanceNames = routingInstanceData.map(function (item) { return item.RoutingInstanceName });

        return {
            "RoutingInstanceNames": routingInstanceNames,
            "VpnTenantIpNetworkInRequests": arr
        };
    }

    function GetBgpIpNetworkOutboundPolicyData(arr) {

        if (typeof (arr) === "undefined") arr = [];
        var $ipNetworkOutboundPolicyGridRows = $('#bgp-ip-network-outbound-policy-grid').find('tbody tr');

        $ipNetworkOutboundPolicyGridRows.each(function () {

            var $row = $(this);
            var tenantId = $row.data('tenant-id');
            var tenantName = $row.data('tenant-name');
            var cidrName = $row.data('cidr-name');
            var advertisedIpRoutingPreference = $row.data('advertised-ip-routing-preference');
            var addToAllBgpPeersInAttachmentSet = $row.data('add-to-all-bgp-peers-in-attachment-set');
            var ipv4PeerAddress = $row.data('ipv4-peer-address');

            arr.push({
                "TenantId": tenantId,
                "TenantName": tenantName,
                "TenantIpNetworkCidrName": cidrName,
                "AddToAllBgpPeersInAttachmentSet": addToAllBgpPeersInAttachmentSet,
                "Ipv4PeerAddress": ipv4PeerAddress,
                "AdvertisdIpRoutingPreference": advertisedIpRoutingPreference
            });
        });

        var routingInstanceData = GetRoutingInstanceData();
        // We only need to send the routing instance names to the server
        var routingInstanceNames = routingInstanceData.map(function (item) { return item.RoutingInstanceName });

        return {
            "RoutingInstanceNames": routingInstanceNames,
            "VpnTenantIpNetworkOutRequests": arr
        };
    }

    // Re-apply validation to the form so that validation rules for the new inputs are created
    function RefreshValidation() {

        var $form = $('#createForm');
        $form.removeData('validator');
        $form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('#createForm');
    }

}(jQuery));