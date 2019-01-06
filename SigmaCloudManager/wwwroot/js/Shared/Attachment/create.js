
// Handles user interactions of the Create Attachment wizard

(($) => {

    // Form and wizard component elements

    const $form                     = $('#createForm'),
          $wizard                   = $('#createAttachmentWizard');

    // Determine whether the sync or stage modal form should be shown when the wizard finsihes

    const showStageOrSyncModal      = (/true/i).test($('showStageOrSyncModal').val());


    // Variables to cache element objects needed to load/populate components
    var $regionId,
        $subRegionId,
        $bundleRequired,
        $multiportRequired,
        $portPool,
        $attachmentRole,
        $attachmentBandwidthGbps;
        
    // Create the wizard

    Mind.Utilities.createWizardWithNetworkStageOrSyncModal($wizard, $form, showStageOrSyncModal); 

    // Component container elements

    const $attachmentBundleAndMultiportOptionsComponent = $('#attachmentBundleAndMultiportOptionsComponent'),
          $ipAddressingComponent                        = $('#ipAddressingComponent'),
          $contractBandwidthPoolComponent               = $('#contractBandwidthPoolComponent'),
          $bgpPeersComponent                            = $('#bgpPeersComponent'),
          $locationSelector                             = $('#locationSelector'),
          $attachmentBandwidthComponent                 = $('#attachmentBandwidthComponent'),
          $attachmentPortPoolAndRoleComponent           = $('#attachmentPortPoolAndRoleComponent'),
          $portRoleTypeEnumName                         = $('#portRoleTypeEnumName'),
          $deviceRoleId                                 = $('#deviceRoleId');

    // Handle changes to the location selector control

    function handleLocationSelectorChangeEvents() {

        $locationSelector.on('change', '#RegionId', function (e) {
        
            if (this.value !== null) getLocationSelectorComponent();
        });

        $locationSelector.on('change', '#SubRegionId', function (e) {
        
            if (this.value !== null) getLocationSelectorComponent();
        });
    }

    function handleBundleAndMultiportCheckboxClickEvents() {

        $attachmentBundleAndMultiportOptionsComponent.on('click', '#MultiportRequired', () => {

            $bundleRequired[0].checked = false;
            getAttachmentBandwidthComponent();
        });

        $attachmentBundleAndMultiportOptionsComponent.on('click', '#BundleRequired', function (e) {

            $multiportRequired[0].checked = false;
            getAttachmentBandwidthComponent();
        });
    }

    function handleAttachmentBandwidthChangeEvent() {

        $attachmentBandwidthComponent.on('change', '#AttachmentBandwidthGbps', function (e) {
        
            cacheElements();

            defaultPortPool();
            defaultAttachmentRole();
        });
    }

    function handlePortPoolChangeEvent() {

        $attachmentPortPoolAndRoleComponent.on('change', '#PortPoolName', function (e) {

            clearIpAddressingComponent();
            clearContractBandwidthPoolComponent();
            clearBgpPeersComponent();
           
            if (this.value === null || this.value === "") {

                defaultAttachmentRole();
            }
            else {

                Mind.Utilities.populateElement($attachmentPortPoolAndRoleComponent, "GetAttachmentPortPoolAndRoleComponent", {

                    PortRoleTypeEnumName: $portRoleTypeEnumName.val(),
                    portPoolName: this.value,
                    deviceRoleId: $deviceRoleId.val()
                });
            }
        });
    }

    function handleAttachmentRoleChangeEvent() {

        $attachmentPortPoolAndRoleComponent.on('change', '#AttachmentRoleName', function (e) {

            if (this.value !== null) {

                getIpAddressingComponent();
                getContractBandwidthPoolComponent();
                getBgpPeersComponent();
            }
        });
    }

    // Helpers

    const defaultPortPool                       = () => $portPool[0].selectedIndex = 0;
    const defaultAttachmentRole                 = () => { $attachmentRole[0].selectedIndex = 0; $attachmentRole[0].disabled = true; }
    const clearIpAddressingComponent            = () => $ipAddressingComponent.empty();
    const clearContractBandwidthPoolComponent   = () => $contractBandwidthPoolComponent.empty();
    const clearBgpPeersComponent                = () => $bgpPeersComponent.empty();

    const getLocationSelectorComponent = () => { 

        if ($locationSelector.length > 0) {

            cacheElements();

            Mind.Utilities.populateElement($locationSelector, "GetLocationSelectorComponent", 
                 { 
                    regionId: $regionId[0].value,
                    subRegionId: $subRegionId[0].value
                 }, 
                 initToolTipsAndValidation
             );
        }
    }

    const getAttachmentPortPoolAndRoleComponent = () => {

        if ($attachmentPortPoolAndRoleComponent.length > 0) {

            Mind.Utilities.populateElement($attachmentPortPoolAndRoleComponent, "GetAttachmentPortPoolAndRoleComponent", 
                {
                    portPoolName: $portPool[0].value
                });              
        }
    }

    const getAttachmentBandwidthComponent = () => {

        if ($attachmentBandwidthComponent.length > 0) {
        
            Mind.Utilities.populateElement($attachmentBandwidthComponent, "GetAttachmentBandwidthComponent", 
                { 
                    bundleRequired: $bundleRequired[0].checked,
                    multiportRequired: $multiportRequired[0].checked 
                });
        }
    }
    
    const getIpAddressingComponent = () => {

        if ($ipAddressingComponent.length > 0) {

            cacheElements();

            Mind.Utilities.populateElement($ipAddressingComponent, "GetIpAddressingComponent", 
            {
                portPoolName: $portPool[0].value,
                attachmentRoleName: $attachmentRole[0].value,
                attachmentBandwidthGbps: $attachmentBandwidthGbps[0].value,
                isMultiport: $multiportRequired[0].checked
            },
                (data) => initToolTipsAndValidation()
            );     
        }
    }

    const getContractBandwidthPoolComponent = () => {

        if ($contractBandwidthPoolComponent.length > 0) {

            cacheElements();

            Mind.Utilities.populateElement($contractBandwidthPoolComponent, "GetContractBandwidthPoolComponent", 
            {
                portPoolName: $portPool[0].value,
                attachmentRoleName: $attachmentRole[0].value,
                attachmentBandwidthGbps: $attachmentBandwidthGbps[0].value
            },
                (data) => initToolTipsAndValidation()
            );
        }
    }

    const getBgpPeersComponent = () => {

        if ($bgpPeersComponent.length > 0) {

            cacheElements();

            Mind.Utilities.populateElement($bgpPeersComponent, "GetBgpPeersComponent", 
            {
                portPoolName: $portPool[0].value,
                attachmentRoleName: $attachmentRole[0].value,
            },
                (data) => initToolTipsAndValidation()
            );
        }
    }

    const cacheElements = () => {

        $regionId                   = $('#RegionId');
        $subRegionId                = $('#SubRegionId');
        $attachmentBandwidthGbps    = $('#AttachmentBandwidthGbps');
        $bundleRequired             = $('#BundleRequired');
        $multiportRequired          = $('#MultiportRequired');
        $portPool                   = $('#PortPoolName');
        $attachmentRole             = $('#AttachmentRoleName');        
    }

    const initToolTipsAndValidation = () => {

        // Initialise new tool-tips

        $('[data-toggle="tooltip"]').tooltip();

        // Re-initialise unobtrusive validation on the form
        $form.removeData("validator")
             .removeData("unobtrusiveValidation");

        $.validator.unobtrusive.parse($form);
    }

    // Main

    // Cache element object references
    cacheElements();

    // Bind event handlers to the various form controls

    handleLocationSelectorChangeEvents();
    handleBundleAndMultiportCheckboxClickEvents();
    handleAttachmentBandwidthChangeEvent();
    handlePortPoolChangeEvent();
    handleAttachmentRoleChangeEvent();

})(jQuery);
