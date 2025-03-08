import { Component, ViewChild } from '@angular/core';
import { NgForm, FormsModule } from '@angular/forms';
import { URLService } from '../../services/url.service';
import { CommonModule } from '@angular/common';
import { ViewUrlsComponent } from '../view-urls/view-urls.component';
import { ShortenURL } from '../../models/shorten-url.model';

@Component({
  selector: 'app-home',
  imports: [FormsModule, CommonModule, ViewUrlsComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  @ViewChild('f') signUpForm!: NgForm;
  urlShortenMessage!: string;
  constructor(
    private urlService: URLService
  ) { }

  onSubmit() {
    this.urlShortenMessage = '';
    this.urlService.generateShortenURL(this.signUpForm.form.value['longURL'])
      .subscribe({
        next: (urlShorten: ShortenURL) => {
          this.urlShortenMessage = `URL has been Shorten Succesfully!`;
        },
        error:() => {
          this.urlShortenMessage = 'URL is failed to Shorten';
        }
      });
    this.signUpForm.reset();
  }
}
