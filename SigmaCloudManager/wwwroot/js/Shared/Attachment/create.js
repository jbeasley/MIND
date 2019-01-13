
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

    // Handle click events for the bundle and multiport checkbox controls

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

    // Handle chenges to the attachment bandwidth drop-down list

    function handleAttachmentBandwidthChangeEvent() {

        $attachmentBandwidthComponent.on('change', '#AttachmentBandwidthGbps', function (e) {
        
            cacheElements();

            defaultPortPool();
            defaultAttachmentRole();
        });
    }

    // Handle changes to the port pool drop-down list
    // Reset the IP addresssingm contract bandwidth, and BGP components. 
    // Depending on the attachment role option which the user subquently selects, these component may
    // be re-populated

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

    // Handle changes to the attachment role drop-down list

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

    // Reset the port pool drop-down list

    const defaultPortPool                       = () => $portPool[0].selectedIndex = 0;

    // Reset the attachment role drop-down list

    const defaultAttachmentRole                 = () => { $attachmentRole[0].selectedIndex = 0; $attachmentRole[0].disabled = true; }
   
    // Clear content of the IP addressing component

    const clearIpAddressingComponent            = () => $ipAddressingComponent.empty();

    // Clear the content of the Contract Bandwidth component

    const clearContractBandwidthPoolComponent   = () => $contractBandwidthPoolComponent.empty();

    // Clear the BGP component

    const clearBgpPeersComponent                = () => $bgpPeersComponent.empty();


    // Get the HTML for the location selector component

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

    // Get the HTML for the port pool/port role component

    const getAttachmentPortPoolAndRoleComponent = () => {

        if ($attachmentPortPoolAndRoleComponent.length > 0) {

            Mind.Utilities.populateElement($attachmentPortPoolAndRoleComponent, "GetAttachmentPortPoolAndRoleComponent", 
                {
                    portPoolName: $portPool[0].value
                });              
        }
    }

    // Get the HTML for the attachment bandwidth component

    const getAttachmentBandwidthComponent = () => {

        if ($attachmentBandwidthComponent.length > 0) {
        
            Mind.Utilities.populateElement($attachmentBandwidthComponent, "GetAttachmentBandwidthComponent", 
                { 
                    bundleRequired: $bundleRequired[0].checked,
                    multiportRequired: $multiportRequired[0].checked 
                });
        }
    }

    // Get the HTML for the IP addressing component

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
                () => initToolTipsAndValidation()
            );     
        }
    }

    // Get the HTML for the contract bandwidth component

    const getContractBandwidthPoolComponent = () => {

        if ($contractBandwidthPoolComponent.length > 0) {

            cacheElements();

            Mind.Utilities.populateElement($contractBandwidthPoolComponent, "GetContractBandwidthPoolComponent", 
            {
                portPoolName: $portPool[0].value,
                attachmentRoleName: $attachmentRole[0].value,
            },
                () => initToolTipsAndValidation()
            );
        }
    }

    // Get the HTML for the BGP component

    const getBgpPeersComponent = () => {

        if ($bgpPeersComponent.length > 0) {

            cacheElements();

            Mind.Utilities.populateElement($bgpPeersComponent, "GetBgpPeersComponent", 
            {
                portPoolName: $portPool[0].value,
                attachmentRoleName: $attachmentRole[0].value,
            },
                () => initToolTipsAndValidation()
            );
        }
    }

    // Cache jquery objects for various elements

    const cacheElements = () => {

        $regionId                   = $('#RegionId');
        $subRegionId                = $('#SubRegionId');
        $attachmentBandwidthGbps    = $('#AttachmentBandwidthGbps');
        $bundleRequired             = $('#BundleRequired');
        $multiportRequired          = $('#MultiportRequired');
        $portPool                   = $('#PortPoolName');
        $attachmentRole             = $('#AttachmentRoleName');        
    }

    // Initialise new tool-tips
    // Re-initialise unobtrusive validation on the form

    const initToolTipsAndValidation = () => {
       
        $('[data-toggle="tooltip"]').tooltip();
        
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
