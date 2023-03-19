$(() => {
    $('#recurrenceCheck').change(() => {
        const inputs = $('#recurrenceInputs')

        if ($('#recurrenceCheck').is(':checked')) {
            $.ajax({
                type: 'GET',
                url: '/Home/Partial',
                data: { viewName: 'Forms/_RecurrenceInputs' },
                success: (result) => {
                    inputs.empty()
                    inputs.html(result)
                }
            })

            return
        }

        inputs.empty()
    })
})