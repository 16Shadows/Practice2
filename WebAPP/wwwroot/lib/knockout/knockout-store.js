ko.extenders.store = function (target) {
    target.subscribe(function (oldValue) {
        if (Object.hasOwn(target, "original"))
            return;
        target.original = oldValue;
    }, null, "beforeChange");

    target.commit = function () {
        if (!Object.hasOwn(target, "original"))
            return;
        delete target.original;
    };

    target.revert = function () {
        if (!Object.hasOwn(target, "original"))
            return;
        target(target.original);
        delete target.original;
    };

    target.hasChanged = function () {
        return Object.hasOwn(target, "original");
    };
}