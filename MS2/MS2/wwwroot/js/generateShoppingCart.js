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

async function generateCartView() {
    // TODO: break this up into multiple functions.
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
    let orderTypeDelivery = document.createElement('h4');

    orderTypeDelivery.innerText = 'Order Type: Pick Up';
    orderTypeDelivery.setAttribute('id', 'orderTypeDelivery');
    orderTypeDelivery.style.display = 'none';

    checkOutDiv.setAttribute('id', 'check-out');
    total.innerText = "Total: $" + dollarCADFormat.format(totalAmount);
    orderBtn.classList.add("btn","btn-success")
    orderBtn.innerText = "Check out";

    orderBtn.addEventListener('click', submitOrder)

    checkOutDiv.appendChild(total);
    checkOutDiv.appendChild(makeDeliveryOptions());

   

    checkOutDiv.appendChild(makeAddressInput());
    checkOutDiv.appendChild(orderTypeDelivery);
    checkOutDiv.appendChild(orderBtn);
    container.appendChild(checkOutDiv);
    setQtyBtnListeners();
    makeAddressInput();
    deliveryAndPickUpLogic();
}

function makeDeliveryOptions() {
    let deliveryOptionDiv = document.createElement('div');
    let pickupButton = document.createElement('button');
    let deliveryButton = document.createElement('button');

    deliveryButton.style.margin = '1em';

    pickupButton.setAttribute('id', 'pick-up-button');
    deliveryButton.setAttribute('id', 'delivery-button');

    pickupButton.classList.add("btn", "btn-success");
    deliveryButton.classList.add("btn", "btn-success");
    deliveryOptionDiv.appendChild(pickupButton);
    deliveryOptionDiv.appendChild(deliveryButton);

    pickupButton.innerText = 'Pick Up';
    deliveryButton.innerText ='Delivery'

    return deliveryOptionDiv;
}

function deliveryAndPickUpLogic() {
    let pickupButton = document.getElementById('pick-up-button');
    let deliveryButton = document.getElementById('delivery-button');

    pickupButton.addEventListener('click', async () => {
        let cart = await ShoppingCart.getCartFromLocalStorage();
        document.getElementById('address-div').style.display = 'none';
        document.getElementById('orderTypeDelivery').style.display = 'flex';
        cart.setIsDelivery(false);
    });

    deliveryButton.addEventListener('click', async () => {
        let cart = await ShoppingCart.getCartFromLocalStorage();
        document.getElementById('address-div').style.display = 'flex';
        document.getElementById('orderTypeDelivery').style.display = 'none';
        cart.setIsDelivery(true);
    })
}

function makeCitySelection() {
    let citySelection = document.createElement('select');
    citySelection.setAttribute('id', 'city-selection');

    let cities = [
        "Sainte-Anne-De-Bellevue",
        "Baie-D'Urfé",
        "Senneville",
        "Kirkland Dollard-Des-Ormeaux",
        "Beaconsfield Pierrefonds and Roxboro",
        "L'Île-Bizard–Sainte-Geneviève",
        "Pointe-Claire",
        "Dorval"
    ];

    let defaultOption = document.createElement('option');
    defaultOption.innerText = 'Choose City';
    defaultOption.setAttribute('selected','');
    defaultOption.setAttribute('disabled','');
    defaultOption.setAttribute('hidden','');
    citySelection.appendChild(defaultOption);

    for (let i = 0; i < cities.length; i++) {
        let tempOption = document.createElement('option');
        tempOption.value = cities[i];
        tempOption.text = cities[i];
        citySelection.appendChild(tempOption);
    }

    return citySelection;
}

function makeAddressInput() {
    let userAddress = document.getElementById("user-address").innerText;
    let street = document.createElement('input');
    let header = document.createElement('h4');
    let addressDiv = document.createElement('div');
    let postalCode = document.createElement('input');

    addressDiv.style.display = 'none';

    let citySelection = makeCitySelection();

    header.innerText = 'Delivery Address';
    addressDiv.setAttribute('id', 'address-div');

    street.setAttribute('type', 'street');
    street.setAttribute('id', 'street');
    street.setAttribute('placeholder', 'Street');

    postalCode.setAttribute('type', 'postalCode');
    postalCode.setAttribute('id', 'postalCode');
    postalCode.setAttribute('placeholder', 'Postal Code');

    addressDiv.appendChild(header);
    addressDiv.appendChild(street);
    addressDiv.appendChild(postalCode);
    addressDiv.appendChild(citySelection);


    if (userAddress) {

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

function makeOrderPlacedSuccessfully() {

    let modal = document.getElementById("modal-container");
    let okayButton = document.getElementById("modal-button");

    modal.style.visibility = 'visible';

    okayButton.addEventListener('click', () => {
        modal.style.visibility = 'hidden';
    });
}

async function submitOrder() {

    let userCart = await ShoppingCart.getCartFromLocalStorage();
    let street = document.getElementById('street').value;
    let postalCode = document.getElementById('postalCode').value;
    let selectedCity = document.getElementById('city-selection').value;

    let address = street + ',' + selectedCity + ',' + postalCode;

    let tempCartObject = {};

    tempCartObject.address = address;
    tempCartObject.itemQuantity = userCart.itemQuantity;
    tempCartObject.itemSize = userCart.itemSize;
    tempCartObject.orderItems = userCart.orderItems;
    tempCartObject.isDelivery = userCart.isDelivery;

    const response = await fetch(ShoppingCart.URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(tempCartObject),
    });

    if (response.status === 201) {
        ShoppingCart.clearCart();
        userCart = await ShoppingCart.getInstance();
        localStorage.setItem(ShoppingCart.LOCAL_STORAGE_CART_NAME, JSON.stringify(userCart));
        await updateUI();
        makeOrderPlacedSuccessfully();
    }
}
