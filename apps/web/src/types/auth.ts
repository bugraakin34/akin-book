export interface LoginRequest {
    email: string;
    password: string;
}

export interface AuthResponse {
    accessToken: string;
    refreshToken: string;
}

export interface MeResponse {
    userId: string;
    email: string;
    role: string;
}