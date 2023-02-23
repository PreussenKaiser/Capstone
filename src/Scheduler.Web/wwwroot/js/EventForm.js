function navClick(eventType) {
    const refresh = eventType == 'Event'
        ? $('#EventInputs').empty()
        : (result) => {
            $('#EventInputs').empty();
            $('#EventInputs').html(result);
        }

    $.ajax({
        type: 'GET',
        url: `../Schedule/EventPartial`,
        data: { type: eventType },
        success: refresh
    })

    $('#Event').removeClass('active')
    $('#Practice').removeClass('active')
    $('#Game').removeClass('active')

    $(`#${eventType}`).addClass('active')

    $('#EventForm').attr('action', `../${eventType}/Create`)
}