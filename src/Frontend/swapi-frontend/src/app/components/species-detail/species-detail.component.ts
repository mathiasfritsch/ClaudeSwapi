import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatChipsModule } from '@angular/material/chips';

import { SpeciesService } from '../../services/species.service';
import { Species } from '../../models/species';

@Component({
  selector: 'app-species-detail',
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
  templateUrl: './species-detail.component.html',
  styleUrls: ['./species-detail.component.scss']
})
export class SpeciesDetailComponent implements OnInit {
  species: Species | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private speciesService: SpeciesService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadSpecies(id);
    }
  }

  loadSpecies(id: string): void {
    this.loading = true;
    this.error = null;
    
    this.speciesService.getSpeciesDetail(id).subscribe({
      next: (species: Species) => {
        this.species = species;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading species:', error);
        this.error = 'Failed to load species details';
        this.loading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/species']);
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }
}