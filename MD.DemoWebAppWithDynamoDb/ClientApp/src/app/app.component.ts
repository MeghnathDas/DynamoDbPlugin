import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
    appTitle = 'MD';
    constructor(private title: Title) {
        title.setTitle('Notes - Using AWS Dynamo DB');
        this.appTitle = title.getTitle();
    }
}
