import { Component, forwardRef, Inject, Input, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Utils } from 'src/app/helpers/utils';
import { RestaurantCategory } from 'src/app/shared/models/restaurant-category.model';
import { RestaurantProduct } from 'src/app/shared/models/restaurant-product.model';
import { RestaurantService } from 'src/app/shared/services/restaurant.service';
import { RestaurantComponent } from '../restaurant.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styles: [
  ]
})
export class ProductComponent implements OnInit {

  @Input() languageId!: string;
  @Input() restaurantId!: string;
  @Input() category!: RestaurantCategory;
  @Input() products!: RestaurantProduct[];

  oldProducts: RestaurantProduct[] = [];
  producsToSave: RestaurantProduct[] = [];

  isEditMode: boolean = false;
  categoryOrderChange: boolean = false;

  constructor(
    @Inject(forwardRef(() => RestaurantComponent)) private restaurantComponent: RestaurantComponent,
    private restaurantService: RestaurantService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService) {
  }


  ngOnInit(): void {
    Utils.cloneArray(this.products, this.oldProducts);
  }

  addNewProduct() {
    return { ProductId: '', DefaultTitle: '', Title: '', Description: '', UnitOfMeasure: '', Price: '' };
  }

  onSave() {
    this.products.forEach(product => {
      let found = this.oldProducts.find(x => x.ProductId == product.ProductId);
      if (!product.ProductId || (product.DefaultTitle !== found?.DefaultTitle || product.Title !== found?.Title || product.Description !== found?.Description || product.Price !== found?.Price)) {
        if (!this.producsToSave.includes(product)) {
          this.producsToSave.push(product);
        }
      }
    });

    const dataToSave = { LanguageId: this.languageId, RestaurantId: this.restaurantId, CategoryId: this.category.CategoryId, Products: this.producsToSave };

    this.restaurantService.post(dataToSave).subscribe(
      res => {
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
        this.isEditMode = false;
        if (this.categoryOrderChange) {
          this.updateCategory();
        }
        this.restaurantComponent.loadData();
      },
      err => console.log(err));

  }

  updateCategory() {
    let updateData = { RestaurantId: this.restaurantId, Category: this.category }

    this.restaurantService.updateCategories(updateData).subscribe(res => {
      this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
      this.restaurantComponent.loadData();
    }, err => console.log(err));
  }

  deleteCategory() {
    this.confirmationService.confirm({
      message: 'This category and all its products will be deleted! Are you sure that you want to perform this action?',
      accept: () => {
        let deleteData = { RestaurantId: this.restaurantId, Category: this.category }

        this.restaurantService.deleteCategories(deleteData).subscribe(res => {
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
          this.restaurantComponent.loadData();
        }, err => console.log(err));
      }
    });
  }

  deleteProduct(e: any, product: RestaurantProduct) {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to perform this action?',
      accept: () => {
        let deleteData = { RestaurantId: this.restaurantId, CategoryId: this.category.CategoryId, Product: product }

        this.restaurantService.deleteProduct(deleteData).subscribe(res => {
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
          this.restaurantComponent.loadData();
        }, err => console.log(err));
      }
    });
  }

  onChange(e: any) {
    this.isEditMode = true;
  };

  onChangeOrder(e: any, category: RestaurantCategory) {
    this.isEditMode = true;
    this.categoryOrderChange = true;
  }

  cancel() {
    this.isEditMode = false;
    this.categoryOrderChange = false;
    this.restaurantComponent.loadData();
  }
}
