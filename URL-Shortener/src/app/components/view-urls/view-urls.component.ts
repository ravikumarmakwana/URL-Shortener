import { Component, OnInit } from '@angular/core';
import { URLService } from '../../services/url.service';
import { ShortenURL } from '../../models/shorten-url.model';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-view-urls',
  imports: [CommonModule],
  templateUrl: './view-urls.component.html',
  styleUrl: './view-urls.component.css'
})
export class ViewUrlsComponent implements OnInit{
  shortenURLs: ShortenURL[] = [];

  constructor(
    private urlService: URLService
  ) { }

  ngOnInit(): void {
    this.urlService.getShortenURLs()
      .subscribe({
        next: (urls: ShortenURL[]) => {
          this.shortenURLs = urls;
        },
        error: (err) => {
          console.error('Error fetching URLs:', err);
        }
      });    
  }
}
