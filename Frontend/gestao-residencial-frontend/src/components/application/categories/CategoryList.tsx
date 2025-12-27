import { useEffect, useState } from "react";
import type {
  Category,
  CategoryPurpose,
  CategoryTotalsResponse,
  CategorySpentResult,
} from "../../../types/category.types";
import {
  getCategories,
  deleteCategory,
  updateCategory,
  getCategoryTotals,
  getCategorySpent,
} from "../../../services/category.service";
import styles from "./CategoryList.module.css";

interface CategoryListProps {
  refreshTrigger?: number;
}

export function CategoryList({ refreshTrigger }: CategoryListProps) {
  const [categorias, setCategorias] = useState<Category[]>([]);
  const [totals, setTotals] = useState<CategoryTotalsResponse | null>(null);
  const [spent, setSpent] = useState<CategorySpentResult[] | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [editingData, setEditingData] = useState<{
    description: string;
    purpose: CategoryPurpose;
  } | null>(null);

  async function carregarCategorias() {
    setLoading(true);
    setError(null);
    try {
      const [data, totalsData, spentData] = await Promise.all([
        getCategories(),
        getCategoryTotals(),
        getCategorySpent(),
      ]);
      setCategorias(data);
      setTotals(totalsData);
      setSpent(spentData);
    } catch (err) {
      const errorMessage =
        err instanceof Error ? err.message : "Erro ao carregar categorias";
      setError(errorMessage);
    } finally {
      setLoading(false);
    }
  }

  useEffect(() => {
    carregarCategorias();
  }, [refreshTrigger]);

  async function handleDelete(id: number) {
    if (!confirm("Deseja deletar esta categoria?")) return;

    try {
      await deleteCategory(id);
      setCategorias((prev) => prev.filter((c) => c.id !== id));

      const [totalsData, spentData] = await Promise.all([
        getCategoryTotals(),
        getCategorySpent(),
      ]);
      setTotals(totalsData);
      setSpent(spentData);
    } catch (err) {
      const errorMessage =
        err instanceof Error ? err.message : "Erro ao deletar";
      setError(errorMessage);
    }
  }

  function handleEdit(categoria: Category) {
    setEditingId(categoria.id);
    setEditingData({
      description: categoria.description,
      purpose: categoria.purpose,
    });
  }

  async function handleSaveEdit(id: number) {
    if (!editingData) return;

    try {
      await updateCategory(id, {
        description: editingData.description,
        purpose: editingData.purpose,
      });
      setCategorias((prev) =>
        prev.map((c) => (c.id === id ? { ...c, ...editingData } : c))
      );
      setEditingId(null);
      setEditingData(null);

      const [totalsData, spentData] = await Promise.all([
        getCategoryTotals(),
        getCategorySpent(),
      ]);
      setTotals(totalsData);
      setSpent(spentData);
    } catch (err) {
      const errorMessage =
        err instanceof Error ? err.message : "Erro ao atualizar";
      setError(errorMessage);
    }
  }

  function handleCancelEdit() {
    setEditingId(null);
    setEditingData(null);
  }

  function getPurposeLabel(purpose: CategoryPurpose) {
    if (purpose === "Receita") return "Receita";
    if (purpose === "Despesa") return "Despesa";
    return "Ambas";
  }

  function getPurposeColor(purpose: CategoryPurpose) {
    if (purpose === "Receita") return styles.receitaBadge;
    if (purpose === "Despesa") return styles.despesaBadge;
    return styles.ambasBadge ?? styles.despesaBadge;
  }

  if (loading && categorias.length === 0) return <div>Carregando...</div>;

  return (
    <div className={styles.container}>
      <h2>Lista de Categorias</h2>

      {spent && spent.length === 2 && (
        <div className={styles.badgesContainer}>
          <span className={`${styles.spentBadge} ${styles.spentBadgeMax}`}>
            Maior gasto: {spent[1].description} ({spent[1].expense.toFixed(2)})
          </span>
          <span className={`${styles.spentBadge} ${styles.spentBadgeMin}`}>
            Menor gasto: {spent[0].description} ({spent[0].expense.toFixed(2)})
          </span>
        </div>
      )}

      {error && <div className={styles.error}>{error}</div>}

      {categorias.length === 0 ? (
        <p className={styles.empty}>Nenhuma categoria encontrada</p>
      ) : (
        <>
          <table className={styles.table}>
            <thead>
              <tr>
                <th>ID</th>
                <th>Descrição</th>
                <th>Finalidade</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {categorias.map((categoria) => (
                <tr key={categoria.id}>
                  <td>{categoria.id}</td>
                  <td>
                    {editingId === categoria.id && editingData ? (
                      <input
                        type="text"
                        value={editingData.description}
                        onChange={(e) =>
                          setEditingData((prev) =>
                            prev
                              ? { ...prev, description: e.target.value }
                              : prev
                          )
                        }
                        className={styles.editInput}
                      />
                    ) : (
                      categoria.description
                    )}
                  </td>
                  <td>
                    {editingId === categoria.id && editingData ? (
                      <select
                        value={editingData.purpose}
                        onChange={(e) =>
                          setEditingData((prev) =>
                            prev
                              ? {
                                  ...prev,
                                  purpose: e.target.value as CategoryPurpose,
                                }
                              : prev
                          )
                        }
                        className={styles.editSelect}
                      >
                        <option value="Despesa">Despesa</option>
                        <option value="Receita">Receita</option>
                        <option value="Ambas">Ambas</option>
                      </select>
                    ) : (
                      <span
                        className={`${styles.badge} ${getPurposeColor(
                          categoria.purpose
                        )}`}
                      >
                        {getPurposeLabel(categoria.purpose)}
                      </span>
                    )}
                  </td>
                  <td>
                    <div className={styles.actions}>
                      {editingId === categoria.id ? (
                        <>
                          <button
                            onClick={() => handleSaveEdit(categoria.id)}
                            className={styles.saveButton}
                          >
                            Salvar
                          </button>
                          <button
                            onClick={handleCancelEdit}
                            className={styles.cancelButton}
                          >
                            Cancelar
                          </button>
                        </>
                      ) : (
                        <>
                          <button
                            onClick={() => handleEdit(categoria)}
                            className={styles.editButton}
                          >
                            Editar
                          </button>
                          <button
                            onClick={() => handleDelete(categoria.id)}
                            className={styles.deleteButton}
                          >
                            Deletar
                          </button>
                        </>
                      )}
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>

          {totals && (
            <div className={styles.totalsContainer}>
              <h3>Totais por Categoria</h3>
              <table className={styles.table}>
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Categoria</th>
                    <th>Total Receitas</th>
                    <th>Total Despesas</th>
                    <th>Saldo</th>
                  </tr>
                </thead>
                <tbody>
                  {totals.categories.map((c) => (
                    <tr key={c.categoryId}>
                      <td>{c.categoryId}</td>
                      <td>{c.description}</td>
                      <td>{c.totalIncome.toFixed(2)}</td>
                      <td>{c.totalExpense.toFixed(2)}</td>
                      <td>{c.balance.toFixed(2)}</td>
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

      <button onClick={carregarCategorias} className={styles.refreshButton}>
        Recarregar
      </button>
    </div>
  );
}
