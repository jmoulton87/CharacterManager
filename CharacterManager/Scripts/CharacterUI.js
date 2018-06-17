    //DEBUGGING CONSOLE LOGS
    //console.log("FirstItem: ", FirstItem);
    //console.log("FirstItemID: " + FirstItemID);
    //console.log("FirstBaseItemID: " + FirstBaseItemID);
    //console.log("FirstItemQuan: " + FirstItemQuan);
    //console.log("FirstItemMaxQ: " + FirstItemMaxQ);
    //console.log("FirstLocationID: " + FirstLocationID);
    //console.log("FirstSlotID: " + FirstSlotID);
    //console.log("FirstItemType: " + FirstItemType);

    //console.log("SecondSlot: ", SecondSlot);
    //console.log("SecondItem: " + SecondItem);
    //console.log("SecondItemID: " + SecondItemID);
    //console.log("SecondBaseItemID: " + SecondBaseItemID);
    //console.log("SecondItemQuan: " + SecondItemQuan);
    //console.log("SecondItemMaxQ: " + SecondItemMaxQ);
    //console.log("SecondLocationID: " + SecondLocationID);
    //console.log("SecondSlotID: " + SecondSlotID);
    //console.log("SecondSlotType: " + SecondSlotType);




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


//this function draws out a location table
function drawTable(targetID, x, y) {
    //for each row
    var TargetTable = $('#' + targetID);
    var CellID = 1;
    var LocationID = $('#' + targetID).attr('location-id');
    for (i = 0; i < y; i++) {
        //console.log("for loop through rows: " + i);
        TargetTable.append('<div class="inventory-row"><div>');
        var ThisRow = TargetTable.children().last();
        for (j = 0; j < x; j++) {
            //console.log("for loop through columns: " + j);
            ThisRow.append('<div class="inventory-cell" cellid="' + CellID + '" location-id="' + LocationID + '" item-type="all"></div>');
            CellID++;
        }
    }
}



//draw the grid for locations
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
            if ($(this).attr('item-id') !== null) {
                var slotid = $(this).attr('slot-id');
                var locationid = $(this).attr('location-id');
                $(this).detach().appendTo($('[cellid=' + slotid + '][location-id=' + locationid + ']'));
            }
        }
    });
});



//this function checks if the move an item is making is legal
function legalMoveCheck(
    FirstItem,
    FirstItemID,
    FirstBaseItemID,
    FirstItemQuan,
    FirstItemMaxQ,
    FirstLocationID,
    FirstSlotID,
    FirstItemType,
    SecondSlot,
    SecondItemID,
    SecondBaseItemID,
    SecondItemQuan,
    SecondItemMaxQ,
    SecondLocationID,
    SecondSlotID,
    SecondSlotType) {
    var legalMove = true;

    if (FirstItemID == SecondItemID) {
        console.log("item is being moved into its own slot");
        legalMove = false;
    }

    if (!(SecondSlotType.indexOf(FirstItemType) >= 0 || SecondSlotType == "all")) {
        console.log("item is being moved into a slot with invalid type");
        legalMove = false;
    }

    //if there is a two handed weapon in the primary slot AND the item is being moved into the secondary slot
    if ($('[item-type="onehandedweapon twohandedweapon rangedweapon"]').children(0).attr("item-type") == "twohandedweapon" && SecondSlotType == "onehandedweapon shield ammo") {
        console.log("an item is being moved into the secondary slot with a twohandedweapon equipped in the primary slot");
        legalMove = false;
    }

    //if there is an item in the secondary slot AND the item being moved is a twohanded weapon AND the target slot is the primary slot
    if ($('[item-type="onehandedweapon shield ammo"]').children().length > 0 && FirstItemType == "twohandedweapon" && SecondSlotType == "onehandedweapon twohandedweapon rangedweapon") {
        console.log("a twohandedweapon is being moved into the primary slot while there is an item equipped in the secondary slot");
        legalMove = false;
    }

    return legalMove;
};

function moveQuantity(
    FirstItem,
    FirstItemID,
    FirstBaseItemID,
    FirstItemQuan,
    FirstItemMaxQ,
    FirstLocationID,
    FirstSlotID,
    FirstItemType,
    SecondSlot,
    SecondItemID,
    SecondBaseItemID,
    SecondItemQuan,
    SecondItemMaxQ,
    SecondLocationID,
    SecondSlotID,
    SecondSlotType) {
    console.log("doing a thing");



    var FirstStack, SecondStack;

    //if the combined quantity would result in two stacks
    if ((FirstItemQuan + SecondItemQuan) > FirstItemMaxQ) {
        console.log("thing1");
        FirstStack = (FirstItemQuan + SecondItemQuan) - FirstItemMaxQ;
        SecondStack = FirstItemMaxQ;

        $.ajax({
            url: "/Item/EditQuantities",
            data: {
                FirstItemID: FirstItemID,
                FirstItemNewQuan: FirstStack,
                SecondItemID: SecondItemID,
                SecondItemNewQuan: SecondStack
            },
            success: function () {
                //update the item quantities of the two items
                FirstItem.children(0).text(FirstStack);
                SecondItem.children(0).text(SecondStack);
            }
        });

    }

    //if the conbined quantity would result in one stack
    else if ((FirstItemQuan + SecondItemQuan) <= FirstItemMaxQ) {
        FirstStack = 0
        SecondStack = FirstItemQuan + SecondItemQuan;

        $.ajax({
            url: "/Item/EditQuantities",
            data: {
                FirstItemID: FirstItemID,
                FirstItemNewQuan: FirstStack,
                SecondItemID: SecondItemID,
                SecondItemNewQuan: SecondStack
            },
            success: function () {
                //delete the first item and update the quantity of the second item
                FirstItem.detach();
                SecondItem.children(0).text(SecondStack);
            }
        });
    }
};

