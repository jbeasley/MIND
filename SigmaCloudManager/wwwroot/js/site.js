// Define utility methods for the client-side application

var Mind = {
};

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

    var populateDropDownList = function ($e, url, data) {

        // Disable the drop down element during loading
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
        populateDropDownList: populateDropDownList
    };

}(jQuery));