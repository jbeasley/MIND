// Helper for handling the user selecting a card. Depending on the card, the user may be 
// redirected to another page, or a network sync may be initiated.

(function ($) {

    var $selectItemModal = $("#selectItemModal"),
        $multipleItemsSelectedModal = $("#multipleItemsSelectedModal"),
        $networkSyncCompleteModal = $("#networkSyncCompleteModal");
         
    $(".custom-card").click(function (e) {

        e.preventDefault();

        var $inputs = $(".form-check-input:checkbox:checked");
        if ($inputs.length === 0) {

            // No items are checked
            if ($selectItemModal.length > 0) {

                $selectItemModal.modal();
            }
        }
        else if ($inputs.length > 1) {

            // More than one checked item
            if ($multipleItemsSelectedModal.length > 0) {

                $multipleItemsSelectedModal.modal();
            }
        }
        else {

            var $this = $(this),
                action = $this.data('action'),
                url = $inputs.data('action-url');

            if (action === "sync-to-network") 
            {
                var $networkSyncDoneModal = $('#networkSyncDoneModal');
                var $networkSyncFailModal = $('#networkSyncFailModal');

                Mind.Utilities.showSpinner();

                $.post(url)
                
                    .done(function() {
                    
                        // Show done message
                        if ($networkSyncDoneModal.length > 0) {

                            $networkSyncDoneModal.modal('show');
                        }
                    })

                    .fail(function(data) {

                         // Something went wrong - show fail message
                         if ($networkSyncFailModal.length > 0) {

                             var $modalBody = $("<div class=\"col-sm-12\">\n" +
                                                "<div class=\"alert alert-danger\"" +
                                                "role=\"alert\">\n" +
                                                "<div class=\"row vertical-align\">\n" +
                                                "<div class=\"col-sm-1 text-center\">\n" +
                                                "<i class=\"fa fa-exclamation-triangle\"></i>\n" +
                                                "</div>\n" +
                                                "<div class=\"col-sm-11\">\n" +
                                                "" + data.responseJSON.message + "\n" +
                                                "</div>\n" +
                                                "</div>\n" +
                                                "</div>\n" +
                                                "</div>\n" +
                                                "</div>");

                             $networkSyncFailModal.find('.modal-body').empty().append($modalBody);
                             $networkSyncFailModal.modal('show');
                        }
                    })

                    .always(function() {

                        Mind.Utilities.hideSpinner();
                    })
            }
            else {

                var href = $this.data("href"),
                    queryParameter = $inputs.data('query-parameter'),
                    propertyVal = $inputs.data('property-val');

                window.location.href = href + queryParameter + propertyVal;
            }
        }
    });

}(jQuery));