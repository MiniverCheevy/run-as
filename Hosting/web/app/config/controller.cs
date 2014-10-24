(function () {
    "use strict";    
    angular.module('app')
        .controller("config", ['$scope'
            function ($scope) {
                $scope.$on('$destroy', function() {
                  window.onbeforeunload = undefined;
                });
              $scope.$on('$locationChangeStart', function(event, next, current) {
                if(!confirm("Are you sure you want to leave this page?")) {
                  event.preventDefault();
                }
});
            }]);
}())



