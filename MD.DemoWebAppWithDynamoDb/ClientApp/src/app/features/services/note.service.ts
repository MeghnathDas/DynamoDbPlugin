import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Note, Category } from '../models';

@Injectable()
export class NoteService {

    constructor(private httpc: HttpClient) {
    }

    getNotes(id: string): Observable<Note[]> {
        return this.httpc.get<Note[]>(`${environment.apiHost}/notes`);
    }

    getCategories(id: string): Observable<Category[]> {
        return this.httpc.get<Category[]>(`${environment.apiHost}/categories`);
    }
}
