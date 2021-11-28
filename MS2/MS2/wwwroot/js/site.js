// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

document.addeventlistener("domcontentloaded", () => {
    let sendbtn = document.getelementbyid("sendbtn");
    sendbtn.disabled = true;

    let resetbtn = document.getelementbyid("resetbtn");
    resetbtn.addeventlistener("click", () => {
        grecaptcha.reset();
        sendbtn.disabled = true;
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
