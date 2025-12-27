import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { PersonsPage } from "./pages/PersonsPage";
import { CategoryPage } from "./pages/CategoryPage";
import { TransactionsPage } from "./pages/TransactionsPage";
import { AppLayout } from "./components/layout/AppLayout";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<AppLayout />}>
          <Route index element={<Navigate to="/pessoas" replace />} />
          <Route path="/pessoas" element={<PersonsPage />} />
          <Route path="/categorias" element={<CategoryPage />} />
          <Route path="/transacoes" element={<TransactionsPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
