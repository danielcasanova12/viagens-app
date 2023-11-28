import { createContext, useCallback, useContext, useEffect, useMemo, useState } from "react";

import { AuthService } from "../services/api/auth/AuthService";
import { IReservation, IUser } from "../Interfaces/Interfaces";
import { ReservationService } from "../services/api/reservation/ReservationService";



interface IAuthContextData {
  user: IUser | null; // Adicione esta linha
  logout: () => void;
	cart: IReservation[];
  isAuthenticated: boolean;
  login: (email: string, password: string) => Promise<string | void>;
	AddToCart: (reservation: IReservation, userId : number) => void;
	RemoveAllFromCart : (Number: number) => void;
	RemoveFromCart : (Number: number) => void;
}

const AuthContext = createContext({} as IAuthContextData);

const LOCAL_STORAGE_KEY__ACCESS_TOKEN = "APP_ACCESS_TOKEN";

interface IAuthProviderProps {
  children: React.ReactNode;
}

export const AuthProvider: React.FC<IAuthProviderProps> = ({ children }) => {
	const [cart, setCart] = useState<IReservation[]>([]);
	const [accessToken, setAccessToken] = useState<string>();
	const [user, setUser] = useState<IUser | null>(null); // Adicione esta linha
	const [TotalContReservations ,setTotalReservations] = useState(0);
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

	useEffect(() => {
		const user = localStorage.getItem("APP_USER");

		try {
			if (user) {
				setUser(JSON.parse(user));
			} else {
				setUser(null);
			}
		} catch (error) {
			console.error("Error parsing user:", error);
			setUser(null);
		}
	}, []);
	useEffect(() => {
		const cart = localStorage.getItem("APP_CART");
	
		try {
			if (cart) {
				setCart(JSON.parse(cart));
			} else {
				setCart([]);
			}
		} catch (error) {
			console.error("Error parsing cart:", error);
			setCart([]);
		}
	}, []);
	
	const handleAddToCart = useCallback(async (item: IReservation, userId: number) => {
		const newCart = [...cart, item];
		localStorage.setItem("APP_CART", JSON.stringify(newCart));
		setCart(newCart);
		console.log("item:", item);
	
		// Fetch the user's reservations
		const result = await ReservationService.getReservationsByUserId(userId);
		if (result && "reservations" in result && Array.isArray(result.reservations)) {
			setTotalReservations(result.totalReservations);
			console.log(TotalContReservations);
		} else {
			console.error(result);
		}
	}, [cart]);
	
	const handleAllRemoveFromCart = useCallback(() => {
		console.log("Removing the first item from the cart");
		const newCart = cart.slice(100); // This will create a new array without the first item
		console.log("Updated cart:", newCart);
		localStorage.setItem("APP_CART", JSON.stringify(newCart));
		setCart(newCart);
	}, [cart]);

	const handleRemoveFromCart = useCallback(() => {
		console.log("Removing the first item from the cart");
		const newCart = cart.slice(1); // This will create a new array without the first item
		console.log("Updated cart:", newCart);
		localStorage.setItem("APP_CART", JSON.stringify(newCart));
		setCart(newCart);
	}, [cart]);

	const handleLogin = useCallback(async (email: string, password: string) => {
		const result = await AuthService.auth(email, password);
		if (result instanceof Error) {
			return result.message;
		} else {
			localStorage.setItem(LOCAL_STORAGE_KEY__ACCESS_TOKEN, JSON.stringify(result.accessToken));
			localStorage.setItem("APP_USER", JSON.stringify(result.user)); // Adicione esta linha
			setAccessToken(result.accessToken);
			setUser(result.user);
		}
	}, []);

	const handleLogout = useCallback(() => {
		localStorage.removeItem(LOCAL_STORAGE_KEY__ACCESS_TOKEN);
		setAccessToken(undefined);
		setUser(null); // Adicione esta linha
	}, []);

	const isAuthenticated = useMemo(() => !!accessToken, [accessToken]);


	return (
		<AuthContext.Provider value={{ user, isAuthenticated, login: handleLogin, logout: handleLogout, cart, AddToCart: handleAddToCart, RemoveFromCart: handleRemoveFromCart,RemoveAllFromCart:handleAllRemoveFromCart}}>
			{children}
		</AuthContext.Provider>
	);
};

export const useAuthContext = () => useContext(AuthContext);
