import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { SpeciesSummary } from '../../models/species';

@Component({
  selector: 'app-species-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './species-card.component.html',
  styleUrls: ['./species-card.component.scss']
})
export class SpeciesCardComponent {
  @Input() species!: SpeciesSummary;
}