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