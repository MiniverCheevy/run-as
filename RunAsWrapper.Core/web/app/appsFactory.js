//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************

(function () {
    angular
	.module('app')
	.factory('appsFactory', ['$http','$log', function($http,$log) {

    var urlBase = '/api/apps';
    var appsFactory = {};

		
		appsFactory.put = function (request) {
		        var operation= $http.put(urlBase,request );
		
		     return operation.then(function(data, status, headers, config) {
				return data.data;
			}, 
			function(data, status, headers, config) {		
				$log.error(data);
				$log.error(status);
				$log.error(headers);
				$log.error(config);		    
				return  { isOk: false, message: data };
			}
		);
		
    };

		


		appsFactory.post = function (request) {
		        var operation= $http.post(urlBase,request );
		
		     return operation.then(function(data, status, headers, config) {
				return data.data;
			}, 
			function(data, status, headers, config) {		
				$log.error(data);
				$log.error(status);
				$log.error(headers);
				$log.error(config);		    
				return  { isOk: false, message: data };
			}
		);
		
    };

		


		appsFactory.delete = function (request) {
				var operation= $http({url: urlBase, method:'delete', params:request });		
		
		     return operation.then(function(data, status, headers, config) {
				return data.data;
			}, 
			function(data, status, headers, config) {		
				$log.error(data);
				$log.error(status);
				$log.error(headers);
				$log.error(config);		    
				return  { isOk: false, message: data };
			}
		);
		
    };

		


		appsFactory.get = function (request) {
				var operation= $http({url: urlBase, method:'get', params:request });		
		
		     return operation.then(function(data, status, headers, config) {
				return data.data;
			}, 
			function(data, status, headers, config) {		
				$log.error(data);
				$log.error(status);
				$log.error(headers);
				$log.error(config);		    
				return  { isOk: false, message: data };
			}
		);
		
    };

		

	return appsFactory;
}]);
}())
