import { Component, OnInit } from '@angular/core';
import { Note, Category } from '../models';
import { NoteService } from '../services';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  categories: Category[];

  constructor(private noteService: NoteService) {
  }

  ngOnInit(): void {
    this.populateCategoryCollection();
  }
  populateCategoryCollection() {
    this.noteService.getCategories(null).subscribe(catgData => {
      this.categories = catgData;
    });
  }
  removeItem(catg: Category) {
    this.noteService.removeCategory(catg.id).subscribe(() => this.populateCategoryCollection());
  }
}
