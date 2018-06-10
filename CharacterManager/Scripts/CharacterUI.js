$("document").ready(function () {
    $("#tabs").tabs();
});

//$("document").ready(function () {
//    $("#accordion").accordion({
//        collapsible: true
//    });
//});

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

var firstclick = false;
var pickelement, pickitemid, picklocationid, pickslotid, dropelement, dropitemid, droplocationid, dropslotid;
$(".inventory-table").on("click", ".inventory-cell", function () {
    //console.log("clicking a cell");
    //is this first click?
    if (firstclick === false) {
        //if the first click is on an item, initate a move
        if ($(this).children().length > 0) {
            //get information for the item that was clicked on
            pickelement = $(this).children(0);
            pickitemid = pickelement.attr("thisitemid");
            picklocationid = pickelement.attr("locationid");
            pickslotid = pickelement.attr("slotid");
            firstclick = true;
        } 
    } else {
        //if the second click is on an item, swap the items
        if ($(this).children().length > 0) {
            //get information for the item that was clicked on
            dropelement = $(this).children(0);
            dropitemid = dropelement.attr("thisitemid");
            droplocationid = dropelement.attr("locationid");
            dropslotid = dropelement.attr("slotid");
            if (/*move is legal*/ 0 === 0) {
                $.ajax({
                    url: "/Item/MoveItem",
                    data: {
                        pickitemid: pickitemid,
                        picklocationid: picklocationid,
                        pickslotid: pickslotid,
                        dropitemid: dropitemid,
                        droplocationid: droplocationid,
                        dropslotid: dropslotid
                    },
                    success: function () {
                        pickelement.detach().appendTo($('[cellid=' + dropslotid + '][locationid=' + droplocationid + ']'));
                        dropelement.detach().appendTo($('[cellid=' + pickslotid + '][locationid=' + picklocationid + ']'));
                    }
                });

            }
        //else if the second click on an empty space, move the item
        } else {
            dropelement = $(this);
            dropitemid = null;
            droplocationid = dropelement.attr("locationid");
            dropslotid = dropelement.attr("cellid");
            if (/*move is legal*/ 0 === 0) {
                $.ajax({
                    url: "/Item/MoveItem",
                    data: {
                        pickitemid: pickitemid,
                        picklocationid: picklocationid,
                        pickslotid: pickslotid,
                        dropitemid: dropitemid,
                        droplocationid: droplocationid,
                        dropslotid: dropslotid
                    },
                    success: function () {
                        pickelement.detach().appendTo($('[cellid=' + dropslotid + '][locationid=' + droplocationid + ']'));
                    }
                });
            }
        }
        //after moving an item or failing a move, reset the firstclick bool
        firstclick = false;
    }
});



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
});

//put the items in their place
$("document").ready(function () {
    $(".inventory-item").each(function () {
        //console.log("found item");
        if ($(this).attr('thisitemid') !== null) {
            var slotid = $(this).attr('slotid');
            var locationid = $(this).attr('locationid');
            $(this).detach().appendTo($('[cellid=' + slotid + '][locationid=' + locationid + ']'));
        }
    });
});
