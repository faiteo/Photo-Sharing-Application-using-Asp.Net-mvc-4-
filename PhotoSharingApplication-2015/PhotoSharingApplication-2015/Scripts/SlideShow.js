var percentIncrement;
var percentCurrent = 100;

function slideSwitch() {
    //Increase the progress bar
    percentCurrent += percentIncrement;
    if (percentCurrent > 100) {
        percentCurrent = percentIncrement;
    }
    $('#slideshow-progress-bar').progressbar({
        value: percentCurrent
    });

    //Get the currently displayed image and the next one
    var $activeCard = $('#slide-show DIV.active-card');
    //if there is no active one, we'll take the first one
    if ($activeCard.length == 0) {
        $activeCard = $('#slide-show DIV.slide-show-card:last')
    }

    //Get the next image, unless the current one is the last one, in which case get the first image
    var $nextCard = $activeCard.next().length ? $activeCard.next() : $('#slide-show DIV.slide-show-card:first');

    //Set classes and animate the fade
    $activeCard.addClass('last-active-card');
    $nextCard.css({ opacity: 0.0 })
        .addClass('active-card')
        .animate({ opacity: 1.0 }, 1000, function () {
            //The animation has finished so remove the classes from the old image
            $activeCard.removeClass('active-card last-active-card');
        });
}

function calculateIncrement() {
    var cardCount = $('#slide-show DIV.slide-show-card').length;
    percentIncrement = 100 / cardCount;
    $('#slideshow-progress-bar').progressbar({
        value: 100
    });
}

$(document).ready(function () {
    calculateIncrement();
    //Change the slide every 5 seconds
    setInterval("slideSwitch()", 5000);
});