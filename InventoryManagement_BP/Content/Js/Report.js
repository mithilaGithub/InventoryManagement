app.controller("ReportCtrl", function ($scope, $http) {
    var data
    $scope.init = function ()
    {
        $http({
            method: "POST",
            url: "../ItemMaster/StockReport_GetAll",
        }).then(function mySuccess(response) {
            $scope.ReorderLevelDataAvlPoints = response.data.ReorderLevelDataAvlPoints;
            $scope.ReorderLevelDataminPoints = response.data.ReorderLevelDataminPoints;
            $scope.StockDataPoints = response.data.StockDataPoints;
            $scope.DisplayReport();
        }, function myError(response) {
            alert("Error" + response.statusText);
        });
    }

    $scope.DisplayReport = function () {
        var chart = new CanvasJS.Chart("chartContainer", {
            animationEnabled: true,
            title: {text: "Rorder Level chart" },
            axisY: {    title: "Quantity" },
            axisX: {   title: "Item"  },
            data: [{
                 type: "column",
                 legendText: "Available Quantity",
                 showInLegend: "true",
                 dataPoints: $scope.ReorderLevelDataminPoints
                 },
                 {
                 type: "column",
                 legendText: "Reorder Level",
                 showInLegend: "true",
                 dataPoints: $scope.ReorderLevelDataAvlPoints
                 }]
        });
        chart.render();

        var chart1 = new CanvasJS.Chart("chartContainer1", {
            animationEnabled: true,
            title: {text: "Consumption chart for Last month"},
            axisY: {title: "Consumption in percentage" },
            axisX: {title: "Item"},
            data: [{
                    color: "#B0D0B0",
                    type: "column",
                    dataPoints: $scope.StockDataPoints
                  }]
        });
        chart1.render();
    };
})