import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Film, FilmSummary } from '../models/film';
import { PaginatedResponse } from '../models/paginated-response';

@Injectable({
  providedIn: 'root'
})
export class FilmsService {
  private readonly apiUrl = 'http://localhost:5001/api';

  constructor(private http: HttpClient) { }

  getFilms(page?: number, limit?: number): Observable<PaginatedResponse<FilmSummary>> {
    let url = `${this.apiUrl}/films`;
    const params: string[] = [];
    
    if (page !== undefined) {
      params.push(`page=${page}`);
    }
    if (limit !== undefined) {
      params.push(`limit=${limit}`);
    }
    
    if (params.length > 0) {
      url += `?${params.join('&')}`;
    }
    
    return this.http.get<PaginatedResponse<FilmSummary>>(url);
  }

  getFilm(id: string): Observable<Film> {
    return this.http.get<Film>(`${this.apiUrl}/films/${id}`);
  }
}