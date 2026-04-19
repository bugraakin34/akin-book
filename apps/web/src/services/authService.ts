import type { AuthResponse, LoginRequest, MeResponse } from "../types/auth";
import { http } from "./http";

export async function login(payload: LoginRequest) {
  const response = await http.post<AuthResponse>("/auth/login", payload);
  return response.data;
}

export async function getMe() {
  const response = await http.get<MeResponse>("/auth/me");
  return response.data;
}
