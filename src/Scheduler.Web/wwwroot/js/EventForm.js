/**
 * Provides navigation for the EventForm.
 * @param {string} eventType The type of event to select.
 */
function navClick(eventType) {
    const refresh = eventType == 'Event'
        ? $('#EventInputs').empty()
        : (result) => {
            $('#EventInputs').empty();
            $('#EventInputs').html(result);
        }

    $.ajax({
        type: 'GET',
        url: '../Schedule/EventPartial',
        data: { type: eventType },
        success: refresh
    })

    refreshNav(eventType)
}

/**
 * Refreshes navbar to show the current event type as active.
 * @param {any} eventType The type of event to show as active.
 */
function refreshNav() {
    console.log('hi')

    $('#Event').removeClass('active')
    $('#Practice').removeClass('active')
    $('#Game').removeClass('active')

    $(`#${eventType}`).addClass('active')
}

document.onload = refreshNav("Event")