import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatListModule } from '@angular/material/list';
import { MatChipsModule } from '@angular/material/chips';

import { StarshipsService } from '../../services/starships.service';
import { Starship } from '../../models/starship';

@Component({
  selector: 'app-starship-detail',
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
  templateUrl: './starship-detail.component.html',
  styleUrls: ['./starship-detail.component.scss']
})
export class StarshipDetailComponent implements OnInit {
  starship: Starship | null = null;
  loading = false;
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private starshipsService: StarshipsService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadStarship(id);
    }
  }

  loadStarship(id: string): void {
    this.loading = true;
    this.error = null;
    
    this.starshipsService.getStarship(id).subscribe({
      next: (starship: Starship) => {
        this.starship = starship;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading starship:', error);
        this.error = 'Failed to load starship details';
        this.loading = false;
      }
    });
  }

  goBack(): void {
    this.router.navigate(['/starships']);
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }
}