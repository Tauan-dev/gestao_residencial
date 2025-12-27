import { useEffect, useState } from "react";
import { createTransaction } from "../../../services/transaction.service";
import { getPersons } from "../../../services/person.service";
import { getCategories } from "../../../services/category.service";
import type {
  CreateTransactionDto,
  TransactionType,
} from "../../../types/transaction.types";
import type { Person } from "../../../types/person.types";
import type { Category } from "../../../types/category.types";
import styles from "./TransactionForm.module.css";

interface TransactionFormProps {
  onSuccess?: () => void;
}

const TYPE_OPTIONS: TransactionType[] = ["Despesa", "Receita"];

export function TransactionForm({ onSuccess }: TransactionFormProps) {
  const [formData, setFormData] = useState<CreateTransactionDto>({
    description: "",
    value: 0,
    type: "Despesa",
    personId: 0,
    categoryId: 0,
  });

  const [people, setPeople] = useState<Person[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(false);
  const [loadingRefs, setLoadingRefs] = useState(false);
  const [error, setError] = useState<string | null>(null);

  // carrega pessoas e categorias para os selects
  useEffect(() => {
    async function loadRefs() {
      try {
        setLoadingRefs(true);
        const [pessoas, cats] = await Promise.all([
          getPersons(),
          getCategories(),
        ]);
        setPeople(pessoas);
        setCategories(cats);
      } catch (err) {
        const msg =
          err instanceof Error
            ? err.message
            : "Erro ao carregar pessoas/categorias";
        setError(msg);
      } finally {
        setLoadingRefs(false);
      }
    }

    loadRefs();
  }, []);

  function handleChange(
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]:
        name === "value" || name === "personId" || name === "categoryId"
          ? Number(value)
          : name === "type"
          ? (value as TransactionType)
          : value,
    }));
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError(null);

    if (
      !formData.description.trim() ||
      formData.value <= 0 ||
      formData.personId <= 0 ||
      formData.categoryId <= 0
    ) {
      setError("Pessoa, categoria, valor (>0) e descrição são obrigatórios");
      return;
    }

    setLoading(true);
    try {
      await createTransaction({
        ...formData,
        description: formData.description.trim(),
      });

      setFormData({
        description: "",
        value: 0,
        type: "Despesa",
        personId: 0,
        categoryId: 0,
      });
      onSuccess?.();
    } catch (err) {
      const msg =
        err instanceof Error ? err.message : "Erro ao criar transação";
      setError(msg);
    } finally {
      setLoading(false);
    }
  }

  return (
    <form onSubmit={handleSubmit} className={styles.form}>
      <h2>Adicionar Transação</h2>

      <div className={styles.formGroup}>
        <label htmlFor="personId">Pessoa</label>
        <select
          id="personId"
          name="personId"
          value={formData.personId || ""}
          onChange={handleChange}
          disabled={loading || loadingRefs}
        >
          <option value="">Selecione uma pessoa</option>
          {people.map((p) => (
            <option key={p.id} value={p.id}>
              {p.name}
            </option>
          ))}
        </select>
      </div>

      <div className={styles.formGroup}>
        <label htmlFor="categoryId">Categoria</label>
        <select
          id="categoryId"
          name="categoryId"
          value={formData.categoryId || ""}
          onChange={handleChange}
          disabled={loading || loadingRefs}
        >
          <option value="">Selecione uma categoria</option>
          {categories.map((c) => (
            <option key={c.id} value={c.id}>
              {c.description}
            </option>
          ))}
        </select>
      </div>

      <div className={styles.formGroup}>
        <label htmlFor="type">Tipo</label>
        <select
          id="type"
          name="type"
          value={formData.type}
          onChange={handleChange}
          disabled={loading}
        >
          {TYPE_OPTIONS.map((t) => (
            <option key={t} value={t}>
              {t}
            </option>
          ))}
        </select>
      </div>

      <div className={styles.formGroup}>
        <label htmlFor="value">Valor</label>
        <input
          id="value"
          name="value"
          type="number"
          step="0.01"
          min={0.01}
          value={formData.value || ""}
          onChange={handleChange}
          disabled={loading}
        />
      </div>

      <div className={styles.formGroup}>
        <label htmlFor="description">Descrição</label>
        <input
          id="description"
          name="description"
          value={formData.description}
          onChange={handleChange}
          disabled={loading}
        />
      </div>

      {error && <div className={styles.errorMessage}>{error}</div>}

      <button
        type="submit"
        disabled={loading || loadingRefs}
        className={styles.submitButton}
      >
        {loading ? "Salvando..." : "Adicionar"}
      </button>
    </form>
  );
}
