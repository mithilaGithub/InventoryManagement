app.controller("ItemMasterAddCtrl", function ($scope, $http) {
    $scope.Add = function () {
        if ($scope.ItemMaster != undefined && $scope.ItemMaster.Name != '' && $scope.ItemMaster.Name != undefined && $scope.ItemMaster.Unit != undefined && $scope.ItemMaster.Unit != '' && $scope.ItemMaster.ReorderLevel != '' && $scope.ItemMaster.ReorderLevel != undefined) {
            $http({
                method: "POST",
                url: "../ItemMaster/ItemMaster_Add",
                data: $scope.ItemMaster
            }).then(function mySuccess(response) {
                if (response.data > 0) {
                    alert("Successefully Added.");
                    window.location.href = "../ItemMaster/ItemMasterList";
                }
                else
                    alert("Error");
            }, function myError(response) {
                alert("Error" + response.statusText);
            });
        }
        else {
            alert("Please fill all fields.");
        }
    }
})