export default class ShoppingCart {
    constructor(menuItems) {
        this.menuItems = menuItems;  // list of all possible items.
        this.orderItems = [];        // holds the id of the items.
        this.itemQuantity = [];      // holds qty of items.
    }

    static instance = null;

    static async getInstance() {
        if (ShoppingCart.instance === null) {
            const url = 'https://localhost:44354/api/Orders'
            let menuItems = await fetch(url).then((response) => response.json());
            ShoppingCart.instance = new ShoppingCart(menuItems);
        }
        return ShoppingCart.instance;
    }

    addItemToCart(item) {
        const DefaultQty = 1;
        if (item === null) {
            return;
        }

        if (this.orderItems.length > 0) {
            this.orderItems.forEach((currentItem) => {

                if (currentItem === item) {
                    let index = this.orderItems.indexOf(item);
                    this.itemQuantity[index]++;
                    return;
                }
                this.orderItems.push(item);
                this.itemQuantity.push(DefaultQty);
            });
        }
        else {
            this.orderItems.push(item);
            this.itemQuantity.push(DefaultQty);
        }
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

}