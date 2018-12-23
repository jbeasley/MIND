// Define utility methods for the client-side application

var Mind = {
};

Mind.Utilities = (function () {

    "use strict";

    /// Get the current cookie
    var getCookie = function (cname) {
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
    var populateElement = function ($e, url, data, done) {

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
    var showSpinner = function () {

        var $loadingSpinner = $('#loadingSpinner');
        if ($loadingSpinner.length > 0) {
            $loadingSpinner.modal('show');
        }
    }

    /// Hide the spinner
    var hideSpinner = function () {

        var $loadingSpinner = $('#loadingSpinner');
        if ($loadingSpinner.length > 0) {
            $loadingSpinner.modal('hide');
        }
    }

    /// Create a wizard
    var createWizard = function ($wizard, $form, onFinished) {

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

    return {

        getCookie: getCookie,
        populateElement: populateElement,
        createWizard: createWizard,
        showSpinner: showSpinner,
        hideSpinner: hideSpinner
    };

}(jQuery));