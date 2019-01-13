
/* Handle user interactions with the BGP peers grid and modal form for creating/editing BGP peers */

(($) => {

    // The modal containing the BGP peer form

    const $modal                = $('#bgpPeerModal');

    // Input controls of the BGP peer form

    const $bgpPeerId              = $('#BgpPeerId'),
          $ipv4PeerAddressInput   = $('#Ipv4PeerAddress'),
          $peer2ByteAsInput       = $('#Peer2ByteAutonomousSystem'),
          $peerPasswordInput      = $('#PeerPassword'),
          $maximumRoutesInput     = $('#MaximumRoutes'),
          $isBfdEnabledInput      = $('#IsBfdEnabled'),
          $isMultiHopInput        = $('#IsMultiHop'),
          $rowId                  = $('#RowId');

    const bgpPeerId               = $bgpPeerId[0],
          ipv4PeerAddress         = $ipv4PeerAddressInput[0],
          peer2ByteAs             = $peer2ByteAsInput[0],
          peerPassword            = $peerPasswordInput[0],
          maximumRoutes           = $maximumRoutesInput[0],
          isBfdEnabled            = $isBfdEnabledInput[0],
          isMultiHop              = $isMultiHopInput[0],
          rowId                   = $rowId[0];

    const $bgpPeerComponent       = $('#bgpPeersComponent');

    // Bind to click event of button to show the BGP form

    $bgpPeerComponent.on('click', '#showBgpPeerFormBtn', () => {

        $modal.modal('show').off('hidden.bs.modal').on('hidden.bs.modal', () => resetBgpPeerForm() );
    });

    // Handle click of save button to create a new BGP peer

    $('#saveBgpPeer').on('click', (e) => {

        // Check form inputs are valid and then hide the modal form
       
        const $form = $('#bgpPeerForm');
        $form.validate();
        if (!$form.valid()) return;

        $modal.modal('hide');

        // check for an existing row

        if (rowId.value !== null && rowId.value !== "") {

            // Remove the row - a new row with the new form data will be created

            removeRow(rowId.value);  
        }

        // Create a new entry from the form data

        let arr = [];
        arr.push({
            "BgpPeerId"                 : bgpPeerId.value,
            "Ipv4PeerAddress"           : ipv4PeerAddress.value,
            "PeerPassword"              : peerPassword.value,
            "Peer2ByteAutonomousSystem" : peer2ByteAs.value,
            "MaximumRoutes"             : maximumRoutes.value,
            "IsBfdEnabled"              : isBfdEnabled.checked,
            "IsMultiHop"                : isMultiHop.checked
        });

        // Refresh the BGP peer grid

        refreshBgpPeerGrid(arr);
    });

    //Bind to click event of trash buttons in the bgp peer grid rows

    $bgpPeerComponent.on('click', '.mind-grid-delete-row', function (e) {

        // Remove the row

        let removeRowId = $(this).data('row-id');
        removeRow(removeRowId);

        // Refresh the BGP peer grid

        refreshBgpPeerGrid();
    });

    //Bind to click event of edit buttons in the bgp peer grid rows

    $bgpPeerComponent.on('click', '.mind-grid-edit-row', function (e) {

        const editRowId             = $(this).data('row-id');
        const $row                  = $('#bgp-peer-grid-row_' + editRowId);
        const data                  = getRowData($row);

        // Populate the form and then show it

        bgpPeerId.value             = data.BgpPeerId,
        ipv4PeerAddress.value       = data.Ipv4PeerAddress;
        peer2ByteAs.value           = data.Peer2ByteAutonomousSystem;
        peerPassword.value          = data.PeerPassword;
        isBfdEnabled.checked        = (/true/i).test(data.IsBfdEnabled);
        isMultiHop.checked          = (/true/i).test(data.IsMultiHop);
        maximumRoutes.value         = data.MaximumRoutes;
        rowId.value                 = editRowId;

        // Reset form inputs when the modal is hidden (i.e. when the user saves or closes the form)

        $modal.modal('show').off('hidden.bs.modal').on('hidden.bs.modal', (e) => resetBgpPeerForm());
    });

    /* Bind to checkbox change event to set boolen value - this is needed to send correct boolean value
    to the controller on form submit */

    $bgpPeerComponent.on('change', '.mind-grid-checkbox', function (e) {

        this.value = this.checked;
    });

    // Helpers functions

    // Remove a row from the grid

    const removeRow = (rowId) => {

        $('#bgp-peer-grid-row_' + rowId).remove();
    }

    // Refresh the BGP peer grid data

    const refreshBgpPeerGrid = (arr) => {
    
        const bgpPeerData     = getBgpPeerData(arr);
        const data            = JSON.stringify(bgpPeerData);
        const deferred        = $.Deferred();

        Mind.Utilities.showSpinner('Fetching data from the server....');

        $.post({
            url: "GetBgpPeerGridData",
            contentType: 'application/json; charset=utf-8',
            data: data
        })
         .done( (data) => {

                $('#bgp-peer-grid').find('tbody').html(data);
                deferred.resolve();
        })
         .always( () => Mind.Utilities.hideSpinner() );  

        return deferred.promise();
    }

    // Get data from the BGP peer grid

    const getBgpPeerData = (arr) => {

        if (typeof (arr) === "undefined") arr = [];
        const $rows                           = $('#bgp-peer-grid').find('tbody tr');

        $rows.each(function() {

            let data = getRowData($(this));
            arr.push(data);
        });

        return arr;
    }

    // Create data object for a single row

    const getRowData = ($row) => {
   
        const data = {
              "BgpPeerId"                     : $row.data('bgp-peer-id'),
              "Ipv4PeerAddress"               : $row.data('ipv4-peer-address'),
              "PeerPassword"                  : $row.data('peer-password'),
              "Peer2ByteAutonomousSystem"     : $row.data('peer-2-byte-as'),
              "MaximumRoutes"                 : $row.data('maximum-routes'),
              "IsBfdEnabled"                  : $row.data('is-bfd-enabled'),
              "IsMultiHop"                    : $row.data('is-multihop')
        };

        return data;
    }


    // Reset the BGP peer form inputs

    const resetBgpPeerForm = () => {

        bgpPeerId.value         = "";
        ipv4PeerAddress.value   = "";
        peerPassword.value      = "";
        peer2ByteAs.value       = "";
        rowId.value             = "";
        maximumRoutes.value     = $maximumRoutesInput.data('default-value');
        isBfdEnabled.checked    = (/true/i).test($isBfdEnabledInput.data('default-value'));
        isMultiHop.checked      = (/true/i).test($isMultiHopInput.data('default-value'));
    }

})(jQuery);