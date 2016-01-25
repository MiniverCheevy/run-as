(function() {
    angular.module('app', ['ngAnimate','voodoo.ui'])
            .config([
                '$httpProvider', function ($httpProvider) {
                    if (!$httpProvider.defaults.headers.get) {
                        $httpProvider.defaults.headers.get = {};
                    }
                    $httpProvider.defaults.headers.get['cache-control'] = 'no-cache';
                    $httpProvider.defaults.headers.get['expires'] = '-1';
                    $httpProvider.defaults.headers.get['pragma'] = 'no-cache';
                }
             
        ])
        .run([
            '$rootScope', function ($rootScope) {
            $rootScope.template = '';
        }
    ])
    ;
}())
         