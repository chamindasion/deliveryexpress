app.service('deliveryService', function ($http, ngAuthSettings) {
    this.getDeliveries = function () {
        return $http.get(ngAuthSettings.resourceApiServiceBaseUri + 'api/parcel').then(function (results) {
            return results.data;
            });        
    };


    this.getCategories = function () {
        return $http.get(ngAuthSettings.resourceApiServiceBaseUri + 'api/category').then(function (results) {
            return results.data;
        });
    };


    this.insertDelivery = function (dateDelivered, categories) {
        var deliveryJsonArray = [];
        for (var categoryitem in categories) {
            if (categories.hasOwnProperty(categoryitem)) {
                var deliveryJsonIem = { deliveredDate: dateDelivered, deliveredQuantity: categories[categoryitem].quantity, categoryId: categories[categoryitem].id };
                deliveryJsonArray.push(deliveryJsonIem);
            }
        }
        
        return $http.post(
                ngAuthSettings.resourceApiServiceBaseUri + 'api/parcel',
                JSON.stringify(deliveryJsonArray),
                {
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }
            ).then(function (results) {
                return results.data;
        });
    };

    this.deleteDelivery = function (delivery) {
        return $http.delete(
                ngAuthSettings.resourceApiServiceBaseUri + 'api/parcel/' + delivery
            ).then(function (results) {
                return results.data;
            });
    };
});

