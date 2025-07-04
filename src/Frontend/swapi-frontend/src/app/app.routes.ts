import { Routes } from '@angular/router';
import { PeopleListComponent } from './components/people-list/people-list.component';
import { PersonDetailComponent } from './components/person-detail/person-detail.component';
import { FilmsListComponent } from './components/films-list/films-list.component';
import { FilmDetailComponent } from './components/film-detail/film-detail.component';
import { PlanetsListComponent } from './components/planets-list/planets-list.component';
import { PlanetDetailComponent } from './components/planet-detail/planet-detail.component';
import { SpeciesListComponent } from './components/species-list/species-list.component';
import { SpeciesDetailComponent } from './components/species-detail/species-detail.component';
import { StarshipsListComponent } from './components/starships-list/starships-list.component';
import { StarshipDetailComponent } from './components/starship-detail/starship-detail.component';
import { VehiclesListComponent } from './components/vehicles-list/vehicles-list.component';
import { VehicleDetailComponent } from './components/vehicle-detail/vehicle-detail.component';

export const routes: Routes = [
  { path: '', redirectTo: '/people', pathMatch: 'full' },
  { path: 'people', component: PeopleListComponent },
  { path: 'person/:id', component: PersonDetailComponent },
  { path: 'films', component: FilmsListComponent },
  { path: 'film/:id', component: FilmDetailComponent },
  { path: 'planets', component: PlanetsListComponent },
  { path: 'planet/:id', component: PlanetDetailComponent },
  { path: 'species', component: SpeciesListComponent },
  { path: 'species/:id', component: SpeciesDetailComponent },
  { path: 'starships', component: StarshipsListComponent },
  { path: 'starship/:id', component: StarshipDetailComponent },
  { path: 'vehicles', component: VehiclesListComponent },
  { path: 'vehicle/:id', component: VehicleDetailComponent },
  { path: '**', redirectTo: '/people' }
];