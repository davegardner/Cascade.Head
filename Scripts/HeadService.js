(function () {
	angular.module("headApp")
		.factory("headService", [

		// A service that interacts with the server to obtain and save changes to 
		// head objects and their elements
		function () {

			var tempHead = {
				id: 1
			};
			var tempHeadElements = [{
				id: 42,
				headPartRecord_Id: 1,
				type: 'Meta',
				name: 'Baz',
				content: 'this is the content'
			}, {
				id: 44,
				headPartRecord_Id: 1,
				type: 'HttpEquiv',
				name: 'Foo',
				content: 'this yet more content'
			}, {
				id: 56,
				headPartRecord_Id: 1,
				type: 'Char',
				name: 'Crap',
				content: 'Charset of some type'
			}];

			// return the head object itself
			var getHead = function (id) {
				return tempHead;
			};

			// return a list of all elements for this head id
			var getHeadElements = function (headId) {
				return tempHeadElements;
			};

			var saveHead = function (head) {

			}

			var saveHeadElement = function (headElement) {
				if (headElement.id === 0) {
					addHeadElement(headElement);
					headElement.id = 1;
				}
				else {
					updateHeadElement(headElement);
				}
			}

			function addHeadElement(headElement){

			}
			function updateHeadElement(headElement){

			}
		
			return {
				getHead: getHead,
				getHeadElements: getHeadElements,
				saveHead: saveHead,
				saveHeadElement: saveHeadElement
			};
		}
	]);
}());
