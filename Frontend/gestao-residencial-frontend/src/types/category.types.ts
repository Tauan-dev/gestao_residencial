// Enum do backend
export type CategoryPurpose = "Despesa" | "Receita" | "Ambas";

export interface CategoryDto {
  id: number;
  description: string;
  purpose: CategoryPurpose;
}

export type Category = CategoryDto;

export interface CreateCategoryDto {
  description: string;
  purpose: CategoryPurpose;
}

export interface UpdateCategoryDto {
  description: string;
  purpose: CategoryPurpose;
}

export interface CategoryFormData {
  description: string;
  purpose: CategoryPurpose;
}

export interface CategoryTotalsItem {
  categoryId: number;
  description: string;
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

export interface CategoryTotalsResponse {
  categories: CategoryTotalsItem[];
  total: {
    totalIncome: number;
    totalExpense: number;
    balance: number;
  };
}

export interface CategorySpentResult {
  categoryId: number;
  description: string;
  expense: number;
}
