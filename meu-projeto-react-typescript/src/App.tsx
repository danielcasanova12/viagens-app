import ResponsiveAppBar from "./shared/components/responsiveAppBar/ResponsiveAppBar";
import { AppRoutes } from "./routes";
import { BrowserRouter } from "react-router-dom";
import { AppThemeProvider } from "./shared/contexts/ThemeContext"; 
import React from "react";

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
