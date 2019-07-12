// if div exist, clear form
if ($('div.alert-success').length) {
    clearform();
}

function clearform() {
    $(':input').not(':submit, :input[name="__RequestVerificationToken"]').val('');
}