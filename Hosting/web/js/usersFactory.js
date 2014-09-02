
	app.factory('usersFactory', ['$http', function($http) {

    var urlBase = '/api/users';
    var usersFactory = {};

		
		usersFactory.get = function (request,callback) {
        var operation= $http({method: 'GET', url: urlBase, params: request  });
		return operation.then(function(data, status, headers, config) {
            callback(data.Data);
        }, function(data, status, headers, config) {
            
            callback( { IsOk: false, Message: status });
        });
		
    };

		

	return usersFactory;
}]);

