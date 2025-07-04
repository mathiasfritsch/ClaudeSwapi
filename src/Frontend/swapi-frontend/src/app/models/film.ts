export interface FilmSummary {
  uid: string;
  title: string;
  episodeId: number;
  director: string;
  producer: string;
  releaseDate: Date;
  url: string;
}

export interface Film {
  uid: string;
  title: string;
  episodeId: number;
  openingCrawl: string;
  director: string;
  producer: string;
  releaseDate: Date;
  characters: string[];
  planets: string[];
  starships: string[];
  vehicles: string[];
  species: string[];
  created: Date;
  edited: Date;
  url: string;
}