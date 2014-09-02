var app = angular.module('app', ['ui.bootstrap','voodoo.ui']);
app.controller("appsList", ['$scope', 'appsFactory', function ($scope, appsFactory) {
    $scope.loadApps = function (response) {        
        if (response.isOk) {
            $scope.gridState = response.state;
            $scope.apps = response.data;
        }
        else {
            $scope.error = response.message;
        }
    };   
    appsFactory.get({}, $scope.loadApps);

    $scope.post = function () {
        debugger;
        appsFactory.get($scope.gridState, $scope.loadApps);
    };

}]);
app.config(function ($logProvider) {
    $logProvider.debugEnabled(true);
});