utils = {};

/*
    Creates a new function to replace the target to call injection before target (if target exists)
*/
utils.patch = function (target, injection) {
    if (target == undefined || target == null)
        return injection;
    return function () {
        injection.apply(this, arguments);
        target.apply(this, arguments);
    };
}