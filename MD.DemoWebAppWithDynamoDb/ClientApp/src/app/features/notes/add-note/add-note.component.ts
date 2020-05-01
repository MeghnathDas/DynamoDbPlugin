import { Component, OnInit, Input, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NoteService } from '../../services';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Note, Category } from '../../models';

@Component({
  selector: 'app-add-note',
  templateUrl: './add-note.component.html',
  styleUrls: ['./add-note.component.css']
})
export class AddNoteComponent implements OnInit {
  noteForm = new FormGroup({
    title: new FormControl('', Validators.required),
    body: new FormControl(''),
    category: new FormControl('', Validators.required)
  });
  categories: Category[];

  @Input() note: Note;

  constructor(private noteServ: NoteService,
    public activeModal: NgbActiveModal,
    private ref: ChangeDetectorRef) {
  }

  ngOnInit() {
    this.noteServ.getCategories(null).subscribe(catgs => {
      this.categories = catgs;
      if (this.note) {
        this.noteForm.patchValue({
          title: this.note.title,
          body: this.note.body,
          category: this.categories.filter(cg => cg.id === this.note._CategoryId)[0]
        });
      } else {
        this.noteForm.patchValue({
          category: this.categories[0]
        });
      }
    });
  }

  onAction() {
    if (this.noteForm.valid) {
      const noteToActOn = <Note>{
        title: this.noteForm.value.title,
        body: this.noteForm.value.body,
        _CategoryId: this.noteForm.value.category.id
      };
      if (this.note) {
        noteToActOn.id = this.note.id;
        this.noteServ.updateNote(noteToActOn).subscribe(() =>
          this.activeModal.close('Updated')
        );
      } else {
        this.noteServ.addNote(noteToActOn).subscribe(noteAdded =>
          this.activeModal.close(noteAdded)
        );
      }
    }
  }
  public dismissDiag(): void {
    if (this.activeModal) { this.activeModal.dismiss(); }
  }
}
