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

//this function draws out a inventory table for a given location
function drawTable(targetID, x, y) {
    var TargetTable = $('#' + targetID);
    var CellID = 1;
    var LocationID = $('#' + targetID).attr('location-id');
    for (i = 0; i < y; i++) {
        TargetTable.append('<div class="inventory-row"><div>');
        var ThisRow = TargetTable.children().last();
        for (j = 0; j < x; j++) {
            ThisRow.append('<div class="inventory-cell" cellid="' + CellID + '" location-id="' + LocationID + '" item-type="all"></div>');
            CellID++;
        }
    }
}

//for each location on the page, call drawTable() to draw its grid
$('document').ready(function () {
    drawTable('InventoryGrid', 4, 8);
    $(".inventory-bag").each(function () {
        drawTable($(this).attr('id'), 4, 8);
    });
});

//put the items in their place
$("document").ready(function () {
    $(".inventory-item").each(function () {
        if ($(this).hasClass("itemlock") == false) {
            if ($(this).attr('item-id') !== null) {
                var slotid = $(this).attr('slot-id');
                var locationid = $(this).attr('location-id');
                $(this).detach().appendTo($('[cellid=' + slotid + '][location-id=' + locationid + ']'));
            }
        }
    });
});

//this function checks if the move an item is making is legal
function legalMoveCheck(ItemVars) {
    var legalMove = true;

    //if an item is being moved into its own slot
    if (ItemVars.FirstItemID == ItemVars.SecondItemID) {
        console.log("item is being moved into its own slot");
        legalMove = false;
    }

    //if an item is NOT being moved into a matching slot or an all-type slot
    if (!(ItemVars.SecondSlotType.indexOf(ItemVars.FirstItemType) >= 0 || ItemVars.SecondSlotType == "all")) {
        console.log("item is being moved into a slot with invalid type");
        legalMove = false;
    }

    //if there is a two handed weapon in the primary slot AND the item is being moved into the secondary slot
    if ($('[item-type="onehandedweapon twohandedweapon rangedweapon"]').children(0).attr("item-type") == "twohandedweapon" && ItemVars.SecondSlotType == "onehandedweapon shield ammo") {
        console.log("an item is being moved into the secondary slot with a twohandedweapon equipped in the primary slot");
        legalMove = false;
    }

    //if there is an item in the secondary slot AND the item being moved is a twohanded weapon AND the target slot is the primary slot
    if ($('[item-type="onehandedweapon shield ammo"]').children().length > 0 && ItemVars.FirstItemType == "twohandedweapon" && ItemVars.SecondSlotType == "onehandedweapon twohandedweapon rangedweapon") {
        console.log("a twohandedweapon is being moved into the primary slot while there is an item equipped in the secondary slot");
        legalMove = false;
    }

    return legalMove;
};

//this function is called when a stackable item is being moved into another stackable item of the same type
function moveQuantity(ItemVars) {
    var FirstStack, SecondStack;

    //if the combined quantity would result in two stacks
    if ((ItemVars.FirstItemQuan + ItemVars.SecondItemQuan) > ItemVars.FirstItemMaxQ) {
        FirstStack = (ItemVars.FirstItemQuan + ItemVars.SecondItemQuan) - ItemVars.FirstItemMaxQ;
        SecondStack = ItemVars.FirstItemMaxQ;

        $.ajax({
            url: "/Item/EditQuantities",
            data: {
                FirstItemID: ItemVars.FirstItemID,
                FirstItemNewQuan: FirstStack,
                SecondItemID: ItemVars.SecondItemID,
                SecondItemNewQuan: SecondStack
            },
            success: function () {
                //update the item quantities of the two items
                ItemVars.FirstItem.children(0).text(FirstStack);
                ItemVars.SecondItem.children(0).text(SecondStack);
            }
        });
    }

    //if the combined quantity would result in one stack
    else if ((ItemVars.FirstItemQuan + ItemVars.SecondItemQuan) <= ItemVars.FirstItemMaxQ) {
        FirstStack = 0
        SecondStack = ItemVars.FirstItemQuan + ItemVars.SecondItemQuan;

        $.ajax({
            url: "/Item/EditQuantities",
            data: {
                FirstItemID: ItemVars.FirstItemID,
                FirstItemNewQuan: FirstStack,
                SecondItemID: ItemVars.SecondItemID,
                SecondItemNewQuan: SecondStack
            },
            success: function () {
                //delete the first item and update the quantity of the second item
                ItemVars.FirstItem.detach();
                ItemVars.SecondItem.children(0).text(SecondStack);
            }
        });
    }
};


