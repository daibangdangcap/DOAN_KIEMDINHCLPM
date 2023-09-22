var btnOpen = document.querySelector('.btn3')
var modal = document.querySelector('.modal0')
var iconClose = document.querySelector('.modal_header i')
var btnClose = document.querySelector('.comeback-button')

function toggleModal(e) {
    modal.classList.toggle('hide')
}

btnOpen.addEventListener('click', toggleModal)
btnClose.addEventListener('click', toggleModal)
iconClose.addEventListener('click', toggleModal)
modal.addEventListener('click', function (e) {
    if (e.target == e.currentTarget) {
        toggleModal()
    }
})

const deleteButton = document.querySelector('.delete-button');
const confirmCheckbox = document.querySelector('.confirm-checkbox');

confirmCheckbox.addEventListener('change', function () {
    if (this.checked) {
        deleteButton.classList.add('enabled');
        deleteButton.classList.remove('disabled');
    } else {
        deleteButton.classList.add('disabled');
        deleteButton.classList.remove('enabled');
    }
});