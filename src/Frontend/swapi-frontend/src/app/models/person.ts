export interface PersonSummary {
  uid: string;
  name: string;
  url: string;
}

export interface Person {
  uid: string;
  name: string;
  gender?: string;
  skinColor?: string;
  hairColor?: string;
  height?: string;
  eyeColor?: string;
  mass?: string;
  birthYear?: string;
  homeworld?: string;
  created: Date;
  edited: Date;
  url: string;
}