(function (app) {

    var appServices = angular.module('accountService', ['ngResource']);

    appServices.factory('Account', ['$resource',
        function ($resource) {
            return $resource('api/account/', {}, {
                get: {
                    method: 'GET',
                    isArray: true
                }
            });
        }
    ]);
})();
