﻿$("document").ready(function () {
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
};

//this function takes the id of a slot
//it returs the SlotID, and LocationID
function cellValues(valueString) {
    var values = valueString.split("_");
    //console.log(values);
    return values;
};


$("document").ready(function () {
    $(".item").each(function () {
        if ($(this).attr('id') != null) {
            var valueString = $(this).attr('id')
            var values = itemValues(valueString);
            $(this).detach().appendTo('#cell_' + values[1] + "_" + values[2]);
        }
    });

    var i=0
    $(".inventoryCell").each(function () {
        //console.log("doing a thing");
        if ($(this).children().length == 0) {
            $(this).append('<div id=empty_' + i + ' class="item empty"><img class="itemIcon" src="/Content/Icons/empty.png" alt="Empty" height="40" width="40" /></div>');
        }
        i++;
    });
});


var clickItem = false;
var slot1, slot2, item1, item2;
$(".inventoryCell").on('click', function () {
    if (clickItem == false) {
        clickItem = true;
        slot1 = $(this);
        item1 = $(this).children(0);
        $(slot1).css('border-color', 'Chartreuse');
    } else {
        slot2 = $(this);
        item2 = $(this).children(0);
        //console.log("doing a thing");
        //console.log(item1.attr('id'))
        //console.log(item2.attr('id'))
        //console.log(slot1.attr('id'))
        //console.log(slot2.attr('id'))

        if (item1 != item2) {
            $(item1).detach().appendTo(slot2);
            $(item2).detach().appendTo(slot1);
            $(slot1).css('border-color', 'black');
        }

        if (item1.attr('id').indexOf("empty") >= 0) {
            //console.log("item 1 was empty")
        } else {
            var ItemID = itemValues(item1.attr('id'))[0]
            var newSlot = cellValues(slot2.attr('id'))[1]
            var newLoc = cellValues(slot2.attr('id'))[2]
            //console.log(ItemID, newSlot, newLoc);
            $.ajax({
                url: "/Item/MoveItem",
                data: { ItemID: ItemID, Slot: newSlot, LocationID: newLoc },
                success: function () {
                    //console.log("a thing happened");
                }
            })
        }
        if (item2.attr('id').indexOf("empty") >= 0) {
            //console.log("item 2 was empty")
        } else {
            var ItemID = itemValues(item1.attr('id'))[0]
            var newSlot = cellValues(slot2.attr('id'))[1]
            var newLoc = cellValues(slot2.attr('id'))[2]
            $.ajax({
                url: "/Item/MoveItem",
                data: { ItemID: ItemID, Slot: newSlot, LocationID: newLoc },
                success: function () {
                    //console.log("a thing happened");
                }
            })
        }
        clickItem = false;
        slot1 = null;
        slot2 = null;
        item1 = null;
        item2 = null;
    }
})

$("#trade").on('click', function () {
    var destination = $("#tradeSelect option:selected").val();
    console.log("destination: " + destination);
    $(".tradeCell").each(function () {
        if ($(this).children().first().attr('id').indexOf("empty") >= 0) {
            console.log("cell was empty")
        } else {
            var item = $(this).children().first();
            var ItemID = itemValues(item.attr('id'))[0] 
            console.log(item);
            $(item).detach()
            $.ajax({
                url: "/Item/TradeItem",
                data: { ItemID: ItemID, LocationID: destination },
                success: function () {
                    console.log("a thing happened");
                }
            })
        }
    });
});

var HoverID;
$(".item").hover(function () {
    //console.log($(this))
    HoverID = itemValues($(this).attr('id'))[0] 
    //console.log(HoverID);
    $("#" + HoverID).detach().appendTo($(this));
}, function () {
    //console.log($(this))
    $("#" + HoverID).detach().appendTo("#itemCards");

});