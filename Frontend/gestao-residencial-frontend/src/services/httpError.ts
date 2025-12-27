/** A ideia é extrair a mensagem enviada pelo backend e converte em Error para refletir ao usuário , como ele vai ser utilizado em todos os serviços, centralizar em uma função separada é a opção mais viável*/

export function buildError(error: unknown): Error {
  const maybeAxiosError = error as
    | {
        response?: {
          data?: unknown;
        };
      }
    | undefined;

  const data = maybeAxiosError?.response?.data;

  if (typeof data === "string") {
    return new Error(data);
  }

  if (data && typeof data === "object") {
    const obj = data as {
      message?: unknown;
      errors?: Record<string, string[]>;
    };

    if (typeof obj.message === "string") {
      return new Error(obj.message);
    }

    const errors = obj.errors;
    if (errors && typeof errors === "object") {
      const firstKey = Object.keys(errors)[0];
      const firstMsg = errors[firstKey]?.[0];
      if (firstMsg) return new Error(firstMsg);
    }
  }

  return new Error("Erro na requisição");
}
