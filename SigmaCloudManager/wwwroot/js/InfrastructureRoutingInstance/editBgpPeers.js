(($) => {

    // Show the spinner when the Edit BGP Peers form is submitted
    $('#EditBgpPeers').on('click', () =>  Mind.Utilities.showSpinner());

    // Initialise new tool-tips
    $('[data-toggle="tooltip"]').tooltip();
   
})(jQuery);