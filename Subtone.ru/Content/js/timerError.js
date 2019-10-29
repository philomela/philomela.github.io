const time = $('.seconds');
intervalId = setInterval(timerDecrement, 1000);

function timerDecrement() {
    const newTime = time.text() - 1;
    time.text(newTime);
    if (newTime === 0) {
        clearInterval(intervalId);
        window.location = "http://www.subtone.ru/Admin/LoginForm";
    };
}