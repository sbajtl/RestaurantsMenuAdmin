import { ModelBase } from "./base.model";
import { Translation } from "./translation.model";

export class Category extends ModelBase {
    DefaultTitle?: string;
    Translations?: Array<Translation>;
}