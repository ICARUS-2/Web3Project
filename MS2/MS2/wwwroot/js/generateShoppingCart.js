import ShoppingCart from "./ShoppingCart.js";
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
    let itemSize = this.parentNode.parentNode.getAttribute("data-size");
    let cart = await ShoppingCart.getCartFromLocalStorage();

    cart.addItemToCart(id,itemSize);
    await updateUI();
}

async function decreaseItemQty() {
    // get the div that is two nodes up the dom tree.
    let id = this.parentNode.parentNode.getAttribute("data-id");
    let itemSize = this.parentNode.parentNode.getAttribute("data-size");
    let cart = await ShoppingCart.getCartFromLocalStorage();

    cart.removeItemFromCart(id,itemSize);

    await updateUI();
}

async function updateUI() {
    let cart = localStorage.getItem(ShoppingCart.LOCAL_STORAGE_CART_NAME);
    let cartContainer = document.getElementById("cart-items-container");
    cart = await ShoppingCart.deserializeCartData(cart);

    cartContainer.parentNode.removeChild(cartContainer);
    await generateCartView();
}

// TODO: break this up into multiple functions.
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

        let itemSize = document.createElement("p");

        div.classList.add("cart-items");
   
        userCart.menuItems.forEach((item) => {

            if (item.id == userCart.orderItems[i]) {

                img.src = '/img/' + item.itemName + '.jpg';
                img.classList.add("cart-item-image");
                itemHeader.innerText = item.itemName;
                itemPrice.innerText = "$" + getItemPriceBySize(item, userCart.itemSize[i]);
                itemPrice.classList.add("cart-info");
                quantity.classList.add("cart-info");
                subTotal.classList.add("cart-info");

                itemSize.classList.add("cart-info");
                itemSize.innerText = userCart.itemSize[i];

                quantity.innerText = "Quantity: " + userCart.itemQuantity[i];
                subTotal.innerText = "Sub Total: $" + dollarCADFormat.format(getItemPriceBySize(item, userCart.itemSize[i]) * userCart.itemQuantity[i]);
                totalAmount += getItemPriceBySize(item, userCart.itemSize[i]) * userCart.itemQuantity[i];;
                div.setAttribute('data-id', userCart.orderItems[i]);
                div.setAttribute('data-size', userCart.itemSize[i]);
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
        div.appendChild(itemSize);
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
    checkOutDiv.appendChild(makeAddressInput());
    checkOutDiv.appendChild(orderBtn);
    container.appendChild(checkOutDiv);
    setQtyBtnListeners()
    makeAddressInput()
}

function makeAddressInput() {
    let userAddress = document.getElementById("user-address").innerText;
    let splitAddress = "";
    let street = document.createElement('input');
    let city = document.createElement('input');
    let province = document.createElement('input');
    let header = document.createElement('h4');
    let addressDiv = document.createElement('div');

    header.innerText = 'Delivery Address';
    addressDiv.setAttribute('id', 'address-div');

    street.setAttribute('type', 'street');
    street.setAttribute('id', 'street');
    street.setAttribute('placeholder', 'Street');

    city.setAttribute('type', 'city');
    city.setAttribute('id', 'city');
    city.setAttribute('placeholder', 'City');

    province.setAttribute('type', 'province');
    province.setAttribute('id', 'province');
    province.setAttribute('placeholder', 'Province');

    addressDiv.appendChild(header);
    addressDiv.appendChild(street);
    addressDiv.appendChild(city);
    addressDiv.appendChild(province);

    if (userAddress) {
        splitAddress = userAddress.split(',');
        street.value = splitAddress[0];
        city.value = splitAddress[1];
        province.value = splitAddress[2];
    }

    return addressDiv;
}

function getItemPriceBySize(item, itemSize) {
    itemSize = itemSize.toLowerCase();
    switch (itemSize) {
        case 'small': return item.smallPrice;
        case 'medium': return item.mediumPrice;
        case 'large': return item.largePrice;
    }
}
