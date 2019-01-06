
(($) => {

    const $attachmentSet = $('#AttachmentSet'),
          attachmentSet = $attachmentSet[0],
          $vpnTopologyType = $('#TopologyType'),
          vpnTopologyType = $vpnTopologyType[0];

    // Handle addition of attachment set to vpn
    $('#addAttachmentSet').on('click', function (e) {

        var $grid = $('#attachment-set-grid');

        if (attachmentSet.value !== null && attachmentSet.value !== "") {

            var $selected = $attachmentSet.find(":selected");
            var attachmentSetName = $selected.data('name');
            var tenantName = $selected.data('tenant-name');
            var redundancy = $selected.data('attachment-redundancy');
            var region = $selected.data('region-name');

            var exists = $grid
                .find('td > input[type="text"]')
                .filter(function () {
                    return this.value === attachmentSetName;
                })
                .length > 0;

            if (exists) {

                // The routing instance already exists in the table
                $("#duplicateItemModal").modal();
            }
            else {

                var arr = [];
                arr.push({
                    "VpnTopologyType": vpnTopologyType.value,
                    "AttachmentSetName": attachmentSetName,
                    "tenantName": tenantName,
                    "region": region,
                    "AttachmentRedundancy": redundancy
                });

                // Refresh the attachment set grid
                refreshAttachmentSetsGrid(arr);
            }
        }
    });

    //Bind to click event of trash buttons in the attachment set grid rows
    $('#attachment-set-grid').on('click', '.mind-grid-delete-row', function (e) {

        var deleteRowId = $(this).data('row-id');
        var $row = $('#attachment-set-grid-row_' + deleteRowId);
        $row.remove();

        // Refresh the attachment set grid
        refreshAttachmentSetsGrid();
    });

    // Bind to checkbox change event for all grids to set boolen value - this is needed to send correct boolean value
    // to the controller on form submit
    $('.mind-grid').on('change', '.mind-grid-checkbox', function (e) {

        this.value = this.checked;
        var $row = $(this).parents('tr');
        $row.data('is-hub', this.value);
    });

    const $tenancyType = $('#TenancyType'),
          $ownerTenantId = $('#TenantId'),
          ownerTenantId = $ownerTenantId[0],
          $participantTenant = $('#TenantName'),
          participantTenant = $participantTenant[0];

    // Populate the list of participant tenants based upon the tenancy type of the vpn.
    // If the tenancy type is single then the list will contain only one tenant - the owner.
    // If the tenancy type is multi then the list will contain all tenants.
    $tenancyType.on('change', function (e) {

        attachmentSet.selectedIndex = 0;
        attachmentSet.disabled = true;

        if (this.value === null || this.value === "") {

            participantTenant.selectedIndex = 0;
            participantTenant.disabled = true;
        }
        else {

            Mind.Utilities.populateElement($participantTenant, "ParticipantTenants", {
                ownerTenantId: ownerTenantId.value, tenancyType: this.value
            });
        }
    });

    // Populate the list of attachment sets which belong to the selected tenant
    $participantTenant.on('change', function (e) {

        if (this.value === null || this.value === "") {

            attachmentSet.selectedIndex = 0;
            attachmentSet.disabled = true;
        }
        else {

            Mind.Utilities.populateElement($attachmentSet, "AttachmentSets", {
                tenantName: this.value
            });
        }
    });

    // Helpers

    // Refresh the attachment sets grid data
    function refreshAttachmentSetsGrid(arr) {

        var $grid = $('#attachment-set-grid');
        var attachmentSetsData = getAttachmentSetsData(arr);

        var $tbody = $grid.find('tbody');
        var data = JSON.stringify(attachmentSetsData);
        var deferred = $.Deferred();

        $.ajax({
            url: "GetVpnAttachmentSetsGridData",
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

    function getAttachmentSetsData(arr) {

        if (typeof (arr) === "undefined") arr = [];
        var $grid = $('#attachment-set-grid');
        var $rows = $grid.find('tbody tr');

        $rows.each(function () {

            var $row = $(this);
            var attachmentSetName = $row.data('name');
            var tenantName = $row.data('tenant-name');
            var isHub = $row.data('is-hub');
            var region = $row.data('region');
            var redundancy = $row.data('attachment-redundancy');
            arr.push({
                "VpnTopologyType": vpnTopologyType.value,
                "AttachmentSetName": attachmentSetName,
                "tenantName": tenantName,
                "IsHub": isHub,
                "Region": region,
                "AttachmentRedundancy": redundancy
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

})(jQuery);