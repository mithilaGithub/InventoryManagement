app.controller("StockCtrl", function ($scope, $http) {
    $scope.init = function () {
        $http({
            method: "POST",
            url: "../ItemMaster/ItemMaster_GetAll",
        }).then(function mySuccess(response) {
            $scope.ItemList = response.data;
        }, function myError(response) {
            alert("Error" + response.statusText);
        });
    }
    $scope.AddStock = function () {
        if ($scope.Stock != undefined && $scope.Stock.ItemId != '' && $scope.Stock.ItemId != undefined && $scope.Stock.Quantity != undefined && $scope.Stock.Quantity != '' && $scope.Stock.Type != '' && $scope.Stock.Type != undefined) {
            $http({
                method: "POST",
                url: "../ItemMaster/StockTransaction_Add",
                data: $scope.Stock
            }).then(function mySuccess(response) {
                if (response.data.stock > 0 && response.data.item > 0) {
                    alert("Successefully Added.");
                    window.location.href = "../ItemMaster/StockTransaction";
                }
                else
                    alert("Error");
            }, function myError(response) {
                alert("Error" + response.statusText);
            });
        }
        else { alert("Please fill all fields"); }
    }
})