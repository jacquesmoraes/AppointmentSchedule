$(document).ready(function () {
    InitializeCalendar();
});

function InitializeCalendar() {
    try {
        $('#calendar').fullCalendar({
            timezone: false,
            header: {
                left: 'prev,next,today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            selectable: true,
            editable: false,
            select: function (event) {
                onShowModel(event, null)
            }

        }); 
    }
    catch (e) {
        alert(e)
    }
}

function onShowModel(obj, isEventDetails) {
    $("#appointmentInput").modal("show")
}

function onCloseModal() {
    $("#appointmentInput").modal("hide")
}