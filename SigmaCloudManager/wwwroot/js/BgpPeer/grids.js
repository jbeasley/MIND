
(function ($) {

    // The modal containing the BGP peer form
    var $modal = $('#bgpPeerModal');

    // The actual form
    var $form = $('#bgpPeerForm');

    // Input controls of the BGP peer form
    var $bgpPeerId = $('#BgpPeerId'),
        $ipv4PeerAddressInput = $('#Ipv4PeerAddress'),
        $peer2ByteAsInput = $('#Peer2ByteAutonomousSystem'),
        $peerPasswordInput = $('#PeerPassword'),
        $maximumRoutesInput = $('#MaximumRoutes'),
        $isBfdEnabledInput = $('#IsBfdEnabled'),
        $isMultiHopInput = $('#IsMultiHop'),
        $rowId = $('#RowId');

    var bgpPeerId = $bgpPeerId[0],
        ipv4PeerAddress = $ipv4PeerAddressInput[0],
        peer2ByteAs = $peer2ByteAsInput[0],
        peerPassword = $peerPasswordInput[0],
        maximumRoutes = $maximumRoutesInput[0],
        isBfdEnabled = $isBfdEnabledInput[0],
        isMultiHop = $isMultiHopInput[0],
        rowId = $rowId[0];

    var $bgpPeerComponent = $('#bgpPeersComponent');

    // Bind to click event of button to show the BGP form
    $bgpPeerComponent.on('click', '#showBgpPeerFormBtn', function () {

        $modal.modal('show').off('hidden.bs.modal').on('hidden.bs.modal', function () {

            ResetBgpPeerForm();
        });
    });

    // Handle click of save button to create a new BGP peer
    $('#saveBgpPeer').on('click', function (e) {

        // Check form inputs are valid and then hide the modal form
        $form.validate();
        if (!$form.valid()) return;

        $modal.modal('hide');

        // check for an existing row
        if (rowId.value !== null && rowId.value !== "") {

            // Remove the row - a new row with the new form data will be created
            RemoveRow(rowId.value);  
        }

        // Create a new entry from the form data
        var arr = [];
        arr.push({
            "BgpPeerId": bgpPeerId.value,
            "Ipv4PeerAddress": ipv4PeerAddress.value,
            "PeerPassword": peerPassword.value,
            "Peer2ByteAutonomousSystem": peer2ByteAs.value,
            "MaximumRoutes": maximumRoutes.value,
            "IsBfdEnabled": isBfdEnabled.checked,
            "IsMultiHop": isMultiHop.checked
        });

        // Refresh the BGP peer grid
        RefreshBgpPeerGrid(arr);
    });

    //Bind to click event of trash buttons in the bgp peer grid rows
    $bgpPeerComponent.on('click', '.mind-grid-delete-row', function (e) {

        // Remove the row
        var removeRowId = $(this).data('row-id');
        RemoveRow(removeRowId);

        // Refresh the BGP peer grid
        RefreshBgpPeerGrid();
    });

    //Bind to click event of edit buttons in the bgp peer grid rows
    $bgpPeerComponent.on('click', '.mind-grid-edit-row', function (e) {

        var editRowId = $(this).data('row-id');
        var $row = $('#bgp-peer-grid-row_' + editRowId);
        var data = GetRowData($row);

        // Populate the form and then show it
        bgpPeerId.value = data.BgpPeerId,
        ipv4PeerAddress.value = data.Ipv4PeerAddress;
        peer2ByteAs.value = data.Peer2ByteAutonomousSystem;
        peerPassword.value = data.PeerPassword;
        isBfdEnabled.checked = (/true/i).test(data.IsBfdEnabled);
        isMultiHop.checked = (/true/i).test(data.IsMultiHop);
        maximumRoutes.value = data.MaximumRoutes;
        rowId.value = editRowId;

        $modal.modal('show').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {

            // Reset form inputs when the modal is hidden (i.e. when the user saves or closes the form)
            ResetBgpPeerForm();
        });
    });

    // Bind to checkbox change event to set boolen value - this is needed to send correct boolean value
    // to the controller on form submit
    $bgpPeerComponent.on('change', '.mind-grid-checkbox', function (e) {

        this.value = this.checked;
    });

    // Helpers functions

    // Remove a row from the grid
    function RemoveRow(rowId) {

        var $row = $('#bgp-peer-grid-row_' + rowId);
        $row.remove();
    }

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


    // Reset the BGP peer form inputs
    function ResetBgpPeerForm() {

        bgpPeerId.value = "";
        ipv4PeerAddress.value = "";
        peerPassword.value = "";
        peer2ByteAs.value = "";
        rowId.value = "";
        maximumRoutes.value = $maximumRoutesInput.data('default-value');
        isBfdEnabled.checked = (/true/i).test($isBfdEnabledInput.data('default-value'));
        isMultiHop.checked = (/true/i).test($isMultiHopInput.data('default-value'));
    }

}(jQuery));