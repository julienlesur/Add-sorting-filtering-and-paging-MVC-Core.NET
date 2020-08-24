$(document).ready(function () {
    $(document).on("keypress", ".input-search-text", function (e) { submitFormWhenEnter(e); });
    $(document).on("click", ".btn-cancel-search", cancelSearch);
});

function cancelSearch() {
    debugger
    if ($('input.input-search-text').val() != '') {
        $('input.input-search-text').val('');
        $('form.form-search').submit();
    }
}

function submitFormWhenEnter(e) {
    debugger;
    if (e.which === 13) {
        e.preventDefault();
        $('form.form-search').submit();
        return false;
    }
}