import { Component, Input, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Category } from 'src/app/shared/models/category.model';
import { RestaurantCategory } from 'src/app/shared/models/restaurant-category.model';
import { RestaurantService } from 'src/app/shared/services/restaurant.service';

@Component({
  selector: 'app-category-select-dialog',
  templateUrl: './category-select-dialog.component.html',
  styleUrls: ['./multiselect-categories.scss']
})
export class CategorySelectDialogComponent implements OnInit {

  categories: Category[] = [];
  selectedCategories: Category[] = [];

  constructor(
    private restaurantService: RestaurantService,
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ) { }

  ngOnInit(): void {
    this.restaurantService.getCategories(this.config.data?.languageId, this.config.data?.restaurantId).then(data => {
      this.categories = data;
    });
  }

  save() {
    this.selectCategories(this.selectedCategories);
  }

  cancel() {
    this.selectCategories([]);
    this.ref.close();
  }

  selectCategories(categories: Category[]) {
    this.ref.close(categories);
  }
}
