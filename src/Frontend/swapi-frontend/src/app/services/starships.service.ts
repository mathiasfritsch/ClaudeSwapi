import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Starship, StarshipSummary } from '../models/starship';
import { PaginatedResponse } from '../models/paginated-response';

@Injectable({
  providedIn: 'root'
})
export class StarshipsService {
  private readonly apiUrl = 'http://localhost:5001/api';

  constructor(private http: HttpClient) { }

  getStarships(page?: number, limit?: number): Observable<PaginatedResponse<StarshipSummary>> {
    let url = `${this.apiUrl}/starships`;
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
    
    return this.http.get<PaginatedResponse<StarshipSummary>>(url);
  }

  getStarship(id: string): Observable<Starship> {
    return this.http.get<Starship>(`${this.apiUrl}/starships/${id}`);
  }
}