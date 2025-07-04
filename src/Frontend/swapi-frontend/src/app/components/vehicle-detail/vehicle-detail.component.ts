import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatChipsModule } from '@angular/material/chips';

import { VehiclesService } from '../../services/vehicles.service';
import { Vehicle } from '../../models/vehicle';

@Component({
  selector: 'app-vehicle-detail',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatListModule,
    MatChipsModule
  ],
  templateUrl: './vehicle-detail.component.html',
  styleUrls: ['./vehicle-detail.component.scss']
})
export class VehicleDetailComponent implements OnInit {
  vehicle: Vehicle | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehiclesService: VehiclesService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadVehicle(id);
    }
  }

  loadVehicle(id: string): void {
    this.loading = true;
    this.error = null;
    
    this.vehiclesService.getVehicle(id).subscribe({
      next: (vehicle: Vehicle) => {
        this.vehicle = vehicle;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading vehicle:', error);
        this.error = 'Failed to load vehicle details';
        this.loading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/vehicles']);
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }
}