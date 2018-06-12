//$("#trade").on('click', function () {
//    var destination = $("#tradeSelect option:selected").val();
//    console.log("destination: " + destination);
//    $(".tradeCell").each(function () {
//        if ($(this).children().first().attr('id').indexOf("empty") >= 0) {
//            console.log("cell was empty");
//        } else {
//            var item = $(this).children().first();
//            var ItemID = itemValues(item.attr('id'))[0]; 
//            console.log(item);
//            $(item).detach();
//            $.ajax({
//                url: "/Item/TradeItem",
//                data: { ItemID: ItemID, LocationID: destination },
//                success: function () {
//                    console.log("a thing happened");
//                }
//            });
//        }
//    });
//});

//var HoverID;
//$(".item").hover(function () {
//    //console.log($(this))
//    HoverID = itemValues($(this).attr('id'))[0]; 
//    //console.log(HoverID);
//    $("#" + HoverID).detach().appendTo($(this));
//}, function () {
//    //console.log($(this))
//    $("#" + HoverID).detach().appendTo("#itemCards");

//    });

//var firstclick = false;
//var pickelement, pickitemid, picklocationid, pickslotid, dropelement, dropitemid, droplocationid, dropslotid;
//$(".inventory-table").on("click", ".inventory-cell", function () {
//    //console.log("clicking a cell");
//    //is this first click?
//    if (firstclick === false) {
//        //if the first click is on an item, initate a move
//        if ($(this).children().length > 0) {
//            //get information for the item that was clicked on
//            pickelement = $(this).children(0);
//            pickitemid = pickelement.attr("thisitemid");
//            picklocationid = pickelement.attr("locationid");
//            pickslotid = pickelement.attr("slotid");
//            firstclick = true;
//        } 
//    } else {
//        //if the second click is on an item, swap the items
//        if ($(this).children().length > 0) {
//            //get information for the item that was clicked on
//            dropelement = $(this).children(0);
//            dropitemid = dropelement.attr("thisitemid");
//            droplocationid = dropelement.attr("locationid");
//            dropslotid = dropelement.attr("slotid");
//            if (/*move is legal*/ 0 === 0) {
//                $.ajax({
//                    url: "/Item/MoveItem",
//                    data: {
//                        pickitemid: pickitemid,
//                        picklocationid: picklocationid,
//                        pickslotid: pickslotid,
//                        dropitemid: dropitemid,
//                        droplocationid: droplocationid,
//                        dropslotid: dropslotid
//                    },
//                    success: function () {
//                        pickelement.detach().appendTo($('[cellid=' + dropslotid + '][locationid=' + droplocationid + ']'));
//                        dropelement.detach().appendTo($('[cellid=' + pickslotid + '][locationid=' + picklocationid + ']'));
//                    }
//                });

//            }
//        //else if the second click on an empty space, move the item
//        } else {
//            dropelement = $(this);
//            dropitemid = null;
//            droplocationid = dropelement.attr("locationid");
//            dropslotid = dropelement.attr("cellid");
//            if (/*move is legal*/ 0 === 0) {
//                $.ajax({
//                    url: "/Item/MoveItem",
//                    data: {
//                        pickitemid: pickitemid,
//                        picklocationid: picklocationid,
//                        pickslotid: pickslotid,
//                        dropitemid: dropitemid,
//                        droplocationid: droplocationid,
//                        dropslotid: dropslotid
//                    },
//                    success: function () {
//                        pickelement.detach().appendTo($('[cellid=' + dropslotid + '][locationid=' + droplocationid + ']'));
//                    }
//                });
//            }
//        }
//        //after moving an item or failing a move, reset the firstclick bool
//        firstclick = false;
//    }
//});

//var pickelement, pickitemid, picklocationid, pickslotid;
//var dropelement, dropitemid, droplocationid, dropslotid;





function drawTable(targetID, x, y) {
    //for each row
    var TargetTable = $('#' + targetID);
    var CellID = 1;
    var LocationID = $('#' + targetID).attr('locationid');
    for (i = 0; i < y; i++) {
        //console.log("for loop through rows: " + i);
        TargetTable.append('<div class="inventory-row"><div>');
        var ThisRow = TargetTable.children().last();
        for (j = 0; j < x; j++) {
            //console.log("for loop through columns: " + j);
            ThisRow.append('<div class="inventory-cell" cellid="' + CellID + '" locationid="' + LocationID + '"></div>');
            CellID++;
        }
    }
}

//draw the gril for the equipment location
$('document').ready(function () {
});


