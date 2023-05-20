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

    Methods and variables:
        Variable ContextMenu.ActiveMenu contains the element which is currently used as the context menu. May be null.
        Variable ContextMenu.Source contains the element which specified the context-menu attribute.
        Variable ContextMenu.ClickSource contains the element which was clicked to open the context-menu.

        Method ContextMenu.Close() closes the currently opened context menu (if any)
        
        Method ContextMenu.Open(menu, position, source, clickSource) opens a context menu with specified parameters:
            menu - the DOM element to use as context menu
            position - the position at which to open the context menu (an object containing x and y - absolute pixel coordinates): {x:15, y:15}
            source (optional) - the element to specify in ContextMenu.Source. If not provided, the variable will contain undefined.
            clickSource (optional) - the element to specify in ContextMenu.ClickSource. If not specified, will contain the same value as source.
*/

ContextMenu = {
    ActiveMenu : undefined,
    Source : undefined,
    ClickSource: undefined
}

ContextMenu.Close = function () {
    if (ContextMenu.ActiveMenu == undefined)
        return;
    ContextMenu.ActiveMenu.style.display = "none";
    ContextMenu.Source = undefined;
    ContextMenu.ClickSource = undefined;
    ContextMenu.ActiveMenu = undefined;
}
ContextMenu.Open = function (menu, position, source, clickSource) {
    ContextMenu.ActiveMenu = menu;
    ContextMenu.Source = source;
    ContextMenu.ClickSource = clickSource;
    menu.style.display = "block";
    menu.style.position = "absolute";
    menu.style.zindex = 1000;
    menu.style.top = position.y + "px";
    menu.style.left = position.x + "px";
}

$(document).mouseup(event => {
    if (ContextMenu.ActiveMenu == undefined)
        return;
    if (event.target == ContextMenu.ActiveMenu || $(event.target).parents("#" + ContextMenu.ActiveMenu.getAttribute("id")).length > 0)
        return;
    ContextMenu.Close();
});

$(document).contextmenu(event => {
    if (ContextMenu.ActiveMenu != undefined && (ContextMenu.ActiveMenu == event.target || $(event.target).parents("#" + ContextMenu.ActiveMenu.getAttribute("id")).length > 0)) {
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
    ContextMenu.Open(menu, {x:event.pageX, y:event.pageY}, source, event.target);
});