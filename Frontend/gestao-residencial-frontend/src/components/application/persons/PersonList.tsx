import { useEffect, useState } from "react";
import type {
  Person,
  PersonTotalsResponse,
  PersonSpentResult,
} from "../../../types/person.types";
import {
  getPersons,
  deletePerson,
  getPersonTotals,
  getPersonSpent,
} from "../../../services/person.service";
import styles from "./PersonList.module.css";

interface PersonListProps {
  refreshTrigger?: number;
}

export function PersonList({ refreshTrigger }: PersonListProps) {
  const [people, setPeople] = useState<Person[]>([]);
  const [totals, setTotals] = useState<PersonTotalsResponse | null>(null);
  const [spent, setSpent] = useState<PersonSpentResult[] | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  async function load() {
    setLoading(true);
    setError(null);
    try {
      const [personsData, totalsData, spentData] = await Promise.all([
        getPersons(),
        getPersonTotals(),
        getPersonSpent(),
      ]);
      setPeople(personsData);
      setTotals(totalsData);
      setSpent(spentData);
    } catch (err) {
      const msg =
        err instanceof Error ? err.message : "Erro ao carregar pessoas";
      setError(msg);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    load();
  }, [refreshTrigger]);

  async function handleDelete(id: number) {
    if (
      !confirm(
        "Ao deletar a pessoa, todas as transações dela serão removidas. Confirmar?"
      )
    ) {
      return;
    }

    try {
      await deletePerson(id);
      setPeople((prev) => prev.filter((p) => p.id !== id));

      const [newTotals, newSpent] = await Promise.all([
        getPersonTotals(),
        getPersonSpent(),
      ]);
      setTotals(newTotals);
      setSpent(newSpent);
    } catch (err) {
      const msg = err instanceof Error ? err.message : "Erro ao deletar pessoa";
      setError(msg);
    }
  }

  if (loading && people.length === 0) {
    return <p>Carregando...</p>;
  }

  return (
    <div className={styles.container}>
      <h2>Lista de Pessoas</h2>

      {/* badges de menor/maior gasto */}
      {spent && spent.length === 2 && (
        <div className={styles.badgesContainer}>
          <span className={`${styles.badge} ${styles.badgeDanger}`}>
            Maior gasto: {spent[1].name} ({spent[1].expense.toFixed(2)})
          </span>
          <span className={`${styles.badge} ${styles.badgeSuccess}`}>
            Menor gasto: {spent[0].name} ({spent[0].expense.toFixed(2)})
          </span>
        </div>
      )}

      {error && <div className={styles.error}>{error}</div>}

      {people.length === 0 ? (
        <p className={styles.empty}>Nenhuma pessoa cadastrada.</p>
      ) : (
        <>
          <table className={styles.table}>
            <thead>
              <tr>
                <th>ID</th>
                <th>Nome</th>
                <th>Idade</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {people.map((p) => (
                <tr key={p.id}>
                  <td>{p.id}</td>
                  <td>{p.name}</td>
                  <td>{p.age}</td>
                  <td>
                    <div className={styles.actions}>
                      <button
                        onClick={() => handleDelete(p.id)}
                        className={styles.deleteButton}
                      >
                        Deletar
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          {totals && (
            <div className={styles.totalsContainer}>
              <h3>Totais por Pessoa</h3>
              <table className={styles.table}>
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Pessoa</th>
                    <th>Total Receitas</th>
                    <th>Total Despesas</th>
                    <th>Saldo</th>
                  </tr>
                </thead>
                <tbody>
                  {totals.people.map((p) => (
                    <tr key={p.personId}>
                      <td>{p.personId}</td>
                      <td>{p.name}</td>
                      <td>{p.totalIncome.toFixed(2)}</td>
                      <td>{p.totalExpense.toFixed(2)}</td>
                      <td>{p.balance.toFixed(2)}</td>
                    </tr>
                  ))}
                  <tr className={styles.totalRow}>
                    <td colSpan={2}>Total Geral</td>
                    <td>{totals.total.totalIncome.toFixed(2)}</td>
                    <td>{totals.total.totalExpense.toFixed(2)}</td>
                    <td>{totals.total.balance.toFixed(2)}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          )}
        </>
      )}
    </div>
  );
}
