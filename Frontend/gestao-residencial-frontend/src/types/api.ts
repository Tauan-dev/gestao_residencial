export type OperationStatus = "success" | "error" | "loading";

export interface OperationFeedback {
  status: OperationStatus;
  message?: string;
  error?: string;
}
