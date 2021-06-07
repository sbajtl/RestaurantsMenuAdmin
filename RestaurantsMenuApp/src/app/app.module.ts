import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TableModule } from 'primeng/table';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { ToolbarModule } from 'primeng/toolbar';
import { ToastModule } from 'primeng/toast';
import { CardModule, } from 'primeng/card';
import { TabViewModule } from 'primeng/tabview';
import {TreeTableModule} from 'primeng/treetable';
import { AddRowDirective } from './directives/add-row.directive';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { AccordionModule } from 'primeng/accordion';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { MultiSelectModule } from 'primeng/multiselect';
import {PanelMenuModule} from 'primeng/panelmenu';
import { PanelModule } from 'primeng/panel';
import {TreeModule} from 'primeng/tree';
import { TreeNode } from 'primeng/api';
import {ConfirmDialogModule} from 'primeng/confirmdialog';


import { AppComponent } from './app.component';
import { RestaurantComponent } from './restaurant/restaurant.component';

import { RestaurantService } from './shared/services/restaurant.service';
import { ProductComponent } from './restaurant/product/product.component';
import { ConfirmationService, MessageService } from 'primeng/api';
import { InputNumberModule } from 'primeng/inputnumber';
import { TabViewComponent } from './tab-view/tab-view.component';
import { RestaurantCategoryComponent } from './restaurant/restaurant-category/restaurant-category.component';
import { CategorySelectDialogComponent } from './dialogs/category/category-select-dialog/category-select-dialog.component';
import { CategoryComponent } from './category/category/category.component';


@NgModule({
  declarations: [
    AppComponent,
    RestaurantComponent,
    ProductComponent,
    AddRowDirective,
    TabViewComponent,
    RestaurantCategoryComponent,
    CategorySelectDialogComponent,
    CategoryComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    TableModule,
    TreeTableModule,
    FormsModule,
    ButtonModule,
    ToolbarModule,
    InputTextModule,
    InputNumberModule,
    ToastModule,
    CardModule,
    TabViewModule,
    DropdownModule,
    AccordionModule,
    DynamicDialogModule,
    MultiSelectModule,
    PanelMenuModule,
    PanelModule,
    TreeModule,
    ConfirmDialogModule
  ],
  providers: [RestaurantService, MessageService, ConfirmationService],
  bootstrap: [AppComponent]
})
export class AppModule { }
