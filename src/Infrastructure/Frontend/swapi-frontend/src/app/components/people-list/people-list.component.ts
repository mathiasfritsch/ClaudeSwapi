import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatGridListModule } from '@angular/material/grid-list';
import { Router } from '@angular/router';

import { PeopleService } from '../../services/people.service';
import { PersonSummary } from '../../models/person';
import { PaginatedResponse } from '../../models/paginated-response';
import { PersonCardComponent } from '../person-card/person-card.component';

@Component({
  selector: 'app-people-list',
  standalone: true,
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatGridListModule,
    PersonCardComponent
  ],
  templateUrl: './people-list.component.html',
  styleUrls: ['./people-list.component.scss']
})
export class PeopleListComponent implements OnInit {
  people: PersonSummary[] = [];
  loading = false;
  totalRecords = 0;
  currentPage = 0;
  pageSize = 10;

  constructor(
    private peopleService: PeopleService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.loadPeople();
  }

  loadPeople(page = 1): void {
    this.loading = true;
    this.peopleService.getPeople(page, this.pageSize).subscribe({
      next: (response: PaginatedResponse<PersonSummary>) => {
        this.people = response.results;
        this.totalRecords = response.totalRecords;
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading people:', error);
        this.loading = false;
      }
    });
  }

  onPageChange(event: PageEvent): void {
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadPeople(event.pageIndex + 1); // API is 1-based
  }

  onPersonClick(person: PersonSummary): void {
    this.router.navigate(['/person', person.uid]);
  }
}