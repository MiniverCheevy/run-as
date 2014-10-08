(function () {
    angular.module('app')
        .controller("apps", ['$scope', '$parse', 'appsFactory', function($scope, $parse, appsFactory) {

            $scope.mode = 'grid';

            $scope.gridUrl = '/app/apps/grid.html';
            $scope.editUrl = '/app/apps/edit.html';
            $scope.fieldsUrl = 'app/apps/fields.html';

            $scope.loadApps = function(response) {
                if (response.isOk) {
                    $scope.gridState = response.state;
                    $scope.apps = response.data;
                } else {
                    $scope.error = response.message;
                }
            };

            appsFactory.get({}).then($scope.loadApps);

            $scope.refresh = function() {
                $scope.mode = 'grid';
                $scope.editItem = null;
                $scope.addItem = null;
                appsFactory.get($scope.gridState).then($scope.loadApps);
            };

            $scope.edit = function(key) {
                appsFactory.get({ key: key }).then($scope.loadEditItem);
            };

            $scope.loadEditItem = function (response) {

                if (response.isOk) {
                    $scope.editItem = response.data[0];
                    $scope.editItem.errors = {};
                    $scope.mode = 'edit';
                } else {
                    $scope.error = response.message;
                }
            };

            $scope.editSave = function () {
                $scope.editItem.errors = {};
                appsFactory.post($scope.editItem).then($scope.editDone);
            };

            $scope.editDone = function(response) {

                $scope.failure(response.details);

            };


            $scope.failure = function (details) {
                    //http://icanmakethiswork.blogspot.com/2014/08/angularjs-meet-aspnet-server-validation.html
                    //http://codetunes.com/2013/server-form-validation-with-angular/
                    //http://stackoverflow.com/questions/16168355/angularjs-server-side-validation-and-client-side-forms

                $scope.editItem.errors = {};
                angular.forEach(details, function (e) {
                    var field = e.key.charAt(0).toLowerCase() + e.key.slice(1);
                    $scope.editForm[field].$setValidity("server", false);
                    $scope.editItem.errors[field] = e.value;

                    //for (var i = 0; i < errors.length; i++) {

                    //    debugger;
                    //    var e = errors[i];
                    //    var ccKey = e.key.charAt(0).toLowerCase() + e.key.slice(1);

                    //    var from = $scope.editForm;
                    //    var path = '$scope.editForm.' + ccKey + '.$error.serverMessage';
                    //    var serverMessage = $parse(path);
                    //    from.$setValidity(ccKey, false, from);
                    //    serverMessage.assign($scope, false);
                    //    debugger;
                    //}
                    //};
                });


            };
        }]);
}())


        
        