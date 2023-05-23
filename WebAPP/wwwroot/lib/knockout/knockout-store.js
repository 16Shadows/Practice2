ko.extenders.store = function (target) {
    target.subscribe(function (newValue) {
        if (target.original == undefined)
            target.original = newValue;
    });
    target.commit = function () {
        if (target.original == undefined)
            return;
        delete target.original;
    }
    target.revert = function () {
        if (target.original == undefined)
            return;
        target(target.original);
        delete target.original;
    }
}