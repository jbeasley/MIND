// Define utility methods for the client-side application

var Mind = {
};

Mind.Utilities = (() => {

    "use strict";

    /// Get the current cookie
    const getCookie = (cname) => {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    };

    /// Populate an element with html from the server
    const populateElement = ($e, url, data, done) => {

        $e[0].disabled = true;

        $.get(url, data)
            .done(function (data) {

                // Enable the html element to be populated
                $e[0].disabled = false;
                $e.html(data);

                if (done !== undefined && typeof done === 'function') {

                    done(data);
                }
            });
    };

    /// Show the spinner
    const showSpinner = (message, done) => {

        var $loadingSpinner = $('#loadingSpinner');
        if ($loadingSpinner.length > 0) {

            // Make sure any previously bound 'modal shown' event is removed first
            $loadingSpinner.off('shown.bs.modal');
            
            if (done !== undefined && typeof done === 'function') {

                // Must implement the 'done' callback after any bootstrap modal transitions
                // have completed
                $loadingSpinner.on('shown.bs.modal', function (e) {

                        done(e);
                    });
            }

            message = message == null ? "Applying updates...." : message;
            $('#spinnerMessage').empty().html(message);

            $loadingSpinner.modal('show');            
        }
    }

    /// Hide the spinner
    const hideSpinner = () => {

        var $loadingSpinner = $('#loadingSpinner');
        if ($loadingSpinner.length > 0) {
            $loadingSpinner.modal('hide');
        }
    }

    /// Create a wizard
    const createWizard = ($wizard, $form, onFinished) => {

        $wizard.steps({
            headerTag: "h3",
            bodyTag: "section",
            transitionEffect: "fade",
            onStepChanging: function (event, currentIndex, newIndex) {

                // Allways allow previous action even if the current form is not valid!
                if (currentIndex > newIndex) {
                    return true;
                }

                $form.validate().settings.ignore = ":disabled,:hidden";
                return $form.valid();
            },
            onFinishing: function (event, currentIndex) {
                $form.validate().settings.ignore = ":disabled";
                return $form.valid();
            },
            onFinished: function (event, currentIndex) {

                if (typeof onFinished === "function") {

                    onFinished();
                }
                else {

                    showSpinner();
                    $form.submit();
                }
             }
        });
    };

     /// Show the duplicate item modal dialog
    const showDuplicateItemDialog = (title, message) => {

        var $duplicateItemModal = $('#duplicateItemModal');
        if ($duplicateItemModal.length > 0) {

            title = title == null ? "Duplicate Item" : title;
            message = message == null ? "The selected item already exists" : message;
            $('#duplicateItemModalLabel').empty().html(title);
            $('#duplicateItemModalMessage').empty().html(message);

            $duplicateItemModal.modal('show');            
        }
    }

    return {

        getCookie: getCookie,
        populateElement: populateElement,
        createWizard: createWizard,
        showSpinner: showSpinner,
        hideSpinner: hideSpinner,
        showDuplicateItemDialog: showDuplicateItemDialog
    };

})(jQuery);