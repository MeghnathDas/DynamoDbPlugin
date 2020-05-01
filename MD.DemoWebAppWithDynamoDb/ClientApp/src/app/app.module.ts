import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NotesComponent } from './features/notes/notes.component';
import { CategoriesComponent } from './features/categories/categories.component';
import { NoteService } from './features/services';
import { AddNoteComponent } from './features/notes/add-note/add-note.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    NotesComponent,
    CategoriesComponent,
    AddNoteComponent
  ],
  imports: [
    NgbModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: NotesComponent, pathMatch: 'full' },
      { path: 'categories', component: CategoriesComponent },
    ])
  ],
  providers: [NoteService],
  bootstrap: [AppComponent],
  entryComponents: [AddNoteComponent]
})
export class AppModule { }