//draw the grid for the inventory location
$('document').ready(function () {
    console.log("document ready, drawing inventory grid");
    drawTable('InventoryGrid', 4, 8);

    $(".inventory-bag").each(function () {
        drawTable($(this).attr('id'), 4, 8);
    });


});

//put the items in their place
$("document").ready(function () {
    $(".inventory-item").each(function () {
        if ($(this).hasClass("itemlock") == false) {
            //console.log("found item");
            if ($(this).attr('thisitemid') !== null) {
                var slotid = $(this).attr('slotid');
                var locationid = $(this).attr('locationid');
                $(this).detach().appendTo($('[cellid=' + slotid + '][locationid=' + locationid + ']'));
            }
        }
    });
});

        //pickelement = $(this).parent(),
        //pickitemid = $(this).attr("thisitemid"),
        //picklocationid = $(this).attr("locationid"),
        //pickslotid = $(this).attr("slotid"),
        //dropelement = $(this).parent();
        //dropitemid = $(this).attr("thisitemid");
        //droplocationid = $(this).attr("locationid");
        //dropslotid = $(this).attr("slotid");



//this function forces icons that are being dragged to appear on top of other icons

$(function () {

    var FirstItem, FirstItemID, FirstLocationID, FirstSlotID;
    var CellElement, SecondItemID, SecondLocationID, SecondSlotID;

    function resetPosition(element) {
        element.css('z-index', '');
        element.css('left', '');
        element.css('top', '');
    }

    $(".inventory-item:not(.itemlock)").draggable({
        revert: "invalid",
        //snap: ".inventory-cell",
        //snapMode: "inner",
        cursor: "move",
        cursorAt: { top: 20, left: 20 },
        start: function (event, ui) {
            FirstItem = $(this);
            FirstItemID = $(this).attr("thisitemid");
            FirstLocationID = $(this).attr("locationid");
            FirstSlotID = $(this).attr("slotid");
            FirstItem.css('z-index', 9999);
        }
    });

    $(".inventory-cell:not(.itemlock)").droppable({
        accept: ".inventory-item",
        drop: function (event, ui) {
            DestinationCell = $(this);
            SecondItem = $(this).children(0);
            SecondItemID = $(this).children(0).attr("thisitemid");
            SecondLocationID = $(this).attr("locationid");
            SecondSlotID = $(this).attr("cellid");

            //DEBUGGING CONSOLE LOGS
            //console.log("picked up: ", FirstItem);
            //console.log("first item id: " + FirstItemID);
            //console.log("first item location: " + FirstLocationID);
            //console.log("first item location: " + FirstSlotID);
            //console.log("dropped: ", DestinationCell);
            //console.log("second item id: " + SecondItemID);
            //console.log("destination location: " + SecondLocationID);
            //console.log("destination cell: " + SecondSlotID);

            if (FirstSlotID != SecondSlotID) {
                $.ajax({
                    url: "/Item/MoveItem",
                    data: {
                        FirstItemID: FirstItemID,
                        FirstLocationID: FirstLocationID,
                        FirstSlotID: FirstSlotID,
                        SecondItemID: SecondItemID,
                        SecondLocationID: SecondLocationID,
                        SecondSlotID: SecondSlotID
                    },
                    success: function () {
                        //if item is moving into a cell with another item, switch the items
                        if (SecondItemID != null) {
                            FirstItem
                                .detach()
                                .appendTo($('[cellid=' + SecondSlotID + '][locationid=' + SecondLocationID + ']'));
                            FirstItem
                                .attr({
                                    locationid: SecondLocationID,
                                    slotid: SecondSlotID
                                });
                            resetPosition(FirstItem);

                            SecondItem
                                .detach()
                                .appendTo($('[cellid=' + FirstSlotID + '][locationid=' + FirstLocationID + ']'));
                            SecondItem.attr({
                                locationid: FirstLocationID,
                                slotid: FirstSlotID
                            });
                        //if the item is moving into an empty cell, move the item
                        } else {
                            FirstItem
                                .detach()
                                .appendTo($('[cellid=' + SecondSlotID + '][locationid=' + SecondLocationID + ']'));
                            FirstItem
                                .attr({
                                    locationid: SecondLocationID,
                                    slotid: SecondSlotID
                                });
                            resetPosition(FirstItem);
                        }
                    }
                });
            //if the item has made an invalid move, reset position without AJAX call
            } else {
                resetPosition(FirstItem);
            }
        }
    });

});



