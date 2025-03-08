import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ShortenURL } from '../models/shorten-url.model';

@Injectable({
    providedIn: 'root'
})
export class URLService {
    constructor(
        private http: HttpClient
    ) { }

    generateShortenURL(longURL: string) {
        return this.http.post<ShortenURL>('https://localhost:7058/URLs/Shorten', {
                longURL: longURL
            });
    }

    getShortenURLs() {
        return this.http.get<ShortenURL[]>('https://localhost:7058/URLs');
    }
}