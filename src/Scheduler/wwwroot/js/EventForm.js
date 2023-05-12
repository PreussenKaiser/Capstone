/**
 * Provides navigation for the EventForm.
 * @param {string} eventType The type of event to select.
 */
function navClick(eventType) {
    const eventInputs = $('#eventInputs')
    const blackoutInputs = $('#blackoutInputs')

    const refreshEventInputs = eventType == 'Event'
        ? eventInputs.empty()
        : (result) => {
            eventInputs.empty()
            eventInputs.html(result);
        }

    const refreshBlackoutInputs = eventType != 'Event'
        ? blackoutInputs.empty()
        : (result) => {
            blackoutInputs.empty()
            blackoutInputs.html(result)
        }

    $.ajax({
        type: 'GET',
        url: '/Schedule/RenderInputs',
        data: { type: eventType},
        success: refreshEventInputs, refreshNav(eventType)
    })

    $.ajax({
        type: 'GET',
        url: '/Schedule/RenderInputs',
        data: { type: 'Blackout' },
        success: refreshBlackoutInputs
    })
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