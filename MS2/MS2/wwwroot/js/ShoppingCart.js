
export default class ShoppingCart {
    constructor(menuItems) {
        this.menuItems = menuItems;  // list of all possible items.
        this.orderItems = [];        // holds the id of the items.
        this.itemQuantity = [];      // holds qty of items.
        this.itemSize = [];
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
        newCart.itemSize = tempCart.itemSize;

        return newCart;
    }

    static async getCartFromLocalStorage() {
        return await ShoppingCart.deserializeCartData(
            localStorage.getItem(
                ShoppingCart.LOCAL_STORAGE_CART_NAME
            )
        );
    }

    addItemToCart(item, size) {
        const DefaultQty = 1;
        if (item === null) {
            return;
        }

        let occurrencesOfItem = this.getOccurrencesOfItem(item);

        for (let i = 0; i < occurrencesOfItem.length; i++) {

            let index = occurrencesOfItem[i];
            if (this.orderItems[index] == item && this.itemSize[index] == size) {
                this.itemQuantity[index]++;
                this.updateLocalStorage();
                return;
            }
        }

        this.orderItems.push(item);
        this.itemQuantity.push(DefaultQty);
        this.itemSize.push(size);
        this.updateLocalStorage();   
    }

    removeItemFromCart(item,size) {
        if (item === null) {
            return;
        }

        let occurrencesOfItem = this.getOccurrencesOfItem(item);

        for (let i = 0; i < occurrencesOfItem.length; i++) {
            let index = occurrencesOfItem[i];

            if (this.orderItems[index] == item && this.itemSize[index] == size && this.itemQuantity[index] === 1)
            {
                this.orderItems.splice(index, 1);
                this.itemQuantity.splice(index, 1);
                this.itemSize.splice(index, 1);
                this.updateLocalStorage();
                return;
            }

            else if (this.orderItems[index] == item && this.itemSize[index] == size) {
                this.itemQuantity[index]--;
                this.updateLocalStorage();
                return;
            }
        }
    }

    updateLocalStorage() {
        localStorage.setItem(ShoppingCart.LOCAL_STORAGE_CART_NAME, JSON.stringify(this));
    }

    // find indexs of all occurrences of an item regardless of size.
    getOccurrencesOfItem(item) {
        let occurrencesOfItem = []; 
        for (let i = 0; i < this.orderItems.length; i++) {
            if (this.orderItems[i] == item) {
                occurrencesOfItem.push(i);
            }
        }
        return occurrencesOfItem;
    }

    static clearCart() {
        localStorage.removeItem(ShoppingCart.LOCAL_STORAGE_CART_NAME);
    }

}