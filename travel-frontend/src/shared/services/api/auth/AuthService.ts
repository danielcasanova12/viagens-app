import { IAuth } from "../../../Interfaces/Interfaces";
import { Api } from "../axios-config";


const auth = async (email: string, password: string): Promise<IAuth | Error> => {
	try {
		// Atualize o endpoint da API e os parâmetros
		const { data } = await Api.get(`/users/login?email=${email}&password=${password}`);

		if (data) {
			console.log(data);
			return {
				accessToken: data.accessToken,
				user: data.user, // Adicione esta linha
			};
		}

		return new Error("Erro no login.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro no login.");
	}
};

export const AuthService = {
	auth,
};