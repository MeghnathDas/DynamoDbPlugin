import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Note, Category } from '../models';

@Injectable()
export class NoteService {
    private basePath = environment.apiHost;
    private notesPath = '/notes';
    private catgsPath = '/categories';

    constructor(private httpc: HttpClient) {
    }

    getNotes(id: string): Observable<Note[]> {
        return this.httpc.get<Note[]>(this.basePath + this.notesPath);
    }

    getCategories(id: string): Observable<Category[]> {
        return this.httpc.get<Category[]>(this.basePath + this.catgsPath);
    }

    removeNote(id: string): Observable<any> {
      return this.httpc.delete(this.basePath + this.notesPath + '/' + id);
    }
    removeCategory(id: string): Observable<any> {
        return this.httpc.delete(this.basePath + this.catgsPath + '/' + id);
    }
}
