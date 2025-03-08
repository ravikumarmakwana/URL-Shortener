import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  private shortenURL = new BehaviorSubject<string | null>(null);

  constructor(
    private route: ActivatedRoute, 
    private http: HttpClient) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe((params: Params) => {
      const shorten = params['shorten'];
      if (shorten) {
        this.shortenURL.next(shorten);
      }
    });

    this.shortenURL.subscribe((shorten: string | null) => {
      if (shorten) {
        this.http.get(`https://localhost:7058/URLs/Access?shorten=${shorten}`, { responseType: 'text' })
    .subscribe({
        next: (longURL: string) => {
            console.log('Redirecting to:', longURL);
            window.location.href = longURL; // ✅ Redirect dynamically
        }
    });
      }
    });
  }
}