//this function resets the position of a element that has been dragged
function resetPosition(element) {
    element.css('z-index', '');
    element.css('left', '');
    element.css('top', '');
}



var ItemVars = new Object();

$(function () {
    //declare variables


    $(".inventory-item:not(.itemlock)").on("dblclick", function (e) {
        if ($(this).children(0).text() > 1) {
            var ItemVars = new Object();

            console.log("double click!");
            ItemVars.FirstItem = $(this);
            ItemVars.FirstItemID = ItemVars.FirstItem.attr("item-id");
            ItemVars.FirstBaseItemID = ItemVars.FirstItem.attr("baseitem-id");
            ItemVars.FirstItemQuan = parseInt(ItemVars.FirstItem.children(0).text());
            ItemVars.FirstItemMaxQ = parseInt(ItemVars.FirstItem.attr("max-quantity"));
            ItemVars.FirstLocationID = ItemVars.FirstItem.attr("location-id");
            ItemVars.FirstSlotID = ItemVars.FirstItem.attr("slot-id");
            ItemVars.FirstItemType = ItemVars.FirstItem.attr("item-type");

            $('#quantity-picker')
                .css({
                    "left": e.pageX + 'px',
                    "top": e.pageY + 'px'
                })
                .removeClass('hidden');


            var select = $("#quantity-picker-number");
            select.attr("max", ItemVars.FirstItemQuan)
            select.val(ItemVars.FirstItemQuan);

            var slider = $("#quantity-picker-slider").slider({
                min: 1,
                max: ItemVars.FirstItemQuan,
                value: ItemVars.FirstItemQuan,

                slide: function (event, ui) {
                    select.val(ui.value);
                }
            });

            $("#quantity-picker-number").on("change", function () {
                slider.slider("value", this.value);
            });


            $("#quantity-picker-cancel").on('click', function () {
                //hide the quantity picker
                $('#quantity-picker')
                    .css('z-index', '')
                    .css('left', '')
                    .css('top', '')
                    .addClass('hidden');
            });

            $("#quantity-picker-button").on('click', function () {
                console.log("did a thingyyyyyy");
                //hide the quantity picker
                $('#quantity-picker')
                    .css('z-index', '')
                    .css('left', '')
                    .css('top', '')
                    .addClass('hidden');

                var SplitQuan = $("#quantity-picker-number").val();

                if ((SplitQuan != ItemVars.FirstItemMaxQ) && (SplitQuan != 0)) {
                    $.ajax({
                        type: "GET",
                        url: "/Item/SplitQuantity",
                        data: {
                            ItemID: ItemVars.FirstItemID,
                            SplitQuan: SplitQuan
                        },
                        dataType: "text",
                        success: function (data) {
                            console.log("did a thingy");
                            var newItemData = JSON.parse(data);
                            var newItemElement = ItemVars.FirstItem.clone();
                            newItemElement
                                .attr("item-id", newItemData.NewItemID)
                                .attr("slot-id", newItemData.NewItemSlot)
                                .children(0).text(newItemData.NewItemQuan);


                            ItemVars.FirstItem.children(0).text(newItemData.FirstItemQuan);
                            $('[cellid=' + newItemData.NewItemSlot + '][location-id=' + ItemVars.FirstLocationID + ']').append(newItemElement);
                            makeDraggable();


                        }
                    });

                };
            });


        }
    });











    function makeDraggable() {
        $(".inventory-item:not(.itemlock)").draggable({
            revert: "invalid",
            cursor: "move",
            cursorAt: { top: 20, left: 20 },
            start: function (event, ui) {
                ItemVars.FirstItem = $(this);
                ItemVars.FirstItemID = ItemVars.FirstItem.attr("item-id");
                ItemVars.FirstBaseItemID = ItemVars.FirstItem.attr("baseitem-id");
                ItemVars.FirstItemQuan = parseInt(ItemVars.FirstItem.children(0).text());
                ItemVars.FirstItemMaxQ = parseInt(ItemVars.FirstItem.attr("max-quantity"));
                ItemVars.FirstLocationID = ItemVars.FirstItem.attr("location-id");
                ItemVars.FirstSlotID = ItemVars.FirstItem.attr("slot-id");
                ItemVars.FirstItemType = ItemVars.FirstItem.attr("item-type");
                ItemVars.FirstItem.css('z-index', 9999);
            }
        });
    }
    makeDraggable();

    //$(".inventory-item:not(.itemlock)").draggable({
    //    revert: "invalid",
    //    cursor: "move",
    //    cursorAt: { top: 20, left: 20 },
    //    start: function (event, ui) {
    //        ItemVars.FirstItem = $(this);
    //        ItemVars.FirstItemID = ItemVars.FirstItem.attr("item-id");
    //        ItemVars.FirstBaseItemID = ItemVars.FirstItem.attr("baseitem-id");
    //        ItemVars.FirstItemQuan = parseInt(ItemVars.FirstItem.children(0).text());
    //        ItemVars.FirstItemMaxQ = parseInt(ItemVars.FirstItem.attr("max-quantity"));
    //        ItemVars.FirstLocationID = ItemVars.FirstItem.attr("location-id");
    //        ItemVars.FirstSlotID = ItemVars.FirstItem.attr("slot-id");
    //        ItemVars.FirstItemType = ItemVars.FirstItem.attr("item-type");
    //        ItemVars.FirstItem.css('z-index', 9999);
    //    }
    //});

    //this makes some element droppable
    $(".inventory-cell:not(.itemlock)").droppable({
        accept: ".inventory-item",
        drop: function (event, ui) {
            ItemVars.SecondSlot = $(this);
            ItemVars.SecondItem = ItemVars.SecondSlot.children(0);
            ItemVars.SecondItemID = ItemVars.SecondItem.attr("item-id");
            ItemVars.SecondBaseItemID = ItemVars.SecondItem.attr("baseitem-id");
            ItemVars.SecondItemQuan = parseInt(ItemVars.SecondItem.children(0).text());
            ItemVars.SecondItemMaxQ = parseInt(ItemVars.SecondItem.attr("max-quantity"));
            ItemVars.SecondLocationID = ItemVars.SecondSlot.attr("location-id");
            ItemVars.SecondSlotID = ItemVars.SecondSlot.attr("cellid");
            ItemVars.SecondSlotType = ItemVars.SecondSlot.attr("item-type");
            
            if (legalMoveCheck(ItemVars) == true && ItemVars.FirstBaseItemID == ItemVars.SecondBaseItemID && ItemVars.FirstItemMaxQ > 1) {
                //if the items are stackable, move quantities
                console.log("doing item quantity move thing");
                moveQuantity(ItemVars);
                resetPosition(ItemVars.FirstItem);
            } else if (legalMoveCheck(ItemVars) == true) {
                //else if the items are making a legal move, move them
                $.ajax({
                    url: "/Item/MoveItem",
                    data: {
                        FirstItemID: ItemVars.FirstItemID,
                        FirstLocationID: ItemVars.FirstLocationID,
                        FirstSlotID: ItemVars.FirstSlotID,
                        SecondItemID: ItemVars.SecondItemID,
                        SecondLocationID: ItemVars.SecondLocationID,
                        SecondSlotID: ItemVars.SecondSlotID
                    },
                    success: function () {
                        if (ItemVars.SecondItemID != null) {
                            //if item is moving into a cell with another item, switch the items
                            ItemVars.FirstItem.detach().appendTo($('[cellid=' + ItemVars.SecondSlotID + '][location-id=' + ItemVars.SecondLocationID + ']'));
                            ItemVars.FirstItem.attr({ "location-id": ItemVars.SecondLocationID, "slot-id": ItemVars.SecondSlotID });
                            resetPosition(ItemVars.FirstItem);
                            ItemVars.SecondItem.detach().appendTo($('[cellid=' + ItemVars.FirstSlotID + '][location-id=' + ItemVars.FirstLocationID + ']'));
                            ItemVars.SecondItem.attr({ "location-id": ItemVars.FirstLocationID, "slot-id": ItemVars.FirstSlotID });
                        } else {
                            //if the item is moving into an empty cell, move the item
                            console.log("moving an item");
                            console.log(ItemVars);
                            ItemVars.FirstItem.detach().appendTo($('[cellid=' + ItemVars.SecondSlotID + '][location-id=' + ItemVars.SecondLocationID + ']'));
                            ItemVars.FirstItem.attr({ "location-id": ItemVars.SecondLocationID, "slot-id": ItemVars.SecondSlotID });
                            resetPosition(ItemVars.FirstItem);
                        }
                    }
                });
            } else {
                //if the item has made an invalid move, reset position without AJAX call
                resetPosition(ItemVars.FirstItem);
            }
        }
    });
});