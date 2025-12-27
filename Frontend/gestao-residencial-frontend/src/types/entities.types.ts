

export interface CategoryFormProps {
  initialData: Category | null;
  loading: boolean;
  onSubmit: (payload: Omit<Category, "id">, id?: number) => Promise<void>;
  onCancel: () => void;
  
}

export interface Person {
  id: number;
  nome: string;
  idade: number;
}

export type CategoryPurpose = "despesa" | "receita" | "ambas";

export interface Category {
  id: number;
  descricao: string;
  finalidade: CategoryPurpose;
}

export type TransactionType = "despesa" | "receita";

export interface Transaction {
  id: number;
  descricao: string;
  valor: number;
  tipo: TransactionType;
  categoriaId: number;
  pessoaId: number;
}
