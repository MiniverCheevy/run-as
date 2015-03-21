(function() {
    angular.module('voodoo.ui.lessThan', [])
   .directive('vLessThan', function () {
       return {
           require: 'ngModel',
           restrict: 'A',
           link: function (scope, elem, attrs, ctrl) {
               var startDate,
                   endDate;
               scope.$watch(attrs.ngModel, function (newVal, oldVal, scope) {
                   startDate = newVal;
                   check();
               });

               scope.$watch(attrs.vLessThan, function (newVal, oldVal, scope) {
                   endDate = newVal;
                   check();
               });

               var check = function () {
                   if (typeof startDate === 'undefined' || typeof endDate === 'undefined') {
                       return;
                   }

                   if (!validate(startDate)) {
                       startDate = new Date(startDate);
                       if (!validate(startDate)) {
                           return;
                       }
                   }

                   if (!validate(endDate)) {
                       endDate = new Date(endDate);
                       if (!validate(endDate)) {
                           return;
                       }
                   }

                   if (startDate < endDate) {
                       ctrl.$setValidity('vLessThan', true);
                   }
                   else {
                       ctrl.$setValidity('vLessThan', false);
                   }
                   return;
               };

               var validate = function (date) {
                   if (Object.prototype.toString.call(date) === '[object Date]') {
                       if (isNaN(date.getTime())) {
                           return false;
                       }
                       else {
                           return true;
                       }
                   }
                   else {
                       return false;
                   }
               };
           }
       };
   })
    ;
}())