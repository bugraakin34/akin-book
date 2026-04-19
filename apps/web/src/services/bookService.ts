import type {
  PagedResponse,
  Book,
  CreateBookRequest,
  UpdateBookRequest,
} from "../types/book";
import { http } from "./http";

export async function getBooks(params: {
  page: number;
  pageSize: number;
  search?: string;
}) {
  const response = await http.get<PagedResponse<Book>>("/books", { params });
  return response.data;
}

export async function createBook(payload: CreateBookRequest) {
  const response = await http.post<Book>("/books", payload);
  return response.data;
}

export async function updateBook(id: string, payload: UpdateBookRequest) {
  const response = await http.put<Book>(`/books/${id}`, payload);
  return response.data;
}

export async function deleteBook(id: string) {
  await http.delete(`/books/${id}`);
}
