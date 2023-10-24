import { Environment } from "../../../environment";
import { Api } from "../axios-config";


interface IUser {
  IdUser: number;
  Username: string;
  Email : string;
  Password: string;
  Reservations :[];
  Image: string;
  TypePermission: number;
}

const gerAllUsers = async (page = 1, filter = ""): Promise<IUser | Error> => {
	try {
		const urlRelativa = `/pessoas?_page=${page}&_limit=${Environment.LIMIT_DEFAULT}&Username_like=${filter}`;

		const { data, headers } = await Api.get(urlRelativa);

		if (data) {
			return {
				data,
				totalCount: Number(headers["x-total-count"] || Environment.LIMIT_DEFAULT),
			};
		}

		return new Error("Erro ao listar os registros.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao listar os registros.");
	}
};

const gerAllUsersById = async () => {
	const response = await Api.get("/users");
	return response.data;
};

const create = async () => {
	const response = await Api.get("/users");
	return response.data;
};
const updateById = async () => {
	const response = await Api.get("/users");
	return response.data;
};
const deleteById = async () => {
	const response = await Api.get("/users");
	return response.data;
};



export const UserService = {
	gerAllUsers,
	gerAllUsersById,
	create,
	updateById,
	deleteById
};