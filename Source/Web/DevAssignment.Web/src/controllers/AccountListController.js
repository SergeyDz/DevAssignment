(function () {
    var moduleControllers = angular.module('AccountCtrl', ['accountService']);

    moduleControllers.controller('AccountListController', ['$scope', '$stateParams', 'Account',
        function ($scope, $stateParams, Account) {

            $scope.getAccountAmount = function(account) {
                Account.getAccountAmount(account.Id).then(function(data) {
                    account.Amount = data;
                });
            };

            Account.getAccountList().then(function(data) {
                $scope.Accounts = data;
            });
        }
    ]);
})();
