import { ModelBase } from "./base.model";
import { CategoryTranslation } from "./category-translation.model";

export class Restaurant extends ModelBase {
    Title?: string;
    Description?: string;
    StyleCode?: string;
    Translations?: Array<CategoryTranslation>;
}