$(document).ready(function () {
	$(document).delegate('#WriteTextBox', 'keydown', function (e) {
		var keyCode = e.keyCode || e.which;

		if (keyCode == 9) {
			e.preventDefault();
			var start = this.selectionStart;
			var end = this.selectionEnd;

			// set textarea value to: text before caret + tab + text after caret
			$(this).val($(this).val().substring(0, start)
				+ "\t"
				+ $(this).val().substring(end));

			// put caret at right position again
			$(this).get(0).selectionStart =
				$(this).get(0).selectionEnd = start + 1;
		}
	});

	$(document).delegate('#WriteCodeBox', 'keydown', function (e) {
		var keyCode = e.keyCode || e.which;

		if (keyCode == 9) {
			e.preventDefault();
			var start = this.selectionStart;
			var end = this.selectionEnd;

			// set textarea value to: text before caret + tab + text after caret
			$(this).val($(this).val().substring(0, start)
				+ "\t"
				+ $(this).val().substring(end));

			// put caret at right position again
			$(this).get(0).selectionStart =
				$(this).get(0).selectionEnd = start + 1;
		}
	});
});