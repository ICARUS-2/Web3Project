// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addEventListener("DOMContentLoaded", () => {
    let sendBtn = document.getElementById("sendBtn");
    sendBtn.disabled = true;

    let resetBtn = document.getElementById("resetBtn");
    resetBtn.addEventListener("click", () => {
        grecaptcha.reset();
        sendBtn.disabled = true;
    });
});

function enableButton() {
    let btn = document.getElementById("sendBtn");
    btn.disabled = false;
    setTimeout(() => {
        let btn = document.getElementById("sendBtn");
        btn.disabled = true;
    }, 60000); // Disables the button in 60s
}