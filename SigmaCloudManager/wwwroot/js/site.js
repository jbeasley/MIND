﻿// Define utility methods for the client-side application

var Mind = {
};

Mind.Constants = (function () {

    "use strict";

    const uris = {
        PROVIDER_DOMAIN_ATTACHMENT: "ProviderDomainAttachment/",
        ATTACHMENT_SET: "AttachmentSet/"
    };

    return {

        uris: uris
    };

}(jQuery));

Mind.Utilities = (function () {

    "use strict";

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

    var populateElement = function ($e, url, data) {

        $e[0].disabled = true;

        $.get(url, data)
            .done(function (data) {

                // Enable the html element to be populated
                $e[0].disabled = false;
                $e.html(data);
            });
    };

    var createWizard = function ($wizard, $form) {

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

                $('#loadingSpinner').modal('show');
                $form.submit();
            }
        });
    };

    return {

        getCookie: getCookie,
        populateElement: populateElement,
        createWizard: createWizard
    };

}(jQuery));