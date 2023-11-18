import { ICarRental } from "../../../Interfaces/Interfaces";
import { Environment } from "../../../environment/Environments";
import { Api } from "../axios-config";

type TCarRentalsCount = {
  data: ICarRental[];
  totalCount: number;
}

const getAllCarRentals = async (pageNumber = 1): Promise<TCarRentalsCount[] | Error> => {
	try {
		const urlRelative = `/CarRental?pageNumber=${pageNumber}&pageSize=${Environment.LIMIT_DEFAULT}`;
		const { data } = await Api.get(urlRelative);

		if (data) {
			return [{
				data: data.carRentals,
				totalCount: data.totalCarRentals,
			}];
		}

		return new Error("Erro ao listar os registros.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao listar os registros.");
	}
};

const getCarRentalById = async (id: number): Promise<ICarRental | Error> => {
	try {
		const urlRelative = `/CarRental/${id}`;
		const { data } = await Api.get(urlRelative);

		if (data) {
			return data;
		}

		return new Error("Erro ao buscar o aluguel de carro.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao buscar o aluguel de carro.");
	}
};

export const CarService = {
	getAllCarRentals,
	getCarRentalById,
};
