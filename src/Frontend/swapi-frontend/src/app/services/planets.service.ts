import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Planet, PlanetSummary } from '../models/planet';
import { PaginatedResponse } from '../models/paginated-response';

@Injectable({
  providedIn: 'root'
})
export class PlanetsService {
  private readonly apiUrl = 'http://localhost:5001/api';

  constructor(private http: HttpClient) { }

  getPlanets(page?: number, limit?: number): Observable<PaginatedResponse<PlanetSummary>> {
    let url = `${this.apiUrl}/planets`;
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
    
    return this.http.get<PaginatedResponse<PlanetSummary>>(url);
  }

  getPlanet(id: string): Observable<Planet> {
    return this.http.get<Planet>(`${this.apiUrl}/planets/${id}`);
  }
}