import axios from "axios";
import { getAccessToken, removeAccessToken } from "../utils/storage";
import { redirectToLoginWithSessionExpired } from "../utils/authRedirect";

export const http = axios.create({
  baseURL: "http://localhost:5057/api",
});

http.interceptors.request.use((config) => {
  const token = getAccessToken();

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

http.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      removeAccessToken();
      redirectToLoginWithSessionExpired();
    }

    return Promise.reject(error);
  },
);
