(function (app) {

    var appServices = angular.module('accountService', ['ngResource']);

    appServices.factory('Account', ['$http', '$q',
        function ($http, $q) {
            return {
                getAccountList: function () {
                    var deferred = $q.defer();
                    $http.get('api/account').success(function (data) { deferred.resolve(data); });
                    return deferred.promise;
                },

                getAccountAmount: function (accountId) {
                    var deferred = $q.defer();
                    $http.get(['api/account', accountId, 'amount'].join('/')).success(function (data) { deferred.resolve(data); });
                    return deferred.promise;
                }
            };
        }
    ]);
})();
