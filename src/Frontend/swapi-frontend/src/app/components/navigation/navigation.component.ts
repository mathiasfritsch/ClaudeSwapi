import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-navigation',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule
  ],
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent {
  navigationItems = [
    { path: '/people', label: 'People', icon: 'person' },
    { path: '/films', label: 'Films', icon: 'movie' },
    { path: '/planets', label: 'Planets', icon: 'public' },
    { path: '/species', label: 'Species', icon: 'pets' },
    { path: '/starships', label: 'Starships', icon: 'rocket_launch' },
    { path: '/vehicles', label: 'Vehicles', icon: 'directions_car' }
  ];
}