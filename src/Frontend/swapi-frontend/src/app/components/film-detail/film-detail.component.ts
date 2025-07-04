import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatChipsModule } from '@angular/material/chips';

import { FilmsService } from '../../services/films.service';
import { Film } from '../../models/film';

@Component({
  selector: 'app-film-detail',
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
  templateUrl: './film-detail.component.html',
  styleUrls: ['./film-detail.component.scss']
})
export class FilmDetailComponent implements OnInit {
  film: Film | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private filmsService: FilmsService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadFilm(id);
    }
  }

  loadFilm(id: string): void {
    this.loading = true;
    this.error = null;
    
    this.filmsService.getFilm(id).subscribe({
      next: (film: Film) => {
        this.film = film;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading film:', error);
        this.error = 'Failed to load film details';
        this.loading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/films']);
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }
}