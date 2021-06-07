import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../models/category.model';
import { Language } from '../models/language.model';
import { Restaurant } from '../models/restaurant.model';
import { CategoryTranslation } from '../models/category-translation.model';
import { CategoryGet } from '../models/extenders/category-get.model';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  readonly baseUrl = "https://localhost:44354/api";

  constructor(private http: HttpClient) { }

  getRestaurants(languageId?: string) {
    return this.http.get<any>(this.baseUrl + "/Restaurants/" + languageId)
      .toPromise()
      .then(res => <Restaurant[]>res)
      .then(data => {
        return data;
      });
  }

  getLanguages() {
    return this.http.get<any>(this.baseUrl + "/Languages")
      .toPromise()
      .then(res => <Language[]>res)
      .then(data => {
        return data;
      });
  }

  getAllCategories(languageId?: string) {
    return this.http.get<any>(this.baseUrl + "/Categories/" + languageId)
      .toPromise()
      .then(res => <CategoryGet[]>res)
      .then(data => {
        return data;
      });
  }

  getCategories(languageId?: string, restaurantId?: string) {
    return this.http.get<any>(this.baseUrl + "/Categories/" + languageId + "/" + restaurantId)
      .toPromise()
      .then(res => <Category[]>res)
      .then(data => {
        return data;
      });
  }

  post(data: any) {
    return this.http.post<any>(this.baseUrl + "/Restaurants/", data);
  }

  insertOrUpdateCategories(data: any) {
    return this.http.post<any>(this.baseUrl + "/Categories/", data);
  }

  postRestaurants(data: any) {
    return this.http.post<any>(this.baseUrl + "/Restaurants/Save/", data);
  }

  saveCategories(data: any) {
    return this.http.post<any>(this.baseUrl + "/Categories/Save/", data);
  }

  updateCategories(data: any) {
    return this.http.post<any>(this.baseUrl + "/Categories/Update/", data);
  }

  deleteCategories(data: any) {
    return this.http.post<any>(this.baseUrl + "/Categories/Delete/", data);
  }

  deleteCategory(data: any) {
    return this.http.post<any>(this.baseUrl + "/Categories/DeleteOne/", data);
  }

  deleteProduct(data: any) {
    return this.http.post<any>(this.baseUrl + "/Restaurants/Product/Delete/", data);
  }

  postProductTranslations(translationData: { items: string[], translation: CategoryTranslation }) {
    return this.http.post<any>(this.baseUrl, translationData);
  }

  putProductTranslations(translation: CategoryTranslation) {
    return this.http.put<any>(`${this.baseUrl}/${translation.LanguageId}`, translation);
  }
}
