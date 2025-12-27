import { NavLink, Outlet } from "react-router-dom";
import styles from "./AppLayout.module.css";

export function AppLayout() {
  return (
    <div className={styles.appShell}>
      <header className={styles.header}>
        <div className={styles.logo}>Gastos Residenciais</div>

        <nav className={styles.nav}>
          <NavLink
            to="/pessoas"
            className={({ isActive }) =>
              isActive
                ? `${styles.navLink} ${styles.navLinkActive}`
                : styles.navLink
            }
          >
            Pessoas
          </NavLink>
          <NavLink
            to="/categorias"
            className={({ isActive }) =>
              isActive
                ? `${styles.navLink} ${styles.navLinkActive}`
                : styles.navLink
            }
          >
            Categorias
          </NavLink>
          <NavLink
            to="/transacoes"
            className={({ isActive }) =>
              isActive
                ? `${styles.navLink} ${styles.navLinkActive}`
                : styles.navLink
            }
          >
            Transações
          </NavLink>
        </nav>
      </header>

      <main className={styles.main}>
        <div className={styles.content}>
          <Outlet />
        </div>
      </main>
    </div>
  );
}
