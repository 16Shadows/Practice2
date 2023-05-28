utils = {};

/*
    Creates a patch function to replace target:
        target - the function that will be replaced
        injection - the function to be injected into execution
        options - injection generation parameters:
            after - if true, injection will be executed after target, otherwise injection is executed before target. Default: false.
            returnOverride - if true, the result of the injection will be returned when the function is called, otherwise the result of target will be returned. Default: false
            interceptOnReturn - if after is set to false and this is set to true, when injection returns something, target will not be called. Default: false
*/
utils.patch = function (target, injection, options) {
    if (!target)
        return injection;

    options = options || {
        after: false,
        returnOverride: false,
        interceptOnReturn : false
    };

    if (options.after) {
        if (options.returnOverride) {
            return function () {
                target.apply(this, arguments);
                return injection.apply(this, arguments);
            };
        }
        else {
            return function () {
                var r = target.apply(this, arguments);
                injection.apply(this, arguments);
                return r;
            };
        }
    }
    else {
        if (options.returnOverride) {
            if (options.interceptOnReturn) {
                return function () {
                    var r = injection.apply(this, arguments);
                    if (r === undefined)
                        target.apply(this, arguments);
                    return r;
                };
            }
            else {
                return function () {
                    var r = injection.apply(this, arguments);
                    target.apply(this, arguments);
                    return r;
                };
            }
        }
        else {
            if (options.interceptOnReturn) {
                return function () {
                    if (injection.apply(this, arguments) == undefined)
                        return target.apply(this, arguments);
                    return undefined;
                };
            }
            else {
                return function () {
                    injection.apply(this, arguments);
                    return target.apply(this, arguments);
                };
            }
        }
    }
}