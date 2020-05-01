import { Component, OnInit } from '@angular/core';
import { Note } from '../models';
import { NoteService } from '../services';
import { AddNoteComponent } from './add-note/add-note.component';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {
  notes: Note[];

  constructor(private noteService: NoteService,
    private modalService: NgbModal) {
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
  openAddNoteDiag(note: Note) {
    const modalRef: NgbModalRef =
      this.modalService.open(AddNoteComponent);
    if (note) {
      modalRef.componentInstance.note = note;
    }
    modalRef.result.then((result) => {
      this.populateNoteCollection();
    },
      () => { }
    );
  }
}
