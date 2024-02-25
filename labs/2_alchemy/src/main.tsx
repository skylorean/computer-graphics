import ReactDOM from "react-dom/client";
import App from "./App.tsx";
import "./index.scss";
import ElementsProvider from "./components/ElementsProvider/ElementsProvider.tsx";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <ElementsProvider>
    <App />
  </ElementsProvider>
);
