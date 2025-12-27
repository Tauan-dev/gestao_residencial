// src/services/category.service.ts
import apiClient from "./api";
import type {
  Category,
  CategoryFormData,
  CategorySpentResult,
  CategoryTotalsResponse,
} from "../types/category.types";
import { buildError } from "./httpError";

export async function getCategories(): Promise<Category[]> {
  try {
    const { data } = await apiClient.get<Category[]>("/Category");
    return data;
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function createCategory(dto: CategoryFormData): Promise<Category> {
  try {
    const { data } = await apiClient.post<Category>("/Category", dto);
    return data;
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function updateCategory(
  id: number,
  dto: CategoryFormData
): Promise<void> {
  try {
    await apiClient.put(`/Category/${id}`, dto);
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function deleteCategory(id: number): Promise<void> {
  try {
    await apiClient.delete(`/Category/${id}`);
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function getCategoryTotals(): Promise<CategoryTotalsResponse> {
  try {
    const { data } = await apiClient.get<CategoryTotalsResponse>(
      "/Category/totals"
    );
    return data;
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function getCategorySpent(): Promise<CategorySpentResult[]> {
  try {
    const { data } = await apiClient.get<CategorySpentResult[]>(
      "/Category/spent"
    );
    return data;
  } catch (error: unknown) {
    throw buildError(error);
  }
}

