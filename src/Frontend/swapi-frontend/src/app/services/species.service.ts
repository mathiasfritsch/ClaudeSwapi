import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Species, SpeciesSummary } from '../models/species';
import { PaginatedResponse } from '../models/paginated-response';

@Injectable({
  providedIn: 'root'
})
export class SpeciesService {
  private readonly apiUrl = 'http://localhost:5001/api';

  constructor(private http: HttpClient) { }

  getSpecies(page?: number, limit?: number): Observable<PaginatedResponse<SpeciesSummary>> {
    let url = `${this.apiUrl}/species`;
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
    
    return this.http.get<PaginatedResponse<SpeciesSummary>>(url);
  }

  getSpeciesDetail(id: string): Observable<Species> {
    return this.http.get<Species>(`${this.apiUrl}/species/${id}`);
  }
}