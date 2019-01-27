app.controller("ItemMasterCtrl", function ($scope, $http) {
    $scope.Operation = 'Edit';
    $scope.init = function () {
        $http({
            method: "POST",
            url: "../ItemMaster/ItemMaster_GetAll",
        }).then(function mySuccess(response) {
            $scope.ItemMasterList = response.data;
        }, function myError(response) {
            alert("Error" + response.statusText);
        });
    }
    $scope.edit = function (ItemData) {
        if (ItemData.Name != '' && ItemData.Unit != '' && ItemData.ReorderLevel != '') {
            $http({
                method: "POST",
                url: "../ItemMaster/ItemMaster_Update",
                data: ItemData
            }).then(function mySuccess(response) {
                if (response.data > 0) {
                    alert("Successefully Updated.");
                    window.location.href = "../ItemMaster/ItemMasterList";
                }
                else
                    alert("Error");
            }, function myError(response) {
                alert("Error" + response.statusText);
            });
        } else {
            alert("Please fill all fields");
        }
    }
    $scope.delete = function (ItemData) {
        $http({
            method: "POST",
            url: "../ItemMaster/ItemMaster_Delete",
            data: ItemData
        }).then(function mySuccess(response) {
            if (response.data > 0) {
                alert("Successefully Deleted.");
                window.location.href = "../ItemMaster/ItemMasterList";
            }
            else
                alert("Error");
        }, function myError(response) {
            alert("Error" + response.statusText);
        });
    }


   

})