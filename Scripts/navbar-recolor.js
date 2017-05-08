$(document).ready(function () {
	console.log(window.location.pathname);
	// Newsfeed link
	if (window.location.pathname === "/NewsFeed") {
		$("#navbar-feed").addClass('current-page');
		console.log($("#navbar-feed").hasClass('current-page'));
	}
	// Own profile link
	else if (window.location.pathname === "/UserProfile") {
		$("#navbar-profile").addClass('current-page');
	}
	// Group list link
	else if (window.location.pathname === "/GroupList") {
		$("#navbar-groups").addClass('current-page');
	}
});

// Cleanses URL path of its query strings
function removeQuery(path) {
	if (path.indexOf("?") != -1) {
		return path.substring(0, path.indexOf("?"));
	}
}