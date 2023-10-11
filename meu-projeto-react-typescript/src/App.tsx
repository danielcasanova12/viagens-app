import ResponsiveAppBar from './shared/components/responsilveAppBar/ResponsiveAppBar';
import { AppRoutes } from './routes';
import { BrowserRouter } from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
      <ResponsiveAppBar/>
      <AppRoutes/>
    </BrowserRouter>
  );
}

export default App;
