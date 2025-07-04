import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatGridListModule } from '@angular/material/grid-list';
import { Router } from '@angular/router';

import { FilmsService } from '../../services/films.service';
import { FilmSummary } from '../../models/film';
import { PaginatedResponse } from '../../models/paginated-response';
import { FilmCardComponent } from '../film-card/film-card.component';

@Component({
  selector: 'app-films-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    FilmCardComponent
  ],
  templateUrl: './films-list.component.html',
  styleUrls: ['./films-list.component.scss']
})
export class FilmsListComponent implements OnInit {
  films: FilmSummary[] = [];
  loading = false;
  totalRecords = 0;
  currentPage = 0;
  pageSize = 10;

  constructor(
    private filmsService: FilmsService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadFilms();
  }

  loadFilms(page = 1): void {
    this.loading = true;
    this.filmsService.getFilms(page, this.pageSize).subscribe({
      next: (response: PaginatedResponse<FilmSummary>) => {
        this.films = response.results;
        this.totalRecords = response.totalRecords;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading films:', error);
        this.loading = false;
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadFilms(event.pageIndex + 1);
  }

  onFilmClick(film: FilmSummary): void {
    this.router.navigate(['/film', film.uid]);
  }
}