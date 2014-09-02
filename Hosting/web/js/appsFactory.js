
	app.factory('appsFactory', ['$http', function($http) {

    var urlBase = '/api/apps';
    var appsFactory = {};
		appsFactory.get = function (request,callback) {
        var operation= $http({method: 'GET', url: urlBase, params: request  });
		return operation.then(function(data, status, headers, config) {
            callback(data.data);
        }, function(data, status, headers, config) {
            
            callback( { IsOk: false, Message: status });
        });
		
    };

		

	return appsFactory;
}]);

