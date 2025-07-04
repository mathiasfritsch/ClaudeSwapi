import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatGridListModule } from '@angular/material/grid-list';
import { Router } from '@angular/router';

import { PlanetsService } from '../../services/planets.service';
import { PlanetSummary } from '../../models/planet';
import { PaginatedResponse } from '../../models/paginated-response';
import { PlanetCardComponent } from '../planet-card/planet-card.component';

@Component({
  selector: 'app-planets-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    PlanetCardComponent
  ],
  templateUrl: './planets-list.component.html',
  styleUrls: ['./planets-list.component.scss']
})
export class PlanetsListComponent implements OnInit {
  planets: PlanetSummary[] = [];
  loading = false;
  totalRecords = 0;
  currentPage = 0;
  pageSize = 10;

  constructor(
    private planetsService: PlanetsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadPlanets();
  }

  loadPlanets(page = 1): void {
    this.loading = true;
    this.planetsService.getPlanets(page, this.pageSize).subscribe({
      next: (response: PaginatedResponse<PlanetSummary>) => {
        this.planets = response.results;
        this.totalRecords = response.totalRecords;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading planets:', error);
        this.loading = false;
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadPlanets(event.pageIndex + 1);
  }

  onPlanetClick(planet: PlanetSummary): void {
    this.router.navigate(['/planet', planet.uid]);
  }
}