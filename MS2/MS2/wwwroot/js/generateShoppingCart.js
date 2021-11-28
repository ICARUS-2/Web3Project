import ShoppingCart from "./ShoppingCart.js";
await generateCartView();
async function generateCartView() {

    let userCart = localStorage.getItem(ShoppingCart.LOCAL_STORAGE_CART_NAME);
    let totalAmount = 0;
    let container = document.getElementById("cart-items-container");
    let dollarCADFormat = Intl.NumberFormat('en-CA');

    if (userCart == null) {
        userCart = await ShoppingCart.getInstance();
        localStorage.setItem(ShoppingCart.LOCAL_STORAGE_CART_NAME, JSON.stringify(userCart));
    }
    else {
        userCart = await ShoppingCart.deserializeCartData(userCart);
    }

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
            }
        });

        buttonDiv.classList.add("Qty-btn-div");

        addBtn.innerText = '+';
        addBtn.classList.add("btn", "btn-success");
        removeBtn.innerText = '-';
        removeBtn.classList.add("btn", "btn-success");

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
}