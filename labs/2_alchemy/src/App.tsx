import DiscoveredElementsArea from "./components/DiscoveredElementsArea/DiscoveredElementsArea";
import ExperimentsArea from "./components/ExperimentsArea/ExperimentsArea";

function App() {
  return (
    <div className="container">
      <div className="board">
        <DiscoveredElementsArea />
        <ExperimentsArea />
      </div>
    </div>
  );
}

export default App;
