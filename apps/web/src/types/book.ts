export interface Book {
    id:string;
    titleTr: string;
    titleEn: string;
    author: string;
    descriptionTr?: string;
    descriptionEn?: string;
    isbn?: string;
    coverUrl?: string;
    publishedYear?: number;
    userId: string;
    createdAt: string;
    updatedAt: string;
}

export interface PagedResponse<T> {
    items: T[];
    page: number;
    totalCount: number;
    totalPages: number;
    hasNext: boolean;
    hasPrevious: boolean;
}

export interface CreateBookRequest {
    titleTr: string;
    titleEn: string;
    author: string;
    descriptionTr?: string;
    descriptionEn?: string;
    isbn?: string;
    coverUrl?: string;
    publishedYear?: number;
}

export interface UpdateBookRequest extends CreateBookRequest {}