import { Api } from "../axios-config";

interface IAuth {
  accessToken: string;
}

const auth = async (email: string, password: string): Promise<IAuth | Error> => {
	try {
		// Update the API endpoint and parameters
		const { data } = await Api.get(`/users/login?email=${email}&password=${password}`);

		if (data) {
			console.log(data);
			return data;
			
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
