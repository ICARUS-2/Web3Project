import ShoppingCart from "./ShoppingCart.js";

let userCart = localStorage.getItem(ShoppingCart.LOCAL_STORAGE_CART_NAME);

if (userCart == null) {
    userCart = await ShoppingCart.getInstance();
    localStorage.setItem(ShoppingCart.LOCAL_STORAGE_CART_NAME, JSON.stringify(userCart));
}
else {
    userCart = await ShoppingCart.deserializeCartData(userCart);
}

console.log(userCart instanceof ShoppingCart);
console.log(userCart);

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

