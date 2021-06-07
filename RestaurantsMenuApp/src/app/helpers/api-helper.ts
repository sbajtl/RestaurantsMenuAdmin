import { ConfirmationService, MessageService } from "primeng/api";
import { RestaurantService } from "../shared/services/restaurant.service";

export class ApiHelper {
    constructor(
        private restaurantService: RestaurantService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) {

    }

    public post(data: any) {
        return this.restaurantService.post(data).subscribe(
            res => {
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
            },
            err => console.log(err));
    }
}
