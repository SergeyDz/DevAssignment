(function() {
    angular.module('accountApp', [
            'ui.router',
            'AccountCtrl'
    ])
        .config(['$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
            $urlRouterProvider.otherwise('/account/list');

        $stateProvider
            .state('account', {
                url: '/account/list',
                templateUrl: 'src/views/account/list.html',
                controller: 'AccountListController'
            });
    }]);
})();
