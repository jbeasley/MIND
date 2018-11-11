// Define utility methods for the client-side application

var Mind = {
};

Mind.Constants = (function () {

    "use strict";

    const uris = {
        ATTACHMENT_SET: "AttachmentSet/"
    };

    const bgp = {
        LOCAL_IP_ROUTING_PREFERENCE_DEFAULT: 100,
        ADVERTISED_IP_ROUTING_PREFERENCE_DEFAULT: 1,
        LOCAL_IP_ROUTING_PREFERENCE_MIN_VALUE: 1,
        LOCAL_IP_ROUTING_PREFERENCE_MAX_VALUE: 500,
        ADVERTISED_IP_ROUTING_PREFERENCE_MIN_VALUE: 1,
        ADVERTISED_IP_ROUTING_PREFERENCE_MAX_VALUE: 20
    };  

    return {

        uris: uris,
        bgp: bgp
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

    return {

        getCookie: getCookie,
        populateElement: populateElement
    };

}(jQuery));