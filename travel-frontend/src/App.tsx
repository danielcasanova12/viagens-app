import ResponsiveAppBar from "./shared/components/responsiveAppBar/ResponsiveAppBar";
import { AppRoutes } from "./routes";
import { BrowserRouter } from "react-router-dom";
import { AppThemeProvider } from "./shared/contexts/ThemeContext"; 
import React from "react";
import { AuthProvider } from "./shared/contexts";
import { Login } from "./shared/components/login/Login";

function App() {
	return (
		<AuthProvider>
			<AppThemeProvider>
				<Login>
					<BrowserRouter>
						<ResponsiveAppBar/>
						<AppRoutes/>
					</BrowserRouter>
				</Login>
			</AppThemeProvider>
		</AuthProvider>
	);
}

export default App;
