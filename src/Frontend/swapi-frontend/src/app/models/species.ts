export interface SpeciesSummary {
  uid: string;
  name: string;
  url: string;
}

export interface Species {
  uid: string;
  name: string;
  classification: string;
  designation: string;
  averageHeight: string;
  skinColors: string;
  hairColors: string;
  eyeColors: string;
  averageLifespan: string;
  homeworld: string;
  language: string;
  people: string[];
  films: string[];
  created: Date;
  edited: Date;
  url: string;
}