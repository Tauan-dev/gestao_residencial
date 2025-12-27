// src/services/transaction.service.ts
import api from "./api";
import type {
  Transaction,
  CreateTransactionDto,
  UpdateTransactionDto,
} from "../types/transaction.types";
import { buildError } from "./httpError";

export async function getTransactions(): Promise<Transaction[]> {
  try {
    const { data } = await api.get<Transaction[]>("/Transaction");
    return data;
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function createTransaction(
  payload: CreateTransactionDto
): Promise<void> {
  try {
    await api.post("/Transaction", payload);
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function updateTransaction(
  id: number,
  payload: UpdateTransactionDto
): Promise<void> {
  try {
    await api.put(`/Transaction/${id}`, payload);
  } catch (error: unknown) {
    throw buildError(error);
  }
}

export async function deleteTransaction(id: number): Promise<void> {
  try {
    await api.delete(`/Transaction/${id}`);
  } catch (error: unknown) {
    throw buildError(error);
  }
}
