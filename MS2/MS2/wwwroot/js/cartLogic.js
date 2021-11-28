import ShoppingCart from "./ShoppingCart.js";

let tempCart = localStorage.getItem('cart');

if (tempCart == null) {
    tempCart = await ShoppingCart.getInstance();
    localStorage.setItem('cart', JSON.stringify(tempCart));
}
else {
    tempCart = await ShoppingCart.deserializeCartData(tempCart);
}

console.log(tempCart instanceof ShoppingCart);
console.log(tempCart);

let addfunc = async function () {
    let id = this.getAttribute("data-id");
    let cart = await ShoppingCart.getCartFromLocalStorage();

    cart.addItemToCart(id);
    console.log(cart);
}
// for each "add to oder button" set the click listener.
let elements = document.getElementsByClassName("addToOrder");
for (let i = 0; i < elements.length; i++) {
    elements[i].addEventListener('click', addfunc, false);
}

