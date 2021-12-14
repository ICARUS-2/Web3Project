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
    renderOrderItems()
}

// for each "add to oder button" set the click listener.
let elements = document.getElementsByClassName("addToOrder");

for (let i = 0; i < elements.length; i++) {
    elements[i].addEventListener('click', addfunc, false);
}

setProductSelections();
renderOrderItems();

function getItemPriceBySize(item, itemSize) {
    itemSize = itemSize.toLowerCase();
    switch (itemSize) {
        case 'small': return item.smallPrice;
        case 'medium': return item.mediumPrice;
        case 'large': return item.largePrice;
    }
}

async function renderOrderItems() {
    let userCart = localStorage.getItem(ShoppingCart.LOCAL_STORAGE_CART_NAME);
    userCart = await ShoppingCart.deserializeCartData(userCart);
    let dollarCADFormat = Intl.NumberFormat('en-CA');

    let orderViewTableBody = document.getElementById('order-view-table-body');
    let entries = document.getElementsByClassName('order-items-entry');
    let entriesAmount = entries.length;

    if (entries.length > 0) {
        for (let i = 0; i < entriesAmount ; i++) {
            orderViewTableBody.parentNode.deleteRow(2);
        }
    }

    let totalAmount = 0;

    for (let i = 0; i < userCart.orderItems.length; i++) {
        userCart.menuItems.forEach((item) => {
            if (item.id == userCart.orderItems[i]) {

                let tempTr = document.createElement('tr');
                tempTr.classList.add('order-items-entry');

                let itemName = item.itemName;
                let itemPrice = "$" + getItemPriceBySize(item, userCart.itemSize[i]);
                let itemSize = userCart.itemSize[i];
                let itemQuantity = "Quantity: " + userCart.itemQuantity[i];
                let subTotal = "Sub Total: $" + dollarCADFormat.format(getItemPriceBySize(item, userCart.itemSize[i]) * userCart.itemQuantity[i]);

                tempTr.setAttribute('data-id', item.id);
                tempTr.setAttribute('data-size', userCart.itemSize[i]);
                tempTr.setAttribute('data-qty', userCart.itemQuantity[i]);

                let removeBtn = document.createElement('button');
                removeBtn.classList.add('remove-btn');
                let text = document.createElement('p');

                removeBtn.innerText = 'Remove';
                text.innerText = `${itemName} ${itemPrice} ${itemSize} ${itemQuantity} ${subTotal}`;

                let tempCell = tempTr.insertCell();

                tempCell.appendChild(text);
                tempCell.appendChild(removeBtn);
                orderViewTableBody.appendChild(tempTr);
            }
        });
    }
    setRemoveClickListeners()
}

function setRemoveClickListeners() {
    let removeBtns = document.getElementsByClassName('remove-btn');

    for (let i = 0; i < removeBtns.length; i++) {
        removeBtns[i].addEventListener('click', removeOrderItem, false);
    }
}
async function removeOrderItem() {
    let id = this.parentNode.parentNode.getAttribute("data-id");
    let itemSize = this.parentNode.parentNode.getAttribute("data-size");
    let qty = parseInt(this.parentNode.parentNode.getAttribute("data-qty"));
    let cart = await ShoppingCart.getCartFromLocalStorage();

    for (let i = 0; i < qty; i++) {
        cart.removeItemFromCart(id, itemSize);
    }

    await renderOrderItems();
}
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
            option.value = cart.menuItems[i].id;
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