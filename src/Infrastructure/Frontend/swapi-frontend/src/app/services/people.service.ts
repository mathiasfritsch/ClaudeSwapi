import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Person, PersonSummary } from '../models/person';
import { PaginatedResponse } from '../models/paginated-response';

@Injectable({
  providedIn: 'root'
})
export class PeopleService {
  private readonly apiUrl = 'http://localhost:5001/api';

  constructor(private http: HttpClient) { }

  getPeople(page?: number, limit?: number): Observable<PaginatedResponse<PersonSummary>> {
    let url = `${this.apiUrl}/people`;
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
    
    return this.http.get<PaginatedResponse<PersonSummary>>(url);
  }

  getPerson(id: string): Observable<Person> {
    return this.http.get<Person>(`${this.apiUrl}/people/${id}`);
  }
}