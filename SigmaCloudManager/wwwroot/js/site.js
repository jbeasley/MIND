// Define utility methods for the client-side application

var SCM = {
};

SCM.Utilities = (function () {

    "use strict";

    var isConnected = false;
    function invoke(connection, method, ...args) {
        if (!isConnected) {
            return;
        }
        var argsArray = Array.prototype.slice.call(arguments);
        connection.invoke.apply(connection, argsArray.slice(1))
            .then(result => {
                console.log("invocation completed successfully: " + (result === null ? '(null)' : result));

                if (result) {
                    //addLine('message-list', result);
                }
            })
            .catch(err => {
                console.log(err);
            });
    }

    var connection;

    var connectToNetworkSyncHub = function (args) {

        var $successMessage = $('#successMessage'),
            $errorMessage = $('#errorMessage'),
            $warningMessage = $('#warningMessage'),
            $successMessageContainer = $('#successMessageContainer'),
            $errorMessageContainer = $('#errorMessageContainer'),
            $warningMessageContainer = $('#warningMessageContainer'),
            $networkSuccessMessageContainer = $('#networkSuccessMessageContainer'),
            $networkErrorMessageContainer = $('#networkErrorMessageContainer'),
            $networkWarningMessageContainer = $('#networkWarningMessageContainer');

        var errorGlyph = '<div class="alert alert-danger" role="alert">'
            + '<span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>'
            + '<span class="sr-only">Error:</span>'
            + '</div>';

        var successGlyph = '<div class="alert alert-success" role="alert">'
            + '<span class="glyphicon glyphicon-ok-sign" aria-hidden="true"></span>'
            + '<span class="sr-only">Success:</span>'
            + '</div>';

        var warningGlyph = '<div class="alert alert-warning" role="alert">'
            + '<span class="glyphicon glyphicon-warning-sign" aria-hidden="true"></span>'
            + '<span class="sr-only">Warning:</span>'
            + '</div>';

        let transportType = signalR.TransportType.WebSockets;
        let http = new signalR.HttpConnection(`http://${document.location.host}/networkSyncHub`, { transport: transportType });
        connection = new signalR.HubConnection(http);
        connection.on('onSingleComplete', (item, success, message) => {

            // Stop the spinner
            var $syncStatus = $('#syncStatus_' + item[args.itemKey]);
            $syncStatus.data('spinner').stop();

            // Set requiresSync checkbox state
            $('#requiresSync_' + item[args.itemKey] + ' > input').prop('checked', !success);

            // Enable buttons
            $syncStatus.parents('tr').find('.btn').prop('disabled', false);

            // Show success or error glyph and message
            if (success) {

                $syncStatus.html(successGlyph);
                if (message) showSuccessMessage(format(message));
            }
            else {

                $syncStatus.html(warningGlyph);
                if (message) showWarningMessage(format(message));
            }
        });

        connection.onClosed = e => {
            if (e) {
                // add logging here
            }
            else {

                console.log("Connection is closed");
            }
        };

        connection.start()
            .then(() => {
                isConnected = true;

                // Join the group so that we can receive messages from the server sent to the group
                invoke(connection, 'JoinGroup', args.groupName);
            })
            .catch(err => {
                // Add error logging here
            });

        var $spinnerContainer = $('.row-spinner');

        // Handle Sync All and Check Sync All button click events

        if (args.syncAllUrl) {

            // wire-up button click event handler
            handleButtonClick({
                $button: $('#Sync'),
                $spinnerContainer: $spinnerContainer,
                url: args.syncAllUrl,
                beforeSend: function () {

                    initSpinners($spinnerContainer);

                    // Disable all buttons, including row buttons, whilst sync executes
                    $('.btn').prop('disabled', true);
                },
                onSuccess: function (responseItem) {

                    $spinnerContainer.each(function () {

                        $(this).data('spinner').stop();
                    });
                    $('.btn').prop('disabled', false);

                    if (responseItem.success) {

                        showSuccessMessage(format(responseItem.message));
                    }
                    else {

                        showWarningMessage(format(responseItem.message));
                    }
                },
                onError: function (oMessages) {

                    $('.btn').prop('disabled', false);
                    $spinnerContainer.each(function () {

                        $(this).data('spinner').stop();
                    });

                    showErrorMessage(format(oMessages));
                }
            });
        }
        if (args.checkSyncAllUrl) {

            // wire-up button click event handler
            handleButtonClick({
                $button: $('#CheckSync'),
                $spinnerContainer: $spinnerContainer,
                url: args.checkSyncAllUrl,
                beforeSend: function () {

                    initSpinners($spinnerContainer);

                    // Disable all buttons, including row buttons, whilst CheckSync executes
                    $('.btn').prop('disabled', true);
                },
                onSuccess: function (responseItem) {

                    $('.btn').prop('disabled', false);
                    $spinnerContainer.each(function () {

                        $(this).data('spinner').stop();
                    });

                    if (responseItem.success) {

                        showSuccessMessage(format(responseItem.message));
                    }
                    else {

                        showWarningMessage(format(responseItem.message));
                    }
                },
                onError: function (oMessages) {

                    $('.btn').prop('disabled', false);
                    $spinnerContainer.each(function () {

                        $(this).data('spinner').stop();
                    });

                    showErrorMessage(format(oMessages));
                }
            });
        }

        // Handle click events for buttons in each table row

        $('.table .btn-sync, .table .btn-checksync').each(function () {

            var $this = $(this);
            var $buttons = $this.parents('td').children('.btn');
            var data = $this.data('item');
            var $spinnerContainer = $this.parents('tr').children('.row-spinner');
            var $requiresSync = $this.parents('tr').children('.checkbox-insync').children('input');

            // Calculate url and replace tokens with data 
            var url = $this.hasClass('btn-sync') ? args.syncUrl : args.checkSyncUrl;

            for (var item in data) {

                var token = '{' + item + '}';
                url = url.replace(token,
                    function (match) {
                        return data[item];
                    });
            }

            // wire-up button click event handler
            handleButtonClick({
                $button: $this,
                $spinnerContainer: $spinnerContainer,
                url: url,
                beforeSend: function () {

                    initSpinners($spinnerContainer);

                    // Disable row buttons
                    $buttons.prop('disabled', true);
                },
                onSuccess: function (responseItem) {

                    $spinnerContainer.data('spinner').stop();
                    $buttons.prop('disabled', false);

                    // Set requiresSync checkbox state
                    $requiresSync.prop('checked', !responseItem.success);

                    if (responseItem.success) {

                        // POST was successful so show success message
                        showSuccessMessage(format(responseItem.message));
                        $spinnerContainer.html(successGlyph);
                    }
                    else {

                        // It appears the POST was successful but the server detected some sort of problem
                        // so display a warning message
                        showWarningMessage(format(responseItem.message));

                        // Set requiresSync checkbox state to indicate the item requires sync with the network
                        $requiresSync.prop('checked', true);

                        $spinnerContainer.html(warningGlyph);
                    }
                },
                onError: function (errorThrown) {

                    // POST failed in some way so show an error message
                    $spinnerContainer.data('spinner').stop();
                    $buttons.prop('disabled', false);
                    showErrorMessage(format(errorThrown));

                    // Show error glyph
                    $spinnerContainer.html(errorGlyph);
                }
            });
        });

        initMessaging();

        function initMessaging() {

            if ($successMessage.html().length > 0) {

                $successMessageContainer.fadeIn();
            }

            if ($errorMessage.html().length > 0) {

                $errorMessageContainer.fadeIn();
            }

            if ($warningMessage.html().length > 0) {

                $warningMessageContainer.fadeIn();
            }
        }

        function handleButtonClick(args) {

            args.$button.on('click', function () {

                if (typeof args.beforeSend === 'function') {

                    args.beforeSend();
                }

                // Call the server
                $.ajax({
                    url: args.url,
                    method: 'POST',
                    success: function (data, textStatus, jqXHR) {

                        if (typeof args.onSuccess === 'function') {

                            args.onSuccess.call(this, data);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {

                        if (typeof args.onError === 'function') {

                            var messages = [errorThrown];
                            if (jqXHR.responseJSON) {
                                var json = jqXHR.responseJSON;
                                messages = messages.concat(json.message);
                                if (json.errors) {
                                    var errors = json.errors.map(function (item) {
                                        return item['message'];
                                    });
                                    messages = messages.concat(errors);
                                }
                            }

                            args.onError.call(this, messages);
                        }
                    }
                });
            });
        }

        function format(oMessages) {

            var str = '<ul>';
            if (typeof oMessages === 'object') {
                oMessages.forEach(function (m) {

                    str += '<li>' + m + '</li>';
                });
            }
            else {

                str += '<li>' + oMessages + '</li>';
            }

            return str += '</ul>';
        }

        function initSpinners($item) {

            $item.each(function () {

                var $this = $(this);
                $this.empty();

                var spinnerOpts = {
                    lines: 13,
                    length: 28,
                    width: 14,
                    radius: 42,
                    scale: 0.15
                };

                var spinner = new Spinner(spinnerOpts).spin(this);
                $this.data('spinner', spinner);
            });
        }

        function showSuccessMessage(message) {

            $successMessage.html(message);
            $successMessageContainer.fadeIn();
            hideErrorMessage();
            hideWarningMessage();
        }

        function showErrorMessage(message) {

            $errorMessage.html(message);
            $errorMessageContainer.fadeIn();
            hideSuccessMessage();
            hideWarningMessage();
        }

        function showWarningMessage(message) {

            $warningMessage.html(message);
            $warningMessageContainer.fadeIn();
            hideErrorMessage();
            hideSuccessMessage();
        }

        function hideErrorMessage() {

            $errorMessage.empty();
            $errorMessageContainer.fadeOut();
            $networkErrorMessageContainer.fadeOut();
        }

        function hideSuccessMessage() {

            $successMessage.empty();
            $successMessageContainer.fadeOut();
            $networkSuccessMessageContainer.fadeOut();
        }

        function hideWarningMessage() {

            $warningMessage.empty();
            $warningMessageContainer.fadeOut();
            $networkWarningMessageContainer.fadeOut();
        }
    };

    return {

        connectToNetworkSyncHub: connectToNetworkSyncHub
    };

}(jQuery));