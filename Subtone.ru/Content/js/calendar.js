var mapTimes = new Map();
var selectedStudio;
var selectDate;
var responseFreeTimeByDate;
var selectTime;

$("#datepicker").datepicker({
    firstDay: 1,
    inline: true,
    dateFormat: 'yyyy-mm-dd',
    minDate: 0,
    beforeShowDay: $.datepicker.noWeekends,
    onSelect: function () {
        var date = $(this).datepicker('getDate');
        selectDate = date.getFullYear() + '-' + ('0' + (date.getMonth() + 1)).slice(-2) + '-' + ('0' + date.getDate()).slice(-2);
        if (selectedStudio != null && selectDate != null) {
            GetFreeTimeByDate(selectDate);
        }
    }
});

function GetFreeTimeByDate(selectDate) {
    $.when(
        $.getJSON("/Home/Calendar/" + selectDate + "-" + selectedStudio)
    ).done(function (json) {
        responseFreeTimeByDate = json;
        ShowFreeTimeByDate();
        resetStyleCSS();
    });
};

function ShowFreeTimeByDate() {
    GenerateFreeTimes(responseFreeTimeByDate);
    if (responseFreeTimeByDate != undefined && responseFreeTimeByDate != null) {
        $('.time_free').css({
            'display': 'block',
        });
        var $page = $('html, body');
        $page.animate({
            scrollTop: $('#booking_form').offset().top
        }, 600);
        return false;
    }
    else {
    }
};

function GenerateFreeTimes(BusyTimes) {
    var arrayBusyTimes = $.parseJSON(BusyTimes);
    $('.td_choise').each(function (index, element) {
        $(this).prop('disabled', false);
        $(this).css({
            'background-color': 'rgb(25, 25, 25)',
        });
        for (j = 0; j < arrayBusyTimes.BusyDatesTimes.length; j++) {
            if ($(element).attr('id') == arrayBusyTimes.BusyDatesTimes[j]) {
                $(this).prop('disabled', true);
                $(this).css({
                    'background-color': '#fa5252',
                });
            }
        }
    });
}

$('.td_choise').click(function () {
    mapTimes.set($(this).attr('id'), $(this).attr('id'));
    if ($(this).css('background-color') == 'rgb(25, 25, 25)') {
        $(this).css({ 'background-color': 'rgb(0, 128, 0)' });
        $('.booking_submit').removeAttr('disabled', 'disabled');
    }
    else if ($(this).css('background-color') == 'rgb(0, 128, 0)') {
        $(this).css({ 'background-color': 'rgb(25, 25, 25)' });
        mapTimes.delete($(this).attr('id'));
        if (mapTimes.size < 1) {
            $('.booking_submit').attr('disabled', 'disabled');
        }
        $('.popup_booking').css({
            'display': 'none',
            'height': '0px'
        });
    }
});

$('#finish_booking').click(function () {
    var arrayInputsValues = [$('.Name_Person'), $('.Surname_Person'), $('.Phone_Person'), $('.Email_Person')];
    for (i = 0; i < arrayInputsValues.length; i++) {
        if (arrayInputsValues[i].val() != null && arrayInputsValues[i].val() != '' && arrayInputsValues[i].val() != undefined) {
            continue;
        }
        else {
            $('.form_inputs form').css({ 'border': '1px solid red', 'padding': '5px' });
            $('.info_check').css({ 'display': 'block' });
            var $page = $('html, body');
            $page.animate({
                scrollTop: $('#form_inp').offset().top
            }, 600);
            return false;
        }
        boolCheck = true;
    };
    var dataReservation = new Date(selectDate);
    dataReservation = dateFormat(dataReservation, 'yyyy-mm-dd');
    $('#date').val(dataReservation);
    $('.studioId').val(selectedStudio);

    var stringTimeBusy = '';
    mapTimes.forEach(function (item) {
        stringTimeBusy += item + ';';
    });
    $('.timeBusy').val(stringTimeBusy);
});

$('.Name_Person, .Surname_Person, .Phone_Person, .Email_Person').click(function () {
    $('.form_inputs form').css({ 'border': 'none' });
    $('.info_check').css({ 'display': 'none' });
});

$('#select_studioOne').click(function () {
    selectedStudio = 's1';
});

$('#select_studioTwo').click(function () {
    selectedStudio = 's2';
});

$('#select_studioThree').click(function () {
    selectedStudio = 's3';
});

$('#select_studioOne, #select_studioTwo, #select_studioThree').click(function () {
    resetStyleCSS();
    $('.ui-datepicker').css({ 'pointer-events': 'auto' });
    if ($(this).css('color') != 'rgb(238, 238, 238)') {
        $('.studios>button').css({ 'color': 'rgb(150, 150, 150)' })
        $(this).css({ 'color': 'rgb(238, 238, 238)' })
    }
});

$('.booking_submit').click(function () {
    if ($('.popup_booking').css('display') == 'none') {
        $('.popup_booking').css({
            'display': 'block',
        });
        $('.popup_booking').animate({ 'height': '420px' }, 1000);
    }
    else {
        $('.popup_booking').css({
            'display': 'none',
            'height': '0px'
        });
    }
});

$('#send_feedback').click(function () {
    if ($('.name_sendor').val() == "" || $('.email_form').val() == "") {
        $('.comments_box form').css({
            'border': '1px solid red',
            'padding': '5px'
        });
        $('.info_check_feedback').css({
            'display': 'block'
        })
        return false;
    }
});

$('.name_sendor, .email_form, .comment_message').click(function () {
    $('.comments_box form').css({ 'border': 'none' });
    $('.info_check_feedback').css({ 'display': 'none' });
});

$('.slogan button').click(function () {
    var $page = $('html, body');
    $page.animate({
        scrollTop: $('#booking_cal').offset().top
    }, 600);
});

function resetStyleCSS() {
    $('.ui-datepicker').css({ 'color': 'rgba(238, 238, 238, 1.0)' });
    $('.ui-datepicker-calendar').css({ 'color': 'rgba(238, 238, 238, 1.0)' });
    $('td.ui-datepicker-week-end').css({ 'color': 'rgba(250, 82, 82, 1.0)' });
}
