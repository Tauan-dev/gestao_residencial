import { useEffect, useState } from "react";
import type { Transaction } from "../../../types/transaction.types";
import {
  getTransactions,
  deleteTransaction,
} from "../../../services/transaction.service";
import styles from "./TransactionList.module.css";

interface TransactionListProps {
  refreshTrigger?: number;
}

export function TransactionList({ refreshTrigger }: TransactionListProps) {
  const [items, setItems] = useState<Transaction[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function load() {
    setLoading(true);
    setError(null);
    try {
      const data = await getTransactions();
      setItems(data);
    } catch (err) {
      const msg =
        err instanceof Error ? err.message : "Erro ao carregar transações";
      setError(msg);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    load();
  }, [refreshTrigger]);

  async function handleDelete(id: number) {
    if (!confirm("Deseja deletar esta transação?")) return;
    try {
      await deleteTransaction(id);
      setItems((prev) => prev.filter((t) => t.id !== id));
    } catch (err) {
      const msg =
        err instanceof Error ? err.message : "Erro ao deletar transação";
      setError(msg);
    }
  }

  if (loading && items.length === 0) return <p>Carregando...</p>;

  return (
    <div className={styles.container}>
      {error && <div className={styles.error}>{error}</div>}

      {items.length === 0 ? (
        <p className={styles.empty}>Nenhuma transação encontrada.</p>
      ) : (
        <table className={styles.table}>
          <thead>
            <tr>
              <th>ID</th>
              <th>Pessoa</th>
              <th>Categoria</th>
              <th>Tipo</th>
              <th>Valor</th>
              <th>Descrição</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {items.map((t) => (
              <tr key={t.id}>
                <td>{t.id}</td>
                <td>{t.person?.name ?? `#${t.personId}`}</td>
                <td>{t.category?.description ?? `#${t.categoryId}`}</td>
                <td>{t.type}</td>
                <td>{t.value.toFixed(2)}</td>
                <td>{t.description}</td>
                <td>
                  <button
                    onClick={() => handleDelete(t.id)}
                    className={styles.deleteButton}
                  >
                    Deletar
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}
