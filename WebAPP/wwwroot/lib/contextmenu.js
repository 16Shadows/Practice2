/*
    Author: 16Shadows
    Dependencies: jQuery
    How to use:
        Set a 'context-menu' attribute on an element which will be used as jQuery selector to find the element to use as context menu.
        Do note, that the following style properties will be modified on the context-menu element: 
        position (to absolute), zindex (to 1000), top, left, display (to 'block' when show, to 'none' when hidden)

        If no 'context-menu' attribute is specified on the element, the script will try to find an element with this attribute up in the element hierarchy.
        To prevent this behaviour, specify 'context-menu-block' attribute on an element and the script will not search the element and any other elements
        higher in the hierarchy.

        Variable ActiveContextMenu contains the element which is currently used as the context menu.
        Variable ActiveContextMenu.ContextMenuSource contains the element which specified the context-menu attribute.
        Variable ActiveContextMenu.ContextMenuClickSource contains the element which was clicked to open the context-menu.
*/

ActiveContextMenu = undefined;

$(document).mouseup(event => {
    if (ActiveContextMenu == undefined)
        return;
    if (event.target == ActiveContextMenu || $(event.target).parents("#" + ActiveContextMenu.getAttribute("id")).length > 0)
        return;
    ActiveContextMenu.style.display = "none";
    delete ActiveContextMenu.ContextMenuSource;
    delete ActiveContextMenu.ContextMenuClickSource;
    ActiveContextMenu = undefined;
});

$(document).contextmenu(event => {
    if (ActiveContextMenu != undefined && (ActiveContextMenu == event.target || $(event.target).parents("#" + ActiveContextMenu.getAttribute("id")).length > 0)) {
        event.preventDefault();
        return;
    }
    source = event.target;
    do {
        menu = source.getAttribute("context-menu");
    }
    while (menu == undefined && (source = $(source).parent()[0]) != undefined && source != document && !source.hasAttribute("context-menu-block"));
    if (menu == undefined)
        return;
    menu = $(menu)[0];
    if (menu == undefined)
        return;
    event.preventDefault();
    ActiveContextMenu = menu;
    menu.ContextMenuSource = source;
    menu.ContextMenuClickSource = event.target;
    ActiveContextMenu.style.display = "block";
    ActiveContextMenu.style.position = "absolute";
    ActiveContextMenu.style.zindex = 1000;
    ActiveContextMenu.style.top = event.pageY + "px";
    ActiveContextMenu.style.left = event.pageX + "px";
});