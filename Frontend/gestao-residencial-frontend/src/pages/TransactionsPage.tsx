import { useState } from "react";
import { TransactionForm } from "../components/application/transactions/TransactionForm";
import { TransactionList } from "../components/application/transactions/TransactionList";
import styles from "./TransactionPage.module.css";

export function TransactionsPage() {
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  function handleSuccess() {
    setRefreshTrigger((prev) => prev + 1);
  }

  return (
    <div className={styles.page}>
      <div className={styles.pageInner}>
        <h1 className={styles.title}>Transações</h1>

        <div className={styles.layout}>
          <div className={styles.formCard}>
            <TransactionForm onSuccess={handleSuccess} />
          </div>

          <section className={styles.listSection}>
            <h2 className={styles.listTitle}>Lista de Transações</h2>
            <div className={styles.listCard}>
              <TransactionList refreshTrigger={refreshTrigger} />
            </div>
          </section>
        </div>
      </div>
    </div>
  );
}
