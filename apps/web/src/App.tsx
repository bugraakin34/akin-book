import { Navigate, Route, Routes } from "react-router-dom";
import { getAccessToken } from "./utils/storage";
import LoginPage from "./pages/LoginPage";
import BooksPage from "./pages/BooksPage";

function PrivateRoute({ children }: { children: React.ReactNode }) {
  const token = getAccessToken();

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  return <>{children}</>;
}

export default function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route
        path="/books"
        element={
          <PrivateRoute>
            <BooksPage />
          </PrivateRoute>
        }
      />
      <Route path="*" element={<Navigate to="/books" replace />} />
    </Routes>
  );
}