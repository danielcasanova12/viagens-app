 import ResponsiveAppBar from './shared/components/responsilveAppBar/ResponsiveAppBar';
import { AppRoutes } from './routes';
import { BrowserRouter } from 'react-router-dom';
import { AppThemeProvider } from './shared/contexts/ThemeContext';

function App() {
  return (
    <AppThemeProvider>
      <BrowserRouter>
        <ResponsiveAppBar/>
        <AppRoutes/>
      </BrowserRouter>
    </AppThemeProvider>
  );
}

export default App;
