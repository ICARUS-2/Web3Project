import ShoppingCart from "./ShoppingCart.js";

let userCart = localStorage.getItem(ShoppingCart.LOCAL_STORAGE_CART_NAME);

if (userCart == null) {
    userCart = await ShoppingCart.getInstance();
    localStorage.setItem(ShoppingCart.LOCAL_STORAGE_CART_NAME, JSON.stringify(userCart));
}
else {
    userCart = await ShoppingCart.deserializeCartData(userCart);
}

let addfunc = async function () {
    let id = this.getAttribute("data-id");

    let tableDataCell = this.parentNode;
    let sizeSelection = tableDataCell.querySelector('.size-selection').value;

    if (!sizeSelection) {
        alert('you must choose a size');
        return;
    }
    console.log(sizeSelection);

    let cart = await ShoppingCart.getCartFromLocalStorage();

    cart.addItemToCart(id);
    console.log(cart);
    //console.log( selectedSize);
    //console.log(radioDiv);
    //console.log(tableDataCell);
}

// for each "add to oder button" set the click listener.
let elements = document.getElementsByClassName("addToOrder");

for (let i = 0; i < elements.length; i++) {
    elements[i].addEventListener('click', addfunc, false);
}
