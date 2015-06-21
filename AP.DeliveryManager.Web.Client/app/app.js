var app = angular.module('customersApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

//This configures the routes and associates each route with a view and a controller
app.config(function ($routeProvider) {
    $routeProvider
        .when('/deliveries',
            {
                controller: 'DeliverySummaryController',
                templateUrl: '/app/partials/deliverysummary.html'
            })
        //Define a route that has a route parameter in it (:customerID)
        .when('/managedeliveries',
            {
                controller: 'DeliveryManagerController',
                templateUrl: '/app/partials/managedelivery.html'
            })
        .when('/login',
            {
                controller: 'LoginController',
                templateUrl: '/app/partials/login.html'
            })
        .otherwise({ redirectTo: '/login' });
});

app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}
]);

//AuthApi service url
var serviceBase = 'http://deliveryexpressauth-api.azurewebsites.net/';
//var serviceBase = 'http://localhost:57476/';

//ResourceApi service url
var resourceServiceBase = 'http://deliveryexpressresource-api.azurewebsites.net/';
//var resourceServiceBase = 'http://localhost:63165/';

app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    resourceApiServiceBaseUri: resourceServiceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);



