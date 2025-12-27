import { useState } from "react";
import type {
  CategoryFormData,
  CategoryPurpose,
} from "../../../types/category.types";
import { createCategory } from "../../../services/category.service";
import styles from "./CategoryForm.module.css";

interface CategoryFormProps {
  onSuccess?: () => void;
}

const PURPOSE_OPTIONS: CategoryPurpose[] = ["Despesa", "Receita", "Ambas"];

export function CategoryForm({ onSuccess }: CategoryFormProps) {
  const [formData, setFormData] = useState<CategoryFormData>({
    description: "",
    purpose: "Despesa",
  });

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  function handleChange(
    e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>
  ) {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "purpose" ? (value as CategoryPurpose) : value,
    }));
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError(null);

    if (!formData.description.trim()) {
      setError("Descrição é obrigatória");
      return;
    }

    setLoading(true);
    try {
      await createCategory({
        description: formData.description.trim(),
        purpose: formData.purpose,
      });

      setFormData({ description: "", purpose: "Despesa" });
      onSuccess?.();
    } catch (err) {
      const msg =
        err instanceof Error ? err.message : "Erro ao criar categoria";
      setError(msg);
    } finally {
      setLoading(false);
    }
  }

  return (
    <form onSubmit={handleSubmit} className={styles.form}>
      <h2>Adicionar Categoria</h2>

      <div className={styles.formGroup}>
        <label htmlFor="description">Descrição</label>
        <input
          id="description"
          name="description"
          type="text"
          value={formData.description}
          onChange={handleChange}
          disabled={loading}
          placeholder="Ex: Salário, Aluguel, Alimentação"
        />
      </div>

      <div className={styles.formGroup}>
        <label htmlFor="purpose">Finalidade</label>
        <select
          id="purpose"
          name="purpose"
          value={formData.purpose}
          onChange={handleChange}
          disabled={loading}
        >
          {PURPOSE_OPTIONS.map((p) => (
            <option key={p} value={p}>
              {p}
            </option>
          ))}
        </select>
      </div>

      {error && <div className={styles.errorMessage}>{error}</div>}

      <button type="submit" disabled={loading} className={styles.submitButton}>
        {loading ? "Salvando..." : "Adicionar"}
      </button>
    </form>
  );
}
