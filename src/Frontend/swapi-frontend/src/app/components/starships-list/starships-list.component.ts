import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatGridListModule } from '@angular/material/grid-list';
import { Router } from '@angular/router';

import { StarshipsService } from '../../services/starships.service';
import { StarshipSummary } from '../../models/starship';
import { PaginatedResponse } from '../../models/paginated-response';
import { StarshipCardComponent } from '../starship-card/starship-card.component';

@Component({
  selector: 'app-starships-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    StarshipCardComponent
  ],
  templateUrl: './starships-list.component.html',
  styleUrls: ['./starships-list.component.scss']
})
export class StarshipsListComponent implements OnInit {
  starships: StarshipSummary[] = [];
  loading = false;
  totalRecords = 0;
  currentPage = 0;
  pageSize = 10;

  constructor(
    private starshipsService: StarshipsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadStarships();
  }

  loadStarships(page = 1): void {
    this.loading = true;
    this.starshipsService.getStarships(page, this.pageSize).subscribe({
      next: (response: PaginatedResponse<StarshipSummary>) => {
        this.starships = response.results;
        this.totalRecords = response.totalRecords;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading starships:', error);
        this.loading = false;
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadStarships(event.pageIndex + 1);
  }

  onStarshipClick(starship: StarshipSummary): void {
    this.router.navigate(['/starship', starship.uid]);
  }
}