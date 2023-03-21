/**
 * Provides navigation for the EventForm.
 * @param {string} eventType The type of event to select.
 */
function navClick(eventType) {
    const eventInputs = $('#eventInputs')

    const refreshInputs = eventType == 'Event'
        ? eventInputs.empty()
        : (result) => {
            eventInputs.empty();
            eventInputs.html(result);
        }

    $.ajax({
        type: 'GET',
        url: '/Home/Partial',
        data: { viewName: `Forms/_${eventType}Inputs`},
        success: refreshInputs
    })

    refreshNav(eventType)
}

/**
 * Refreshes navbar to show the current event type as active.
 * @param {string} eventType The type of event to show as active.
 */
function refreshNav(eventType) {
    $('#Event').removeClass('active')
    $('#Practice').removeClass('active')
    $('#Game').removeClass('active')

    $(`#${eventType}`).addClass('active')

    $('#eventForm').attr('action', `../${eventType}/Schedule`)

    const title = $('#title')

    title.empty()
    title.html(eventType)
}