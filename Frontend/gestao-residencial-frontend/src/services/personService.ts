// src/services/personService.ts
import apiClient from "./api";
import type { Person, CreatePersonRequest } from "../types";

export const personService = {
  async getAll(): Promise<Person[]> {
    const response = await apiClient.get<Person[]>("/persons");
    return response.data;
  },

  async create(data: CreatePersonRequest): Promise<Person> {
    const response = await apiClient.post<Person>("/persons", data);
    return response.data;
  },

  async delete(id: number): Promise<void> {
    await apiClient.delete(`/persons/${id}`);
  },
};
