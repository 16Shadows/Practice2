/*
    if value is an observable, return the value of the observible.
    otherwise, returns the value itself.
*/
ko.utils.get = function (value) {
    if (ko.isObservable(value))
        return value();
    else
        return value;
}

/*
    Stores the value in a underlying observable, 
    calling read to transform underlying value before reading it, 
    write to transform value before writing it to underlying observable.

    options - object:
        write - a method which returns processed value to be stored in underlying observable
        read - a method which return processed underlying value
        initial - an initial value which will be set to the underlying value
*/
ko.pureComputedEx = function (options) {
    var self;
    var readMethod = options?.read || function (value) { return value; }
    var writeMethod = options?.write || function (value) { return value; }
    
    self = ko.pureComputed({
        read: function () {
            return readMethod(self.value());
        },
        write: function (value) {
            self['notifySubscribers'](self(), "beforeChange");
            self.value(writeMethod(value));
        }
    });
    self.value = ko.observable(options?.initial);

    return self;
}