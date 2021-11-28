import ShoppingCart from "./ShoppingCart.js";
const cart = await ShoppingCart.getInstance();
console.log(cart instanceof ShoppingCart);


console.log("help");

let addfunc = async function () {
    let id = this.getAttribute("data-id");
    let cart = await ShoppingCart.getInstance();
    cart.addItemToCart(id);
    console.log(ShoppingCart.getInstance());
}

let elements = document.getElementsByClassName("addToOrder");
for (let i = 0; i < elements.length; i++) {
    elements[i].addEventListener('click', addfunc, false);
}

