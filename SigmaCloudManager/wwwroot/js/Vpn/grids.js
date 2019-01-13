
/* Manages the VPN wizard user interactions */

(($) => {

    // Cache various elements used throughout

    const $attachmentSet            = $('#AttachmentSet');
    const attachmentSet             = $attachmentSet[0];
    const $vpnTopologyType          = $('#TopologyType');
    const vpnTopologyType           = $vpnTopologyType[0];
    const $participantTenant        = $('#TenantName');
    const participantTenant         = $participantTenant[0];
    const $protocolType             = $('#ProtocolType');

    // Handle changes to the protocol type selection

    const handleProtocolTypeChangeEvent = () => {
    
        const $addressFamily         = $('#AddressFamily');
        const addressFamily          = $addressFamily[0];
        const $topologyType          = $('#TopologyType');
        const topologyType           = $topologyType[0];

        if ($protocolType.val() === null || $protocolType.val() === "") {

            addressFamily.selectedIndex = 0;
            addressFamily.disabled = true;
            topologyType.selectedIndex = 0;
            topologyType.disabled = true;
        }

        // Handle changes to the protocol type dropdown list selection

        $protocolType.on('change', function (e) {

            if (this.value === null || this.value === "") {

                addressFamily.selectedIndex = 0;
                addressFamily.disabled = true;
                topologyType.selectedIndex = 0;
                topologyType.disabled = true;
            }
            else {

                Mind.Utilities.populateElement($addressFamily, "AddressFamilies", { protocolType: this.value });
                Mind.Utilities.populateElement($topologyType, "TopologyTypes", { protocolType: this.value });
            }
        });
    }
    
    // Handle addition of attachment set to vpn

    const handleAddAttachmentSetButtonClick = () => {

        $('#addAttachmentSet').on('click', function (e) {
                   
            if (attachmentSet.value !== null && attachmentSet.value !== "") {

                const $selected = $attachmentSet.find(":selected");

                const exists = $('#attachment-set-grid')
                        .find('td > input[type="text"]')
                        .filter(function () {
                            return this.value === $selected.data('name');
                        })
                        .length > 0;

                if (exists) {

                    // The routing instance already exists in the table

                    Mind.Utilities.showDuplicateItemDialog('Duplicate Attachment Set', 
                        'The selected attachment set already exists.');
                }
                else {

                    let arr = [];

                    arr.push({
                        "VpnTopologyType"           : vpnTopologyType.value,
                        "AttachmentSetName"         : $selected.data('name'),
                        "tenantName"                : $selected.data('tenant-name'),
                        "region"                    : $selected.data('region-name'),
                        "AttachmentRedundancy"      : $selected.data('attachment-redundancy')
                    });

                    // Refresh the attachment set grid

                    refreshAttachmentSetsGrid(arr);
                }
            }
        });
    }

    // Handle user interactions with various grid controls

    const handleGridControlActions = () => {

        // Handle click of the 'delete' action button

        $('#attachment-set-grid').on('click', '.mind-grid-delete-row', function () {

            const deleteRowId                 = $(this).data('row-id');
            $('#attachment-set-grid-row_' + deleteRowId).remove();

            // Refresh the attachment set grid

            refreshAttachmentSetsGrid();
        });

        /* Bind to checkbox change event for all grids to set boolen value - this is needed to send correct boolean value
        to the controller on form submit */

        $('.mind-grid').on('change', '.mind-grid-checkbox', function () {

            this.value                      = this.checked;
            let $row                        = $(this).parents('tr');

            $row.data('is-hub', this.value);
        });
    }

    const handleTenancyTypeChange = () => {

        const $tenancyType                  = $('#TenancyType');
        const $ownerTenantId                = $('#TenantId');
        const ownerTenantId                 = $ownerTenantId[0];

        /* Populate the list of participant tenants based upon the tenancy type of the vpn.
        If the tenancy type is single then the list will contain only one tenant - the owner.
        If the tenancy type is multi then the list will contain all tenants. */

        $tenancyType.on('change', function (e) {

            attachmentSet.selectedIndex             = 0;
            attachmentSet.disabled                  = true;

            if (this.value === null || this.value === "") {

                participantTenant.selectedIndex     = 0;
                participantTenant.disabled          = true;
            }
            else {

                Mind.Utilities.populateElement($participantTenant, "ParticipantTenants", {
                    ownerTenantId: ownerTenantId.value, tenancyType: this.value
                });
            }
        });
    }

    // Handle changes to selection of the participant tenant

    const handleParticipantTenantChange = () => {

        // Populate the list of attachment sets which belong to the selected tenant

        $participantTenant.on('change', function (e) {

            if (this.value === null || this.value === "") {

                attachmentSet.selectedIndex         = 0;
                attachmentSet.disabled              = true;
            }
            else {

                Mind.Utilities.populateElement($attachmentSet, "AttachmentSets", {

                    tenantName: this.value
                });
            }
        });
    }

    // Helpers

    // Refresh the attachment sets grid data

    function refreshAttachmentSetsGrid(arr) {
    
        const attachmentSetsData      = getAttachmentSetsData(arr);       
        const data                    = JSON.stringify(attachmentSetsData);
        const deferred                = $.Deferred();

        Mind.Utilities.showSpinner('Fetching data from the server....');

        $.post({
            url: "GetVpnAttachmentSetsGridData",
            contentType: 'application/json; charset=utf-8',
            data: data
        })
         .done( (data) => {

            $('#attachment-set-grid').find('tbody').html(data);
            refreshValidation();

            deferred.resolve();
        })
         .always( () => Mind.Utilities.hideSpinner() );    

        return deferred.promise();
    }

    function getAttachmentSetsData(arr) {

        if (typeof (arr) === "undefined") arr   = [];
        const $rows                             = $('#attachment-set-grid').find('tbody tr');

        $rows.each(function () {

            const $row                          = $(this);

            arr.push({
                "VpnTopologyType"               : vpnTopologyType.value,
                "AttachmentSetName"             : $row.data('name'),
                "tenantName"                    : $row.data('tenant-name'),
                "IsHub"                         : $row.data('is-hub'),
                "Region"                        : $row.data('region'),
                "AttachmentRedundancy"          : $row.data('attachment-redundancy')
            });
        });

        return arr;
    } 

    // Re-apply validation to the form so that validation rules for the new inputs are created

    function refreshValidation() {

        var $form = $('#form');
        $form.removeData('validator');
        $form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('#form');
    }

    // Main

    if ($protocolType.length > 0) {

        handleProtocolTypeChangeEvent();
    }

    handleAddAttachmentSetButtonClick();
    handleGridControlActions();
    handleTenancyTypeChange();
    handleParticipantTenantChange();

})(jQuery);