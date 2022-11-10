$(document).ready(function () {
    InitializeCalendar();
});

function InitializeCalendar() {
    try {
        $('#Calendar').fullCalendar({
            timezone: false,
            header: {
                left: 'prev, next, today',
                center: 'title',
                right: 'month, agendaWeek, agendaDay'
            },
            selectable: true,
            editable: false,

        })
    }
    catch (e) {
        alert(e)
    }
}