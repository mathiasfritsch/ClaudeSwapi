import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatChipsModule } from '@angular/material/chips';

import { PlanetsService } from '../../services/planets.service';
import { Planet } from '../../models/planet';

@Component({
  selector: 'app-planet-detail',
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
  templateUrl: './planet-detail.component.html',
  styleUrls: ['./planet-detail.component.scss']
})
export class PlanetDetailComponent implements OnInit {
  planet: Planet | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private planetsService: PlanetsService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadPlanet(id);
    }
  }

  loadPlanet(id: string): void {
    this.loading = true;
    this.error = null;
    
    this.planetsService.getPlanet(id).subscribe({
      next: (planet: Planet) => {
        this.planet = planet;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading planet:', error);
        this.error = 'Failed to load planet details';
        this.loading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/planets']);
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }
}