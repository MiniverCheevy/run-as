(function () {
    angular.module('app')
        .controller("apps", ['$scope', '$parse', 'appsFactory', 
            function ($scope, $parse, appsFactory) {

            $scope.mode = 'grid';

            $scope.gridUrl = '/app/apps/grid.html';
            $scope.editUrl = '/app/apps/edit.html';
            $scope.addUrl = '/app/apps/add.html';
            $scope.fieldsUrl = 'app/apps/fields.html';            

            $scope.load = function (response) {
                if (response.isOk) {
                    $scope.gridState = response.state;
                    $scope.data = response.data;
                } else {
                    $scope.error = response.message;
                }
            };

            appsFactory.get({}).then($scope.load);

            $scope.refresh = function () {
                $scope.mode = 'grid';
                $scope.detail = null;
                appsFactory.get($scope.gridState).then($scope.load);
            };

            $scope.edit = function (key) {
                appsFactory.get({ key: key }).then($scope.loadEditItem);
            };
            $scope.add = function () {
                debugger;
                $scope.detail ={};
                $scope.mode='add';
            };

            $scope.loadEditItem = function (response) {
                if (response.isOk) {
                    $scope.detail = response.data[0];
                    $scope.detail.errors = {};
                    $scope.mode = 'edit';
                } else {
                    $scope.error = response.message;
                }
            };

            $scope.editSave = function () {
                appsFactory.put($scope.detail).then($scope.editDone);
            };
            $scope.addSave = function () {
                appsFactory.post($scope.detail).then($scope.addDone);                
            };

            $scope.addDone = function (response) 
            {
                $scope.done(response, $scope.addForm);
            }
            $scope.editDone = function (response) 
            {
                $scope.done(response, $scope.editForm);
            }
            $scope.done = function (response, form) {
                if (!response.isOk)
                    $scope.failure(response.details, form);
                else
                {
                    $scope.mode='grid';
                    $scope.info='Save Completed'
                }
            };
            $scope.failure = function (details, form) {
                $scope.detail.errors = {};
                angular.forEach(details, function (e) {
                    var field = e.key.charAt(0).toLowerCase() + e.key.slice(1);
                    form[field].$setValidity("server", false);
                    $scope.detail.errors[field] = e.value;
                });
            };
        }]);
}())



