app.controller('DeliverySummaryController', function ($scope, $window, deliveryService) {
    init();

    function init() {
        var deliveriesPromise = deliveryService.getDeliveries();
        deliveriesPromise.then(function (result) {  // this is only run after $http completes
            $scope.deliveries = result;
        });
    };

    $scope.deleteDelivery = function (delivery) {
        var deliveryPromise = deliveryService.deleteDelivery(delivery);
        deliveryPromise.then(function (result) {            
            $scope.myresult = result;

            for (var i = 0; i < $scope.deliveries.length; i++) {
                if ($scope.deliveries[i].id === delivery) {
                    $scope.deliveries.splice(i, 1);
                    break;
                }
            }
            $window.alert('Success deleting customer: ');
        }, function(error) {
            $window.alert('Error deleting customer: ' + error.message);
        });
    };
});