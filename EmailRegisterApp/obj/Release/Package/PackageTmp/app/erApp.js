var app = angular.module("erApp", ['ui.bootstrap']);
var uri = document.getElementsByTagName('base')[0].getAttribute('href');
app.controller("emailRegisterCtrl", function ($scope, $http, $modal) {
    $scope.isRouteLoading = false;
    $scope.num = "123";
    $scope.user = { Name: '', Email: '', Company:'', Emails: [] };
    $scope.mensaje = { type: '', msg: '' };
    $scope.mensajes = [];
    $scope.add = function () {
        $scope.user.Emails.push({ Email: '', Motive: '' });
    };
    $scope.remove = function (index) {
        $scope.user.Emails.splice(index, 1);
    };
    $scope.abrirAlerta = function () {
        var modalInstance = $modal.open({
            templateUrl: 'Alerta.cshtml',
            controller: 'AlertaCtrl',
            size: 'md',
            scope: $scope,
            backdrop: 'static',
            keyboard: false,
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });
    };
    $scope.save = function () {
        $scope.isRouteLoading = true;
        $http({
            method: 'POST',
            url: uri + "User/Create",
            data: $scope.user
        }).then(function successCallback(response) {
            $scope.mensajes = [];
            var mensaje = angular.copy($scope.mensaje);
            mensaje.type = response.data.status ? 'success' : 'danger';
            mensaje.msg = response.data.data;
            $scope.mensajes.push(mensaje);
            $scope.isRouteLoading = false;
            $scope.abrirAlerta();
            }, function errorCallback(response) {
                $scope.mensajes = [];
                var mensaje = angular.copy($scope.mensaje);
                mensaje.type = response.data.status ? 'success' : 'danger';
                mensaje.msg = response.data.data;
                $scope.mensajes.push(mensaje);
                $scope.isRouteLoading = false;
                $scope.abrirAlerta();
            
        });
        //$http.post(uri + "User", $scope.user)
        //    .then(function mySuccess(response) {
        //        $scope.myWelcome = response.data;
        //    }, function myError(response) {
        //        // called asynchronously if an error occurs
        //        // or server returns response with an error status.
        //    });
    }
}).controller('AlertaCtrl', ['$scope', '$modalInstance', function ($scope, $modalInstance) {
    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $scope.mensajes = [];
        $modalInstance.close();
    };
}]);