import { useState } from "react";
import { createPerson } from "../../../services/person.service";
import type { CreatePersonDto } from "../../../types/person.types";
import styles from "./PersonForm.module.css";

interface PersonFormProps {
  onSuccess?: () => void;
}

export function PersonForm({ onSuccess }: PersonFormProps) {
  const [formData, setFormData] = useState<CreatePersonDto>({
    name: "",
    age: 0,
  });
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "age" ? Number(value) : value,
    }));
  }

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setError(null);

    if (!formData.name.trim() || formData.age <= 0) {
      setError("Nome e idade (>0) são obrigatórios");
      return;
    }

    setLoading(true);
    try {
      await createPerson({
        name: formData.name.trim(),
        age: formData.age,
      });
      setFormData({ name: "", age: 0 });
      onSuccess?.();
    } catch (err) {
      const msg = err instanceof Error ? err.message : "Erro ao criar pessoa";
      setError(msg);
    } finally {
      setLoading(false);
    }
  }

  return (
    <form onSubmit={handleSubmit} className={styles.form}>
      <h2>Adicionar Pessoa</h2>

      <div className={styles.formGroup}>
        <label htmlFor="name">Nome</label>
        <input
          id="name"
          name="name"
          value={formData.name}
          onChange={handleChange}
          disabled={loading}
        />
      </div>

      <div className={styles.formGroup}>
        <label htmlFor="age">Idade</label>
        <input
          id="age"
          name="age"
          type="number"
          min={1}
          value={formData.age || ""}
          onChange={handleChange}
          disabled={loading}
        />
      </div>

      {error && <div className={styles.errorMessage}>{error}</div>}

      <button type="submit" disabled={loading} className={styles.submitButton}>
        {loading ? "Salvando..." : "Adicionar"}
      </button>
    </form>
  );
}
