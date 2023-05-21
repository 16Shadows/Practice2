//A simple page-wide uid
uid = {
    LastUid: 0
};

uid.next = function () {
    return "uid-" + (this.LastUid++);
}