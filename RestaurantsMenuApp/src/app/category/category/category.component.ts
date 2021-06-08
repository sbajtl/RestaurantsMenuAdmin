import { Component, forwardRef, Inject, Input, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Utils } from 'src/app/helpers/utils';
import { Category } from 'src/app/shared/models/category.model';
import { CategoryGet } from 'src/app/shared/models/extenders/category-get.model';
import { Language } from 'src/app/shared/models/language.model';
import { RestaurantService } from 'src/app/shared/services/restaurant.service';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styles: [
  ]
})
export class CategoryComponent implements OnInit {

  categories: CategoryGet[] = [];
  oldCategories: CategoryGet[] = [];
  languages!: Language[];
  selectedLanguage?: Language;
  @Input() language!: string;
  isEditMode: boolean = false;
  categoriesToSave: CategoryGet[] = [];

  constructor(
    private restaurantService: RestaurantService,
    private messageService: MessageService,
    private confirmationService: ConfirmationService,
  ) { }

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.restaurantService.getLanguages().then(data => {
      this.languages = data;
      this.initDropDownLanguage();
      this.initCategories();
    });
  }

  initCategories() {
    this.oldCategories = [];
    this.restaurantService.getAllCategories(this.selectedLanguage!.Id).then(data => {
      this.categories = data;
      this.initCategoriesByLanguage(this.categories, this.selectedLanguage!.Id);
      Utils.cloneArray(this.categories, this.oldCategories);
    });
  }

  initCategoriesByLanguage(data: CategoryGet[], language?: string) {
    this.categories = [];
    data.forEach((item) => {
      let translation = item.Translations?.find(x => x.LanguageId === language);
      item.CategoryTitle = translation?.Title ? translation?.Title : '';
      item.CategoryDescription = translation?.Description ? translation?.Description : '';
      this.categories.push(item);
    });
  }

  languageChange(selectedLanguage?: Language) {
    this.initCategories();
  }

  initDropDownLanguage() {
    this.selectedLanguage = this.languages.find((language) => {
      if (language.Mnemonic == "hr") {
        this.language = language?.Id || "";
        return language;
      }
      return new Language;
    }) || new Language;
  }

  onChange(e: any) {
    this.isEditMode = true;
  };

  cancel() {
    this.isEditMode = false;
    this.loadData();
  }

  onSave() {
    this.categories.forEach((category: CategoryGet) => {
      let found = this.oldCategories!.find(x => x.Id == category.Id);
      if ((category.DefaultTitle !== found?.DefaultTitle || category.CategoryTitle !== found?.CategoryTitle || category.CategoryDescription !== found?.CategoryDescription)) {
        if (!this.categoriesToSave.includes(category)) {
          this.categoriesToSave.push(category);
        }
      }
    });

    const dataToSave = { LanguageId: this.selectedLanguage?.Id, Categories: this.categoriesToSave };
    console.log(dataToSave)

    this.restaurantService.insertOrUpdateCategories(dataToSave).subscribe(
      res => {
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
        this.isEditMode = false;

        this.loadData();
        this.categoriesToSave = [];
      },
      err => console.log(err));
  }

  addNewCategory() {
    return { Id: '', DefaultTitle: '', CategoryTitle: '', CategoryDescription: '' };
  }

  deleteCategory(e: any, category: Category) {
    this.confirmationService.confirm({
      message: 'Are you sure that you want to perform this action?',
      accept: () => {
        let deleteData = { Category: category }

        this.restaurantService.deleteCategory(deleteData).subscribe(res => {
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Operation successfully executed.', life: 3000 });
          this.loadData();
        }, err => this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Cannot delete category beacuse it is already in use!.', life: 5000 }));
      }
    });
  }

}
