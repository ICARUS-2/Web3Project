﻿import ShoppingCart from "./ShoppingCart.js";
await generateCartView();

function showEmptyCartMessage() {
    let container = document.getElementsByTagName('main')[0];
    let header = document.getElementById('cart-header');
    let linkToOderPage = document.createElement('a');

    header.innerText = 'Shopping Cart - Is Empty';
    linkToOderPage.classList.add('btn', 'btn-success');
    linkToOderPage.innerText = 'Add items to Order';
    linkToOderPage.setAttribute("href", "/Order");

    container.appendChild(linkToOderPage);
}
function setQtyBtnListeners() {
    let increaseBtns = document.getElementsByClassName('increase-btn');
    let decreaseBtns = document.getElementsByClassName('decrease-btn');

    for (let i = 0; i < increaseBtns.length; i++) {

        increaseBtns[i].addEventListener('click', increaseItemQty, false);
        decreaseBtns[i].addEventListener('click', decreaseItemQty, false);
    }
}
async function increaseItemQty() {
    // get the div that is two nodes up the dom tree.
    let id = this.parentNode.parentNode.getAttribute("data-id");
    let cart = await ShoppingCart.getCartFromLocalStorage();

    cart.addItemToCart(id);

    console.log("incease");
    console.log(cart);
}
async function decreaseItemQty() {
    // get the div that is two nodes up the dom tree.
    let id = this.parentNode.parentNode.getAttribute("data-id");
    let cart = await ShoppingCart.getCartFromLocalStorage();

    cart.removeItemFromCart(id);

    console.log("decease");
    console.log(cart);
}

async function generateCartView() {

    let userCart = localStorage.getItem(ShoppingCart.LOCAL_STORAGE_CART_NAME);
    let totalAmount = 0;
    let container = document.createElement('div');
    let dollarCADFormat = Intl.NumberFormat('en-CA');

    if (userCart == null) {
        showEmptyCartMessage();
        return;
    }

    userCart = await ShoppingCart.deserializeCartData(userCart);

    if (userCart.orderItems.length === 0) {
        showEmptyCartMessage();
        return;
    }

    container.setAttribute('id', 'cart-items-container');
    document.getElementsByTagName('main')[0].appendChild(container);
    
    for (let i = 0; i < userCart.orderItems.length; i++) {

        let div = document.createElement("div");
        let img = document.createElement("img");
        let itemHeader = document.createElement("h3");
        let itemPrice = document.createElement("p");
        let quantity = document.createElement("p");
        let subTotal = document.createElement("p");
        let buttonDiv = document.createElement("div");
        let addBtn = document.createElement("button");
        let removeBtn = document.createElement("button");

        div.classList.add("cart-items");
   
        userCart.menuItems.forEach((item) => {
            if (item.id == userCart.orderItems[i]) {

                img.src = '/img/' + item.itemName + '.jpg';
                img.classList.add("cart-item-image");
                itemHeader.innerText = item.itemName;
                itemPrice.innerText = "$" + item.smallPrice;
                itemPrice.classList.add("cart-info");
                quantity.classList.add("cart-info");
                subTotal.classList.add("cart-info");
                quantity.innerText = "Quantity: " + userCart.itemQuantity[i];
                subTotal.innerText = "Sub Total: $" + dollarCADFormat.format(item.smallPrice * userCart.itemQuantity[i]);
                totalAmount += item.smallPrice;
                div.setAttribute('data-id', userCart.orderItems[i]);
            }
        });
        
        buttonDiv.classList.add("Qty-btn-div");
        addBtn.innerText = '+';
        addBtn.classList.add("btn", "btn-success", "increase-btn");
        removeBtn.innerText = '-';
        removeBtn.classList.add("btn", "btn-success", "decrease-btn");

        buttonDiv.appendChild(addBtn);
        buttonDiv.appendChild(removeBtn);

        div.appendChild(img);
        div.appendChild(itemHeader);
        div.appendChild(itemPrice);
        div.appendChild(quantity);
        div.appendChild(subTotal);
        div.appendChild(buttonDiv);
        container.appendChild(div);
    }

    let checkOutDiv = document.createElement("div");
    let total = document.createElement('h2');
    let orderBtn = document.createElement('button');

    checkOutDiv.setAttribute('id', 'check-out');
    total.innerText = "Total: $" + dollarCADFormat.format(totalAmount);
    orderBtn.classList.add("btn","btn-success")
    orderBtn.innerText = "Check out";

    checkOutDiv.appendChild(total);
    checkOutDiv.appendChild(orderBtn);
    container.appendChild(checkOutDiv);
    setQtyBtnListeners()
}