
/* Manages a grid of device ports, along with a modal form allowing 
   the user to create or edit ports. */

(($) => {

    // The container for the port form
    const $portFormContainer    = $('#portFormContainer');

    // Container for the ports grid
    const $portComponent        = $('#portsComponent');
    
    // Define variables for caching the form and modal elements
    let $form;
    let $modal;

    // Define variables for caching the input elements of the port form
    let $portId,
        $portType,
        $portName,
        $portBandwidthGbps,
        $portConnector,
        $portSfp,
        $portStatus,
        $portPool,
        $portRole,
        $assignedTenantId;       

    /* Bind to click event of the button to create a new port.
       The port form is loaded from the server and then shown */

    const bindCreatePortButtonClickEvent = () => {

        $portComponent.on('click', '#showPortFormBtn', () => {

            getPortForm().done(() => { onPortFormLoaded(); $modal.modal('show')});
        });
    }

    // Bind to click event of the 'sync from server' grid button.

    const bindSyncPortsGridFromServerButtonClickEvent = () => {

        $portComponent.on('click', '#syncPortsGridFromServerBtn', () => {

            syncPortsGridFromServer();
        });
    }

    // Called after the port form is loaded - set things up ready to take input from the user

    const onPortFormLoaded = () => {
    
       $form = $('#portForm');
       $modal = $('#portModal');

       cacheFormInputElements();

       // Initialise unobtrusive validation on the form

       $.validator.unobtrusive.parse($form);

       // Change the 'ignore' setting in the validator so that hidden elements are validated

       const validator = $form.data("validator");
       validator.settings.ignore = "";

       // Bind to various events fired from the form and the modal elements

       bindModalShowHideEvents();
       bindInvalidFormEvent();
       bindFormSaveButtonClickEvent();
    }

    // Handle changes to the port form port role selection

    const bindPortRoleChangeEvent = () => {

        $portFormContainer.on('change', '#PortRole',function (e) {

            const $portProfileSelector = $('#portProfileSelector');

            // Load the list of port pools related to the selected port role

            Mind.Utilities.populateElement($portProfileSelector, "GetPortProfileComponent", { portRole: this.value });
        });
    }

    // Handle show/hide events of the modal form

    const bindModalShowHideEvents = () => {

        // Empty the port form container element when the form is closed - this resets the container
        // ready for the next time the form is loaded and displayed

        $modal.on('hidden.bs.modal', () =>  $portFormContainer.empty());
    }

    // Bind to the save button click event of the form

    const bindFormSaveButtonClickEvent = () => {
        
        $('#savePort').on('click', (e) => {

            // Check form inputs are valid and then hide the modal form

            $form.validate();
            if (!$form.valid()) return;
            
            // Check if the form was for data for an existing grid row

            const rowId = $('#RowId')[0].value;
            if (rowId !== null && rowId !== "") {

                // Remove the row - a new row with the new form data will be created

                removeRow(rowId);  
            }

            // Make sure we have cached all the form elements we need

            cacheFormInputElements();

            // Create a new port record to send to the server from the form data

            let arr = [];
            arr.push({
                "PortId"            : $portId[0].value,
                "PortType"          : $portType[0].value,
                "PortName"          : $portName[0].value,
                "PortBandwidthGbps" : $portBandwidthGbps[0].value,
                "PortConnector"     : $portConnector[0].value,
                "PortSfp"           : $portSfp[0].value,
                "PortStatus"        : $portStatus[0].value,
                "PortPool"          : $portPool[0].value,
                "PortRole"          : $portRole[0].value,
                "TenantId"          : $assignedTenantId[0].value,

                /* Setting 'isNew' causes the row to be highlighted after the data is 
                   returned from the server */
                "IsNew": true
            });

            // Got the data - we can now hide and destroy the form

            $modal.modal('hide');

            // Refresh the port grid, and highlight the new row

            refreshPortsGrid(arr).done(() => highlightNewRows());
        });
    }

    // Bind to invalid form event
    const bindInvalidFormEvent = () => {

        // Bind to the form invalid event and switch the tab to display the errors
        $form.bind("invalid-form.validate", () => {

            const validator = $form.data("validator");

            // Find the tab containing the first error in the validator and show the tab

            $('#pills-tab a[href="#' + $(validator.errorList[0].element)
                                        .closest(".tab-pane")
                                        .attr('id') 
                                     + '"]')
                                        .tab('show');
        });
    }

    //Bind to click event of the grid row delete action button

    const bindGridDeleteActionButtonClick = () => {
    
        $portComponent.on('click', '.mind-grid-delete-row', function (e) {

            // Hide tooltips to prevent any from dangling after the row is removed

            hideToolTips();

            // Remove the row

            const removeRowId = $(this).data('row-id');
            removeRow(removeRowId);

            // Refresh the port grid

            refreshPortsGrid();
        });
    }

    //Bind to click event of the grid row edit action button

    const bindGridEditActionButtonClick = () => {
    
        $portComponent.on('click', '.mind-grid-edit-row', function (e) {

            const editRowId   = $(this).data('row-id');
            const $row        = $('#port-grid-row_' + editRowId);
            const data        = getRowData($row);

            // Get form from the server and then show it

            getPortForm(data).done(() => {

                onPortFormLoaded();

                // Remember the row being edited by saving the row number in a form element

                $('#RowId')[0].value = editRowId;
                    
                $modal.modal('show');
            });        
        });
    }

    //Bind to click event of the grid row copy action button

    const bindGridCopyActionButtonClick = () => {

        $portComponent.on('click', '.mind-grid-copy-row', function (e) {

            // Copy the row

            const rowId     = $(this).data('row-id');
            const $row      = $('#port-grid-row_' + rowId);
            const data      = getRowData($row);

            // Clear/modify the values we don't want copied to the new port

            data.PortId             = "";
            data.TenantId           = "";
            data.TenantName         = "";
            data.AttachmentName     = "";
            data.PortStatus         = "Free";

            /* Setting the IsNew property on the data allows us to highlight the row using CSS after the grid
            is refreshed */

            data.IsNew              = true;
            let arr                 = [];

            arr.push(data);

            // Refresh the port grid

            refreshPortsGrid(arr).done(() => highlightNewRows());
        });
    }

    // Helpers functions

    // Cache the input elements of the port form

    const cacheFormInputElements = () => { 

        $portId                 = $('#PortId'),
        $portType               = $('#PortType'),
        $portName               = $('#PortName'),
        $portBandwidthGbps      = $('#PortBandwidthGbps'),
        $portConnector          = $('#PortConnector'),
        $portSfp                = $('#PortSfp'),
        $portStatus             = $('#PortStatus'),
        $portPool               = $('#PortPool'),
        $portRole               = $('#PortRole'),
        $assignedTenantId       = $('#AssignedTenantId');
    } 

    // Remove a row from the grid

    function removeRow(rowId) {

        const $row = $('#port-grid-row_' + rowId);
        $row.remove();
    }

    // Refresh the ports grid data

    const refreshPortsGrid = (arr) => {

        const $grid       = $('#port-grid');
        const $tbody      = $grid.find('tbody');
        const portData    = getPortData(arr);

        const data        = JSON.stringify({ Ports: portData });
        const deferred    = $.Deferred();

        Mind.Utilities.showSpinner("Loading data from the server....", () => {

            $.post({
                url: "GetPortsGridData",
                contentType: 'application/json; charset=utf-8',
                type: 'POST',
                data: data
            })
            .done((data) => {
            
                $tbody.html(data);
                initToolTips();

                deferred.resolve();               
            })
            .always(() => {         

                Mind.Utilities.hideSpinner();
            });            
        });

       return deferred.promise();
    }

    // Sync ports grid data from the server. Sync from the server is only performed if
    // the device was previously saved, in which case an ID for the device exists.

    const syncPortsGridFromServer = () => {
    
        const $deviceId     = $('#DeviceId');
        const deviceId      = $deviceId.length > 0 ? $deviceId[0].value : null;
        const deferred      = $.Deferred();

        if (deviceId != null) {

            const $grid     = $('#port-grid');
            const $tbody    = $grid.find('tbody');
            const data      = JSON.stringify({ "DeviceId": deviceId });            

            Mind.Utilities.showSpinner("Loading data from the server....", () => {

                $.post({
                    url: "GetPortsGridData",
                    contentType: 'application/json; charset=utf-8',
                    data: data
                 })
                 .done((data) => {
                    
                    $tbody.html(data);
                    initToolTips();

                    deferred.resolve();
                 })
                 .always(() => {

                     Mind.Utilities.hideSpinner();
                 });                
            });
        }

        return deferred.promise();
    }

    // Get the port form from the server, optionally populated 
    // with data for a port.

    const getPortForm = (portData) => {

        const deferred = $.Deferred();

        Mind.Utilities.showSpinner("Loading data from the server....");

        $.post({
            url: "GetPortForm",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(portData)
        })
        .done((data) => {

            $portFormContainer.empty().html(data);
            deferred.resolve();
        })
        .always(() => {

            Mind.Utilities.hideSpinner();
        });

        return deferred.promise();
    }

    // Get data for populating the port grid

    const getPortData = (arr) => {

        if (typeof (arr) === "undefined") arr = [];

        const $grid     = $('#port-grid');
        const $rows     = $grid.find('tbody tr');

        $rows.each(function () {

            const data = getRowData($(this));
            arr.push(data);
        });

        return arr;
    }

    // Create data object for a single row

    const getRowData = ($row) => {

       const    portId              = $row.data('port-id'),
                portType            = $row.data('port-type'),
                portName            = $row.data('port-name'),
                portBandwidthGbps   = $row.data('port-bandwidth-gbps'),
                portConnector       = $row.data('port-connector'),
                portStatus          = $row.data('port-status'),
                portSfp             = $row.data('port-sfp'),
                portRole            = $row.data('port-role'),
                portPool            = $row.data('port-pool'),
                tenantId            = $row.data('tenant-id');
                tenantName          = $row.data('tenant-name');
                attachmentName      = $row.data('attachment-name');

        const   data = {
                    "PortId"            : portId,
                    "PortType"          : portType,
                    "PortName"          : portName,
                    "PortBandwidthGbps" : portBandwidthGbps,
                    "PortConnector"     : portConnector,
                    "PortSfp"           : portSfp,
                    "PortStatus"        : portStatus,
                    "PortRole"          : portRole,
                    "PortPool"          : portPool,
                    "TenantId"          : tenantId,
                    "TenantName"        : tenantName,
                    "AttachmentName"    : attachmentName
                };

        return data;
    }
    
    // Highlight new rows added to the grid
    const highlightNewRows = () => {
    
        $('#port-grid > tbody > tr')
            .filter(function () { return (/true/i).test($(this).data('is-new'))})
            .addClass('mind-grid-highlight-row');           
    }   

    // Initialise new tool-tips

    const initToolTips = () => $('[data-toggle="tooltip"]').tooltip();

    // Hide tool-tips

    const hideToolTips = () => $('[data-toggle="tooltip"]').tooltip('hide');
    
    // Main

    // Bind handlers to various grid action button click event

    bindGridEditActionButtonClick();
    bindGridDeleteActionButtonClick();
    bindGridCopyActionButtonClick();

    // Handlers for create and server sync button click events

    bindCreatePortButtonClickEvent();
    bindSyncPortsGridFromServerButtonClickEvent();

    // Handler for changes made to the port form's port role drop-down list

    bindPortRoleChangeEvent();

    // Init all the tooltips

    initToolTips();
        
})(jQuery);