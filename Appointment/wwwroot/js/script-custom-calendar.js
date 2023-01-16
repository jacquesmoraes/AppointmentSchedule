var routeURL = location.protocol + "//" + location.host;

$(document).ready(function () {

    $("#appointmentdate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false,
        format: "dd/MM/yyyy" 
    });

    InitializeCalendar();
});

function InitializeCalendar() {
    try {

        
        var calendarEl = document.getElementById('calendar');
        if (calendarEl !== null) {

            var calendar = new FullCalendar.Calendar(calendarEl, {

                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev, next, today',
                    center: 'title',
                    right: 'dayGridMonth, timeGridWeek, timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                },
                eventDisplay: 'block',
                event: function (fetchInfo, successCallback, failureCallback) {
                    $.ajax({
                        url: routeURL + '/api/Appointment/GetCalendarData?doctorId=' + $("#doctorId").val(),
                        type: 'GET',
                        DataType: 'JSON',
                        success: function (response) {
                            var event = []

                            if (response.status === 1) {
                                $each(response.dataenum, function (i, data) {
                                    event.push({
                                        title: data.title,
                                        description: data.description,
                                        start: data.startDate,
                                        end: data.endDate,
                                        backgroundColor: data.isDoctorAproved ? "#28a745" : "#dc3545",
                                        borderColor: "#162466",
                                        textColor: "white",
                                        id: data.id

                                    });

                                })
                            }
                            successCallback(event);
                        },
                        error: function (xhr) {
                            $.notify("Error", "error");
                        }
                    });

                }
            });
            calendar.render();
        }
    }
    catch (e) {
        alert(e)
    }
}

function onShowModal(obj, isEventDetail) {
    $('#appointmentInput').modal("show");
}


function onCloseModal() {
    $('#appointmentInput').modal("hide");
}
function onSubmitForm() {
    if (checkValidation()) {

        var requestData = {
            Id: parseInt($("#id").val()),
            Title: $("#title").val(),
            Description: $("#description").val(),
            StartDate: $("#appointmentdate").val(),
            Duration: $("#Duration").val(),
            DoctorId: $("#doctorId").val(),
            PacientId: $("#PacientId").val(),
        };

        $.ajax({
            url: routeURL + '/api/Appointment/SaveCalendarData',
            type: 'POST',
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    $.notify(response.message, "success");
                    onCloseModal();
                }
                else {
                    $.notify(response.message, "error");
                }
            },
            error: function (xhr) {
                $.notify("Error", "error");
            }
        });
    }
}

function checkValidation() {
    var isValid = true;
    if ($("#title").val() === undefined || $("#title").val() == "") {
        isValid = false;
        $("#title").addClass('error');
    }
    else {
        $("#title").removeClass()
    }
    if ($("#appointmentdate").val() === undefined || $("#appointmentdate").val() == "") {
        isValid = false;
        $("#appointmentdate").addClass('error');
    }
    else {
        $("#appointmentdate").removeClass()
    }
    return isValid;
}