$(document).ready(function () {
    $(document).on("click", "#icon-create", showRowNewApplication);
    $(document).on("click", ".btn-cancel-edit", removeEditRow);
    $(document).on("submit", ".form-application", function (event) {
        event.preventDefault();
        createApplication($(this));
    });
    $(document).on("submit", ".delete-application", function (event) {
        event.preventDefault();
        deleteApplication($(this));
    });
});

function showRowNewApplication() {
    var newApplication = $('.sample-edit-row').clone();
    var form = newApplication.find('#form-application');
    debugger
    var applicantId = form.find('input#ApplicantId').val();
    $.get('/Applications/GetApplicantJobsWithoutApplication/' + applicantId,
        function (items) {
            var options = '';
            $.each(items, function (index, item) {
                options += "<option value='" + item.value + "'>" + item.text + "</option>";
            });
            var selectJob = form.find('select#JobId');
            selectJob.append(options);

            $('#table-applications tbody').prepend(newApplication);
    });

}

function createApplication($form) {
    var json = $form.serialize();

    $.ajax({
        url: $form.attr('action'),
        type: $form.attr('method'),
        data: json
    }).done(function (data) {
        displayRowApplication(data);
    });
}

function displayRowApplication(addedApplication) {
    var newRow = $('.sample-row').clone();
    newRow.removeClass('sample-row');
    newRow.find('job-title').replaceWith(addedApplication.jobTitle);
    newRow.find('job-reference').replaceWith(addedApplication.jobReference);
    newRow.find('application-date').replaceWith(addedApplication.applicationDate);
    var deleteForm = newRow.find('#delete-application');
    deleteForm.append('<input type="hidden" name="JobId" value="' + addedApplication.jobId + '" />');

    removeNoDataRow();
    removeEditRow();
    $('#table-applications tbody').prepend(newRow);
}

function deleteApplication($form) {
    debugger
    var json = $form.serialize();

    $.ajax({
        url: $form.attr('action'),
        type: $form.attr('method'),
        data: json
    }).done(function (data) {
        $form.closest('tr').remove();
    });

}

function removeNoDataRow() {
    $('#table-applications tr.no-data-row').remove();
} 

function removeEditRow() {
    $('#table-applications .sample-edit-row select#JobId').empty();
    $('#table-applications .sample-edit-row').remove();
} 

function FillSelectJobs(applicantId) {
    $.ajax({
        url: '/Applications/GetApplicantJobsWithoutApplication/' + applicantId,
        type: 'get'
    }).done(function (data) {
        debugger;
        alert(options);
    });
}