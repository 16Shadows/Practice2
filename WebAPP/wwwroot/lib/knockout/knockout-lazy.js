//https://www.knockmeout.net/2011/06/lazy-loading-observable-in-knockoutjs.html

//an observable that retrieves its value when first bound
ko.onDemandObservable = function (callback, target) {
    var _value = ko.observable();  //private observable
    
    var result = ko.pureComputed({
        read: function () {
            //if it has not been loaded, execute the supplied function
            if (!result.loaded()) {
                callback(target);
            }
            //always return the current value
            return _value();
        },
        write: function (newValue) {
            //indicate that the value is now loaded and set it
            result.loaded(true);
            _value(newValue);
            result.notifySubscribers(_value());
        },
        deferEvaluation: true  //do not evaluate immediately when created
    });

    //expose the current state, which can be bound against
    result.loaded = ko.observable();
    //load it again
    result.refresh = function () {
        result.loaded(false);
    };

    result.getPromise = function () {
        return new Promise((resolve) => {
            if (!result.loaded()) {
                _value.subscribe((newValue) => resolve(newValue));
                callback(target);
            }
            else
                resolve(_value());
        });
    };

    return result;
};

//an observable that retrieves its value when first bound
ko.onDemandObservableArray = function (callback, target) {
    var _value = ko.observableArray();  //private observable

    var result = ko.pureComputed({
        read: function () {
            //if it has not been loaded, execute the supplied function
            if (!result.loaded()) {
                callback(target);
            }
            //always return the current value
            return _value();
        },
        write: function (newValue) {
            //indicate that the value is now loaded and set it
            result.loaded(true);
            _value(newValue);
            result.notifySubscribers(_value());
        },
        deferEvaluation: true  //do not evaluate immediately when created
    }).extend({ 'trackArrayChanges': true });

    //expose the current state, which can be bound against
    result.loaded = ko.observable();
    //load it again
    result.refresh = function () {
        result.loaded(false);
    };

    //Attach array-specific methods
    ["pop", "push", "reverse", "shift", "sort", "splice", "unshift", "remove", "removeAll", "destroy", "destroyAll", "indexOf", "replace", "sorted", "reversed", "slice"]
        .forEach(name => result[name] = function () { return _value[name].apply(_value, arguments); });

    result.getPromise = function () {
        return new Promise((resolve) => {
            if (!result.loaded()) {
                _value.subscribe((newValue) => resolve(newValue));
                callback(target);
            }
            else
                resolve(_value());
        });
    };

    return result;
};