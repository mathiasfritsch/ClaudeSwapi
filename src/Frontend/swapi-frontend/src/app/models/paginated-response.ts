export interface PaginatedResponse<T> {
  message: string;
  totalRecords: number;
  totalPages: number;
  previous?: string;
  next?: string;
  results: T[];
}