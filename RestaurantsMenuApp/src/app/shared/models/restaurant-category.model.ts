import { RestaurantProductGet } from "./extenders/restaurant-product-get";
import { RestaurantProduct } from "./restaurant-product.model";

export class RestaurantCategory {
    CategoryId?: string;
    CategoryTitle?: string;
    CategoryOrder?: number;
    RestaurantCategories?: Array<RestaurantCategory>;
    Products?: Array<RestaurantProduct>
    OrderChanged: boolean = false;
}
