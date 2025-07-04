import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Vehicle, VehicleSummary } from '../models/vehicle';
import { PaginatedResponse } from '../models/paginated-response';

@Injectable({
  providedIn: 'root'
})
export class VehiclesService {
  private readonly apiUrl = 'http://localhost:5001/api';

  constructor(private http: HttpClient) { }

  getVehicles(page?: number, limit?: number): Observable<PaginatedResponse<VehicleSummary>> {
    let url = `${this.apiUrl}/vehicles`;
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
    
    return this.http.get<PaginatedResponse<VehicleSummary>>(url);
  }

  getVehicle(id: string): Observable<Vehicle> {
    return this.http.get<Vehicle>(`${this.apiUrl}/vehicles/${id}`);
  }
}