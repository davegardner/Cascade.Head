// Angular MainController
(function () {

	// Angular Bootstrap UI
	angular.module('headApp', ['ngAnimate', 'ui.bootstrap']);

	angular.module("headApp")
		.controller("MainController", ['$scope', '$modal', 'headService', '$log',
			function ($scope, $modal, headService, $log) {

				var head = headService.getHead(1);
				$scope.id = head.id;
				$scope.elements = headService.getHeadElements(head.id);
				$scope.message = "It works!";
				$scope.animationsEnabled = true;
				$scope.selected_element = null;
				
				var size = 'sm';

				$scope.addElement = function () {
					$scope.selected_element = null;
					edit();
				};

				$scope.editElement = function (element) {
					$scope.selected_element = element;
					edit();
				}

				function edit(){
					var modalInstance = $modal.open({
						animation: $scope.animationsEnabled,
						templateUrl: 'myModalContent.html',
						controller: 'ModalInstanceCtrl',
						size: size,
						resolve: {
							element: function () {
								return $scope.selected_element
							}
						}
					});

					modalInstance.result.then(function (element) {
						element.headPartRecord_Id = head.id;
						if (element.id === 0) {
							$scope.elements.push(element);
							$scope.selected_element = element;
						}
						else {
							el = $scope.selected_element;
							el.id = element.id;
							el.headPartRecord_Id = element.headPartRecord_Id;
							el.type = element.type;
							el.name = element.name;
							el.content = element.content;
						}
						headService.saveHeadElement(element);

					}, function () {
						$log.info('Modal dismissed at: ' + new Date());
					});
				};

			
				$scope.toggleAnimation = function () {
					$scope.animationsEnabled = !$scope.animationsEnabled;
				};
			}
		]);

	// modal window controller
	angular.module('headApp')
		.controller('ModalInstanceCtrl', ['$scope', '$modalInstance', 'element', function ($scope, $modalInstance, element) {

			if (element) {
				$scope.id = element.id;
				$scope.headPartRecord_Id = element.headPartRecord_Id;
				$scope.type = element.type;
				$scope.name = element.name;
				$scope.content = element.content;
			}
			else {
				$scope.id = 0
				$scope.headPartRecord_Id = 0
				$scope.type = 'Meta';
				$scope.name = 'Keywords';
				$scope.content = 'my, key, words';
			}

			$scope.ok = function () {
				var resultElement = {
					id: $scope.id,
					headPartRecord_Id : $scope.headPartRecord_Id,
					type: $scope.type,
					name: $scope.name,
					content: $scope.content
				};
				$modalInstance.close(resultElement);
			};

			$scope.cancel = function () {
				$modalInstance.dismiss('cancel');
			};
		}]);

	//angular.element(document).ready(function () {
	//	angular.bootstrap(document.getElementById('metaElementsNs'), ['headApp']);
	//});

}());