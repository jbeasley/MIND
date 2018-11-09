
(function ($) {

    var $routingInstance = $('#RoutingInstance'),
        routingInstance = $routingInstance[0],
        $inboundIpNetwork = $('#InboundIpNetwork'),
        inboundIpNetwork = $inboundIpNetwork[0];

    // Handle add/delete of routing instances
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
                RefreshRoutingInstanceGrid(arr);
            }
        }
    });

    //Bind to click event of trash buttons in the routing instance grid rows
    $('#routing-instance-grid').on('click', '.mind-grid-delete-row', function (e) {

        var deleteRowId = $(this).data('row-id');
        var $row = $('#routing-instance-grid-row_' + deleteRowId);
        $row.remove();
        RefreshRoutingInstanceGrid([]);
    });

    // Handle add/delete from BGP IP Network Inbound Policy
    $('#addInboundIpNetwork').on('click', function (e) {

        var $grid = $('#bgp-ip-network-inbound-policy-grid');
        var $tenantId = $('#TenantId'),
            tenantId = $tenantId[0];

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

    //Bind to click event of trash buttons in the ip network inbound policy grid rows
    $('#bgp-ip-network-inbound-policy-grid').on('click', '.mind-grid-delete-row', function (e) {

        var deleteRowId = $(this).data('row-id');
        var $row = $('#bgp-ip-network-inbound-policy-grid-row_' + deleteRowId);
        $row.remove();
        RefreshBgpIpNetworkInboundPolicyGrid([]);
    });

    // Bind to checkbox change event for all grids to set boolen value - this is needed to send correct boolean value
    // to the controller on form submit
    $('.mind-grid').on('change', '.mind-grid-checkbox', function (e) {

        this.value = this.checked;
    });

    // Helpers functions

    // Refresh the routing instance grid data
    function RefreshRoutingInstanceGrid(arr) {

        var $grid = $('#routing-instance-grid');
        var routingInstanceData = GetRoutingInstanceData(arr);

        var $tbody = $grid.find('tbody');
        var data = JSON.stringify(routingInstanceData);

        $.ajax({
            url: "GetAttachmentSetRoutingInstancesGridData",
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            data: data,
            success: function (data) {

                $tbody.html(data);
                RefreshValidation();
            }
        });
    }

    // REfresh the BGP IP network inbound policy grid
    function RefreshBgpIpNetworkInboundPolicyGrid(arr) {

        var $grid = $('#bgp-ip-network-inbound-policy-grid');
        var bgpIpNetworkInboundPolicyData = GetBgpIpNetworkInboundPolicyData(arr);
        var data = JSON.stringify(bgpIpNetworkInboundPolicyData);

        $.ajax({
            url: "GetBgpIpNetworkInboundPolicyGridData",
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            data: data,
            success: function (data) {

                var $tbody = $grid.find('tbody');
                $tbody.html(data);
                RefreshValidation();
            }
        });
    }

    function GetRoutingInstanceData(arr) {

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
    }

    function GetBgpIpNetworkInboundPolicyData(arr) {

        var $grid = $('#bgp-ip-network-inbound-policy-grid');
        var $rows = $grid.find('tbody tr');

        $rows.each(function () {

            var $row = $(this);
            var tenantId = $row.data('tenant-id');
            var cidrName = $row.data('cidr-name');
            var localIpRoutingPreference = $row.data('local-ip-routing-preference');
            var addToAllBgpPeersInAttachmentSet = $row.data('add-to-all-bgp-peers-in-attachment-set');
            var ipv4PeerAddress = $row.data('ipv4-peer-address');

            bgpPolicyArr.push({
                "TenantId": tenantId,
                "TenantIpNetworkCidrName": cidrName,
                "AddToAllBgpPeersInAttachmentSet": addToAllBgpPeersInAttachmentSet,
                "Ipv4PeerAddress": ipv4PeerAddress,
                "LocalIpRoutingPreference": localIpRoutingPreference
            });
        });
    }

    // Re-apply validation to the form so that validation rules for the new inputs are created
    function RefreshValidation() {

        var $form = $('#createForm');
        $form.removeData('validator');
        $form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('#createForm');
    }

}(jQuery));