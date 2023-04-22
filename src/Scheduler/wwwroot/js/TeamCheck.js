function isTeamMember() {
    // Make an AJAX request to your controller endpoint
    $.ajax({
        url: '/Dashboard/IsTeamMember',
        type: 'GET',
        data: { Event: data },
        success: function (response) {
            // Handle the success response from the controller, if needed
        },
        error: function (error) {
            // Handle the error response from the controller, if needed
        }
    });
}