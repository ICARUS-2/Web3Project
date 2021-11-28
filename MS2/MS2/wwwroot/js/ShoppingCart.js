export default class ShoppingCart {
    constructor(menuItems) {
        this.menuItems = menuItems;  // list of all possible items.
        this.orderItems = [];        // holds the id of the items.
        this.itemQuantity = [];      // holds qty of items.
    }

    static instance = null;
    static URL = 'https://localhost:44354/api/Orders';
    static LOCAL_STORAGE_CART_NAME = 'cart';

    static async getInstance() {
        if (ShoppingCart.instance === null) {
            let menuItems = await fetch(ShoppingCart.URL).then((response) => response.json());
            ShoppingCart.instance = new ShoppingCart(menuItems);
        }
        return ShoppingCart.instance;
    }

    static async deserializeCartData(cartJson) {

        const tempCart = JSON.parse(cartJson);

        let menuItems = await fetch(ShoppingCart.URL).then((response) => response.json());
        let newCart = new ShoppingCart(menuItems);

        newCart.orderItems = tempCart.orderItems;
        newCart.itemQuantity = tempCart.itemQuantity;

        return newCart;
    }

    static async getCartFromLocalStorage() {
        return await ShoppingCart.deserializeCartData(
            localStorage.getItem(
                ShoppingCart.LOCAL_STORAGE_CART_NAME
            )
        );
    }

    addItemToCart(item) {
        const DefaultQty = 1;
        if (item === null) {
            return;
        }

        if (this.orderItems.includes(item)) {
            const index = this.orderItems.indexOf(item);
            this.itemQuantity[index]++;
            this.updateLocalStorage();
            return;
        }

        this.orderItems.push(item);
        this.itemQuantity.push(DefaultQty);
        this.updateLocalStorage();
        
    }

    removeItemFromCart(item) {
        if (item === null) {
            return;
        }

        if (!this.orderItems.includes(item)) {
            return;
        }

        const index = this.orderItems.indexOf(item);

        if (this.itemQuantity === 1) {
            this.orderItems.splice(index, 1);
            return;
        }

        this.itemQuantity[index]--;
    }

    updateLocalStorage() {
        localStorage.setItem(ShoppingCart.LOCAL_STORAGE_CART_NAME, JSON.stringify(this));
    }

}