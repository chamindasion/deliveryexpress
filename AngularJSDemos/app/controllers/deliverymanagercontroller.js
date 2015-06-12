app.controller('DeliveryManagerController', function ($scope, $routeParams, $window, deliveryService, $location) {
    $scope.delivery = {};

    init();

    function init() {
        var categoryPromise = deliveryService.getCategories();
        $scope.delivery.datedelivered = {
            value: new Date(2015, 1, 1)
        };
        categoryPromise.then(function (result) {
            $scope.categories = result;
        });
    };

    $scope.insertDelivery = function () {        
        var deliveryPromise = deliveryService.insertDelivery($scope.delivery.datedelivered.value, $scope.categories);
        deliveryPromise.then(function (result) {
            $scope.myresult = result;
            $window.alert('Success adding delivery: ');
            $location.path('/deliveries');
        }, function (error) {
            $window.alert('Error adding delivery: ' + error.message);
        });
    };
});