$(function() {
    //declare variables

    //FirstItem         0
    //FirstItemID       1
    //FirstBaseItemID   2
    //FirstItemQuan     3
    //FirstItemMaxQ     4
    //FirstLocationID   5
    //FirstSlotID       6
    //FirstItemType     7
    //SecondSlot        8
    //SecondItemID      9
    //SecondBaseItemID  10
    //SecondItemQuan    11
    //SecondItemMaxQ    12
    //SecondLocationID  13
    //SecondSlotID      14
    //SecondSlotType    15

    var ItemVars = [];
    var FirstItem, FirstItemID, FirstBaseItemID, FirstItemQuan, FirstItemMaxQ, FirstLocationID, FirstSlotID, FirstItemType;
    var SecondSlot, SecondItemID, SecondBaseItemID, SecondItemQuan, SecondItemMaxQ, SecondLocationID, SecondSlotID, SecondSlotType;

    //this function resets the position of a element that has been dragged
    function resetPosition(element) {
        element.css('z-index', '');
        element.css('left', '');
        element.css('top', '');
    }


    //this makes some elements draggable
    $(".inventory-item:not(.itemlock)").draggable({
        revert: "invalid",
        cursor: "move",
        cursorAt: { top: 20, left: 20 },
        start: function (event, ui) {
            FirstItem = $(this);

            FirstItemID = FirstItem.attr("item-id");
            FirstBaseItemID = FirstItem.attr("baseitem-id");

            FirstItemQuan = parseInt(FirstItem.children(0).text());
            FirstItemMaxQ = parseInt(FirstItem.attr("max-quantity"));

            FirstLocationID = FirstItem.attr("location-id");
            FirstSlotID = FirstItem.attr("slot-id");
            FirstItemType = FirstItem.attr("item-type");

            FirstItem.css('z-index', 9999);
        }
    });

    //this makes some element droppable
    $(".inventory-cell:not(.itemlock)").droppable({
        accept: ".inventory-item",
        drop: function (event, ui) {
            SecondSlot = $(this);

            SecondItem = SecondSlot.children(0);

            SecondItemID = SecondItem.attr("item-id");
            SecondBaseItemID = SecondItem.attr("baseitem-id");

            SecondItemQuan = parseInt(SecondItem.children(0).text());
            SecondItemMaxQ = parseInt(SecondItem.attr("max-quantity"));

            SecondLocationID = SecondSlot.attr("location-id");
            SecondSlotID = SecondSlot.attr("cellid");
            SecondSlotType = SecondSlot.attr("item-type");




            if (FirstBaseItemID == SecondBaseItemID && FirstItemMaxQ > 1) {
                console.log("doing item quantity move thing");

                moveQuantity(
                    FirstItem,
                    FirstItemID,
                    FirstBaseItemID,
                    FirstItemQuan,
                    FirstItemMaxQ,
                    FirstLocationID,
                    FirstSlotID,
                    FirstItemType,
                    SecondSlot,
                    SecondItemID,
                    SecondBaseItemID,
                    SecondItemQuan,
                    SecondItemMaxQ,
                    SecondLocationID,
                    SecondSlotID,
                    SecondSlotType);
                resetPosition(FirstItem);
            }

            else if (legalMoveCheck(
                FirstItem,
                FirstItemID,
                FirstBaseItemID,
                FirstItemQuan,
                FirstItemMaxQ,
                FirstLocationID,
                FirstSlotID,
                FirstItemType,
                SecondSlot,
                SecondItemID,
                SecondBaseItemID,
                SecondItemQuan,
                SecondItemMaxQ,
                SecondLocationID,
                SecondSlotID,
                SecondSlotType) == true) {




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
                                .appendTo($('[cellid=' + SecondSlotID + '][location-id=' + SecondLocationID + ']'));
                            FirstItem
                                .attr({
                                    "location-id": SecondLocationID,
                                    "slot-id": SecondSlotID
                                });
                            resetPosition(FirstItem);

                            SecondItem
                                .detach()
                                .appendTo($('[cellid=' + FirstSlotID + '][location-id=' + FirstLocationID + ']'));
                            SecondItem.attr({
                                "location-id": FirstLocationID,
                                "slot-id": FirstSlotID
                            });
                        //if the item is moving into an empty cell, move the item
                        } else {
                            FirstItem
                                .detach()
                                .appendTo($('[cellid=' + SecondSlotID + '][location-id=' + SecondLocationID + ']'));
                            FirstItem
                                .attr({
                                    "location-id": SecondLocationID,
                                    "slot-id": SecondSlotID
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



