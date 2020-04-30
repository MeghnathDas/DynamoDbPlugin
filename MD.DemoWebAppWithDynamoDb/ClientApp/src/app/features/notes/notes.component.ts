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
    this.noteService.getNotes(null).subscribe(noteData => {
      this.notes = noteData;
    });
  }
}
