interface AlertProps {
  type: "success" | "error";
  children: React.ReactNode;
}

export function Alert({ type, children }: AlertProps) {
  const style: React.CSSProperties =
    type === "success"
      ? {
          padding: "8px 12px",
          borderRadius: 4,
          margin: "8px 0",
          background: "#c6f6d5",
          color: "#22543d",
        }
      : {
          padding: "8px 12px",
          borderRadius: 4,
          margin: "8px 0",
          background: "#fed7d7",
          color: "#742a2a",
        };

  return <div style={style}>{children}</div>;
}
