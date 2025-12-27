import { useState } from "react";
import { CategoryForm } from "../components/application/categories/CategoryForm";
import { CategoryList } from "../components/application/categories/CategoryList";
import { Alert } from "../components/common/Feedback/Alert";
import styles from "./CategoryPage.module.css";

export function CategoryPage() {
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  function handleSuccess() {
    setSuccess("Categoria salva com sucesso");
    setError(null);
    setRefreshTrigger((prev) => prev + 1);
  }

  return (
    <div className={styles.page}>
      <div className={styles.pageInner}>
        <h1 className={styles.title}>Categorias</h1>

        {error && <Alert type="error">{error}</Alert>}
        {success && <Alert type="success">{success}</Alert>}

        <div className={styles.layout}>
          <div className={styles.formCard}>
            <CategoryForm onSuccess={handleSuccess} />
          </div>
    
        </div>

        <section className={styles.listSection}>
          <h2 className={styles.listTitle}>Lista de Categorias</h2>
          <div className={styles.listCard}>
            <CategoryList refreshTrigger={refreshTrigger} />
          </div>
        </section>
      </div>
    </div>
  );
}
