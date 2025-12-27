export type TransactionType = "Despesa" | "Receita";

export interface TransactionDto {
  id: number;
  description: string;
  value: number; // Value no C#
  type: TransactionType; // TransactionType enum
  personId: number;
  categoryId: number;

  // opcionais, vindos de PersonDto / CategoryDto se a API mandar:
  person?: {
    id: number;
    name: string;
  };
  category?: {
    id: number;
    description: string;
    purpose: "Despesa" | "Receita" | "Ambas";
  };
}

export type Transaction = TransactionDto;

export interface CreateTransactionDto {
  description: string;
  value: number;
  type: TransactionType;
  personId: number;
  categoryId: number;
}

export interface UpdateTransactionDto {
  description: string;
  value: number;
  type: TransactionType;
  personId: number;
  categoryId: number;
}
