function getAntiForgeryToken() {
    return document.querySelector('.AntiforgeryTokenContainer').querySelectorAll('input')[0].value;;
}