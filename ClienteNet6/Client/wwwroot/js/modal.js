const modalNames = 'modal-open';

function OpenModal() {
    var bodys = document.getElementsByTagName('body');

    for (var i = 0; i < bodys.length; i++) {
        bodys[i].classList.remove(modalNames);
    }
}

function RemoveModal() {
    var bodys = document.getElementsByTagName('body');

    for (var i = 0; i < bodys.length; i++) {
        bodys[i].classList.add(modalNames);
    }
}