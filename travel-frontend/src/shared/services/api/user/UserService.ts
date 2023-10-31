import { Environment } from "../../../environment/Environments";
import { Api } from "../axios-config";


export interface IUser {
  idUser: number;
  username: string;
  email : string;
  password: string;
  image: string;
  typePermission: number;
}

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
};