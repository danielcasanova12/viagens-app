import ResponsiveAppBar from "./shared/components/responsiveAppBar/ResponsiveAppBar";
import { AppRoutes } from "./routes";
import { BrowserRouter } from "react-router-dom";
import { AppThemeProvider } from "./shared/contexts/ThemeContext"; 
import React from "react";
import { AuthProvider } from "./shared/contexts";
import { Login } from "./shared/components/login/Login";

import { UserProvider } from "./shared/contexts/UserContext"; // Certifique-se de importar o UserProvider

function App() {
	return (
		<AuthProvider>
			<UserProvider> {/* Adicione o UserProvider aqui */}
				<AppThemeProvider>
					<Login>
						<BrowserRouter>
							<ResponsiveAppBar/>
							<AppRoutes/>
						</BrowserRouter>
					</Login>
				</AppThemeProvider>
			</UserProvider>
		</AuthProvider>
	);
}

export default App;

