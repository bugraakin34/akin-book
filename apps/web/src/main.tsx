import React from "react";
import ReactDom from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { QueryProvider } from "./app/providers/QueryProvider.tsx";
import { BrowserRouter } from "react-router-dom";

ReactDom.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <QueryProvider>
      <BrowserRouter>
        <App />
      </BrowserRouter>
    </QueryProvider>
  </React.StrictMode>
);
