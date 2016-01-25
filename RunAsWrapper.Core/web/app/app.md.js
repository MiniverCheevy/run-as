(function () {
    angular.module('app', ['voodoo.ui'])
        .config([
            '$httpProvider','$mdThemingProvider','$animateProvider',
            function($httpProvider, $mdThemingProvider,$animateProvider) {
        $animateProvider.classNameFilter(/ng-animate/);



        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }
        $httpProvider.defaults.headers.get['cache-control'] = 'no-cache';
        $httpProvider.defaults.headers.get['expires'] = '-1';
        $httpProvider.defaults.headers.get['pragma'] = 'no-cache';

        $mdThemingProvider.theme('default')
            .primaryPalette('grey', { 'default': '900' })
            .accentPalette('teal', { 'default': '700' });
    }])
    .run([
        '$rootScope', function ($rootScope) {
            $rootScope.template = '.md';
        }
    ])
    ;
}())
