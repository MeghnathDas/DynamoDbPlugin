import { Component, OnInit, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { BlockInteractionService } from './features/core';
import { KeyValue } from '@angular/common';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, AfterViewChecked {
  appTitle = 'MD';
  interactionBlocker = {
    visible: false,
    msg: undefined
  };

  constructor(private title: Title,
    private loadingHelper: BlockInteractionService,
    private cdr: ChangeDetectorRef) {
    title.setTitle('Notes - Using AWS Dynamo DB');
    this.appTitle = title.getTitle();
  }
  ngOnInit(): void {
    this.loadingHelper.onChange().subscribe(dta => this.onBlockerChage(dta));
  }
  ngAfterViewChecked(): void {
    this.cdr.detectChanges();
  }

  private onBlockerChage(data: KeyValue<boolean, string>) {
    console.log(data);
    this.interactionBlocker = {
      visible: data.key,
      msg: data.value
    };
  }
}
