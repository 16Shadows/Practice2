ko.extenders.store = function (target) {
    target.subscribe(function (oldValue) {
        if (target.ko_original !== undefined)
            return;
        target.ko_original = oldValue;
    }, null, "beforeChange");

    target.commit = function () {
        if (target.ko_original === undefined)
            return;
        target.ko_original = undefined;
    };

    target.revert = function () {
        if (target.ko_original === undefined)
            return;
        target(target.ko_original);
        target.ko_original = undefined;
    };

    target.hasChanged = function () {
        return target.ko_original !== undefined;
    };
}