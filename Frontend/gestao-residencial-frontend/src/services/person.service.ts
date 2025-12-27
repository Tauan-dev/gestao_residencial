import apiClient from "./api";
import type {
  Person,
  CreatePersonDto,
  UpdatePersonDto,
  PersonTotalsResponse,
  PersonSpentResult,
} from "../types/person.types";
import { buildError } from "./httpError";



export async function getPersons(): Promise<Person[]> {
  try {
    const { data } = await apiClient.get<Person[]>("/Person");
    return data;
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function getPerson(id: number): Promise<Person> {
  try {
    const { data } = await apiClient.get<Person>(`/Person/${id}`);
    return data;
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function createPerson(dto: CreatePersonDto): Promise<void> {
  try {
    await apiClient.post("/Person", dto);
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function updatePerson(
  id: number,
  dto: UpdatePersonDto
): Promise<void> {
  try {
    await apiClient.put(`/Person/${id}`, dto);
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function deletePerson(id: number): Promise<void> {
  try {
    await apiClient.delete(`/Person/${id}`);
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function getPersonTotals(): Promise<PersonTotalsResponse> {
  try {
    const { data } = await apiClient.get<PersonTotalsResponse>(
      "/Person/totals"
    );
    return data;
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function getPersonSpent(): Promise<PersonSpentResult[]> {
  try {
    const { data } = await apiClient.get<PersonSpentResult[]>("/Person/spent");
    return data;
  } catch (error: unknown) {
    throw buildError(error);
  }
}

