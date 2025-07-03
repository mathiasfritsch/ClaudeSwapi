import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { PersonSummary } from '../../models/person';

@Component({
  selector: 'app-person-card',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './person-card.component.html',
  styleUrls: ['./person-card.component.scss']
})
export class PersonCardComponent {
  @Input() person!: PersonSummary;
  @Output() personClick = new EventEmitter<PersonSummary>();

  onCardClick(): void {
    this.personClick.emit(this.person);
  }
}