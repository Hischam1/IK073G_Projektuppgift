///<reference path="http://ajax.googleapis.com/ajax/libs/jquery/1.3.1/jquery.min.js">

$(document).ready(function () {

    /* Gömmer allt som inte ska visas i start-up*/

    $(".container").hide();
    $("#main_väljPerson").hide();

    /* Visar det som ska presenteras i start-up */

    $(".container").fadeIn("slow");
    $("#main_väljPerson").fadeIn("slow");
});

