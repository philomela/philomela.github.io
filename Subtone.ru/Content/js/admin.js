$('a').click(function () {
    if ($('#' + $(this).attr('id') + '+' + 'ul').css('display') == 'block') {
        $('#' + $(this).attr('id') + '+' + 'ul').css({ 'display': 'none' });
    }
    else {
        $('#' + $(this).attr('id') + '+' + 'ul').css({ 'display': 'block' });
    }
});
