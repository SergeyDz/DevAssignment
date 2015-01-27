(function () {
    var moduleControllers = angular.module('AccountCtrl', ['accountService']);

    moduleControllers.controller('AccountListController', ['$scope', '$stateParams', 'Account',
        function ($scope, $stateParams, Account) {
            $scope.Accounts = Account.get();
        }
    ]);
})();
