export interface StarshipSummary {
  uid: string;
  name: string;
  url: string;
}

export interface Starship {
  uid: string;
  name: string;
  model: string;
  manufacturer: string;
  costInCredits: string;
  length: string;
  maxAtmospheringSpeed: string;
  crew: string;
  passengers: string;
  cargoCapacity: string;
  consumables: string;
  hyperdriveRating: string;
  mglt: string;
  starshipClass: string;
  pilots: string[];
  films: string[];
  created: Date;
  edited: Date;
  url: string;
}