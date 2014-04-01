/// <reference path="jquery-1.4.1-vsdoc.js" />
$(function () {
    $(".delete-action").click(function () {
        return confirm("Ta bort " + this.getAttribute("data-type") + 
            " '" + this.getAttribute("data-value") + "' permanent?");
    });
});