$(() => {
    $('#scheduleFilter').on('change', () => {
        const type = $('#scheduleFilter option:selected').val()

        $.ajax({
            action: 'GET',
            url: '../Schedule/TablePartial',
            data: { type: type },
            success: (result) => {
                $('#scheduleContainer').empty()
                $('#scheduleContainer').html(result)
            }
        })
    })
})