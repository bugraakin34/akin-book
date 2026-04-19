export function redirectToLoginWithSessionExpired() {
  localStorage.setItem("session_expired", "true");
  window.location.href = "/login";
}