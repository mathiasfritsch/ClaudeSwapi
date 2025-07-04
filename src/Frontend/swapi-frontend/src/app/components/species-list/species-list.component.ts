import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatGridListModule } from '@angular/material/grid-list';
import { Router } from '@angular/router';

import { SpeciesService } from '../../services/species.service';
import { SpeciesSummary } from '../../models/species';
import { PaginatedResponse } from '../../models/paginated-response';
import { SpeciesCardComponent } from '../species-card/species-card.component';

@Component({
  selector: 'app-species-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    SpeciesCardComponent
  ],
  templateUrl: './species-list.component.html',
  styleUrls: ['./species-list.component.scss']
})
export class SpeciesListComponent implements OnInit {
  species: SpeciesSummary[] = [];
  loading = false;
  totalRecords = 0;
  currentPage = 0;
  pageSize = 10;

  constructor(
    private speciesService: SpeciesService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadSpecies();
  }

  loadSpecies(page = 1): void {
    this.loading = true;
    this.speciesService.getSpecies(page, this.pageSize).subscribe({
      next: (response: PaginatedResponse<SpeciesSummary>) => {
        this.species = response.results;
        this.totalRecords = response.totalRecords;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading species:', error);
        this.loading = false;
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadSpecies(event.pageIndex + 1);
  }

  onSpeciesClick(species: SpeciesSummary): void {
    this.router.navigate(['/species', species.uid]);
  }
}