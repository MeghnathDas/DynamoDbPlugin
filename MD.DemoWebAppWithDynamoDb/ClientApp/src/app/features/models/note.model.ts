import { Category } from './category.model';

export interface Note {
    id: string;
    title: string;
    body: string;
    createdOn: string;
    lastUpdatedOn: string;
    _CategoryId: string;
    category: Category;
}
