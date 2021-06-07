import { Input, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { Language } from '../shared/models/language.model';
import { RestaurantCategory } from '../shared/models/restaurant-category.model';
import { Restaurant } from '../shared/models/restaurant.model';
import { RestaurantService } from '../shared/services/restaurant.service';
import { RestaurantCategoryComponent } from './restaurant-category/restaurant-category.component';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CategorySelectDialogComponent } from '../dialogs/category/category-select-dialog/category-select-dialog.component';
import { Category } from '../shared/models/category.model';


@Component({
  selector: 'app-restaurant',
  templateUrl: './restaurant.component.html',
  styles: [`
        :host ::ng-deep .p-cell-editing {
            padding-top: 10 !important;
            padding-bottom: 10 !important;
        }
    `],
  providers: [DialogService]
})
export class RestaurantComponent implements OnInit {

  @Input() language!: string;
  restaurants!: Restaurant[];
  categories: RestaurantCategory[] = [];

  activeIndex: number = 0;
  selectedLanguage?: Language;
  languages!: Language[];
  oldRestaurants: Restaurant[] = [];
  restaurantsToSave: Restaurant[] = [];
  isEditMode: boolean = false;
  @ViewChild('ch') private restaurantCategory!: RestaurantCategoryComponent;

  constructor(
    private dialogService: DialogService,
    private restaurantService: RestaurantService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.restaurantService.getLanguages().then(data => {
      this.languages = data;
      this.initDropDownLanguage();
      this.restaurantService.getRestaurants(this.selectedLanguage!.Id).then(data => {
        this.restaurants = data;
        this.initCategoriesByLanguage(this.restaurants, this.selectedLanguage!.Id);
      });
    });
  }

  initDropDownLanguage() {
    this.selectedLanguage = this.languages.find((language) => {
      if (language.Mnemonic == "hr") {
        this.language = language.Id || "";
        return language;
      }
      return new Language;
    }) || new Language;
  }

  languageChange(selectedLanguage?: Language) {
    this.restaurantService.getRestaurants(selectedLanguage!.Id).then(data => {
      this.initCategoriesByLanguage(data, selectedLanguage!.Id)
    });

    this.language = selectedLanguage?.Id || "";

    this.restaurantService.getRestaurants(this.selectedLanguage!.Id).then(data => {
      this.restaurants = data;
    });
  }

  initCategoriesByLanguage(data: Restaurant[], language?: string) {
    data.forEach((item) => {
      let translation = item.Translations?.find(x => x.LanguageId === language);
      this.categories = translation?.RestaurantCategories !== undefined ? translation!.RestaurantCategories : [];
      // console.log("KATEGORIJE " + item.Title, this.categories)
    });
  }

  addNew() {
    return { Id: '', Title: '', Description: '' };
  }

  cancel() {
    this.isEditMode = false;
    this.loadData();
  }

  onRowEditSave() {
    this.restaurants.forEach(restaurant => {
      let found = this.oldRestaurants.find(x => x.Id == restaurant.Id);
      if (!restaurant.Id || (restaurant.Title !== found?.Title || restaurant.Description !== found?.Description)) {
        if (!this.restaurantsToSave.includes(restaurant)) {
          this.restaurantsToSave.push(restaurant);
        }
      }
    });

    const dataToSave = { Restaurants: this.restaurantsToSave };
    //this.apiHelper.post(dataToSave);
    this.restaurantService.postRestaurants(dataToSave).subscribe(
      res => {
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
        this.isEditMode = false;
        this.loadData();
      },
      err => console.log(err));
  }

  onChange(e: any) {
    this.isEditMode = true;
  };

  categoryDialogReference!: DynamicDialogRef;

  showCategoriesDialog(restaurantId: string) {
    this.categoryDialogReference = this.dialogService.open(CategorySelectDialogComponent, {
      header: 'Choose a Categories',
      width: '30%',
      contentStyle: { "max-height": "500px", "overflow": "auto" },
      //baseZIndex: 10000,
      data: { restaurantId: restaurantId, languageId: this.language, existingCategories: this.restaurantCategory.categories }
    });

    this.saveCategories(restaurantId);
  }

  saveCategories(restaurantId: string) {
    this.categoryDialogReference.onClose.subscribe((selectedCategories: Category[]) => {
      if (selectedCategories?.length > 0) {

        let saveData = { RestaurantId: restaurantId, Categories: selectedCategories }
        this.restaurantService.saveCategories(saveData).subscribe(res => {
          console.log(res);
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
          this.loadData();
        }, err => console.log(err))
      }
    });
  }
}
