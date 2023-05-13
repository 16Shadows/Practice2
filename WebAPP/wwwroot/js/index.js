// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//var indexVM = {
//    testValue : ko.observable("Test")
//}
//ko.applyBindings(indexVM);

// Class for book row in table
function Book(id, name, parent) {
    var self = this;


    self.bookId = ko.observable(id);
    self.bookName = ko.observable(name);
    self.bookParent = ko.observable(parent);
}

function VM() {
    var self = this;

    self.testValue = ko.observable("Test");


    self.addBook = function () { };
    self.removeBook = function () { };
}
ko.applyBindings(new VM());