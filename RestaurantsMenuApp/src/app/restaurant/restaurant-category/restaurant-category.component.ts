import { forwardRef, Inject } from '@angular/core';
import { Component, Input, OnInit } from '@angular/core';
import { MessageService, TreeNode } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CategorySelectDialogComponent } from 'src/app/dialogs/category/category-select-dialog/category-select-dialog.component';
import { Category } from 'src/app/shared/models/category.model';
import { RestaurantCategory } from 'src/app/shared/models/restaurant-category.model';
import { RestaurantService } from 'src/app/shared/services/restaurant.service';
import { RestaurantComponent } from '../restaurant.component';

@Component({
  selector: 'app-restaurant-category',
  templateUrl: './restaurant-category.component.html',
  styleUrls: []
})
export class RestaurantCategoryComponent implements OnInit {

  @Input() categories!: RestaurantCategory[];
  @Input() restaurantId!: string;
  @Input() languageId!: string;

  treeNodes: TreeNode<RestaurantCategory>[] = [];
  cols!: any[];

  constructor(
    private dialogService: DialogService,
    private messageService: MessageService,
    @Inject(forwardRef(() => RestaurantComponent)) private restaurantComponent: RestaurantComponent,
    private restaurantService: RestaurantService
  ) {
    this.cols = [
      { field: 'CategoryTitle', header: 'Category' },
    ];
  }

  ngOnInit(): void {
    this.categoriesToTreeNodes(this.categories);
  }

  private categoriesToTreeNodes(categories: RestaurantCategory[]) {
    for (let cont of categories) {
      this.treeNodes.push(this.mainCategoriesToTreeNode(cont));
    }
  }

  private mainCategoriesToTreeNode(category: RestaurantCategory): TreeNode {
    let categoriesTreeNodes: TreeNode<RestaurantCategory>[] = [];

    for (let c of category.RestaurantCategories!) {
      categoriesTreeNodes.push(this.categoryToTreeNode(c));
    }

    return {
      expanded: categoriesTreeNodes.length > 0 ? true : false,
      label: category.CategoryTitle,
      data: category,
      children: categoriesTreeNodes
    };
  }

  private categoryToTreeNode(category: RestaurantCategory): TreeNode {
    let categoriesTreeNodes: TreeNode<RestaurantCategory>[] = [];
    for (let c of category.RestaurantCategories!) {
      categoriesTreeNodes.push(this.categoryToTreeNode(c));
    }
    return {
      expanded: categoriesTreeNodes.length > 0 ? true : false,
      label: category.CategoryTitle,
      data: category,
      children: categoriesTreeNodes,
    }
  }

  categoryDialogReference!: DynamicDialogRef;

  showCategoriesDialog(e: any, restaurantCategory: RestaurantCategory) {
    e.stopPropagation(); // prevent open accordion on button click
    // if (restaurantCategory!.RestaurantCategories!.length == 0) {
    this.categoryDialogReference = this.dialogService.open(CategorySelectDialogComponent, {
      header: 'Choose a Categories',
      width: '30%',
      contentStyle: { "max-height": "500px", "overflow": "auto" },
      //baseZIndex: 10000,
      data: { restaurantId: this.restaurantId, languageId: this.languageId, existingCategories: this.categories }
    });

    this.saveCategories(this.restaurantId, restaurantCategory);
    // }
  }

  saveCategories(restaurantId: string, restaurantCategory: RestaurantCategory) {
    this.categoryDialogReference.onClose.subscribe((selectedCategories: Category[]) => {
      if (selectedCategories?.length > 0) {

        let saveData = { RestaurantId: restaurantId, Categories: selectedCategories, Parent: restaurantCategory }

        this.restaurantService.saveCategories(saveData).subscribe(res => {
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
          this.restaurantComponent.loadData();
        }, err => console.log(err));
      }
    });
  }

  onChangeOrder(e: any, restaurantCategory: RestaurantCategory) {
    restaurantCategory.OrderChanged = true;
  }

  cancel(e: any, restaurantCategory: RestaurantCategory) {
    restaurantCategory.OrderChanged = false;
    this.restaurantComponent.loadData();
  }

  updateCategory(e: any, restaurantCategory: RestaurantCategory) {
    let updateData = { RestaurantId: this.restaurantId, Category: restaurantCategory }

    this.restaurantService.updateCategories(updateData).subscribe(res => {
      this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
      this.restaurantComponent.loadData();
    }, err => console.log(err));
  }

}
