
(function ($) {

    // The modal containing the port form
    var $modal = $('#portModal');

    // Form for creating a new port
    var $form = $('#portForm');

    // Input controls of the port form
    var $portId = $('#PortId'),
        $portTypeInput = $('#PortType'),
        $portNameInput = $('#PortName'),
        $portBandwidthGbpsInput = $('#PortBandwidthGbps'),
        $portConnectorInput = $('#PortConnector'),
        $portSfpInput = $('#PortSfp'),
        $portStatusInput = $('#PortStatus'),
        $portRole = $('#PortRole'),
        $portPool = $('#PortPool'),
        $tenantId = $('#AssignedTenantId'),
        $rowId = $('#RowId');

    var portId = $portId[0],
        portType = $portTypeInput[0],
        portName = $portNameInput[0],
        portBandwidthGbps = $portBandwidthGbpsInput[0],
        portConnector = $portConnectorInput[0],
        portSfp = $portSfpInput[0],
        portStatus = $portStatusInput[0],
        portRole = $portRole[0],
        portPool = $portPool[0],
        tenantId = $tenantId[0],
        rowId = $rowId[0];

    var $portComponent = $('#portsComponent');

    $modal.on('show.bs.modal', function () {  

             // Make sure the first tab (for the port type and name inputs) is shown
             $('#pills-type-name-tab').tab('show');
          })
          .on('hidden.bs.modal', function () {

             // Reset the form controls ready for the next port to be created
             ResetPortForm();
          });

    // Bind to click event of button to show the port form
    $portComponent.on('click', '#showPortFormBtn', function () {

        // Reset the form when its hidden (i.e. when the save or close button is clicked
        $modal.modal('show');
    });

    // Bind to the form invalid event and switch the tab to display the erros
    $form.bind("invalid-form.validate", function () {

        var validator = $form.data("validator");
        // Find the tab containing the first error in the validator and show the tab
        $('#pills-tab a[href="#' + $(validator.errorList[0].element).closest(".tab-pane").attr('id') + '"]').tab('show');
    });

    // Handle click of the save button on the form
    $('#savePort').on('click', function (e) {

        // Change the 'ignore' setting in the validator so that hidden elements are validated
        var validator = $form.data("validator");
        validator.settings.ignore = "";

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
            "PortId": portId.value,
            "PortType": portType.value,
            "PortName": portName.value,
            "PortBandwidthGbps": portBandwidthGbps.value,
            "PortConnector": portConnector.value,
            "PortSfp": portSfp.value,
            "PortStatus": portStatus.value,
            "PortPool": portPool.value,
            "PortRole": portRole.value,
            "TenantId": tenantId.value,
            "IsNew": true
        });

        // Refresh the port grid
        RefreshPortGrid(arr)
        .done(function() {
            HighlightNewRows();
        });
    });

    //Bind to click event of delete action button
    $portComponent.on('click', '.mind-grid-delete-row', function (e) {

        // Remove the row
        var removeRowId = $(this).data('row-id');
        RemoveRow(removeRowId);

        // Refresh the port grid
        RefreshPortGrid();
    });

    //Bind to click event of edit action button
    $portComponent.on('click', '.mind-grid-edit-row', function (e) {

        var editRowId = $(this).data('row-id');
        var $row = $('#port-grid-row_' + editRowId);
        var data = GetRowData($row);

        // Populate the form and then show it
        portId.value = data.PortId,
        portType.value = data.PortType;
        portName.value = data.PortName;
        portBandwidthGbps.value = data.PortBandwidthGbps;
        portConnector.value = data.PortConnector;
        portSfp.value = data.PortSfp
        portStatus.value = data.PortStatus;
        portPool.value  = data.PortPool;
        portRole.value = data.PortRole;
        tenantId.value = data.TenantId;
        rowId.value = editRowId;

        $modal.modal('show').off('hidden.bs.modal').on('hidden.bs.modal', function (e) {

            // Reset form inputs when the modal is hidden (i.e. when the user saves or closes the form)
            ResetPortForm();
        });
    });

    //Bind to click event of copy action button
    $portComponent.on('click', '.mind-grid-copy-row', function (e) {

        // Copy the row
        var rowId = $(this).data('row-id');
        var $row = $('#port-grid-row_' + rowId);
        var data = GetRowData($row);
        data.PortId = "";

        // Setting the IsNew property on the data allows us to highlight the row using CSS after the grid
        // is refreshed
        data.IsNew = true;

        var arr = [];
        arr.push(data);

        // Refresh the port grid
        RefreshPortGrid(arr)
        .done(function() {
            HighlightNewRows();
        });
    });

    // Helpers functions

    // Remove a row from the grid
    function RemoveRow(rowId) {

        var $row = $('#port-grid-row_' + rowId);
        $row.remove();
    }

    // Refresh the port grid data
    function RefreshPortGrid(arr) {

        var $grid = $('#port-grid');
        var portData = GetPortData(arr);

        var $tbody = $grid.find('tbody');
        var data = JSON.stringify(portData);
        var deferred = $.Deferred();

        $.ajax({
            url: "GetPortsGridData",
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

    // Get data from the port grid
    function GetPortData(arr) {

        if (typeof (arr) === "undefined") arr = [];
        var $grid = $('#port-grid');
        var $rows = $grid.find('tbody tr');

        $rows.each(function () {

            var data = GetRowData($(this));
            arr.push(data);
        });

        return arr;
    }

    // Create data object for a single row
    function GetRowData($row) {

        var portId = $row.data('port-id'),
            portType = $row.data('port-type'),
            portName = $row.data('port-name'),
            portBandwidthGbps = $row.data('port-bandwidth-gbps'),
            portConnector = $row.data('port-connector'),
            portStatus = $row.data('port-status'),
            portSfp = $row.data('port-sfp'),
            portRole = $row.data('port-role'),
            portPool = $row.data('port-pool'),
            tenantId = $row.data('tenant-id');

        var data = {
            "PortId": portId,
            "PortType": portType,
            "PortName": portName,
            "PortBandwidthGbps": portBandwidthGbps,
            "PortConnector": portConnector,
            "PortSfp": portSfp,
            "PortStatus": portStatus,
            "PortRole": portRole,
            "PortPool": portPool,
            "TenantId": tenantId
        };

        return data;
    }


    // Reset the port form inputs
    function ResetPortForm() {

        portId.value = "";
        portType.value = "";
        portName.value = "";
        portBandwidthGbps.selectedIndex = 0;
        rowId.value = "";
        portConnector.selectedIndex = 0;
        portSfp.selectedIndex = 0;
        portStatus.selectedIndex = 0;
        portRole.selectedIndex = 0;
        portPool.selectedIndex = 0;
        tenantId.value = "";
    }

    // Highlight new rows added to the grid
    function HighlightNewRows() {
    
        $('#port-grid > tbody > tr')
        .filter(function() {
                return (/true/i).test($(this).data('is-new'));
        })
        .addClass('mind-grid-highlight-row');           
    }   

}(jQuery));