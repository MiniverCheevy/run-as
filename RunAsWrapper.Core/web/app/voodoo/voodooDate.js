(function() {
    angular.module('voodoo.ui.date', [])
        .directive('vDate', function ($filter) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl ) {
            var dateFormat = attrs['v-date'] || 'MM/dd/yyyy';

            ctrl.$parsers.unshift(function (viewValue) {
                return $filter('date')(viewValue, "MM/dd/yyyy");
            });

            ctrl.$formatters.unshift(function (modelValue) {
                return $filter('date')(modelValue, "MM/dd/yyyy");
            });
        }
    };
        });
}())