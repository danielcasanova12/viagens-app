import axios from "axios";
import { Environment } from "../../../environment/Environments";
import { Api } from "../axios-config";
import { IUser, IUsers } from "../../../Interfaces/Interfaces";




const createUser = async (user: IUsers): Promise<IUser | Error> => {
	try {
		const urlRelative = "/users";
		console.log(urlRelative);
		console.log(user);
		const data = await Api.post(urlRelative, user); 
		console.log(data);
		
		return data.data;
	}catch (error) {
		if (axios.isAxiosError(error) && error.response) {
			console.error(error.response.data);
		}
		return new Error("Erro ao criar o usuário.");
	}

};

const getAllUsers = async (): Promise<IUser[] | Error> => {
	try {
		const pageNumber = 1; 
		const urlRelative = `/users?pageNumber=${pageNumber}&pageSize=${Environment.LIMIT_DEFAULT}`;
		const data = await Api.get(urlRelative); 
		return data.data;
	} catch (error) {
		console.error(error);
		return new Error("Erro ao listar os registros.");
	}
};

export const UserService = {
	getAllUsers,
	createUser,
};
