$(function () {
    $('#StartDate').daterangepicker({
        timePicker: true,
        startDate: moment().startOf('hour'),
        endDate: moment().startOf('hour').add(32, 'hour'),
        timePickerIncrement: 30,
        locale: {
            format: 'M/DD hh:mm A'
        }
    }).on('apply.daterangepicker', function (ev, picker) {
        var startDate = picker.startDate.format('M/DD/YYYY hh:mm A');
        var endDate = picker.endDate.format('M/DD/YYYY hh:mm A');

        $('#StartDate').val(startDate);
        $('#EndDate').val(endDate);
    });
});

