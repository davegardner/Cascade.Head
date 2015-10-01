/* Dave Gardner August 2015
 * Support for adding and editing Head Elements from the admin pages 
 * */

(function ($) {

	var currentRow = null;
	var index = 0;

	var setLabels = function () {
		// adjust the label to match the type
		var label = $('#heType option:selected').text();
		$('#NameOrHttpEquivLabel').text(label);

		// Title and charset don't require a name, etc so hide it
		var row = $('#heName').closest("tr");
		if (label == "Title") {
			$('#heName').val('');
			row.addClass("hidden");
			$('#ContentLabel').text('Title');
		} else if (label == "Charset") {
			$('#heName').val('');
			row.addClass("hidden");
			$('#labelContent').text('Charset');
		} else if (label == "Http-equiv") {
			row.removeClass("hidden");
			$('#nameDropdown').addClass("hidden");
			$('#httpEquivDropdown').removeClass("hidden");
			$('#linkDropdown').addClass("hidden");
			$('#labelContent').text('Content');
		} else if (label == "Link") {
			row.removeClass("hidden");
			$('#nameDropdown').addClass("hidden");
			$('#httpEquivDropdown').addClass("hidden");
			$('#linkDropdown').removeClass("hidden");
			$('#labelContent').text('Image file path');
		} else { // label = Name
			row.removeClass("hidden");
			$('#nameDropdown').removeClass("hidden");
			$('#httpEquivDropdown').addClass("hidden");
			$('#linkDropdown').addClass("hidden");
			$('#labelContent').text('Content');
		}
	}

	$('#heType').on('change', setLabels);

	//$(window).load(setLabels);


	// Copy users selection to the Name Input element
	// Emulates a dropdown that also allows a user to enter a freeform value
	$('.modal-body').click(function (ev) {
		var name = $(ev.target).closest("li").attr("name");
		$('#HtmlMetaElementPart_Name').val(name);
	});

	$('#createHeadElement').on('click', function () {
		// initialize
		currentRow = null;
		$('#myModal input[name="id"]').val(0);
		$('#heType').val('name');
		$('#heName').val('');
		// enable input
		$('#myModal input').attr('disabled', false);
		$('#myModal select').attr('disabled', false);
		setLabels();
		$('#myModal').modal();
	});

	$('#elementTable').on('click', 'a.elementEdit', function editElement(ev) {
		// edit
		var a = $(ev.target);
		currentRow = a.closest('tr');
		index = $('#elementTable tbody tr').index(currentRow);
		var cells = currentRow[0].children;

		var id = cells[0].innerText;
		var type = cells[1].innerText;
		var name = cells[2].innerText;
		var content = cells[3].innerText;
		$('#myModal input[name="id"]').val(id);
		$('#heType').val(type);
		$('#heName').val(name);
		$('#myModal input[name="content"]').val(content);
		$('#myModal input').attr('disabled', false);
		$('#myModal select').attr('disabled', false);
		setLabels();
		$('#myModal').modal();
	});

	$('#elementTable').on('click', 'a.elementDelete', function editElement(ev) {
		// Delete
		var a = $(ev.target);
		currentRow = a.closest('tr')
		index = $('#elementTable tbody tr').index(currentRow);

		var input = currentRow.find('input[name="' + fieldNameBase + '[' + index + '].Deleted"]');
		input.val(true);

		currentRow.attr('hidden', true);
		currentRow = null;
		index = 0;
	});

	$('#saveElement').on('click', function (ev) {
		saveElement();
	});

	function saveElement() {
		var dialog = $('#myModal');
		var body = $('#elementTable > tbody');

		if (!currentRow) {
			body.append("<tr></tr>");
			bodyElement = body[0];
			index = bodyElement.children.length - 1;
			currentRow = $(bodyElement.children[index]);
		}
		else {
			currentRow.empty();
		}

		var id = $('#myModal input[name="id"]').val() | "0";
		id += '<input type="hidden" name="' + fieldNameBase + '[' + index + '].Id" value="' + id + '" />'

		var headId = $('#Head_Id').val() | "0";
		headId = '<input type="hidden" name="' + fieldNameBase + '[' + index + '].HeadPartRecord_Id" value="' + headId + '" />'

		var type = $('#heType').val();
		type += '<input type="hidden" name="' + fieldNameBase + '[' + index + '].Type" value="' + type + '" />'

		var name = $('#heName').val();
		name += '<input type="hidden" name="' + fieldNameBase + '[' + index + '].Name" value="' + name + '" />'

		var content = $('#myModal input[name="content"]').val();
		content += '<input type="hidden" name="' + fieldNameBase + '[' + index + '].Content" value="' + content + '" />'

		var deleted = '<input type="hidden" name="' + fieldNameBase + '[' + index + '].Deleted" value="false" />'


		var actions = '<a href="#" class="elementEdit" >Edit</a> | <a href="#" class="elementDelete" >Delete</a>';

		currentRow.append("<td class='hidden'>" + id + headId + deleted + "</td><td>" + type + "</td><td>" + name + "</td><td>" + content + "</td><td>" + actions + "</td>");
		$('#myModal input').attr('disabled', true);
		$('#myModal').modal('hide');
	}

	$('#myModal').on('keydown', function (ev) {
		if (ev.which == 13) {
			saveElement();
			ev.preventDefault();
		}
	});

})(jQuery);

