import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatGridListModule } from '@angular/material/grid-list';
import { Router } from '@angular/router';

import { VehiclesService } from '../../services/vehicles.service';
import { VehicleSummary } from '../../models/vehicle';
import { PaginatedResponse } from '../../models/paginated-response';
import { VehicleCardComponent } from '../vehicle-card/vehicle-card.component';

@Component({
  selector: 'app-vehicles-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    VehicleCardComponent
  ],
  templateUrl: './vehicles-list.component.html',
  styleUrls: ['./vehicles-list.component.scss']
})
export class VehiclesListComponent implements OnInit {
  vehicles: VehicleSummary[] = [];
  loading = false;
  totalRecords = 0;
  currentPage = 0;
  pageSize = 10;

  constructor(
    private vehiclesService: VehiclesService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadVehicles();
  }

  loadVehicles(page = 1): void {
    this.loading = true;
    this.vehiclesService.getVehicles(page, this.pageSize).subscribe({
      next: (response: PaginatedResponse<VehicleSummary>) => {
        this.vehicles = response.results;
        this.totalRecords = response.totalRecords;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading vehicles:', error);
        this.loading = false;
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadVehicles(event.pageIndex + 1);
  }

  onVehicleClick(vehicle: VehicleSummary): void {
    this.router.navigate(['/vehicle', vehicle.uid]);
  }
}