(function () {
	angular.module('voodoo.ui', ['ngMaterial', 'voodoo.ui.pager', 'voodoo.ui.sorter',
        'voodoo.ui.datePicker', 'serverError', 'ngReallyClickModule', 'voodoo.ui.database',
        'voodoo.ui.utility', 'voodoo.ui.date', 'voodoo.ui.lessThan'
	]);
	angular.factory('templateService', function () {
		return {
			name: '.md'
		};
	});
}())