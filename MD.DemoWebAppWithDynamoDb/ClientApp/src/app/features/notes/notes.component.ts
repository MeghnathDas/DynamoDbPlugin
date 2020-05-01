import { Component, OnInit } from '@angular/core';
import { Note } from '../models';
import { NoteService } from '../services';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  notes: Note[];

  constructor(private noteService: NoteService) {
  }

  ngOnInit(): void {
    this.populateNoteCollection();
  }
  populateNoteCollection() {
    this.noteService.getNotes(null).subscribe(noteData => {
      this.notes = noteData;
    });
  }
  getInfo(note: Note): string {
    let strNfo = `Added on: ${note.createdOn}`;
    if (note.lastUpdatedOn) {
      strNfo += `\nLast updated on: ${note.lastUpdatedOn}`;
    }
    return strNfo;
  }
  removeItem(note: Note) {
    this.noteService.removeNote(note.id).subscribe(() => this.populateNoteCollection());
  }
}
