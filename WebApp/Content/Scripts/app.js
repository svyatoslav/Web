$(document).ready(function () {
    $('input[type=checkbox]').change(function (e) {
        var checkedUsers = $('input[type=checkbox]:checked');
        var arr = "";
        for (var i = 0; i < checkedUsers.length; i++) {
            arr += $(checkedUsers[i]).attr('id') + ';';
        }
        $('#checked').val(arr);
    })
});