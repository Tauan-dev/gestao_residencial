export interface PersonDto {
  id: number;
  name: string;
  age: number;
}

export type Person = PersonDto;

export interface CreatePersonDto {
  name: string;
  age: number;
}

export interface UpdatePersonDto {
  name: string;
  age: number;
}

export interface PersonTotalsItem {
  personId: number;
  name: string;
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

export interface PersonTotalsResponse {
  people: PersonTotalsItem[];
  total: {
    totalIncome: number;
    totalExpense: number;
    balance: number;
  };
}

export interface PersonSpentResult {
  personId: number;
  name: string;
  expense: number;
}
