/*
    Author: 16Shadows
    Dependencies: jQuery
    How to:
        Set a 'context-menu' attribute on an element which will be used as jQuery selector to find the element to use as context menu.
        Do note, that the following style properties will be modified on the context-menu element: 
        position (to absolute), zindex (to 1000), top, left, display (to 'block' when show, to 'none' when hidden)
*/

ActiveContextMenu = undefined;

$(document).mouseup(event => {
    if (ActiveContextMenu == undefined)
        return;
    else if ($(event.target).parents("#" + ActiveContextMenu.getAttribute("id")).length > 0)
        return;
    ActiveContextMenu.style.display = "none";
    delete ActiveContextMenu.ContextMenuSource;
    ActiveContextMenu = undefined;
});

$(document).contextmenu(event => {
    if (ActiveContextMenu != undefined && $(event.target).parents("#" + ActiveContextMenu.getAttribute("id")).length > 0) {
        event.preventDefault();
        return;
    }
    menu = event.target.getAttribute("context-menu");
    if (menu == undefined)
        return;
    menu = $(menu)[0];
    if (menu == undefined)
        return;
    event.preventDefault();
    ActiveContextMenu = menu;
    menu.ContextMenuSource = event.target;
    ActiveContextMenu.style.display = "block";
    ActiveContextMenu.style.position = "absolute";
    ActiveContextMenu.style.zindex = 1000;
    ActiveContextMenu.style.top = event.pageY + "px";
    ActiveContextMenu.style.left = event.pageX + "px";
});