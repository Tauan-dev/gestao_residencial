import { useState } from "react";
import { PersonForm } from "../components/application/persons/PersonForm";
import { PersonList } from "../components/application/persons/PersonList";
import styles from "./PersonPage.module.css";

export function PersonsPage() {
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  function handleSuccess() {
    setRefreshTrigger((prev) => prev + 1);
  }

  return (
    <div className={styles.page}>
      <div className={styles.pageInner}>
        <h1 className={styles.title}>Pessoas</h1>

        <div className={styles.layout}>
          <div className={styles.formCard}>
            <PersonForm onSuccess={handleSuccess} />
          </div>

          <section className={styles.listSection}>
            <h2 className={styles.listTitle}>Lista de Pessoas</h2>
            <div className={styles.listCard}>
              <PersonList refreshTrigger={refreshTrigger} />
            </div>
          </section>
        </div>
      </div>
    </div>
  );
}
