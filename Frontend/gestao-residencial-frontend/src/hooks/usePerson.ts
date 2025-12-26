// src/hooks/usePersons.ts
import { useCallback, useEffect, useState } from "react";
import type { CreatePersonRequest, OperationFeedback, Person } from "../types";
import { personService } from "../services/personService";

export const usePersons = () => {
  const [persons, setPersons] = useState<Person[]>([]);
  const [feedback, setFeedback] = useState<OperationFeedback>({
    status: "idle",
  });

  const loadPersons = useCallback(async () => {
    setFeedback({ status: "loading" });
    try {
      const data = await personService.getAll();
      setPersons(data);
      setFeedback({ status: "success" });
    } catch (error) {
      setFeedback({
        status: "error",
        error: error.message ?? "Erro ao carregar pessoas",
      });
    }
  }, []);

  const createPerson = useCallback(async (payload: CreatePersonRequest) => {
    setFeedback({ status: "loading" });
    try {
      const created = await personService.create(payload);
      setPersons((prev) => [...prev, created]);
      setFeedback({
        status: "success",
        message: "Pessoa criada com sucesso.",
      });
    } catch (error: any) {
      setFeedback({
        status: "error",
        error: error.message ?? "Erro ao criar pessoa",
      });
    }
  }, []);

  const deletePerson = useCallback(async (id: number) => {
    setFeedback({ status: "loading" });
    try {
      await personService.delete(id);
      setPersons((prev) => prev.filter((p) => p.id !== id));
      setFeedback({
        status: "success",
        message: "Pessoa removida com sucesso.",
      });
    } catch (error: any) {
      setFeedback({
        status: "error",
        error: error.message ?? "Erro ao remover pessoa",
      });
    }
  }, []);

  useEffect(() => {
    loadPersons();
  }, [loadPersons]);

  return {
    persons,
    feedback,
    loadPersons,
    createPerson,
    deletePerson,
  };
};
