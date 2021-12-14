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
    
    
    let tableDataCell = this.parentNode.parentNode;
    let itemSelection = tableDataCell.querySelector('.product-group').value;
    let sizeSelection = tableDataCell.querySelector('.size-selection').value;
    let itemQty = tableDataCell.querySelector('.item-qty').value;

    if (!sizeSelection) {
        alert('you must choose a size');
        return;
    }

    let cart = await ShoppingCart.getCartFromLocalStorage();

    for (let i = 0; i < parseInt(itemQty); i++) {
        cart.addItemToCart(itemSelection, sizeSelection);
    }
}

// for each "add to oder button" set the click listener.
let elements = document.getElementsByClassName("addToOrder");

for (let i = 0; i < elements.length; i++) {
    elements[i].addEventListener('click', addfunc, false);
}

setProductSelections()

async function setProductSelections() {
    let cart = await ShoppingCart.getCartFromLocalStorage();

    let pizzasSelection = document.getElementById('pizzas');
    let burgersSelection = document.getElementById('burgers');
    let friesSelection = document.getElementById('fries');
    let drinksSelection = document.getElementById('drinks');

    for (let i = 0; i < cart.menuItems.length; i++) {

        if (cart.menuItems[i].category === 'Pizzas') {
            let option = document.createElement('option');
            option.value = cart.menuItems[i].id;
            option.text = cart.menuItems[i].itemName;
            pizzasSelection.appendChild(option);
        }
        else if (cart.menuItems[i].category === 'Burgers') {
            let option = document.createElement('option');
            option.value = cart.menuItems[i].itemName;
            option.text = cart.menuItems[i].itemName;
            option.setAttribute('data-id', cart.menuItems[i].id);
            burgersSelection.appendChild(option);
        }
        else if (cart.menuItems[i].category === 'Fries') {
            let option = document.createElement('option');
            option.value = cart.menuItems[i].id;
            option.text = cart.menuItems[i].itemName;
            option.setAttribute('data-id', cart.menuItems[i].id);
            friesSelection.appendChild(option);
        }
        else if (cart.menuItems[i].category === 'Drinks') {
            let option = document.createElement('option');
            option.value = cart.menuItems[i].id;
            option.text = cart.menuItems[i].itemName;
            option.setAttribute('data-id', cart.menuItems[i].id);
            drinksSelection.appendChild(option);
        }


    }
}