import { createContext, useCallback, useContext, useEffect, useMemo, useState } from "react";

import { AuthService } from "../services/api/auth/AuthService";

interface IUser {
	IdUser: number;
  username: string;
  email : string;
  password: string;
  image: string;
  typePermission: string;
  Reservations: []; // Add this line
}


interface IAuthContextData {
  user: IUser | null; // Adicione esta linha
  logout: () => void;
  isAuthenticated: boolean;
  login: (email: string, password: string) => Promise<string | void>;
}

const AuthContext = createContext({} as IAuthContextData);

const LOCAL_STORAGE_KEY__ACCESS_TOKEN = "APP_ACCESS_TOKEN";

interface IAuthProviderProps {
  children: React.ReactNode;
}
export const AuthProvider: React.FC<IAuthProviderProps> = ({ children }) => {
	const [accessToken, setAccessToken] = useState<string>();
	const [user, setUser] = useState<IUser | null>(null); // Adicione esta linha

	useEffect(() => {
		const accessToken = localStorage.getItem(LOCAL_STORAGE_KEY__ACCESS_TOKEN);

		try {
			if (accessToken) {
				console.log("accessToken before parsing:", accessToken); // Adicione este log
				setAccessToken(JSON.parse(accessToken));
			} else {
				setAccessToken(undefined);
			}
		} catch (error) {
			console.error("Error parsing accessToken:", error);
			setAccessToken(undefined);
		}
	}, []);


	const handleLogin = useCallback(async (email: string, password: string) => {
		const result = await AuthService.auth(email, password);
		if (result instanceof Error) {
			return result.message;
		} else {
			localStorage.setItem(LOCAL_STORAGE_KEY__ACCESS_TOKEN, JSON.stringify(result.accessToken));
			setAccessToken(result.accessToken);
			setUser(result.user); // Supondo que o resultado também inclua informações do usuário
		}
	}, []);

	const handleLogout = useCallback(() => {
		localStorage.removeItem(LOCAL_STORAGE_KEY__ACCESS_TOKEN);
		setAccessToken(undefined);
		setUser(null); // Adicione esta linha
	}, []);

	const isAuthenticated = useMemo(() => !!accessToken, [accessToken]);


	return (
		<AuthContext.Provider value={{ user, isAuthenticated, login: handleLogin, logout: handleLogout }}>
			{children}
		</AuthContext.Provider>
	);
};

export const useAuthContext = () => useContext(AuthContext);
