$("document").ready(function () {
    $("#tabs").tabs();
});

//$("document").ready(function () {
//    $("#accordion").accordion({
//        collapsible: true
//    });
//});

//this function takes the id of an item
//it returs the ItemID, SlotID, and LocationID
function itemValues(valueString) {
    var values = valueString.split(" ");
    //console.log(values);
    return values;
}

//this function takes the id of a slot
//it returs the SlotID, and LocationID
function cellValues(valueString) {
    var values = valueString.split("_");
    //console.log(values);
    return values;
}




//var clickItem = false;
//var slot1, slot2, item1, item2;
//$(".inventoryCell").on('click', function () {
//    if (clickItem === false) {
//        clickItem = true;
//        slot1 = $(this);
//        item1 = $(this).children(0);
//        $(slot1).css('border-color', 'Chartreuse');
//    } else {
//        slot2 = $(this);
//        item2 = $(this).children(0);
//        //console.log("doing a thing");
//        //console.log(item1.attr('id'))
//        //console.log(item2.attr('id'))
//        //console.log(slot1.attr('id'))
//        //console.log(slot2.attr('id'))

//        if (item1 !== item2) {
//            $(item1).detach().appendTo(slot2);
//            $(item2).detach().appendTo(slot1);
//            $(slot1).css('border-color', 'black');
//        }

//        if (item1.attr('id').indexOf("empty") >= 0) {
//            //console.log("item 1 was empty")
//        } else {
//            //var ItemID = itemValues(item1.attr('id'))[0];
//            //var newSlot = cellValues(slot2.attr('id'))[1];
//            //var newLoc = cellValues(slot2.attr('id'))[2];
//            //console.log(ItemID, newSlot, newLoc);
//            $.ajax({
//                url: "/Item/MoveItem",
//                data: { ItemID: itemValues(item1.attr('id'))[0], Slot: cellValues(slot2.attr('id'))[1], LocationID: cellValues(slot2.attr('id'))[2] },
//                success: function () {
//                    //console.log("a thing happened");
//                }
//            });
//        }
//        if (item2.attr('id').indexOf("empty") >= 0) {
//            //console.log("item 2 was empty")
//        } else {
//            //var ItemID = itemValues(item1.attr('id'))[0];
//            //var newSlot = cellValues(slot2.attr('id'))[1];
//            //var newLoc = cellValues(slot2.attr('id'))[2];
//            $.ajax({
//                url: "/Item/MoveItem",
//                data: { ItemID: itemValues(item1.attr('id'))[0], Slot: cellValues(slot2.attr('id'))[1], LocationID: cellValues(slot2.attr('id'))[2] },
//                success: function () {
//                    //console.log("a thing happened");
//                }
//            });
//        }
//        clickItem = false;
//        slot1 = null;
//        slot2 = null;
//        item1 = null;
//        item2 = null;
//    }
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


function drawTable(targetID, x, y) {
    //for each row
    var TargetTable = $('#' + targetID);
    var CellID = 1;
    for (i = 0; i < y; i++) {
        //console.log("for loop through rows: " + i);
        TargetTable.append('<div class="inventory-row"><div>');
        var ThisRow = TargetTable.children().last();
        for (j = 0; j < x; j++) {
            //console.log("for loop through columns: " + j);
            ThisRow.append('<div class="inventory-cell" CellID="' + CellID + '"></div>');
            CellID++;
        }
    }
}

//draw the grid for the inventory location
$('document').ready(function () {
    console.log("document ready, drawing inventory grid");
    drawTable('InventoryGrid', 10, 5);
});

//put the items in their place
$("document").ready(function () {
    $(".inventory-item").each(function () {
        console.log("found item")
        if ($(this).attr('thisitemid') !== null) {
            var slotid = $(this).attr('slotid');
            $(this).detach().appendTo($('[cellid=' + slotid + ']'));
        }
    });
});
