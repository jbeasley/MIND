
(function ($) {

    // The modal containing the BGP peer form
    var $modal = $('#createBgpPeerModal');

    // The actual form
    var $form = $('#createBgpPeerForm');

    // Input controls of the BGP peer form
    var $ipv4PeerAddressInput = $('#Ipv4PeerAddress'),
        $peer2ByteAsInput = $('#Peer2ByteAutonomousSystem'),
        $peerPasswordInput = $('#PeerPassword'),
        $maximumRoutesInput = $('#MaximumRoutes'),
        $isBfdEnabledInput = $('#IsBfdEnabled'),
        $isMultiHopInput = $('#IsMultiHop');

    var ipv4PeerAddress = $ipv4PeerAddressInput[0],
        peer2ByteAs = $peer2ByteAsInput[0],
        peerPassword = $peerPasswordInput[0],
        maximumRoutes = $maximumRoutesInput[0],
        isBfdEnabled = $isBfdEnabledInput[0],
        isMultiHop = $isMultiHopInput[0];

    // Handle click of save button to create a new BGP peer
    $('#saveBgpPeer').on('click', function (e) {

        // Check form inputs are valid and then hide the modal form
        $form.validate();
        if (!$form.valid()) return;

        $modal.modal('hide');

        // Get data from the form to send to the server
        var $grid = $('#bgp-peer-grid');

            var exists = $grid
                .find('td > input[type="text"]')
                .filter(function () {
                    return this.value === ipv4PeerAddress.value;
                })
                .length > 0;

        if (exists) {

            // The BGP peer already exists in the table
            $("#duplicateItemModal").modal();
        }
        else {

            var arr = [];
            arr.push({
                "Ipv4PeerAddress": ipv4PeerAddress.value,
                "PeerPassword": peerPassword.value,
                "Peer2ByteAutonomousSystem": peer2ByteAs.value,
                "MaximumRoutes": maximumRoutes.value,
                "IsBfdEnabled": isBfdEnabled.checked,
                "IsMultiHop": isMultiHop.checked
            });

            // Refresh the BGP peer grid
            RefreshBgpPeerGrid(arr);

            // Reset form inputs
            ipv4PeerAddress.value = "";
            peerPassword.value = "";
            peer2ByteAs.value = "";
            maximumRoutes.value = $maximumRoutesInput.data('default-value');
            isBfdEnabled.checked = $isBfdEnabledInput.data('default-value');
            isMultiHop.checked = $isMultiHopInput.data('default-value');
        }
    });

    //Bind to click event of trash buttons in the bgp peer grid rows
    $('#bgp-peer-grid').on('click', '.mind-grid-delete-row', function (e) {

        var deleteRowId = $(this).data('row-id');
        var $row = $('#bgp-peer-grid-row_' + deleteRowId);
        $row.remove();

        // Refresh the BGP peer grid
        RefreshBgpPeerGrid();
    });

    //Bind to click event of edit buttons in the bgp peer grid rows
    $('#bgp-peer-grid').on('click', '.mind-grid-edit-row', function (e) {

        var editRowId = $(this).data('row-id');
        var $row = $('#bgp-peer-grid-row_' + editRowId);
        var data = GetRowData($row);

        // Populate the form and then show it
        ipv4PeerAddress.value = data.Ipv4PeerAddress;
        peer2ByteAs.value = data.Peer2ByteAutonomousSystem;
        peerPassword.value = data.PeerPassword;
        isBfdEnabled.checked = data.IsBfdEnabled;
        isMultiHop.checked = data.IsMultiHop;
        maximumRoutes.value = data.MaximumRoutes;

        $modal.modal('show');

    });

    // Bind to checkbox change event for all grids to set boolen value - this is needed to send correct boolean value
    // to the controller on form submit
    $('.mind-grid').on('change', '.mind-grid-checkbox', function (e) {

        this.value = this.checked;
    });

    // Helpers functions

    // Refresh the BGP peer grid data
    function RefreshBgpPeerGrid(arr) {

        var $grid = $('#bgp-peer-grid');
        var bgpPeerData = GetBgpPeerData(arr);

        var $tbody = $grid.find('tbody');
        var data = JSON.stringify(bgpPeerData);
        var deferred = $.Deferred();

        $.ajax({
            url: "GetBgpPeerGridData",
            contentType: 'application/json; charset=utf-8',
            type: 'POST',
            data: data,
            success: function (data) {

                $tbody.html(data);
                deferred.resolve();
            }
        });

        return deferred.promise();
    }

    // Get data from the BGP peer grid
    function GetBgpPeerData(arr) {

        if (typeof (arr) === "undefined") arr = [];
        var $grid = $('#bgp-peer-grid');
        var $rows = $grid.find('tbody tr');

        $rows.each(function () {

            var data = GetRowData($(this));
            arr.push(data);
        });

        return arr;
    }

    // Create data object for a single row
    function GetRowData($row) {

        var bgpPeerId = $row.data('bgp-peer-id'),
            ipv4PeerAddress = $row.data('ipv4-peer-address'),
            peerPassword = $row.data('peer-password'),
            peer2ByteAs = $row.data('peer-2-byte-as'),
            maximumRoutes = $row.data('maximum-routes'),
            isBfdEnabled = $row.data('is-bfd-enabled'),
            isMultiHop = $row.data('is-multihop');

        var data = {
            "BgpPeerId": bgpPeerId,
            "Ipv4PeerAddress": ipv4PeerAddress,
            "PeerPassword": peerPassword,
            "Peer2ByteAutonomousSystem": peer2ByteAs,
            "MaximumRoutes": maximumRoutes,
            "IsBfdEnabled": isBfdEnabled,
            "IsMultiHop": isMultiHop
        };

        return data;
    }

}(jQuery));