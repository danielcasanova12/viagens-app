import { createContext, useContext, useState } from "react";

interface IUser {
  IdUser: number;
  username: string;
  email : string;
  password: string;
  image: string;
  typePermission: string;
  Reservations: []; // Add this line
}

interface IUserContext {
  user: IUser | null;
  setUser: (user: IUser | null) => void;
}
interface IUserProvider{
  children: React.ReactNode
}
const UserContext = createContext<IUserContext | undefined>(undefined);

export const UserProvider: React.FC<IUserProvider> = ({ children }) => {
	const [user, setUser] = useState<IUser | null>(null);

	return (
		<UserContext.Provider value={{ user, setUser }}>
			{children}
		</UserContext.Provider>
	);
};

export const useUserContext = () => {
	const context = useContext(UserContext);
	if (!context) {
		throw new Error("useUserContext must be used within a UserProvider");
	}
	return context;
};