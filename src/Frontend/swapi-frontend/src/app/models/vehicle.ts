export interface VehicleSummary {
  uid: string;
  name: string;
  url: string;
}

export interface Vehicle {
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
  vehicleClass: string;
  pilots: string[];
  films: string[];
  created: Date;
  edited: Date;
  url: string;
}