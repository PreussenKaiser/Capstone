$(() => {
    refresh('')

    $('#scheduleFilter').on('change', () => {
        const type = $('#scheduleFilter option:selected').val()

        refresh(type)
    })

    function refresh(type) {
        $.ajax({
            action: 'GET',
            url: '/Schedule/TablePartial',
            data: { type: type },
            success: (result) => {
                const container = $('#scheduleContainer')

                container.empty()
                container.html(result)
            }
        })

    }
})