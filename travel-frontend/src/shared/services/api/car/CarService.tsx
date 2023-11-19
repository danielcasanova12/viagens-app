import { ICarRental } from "../../../Interfaces/Interfaces";
import { Environment } from "../../../environment/Environments";
import { Api } from "../axios-config";

type TCarRentalsCount = {
  carro: ICarRental[];
  contAllCars: number;
}

const getAllCarRentals = async (searchValue: string,pageNumber = 1): Promise<TCarRentalsCount[] | Error> => {
	try {
		const urlRelative = `/CarRental?pageNumber=${pageNumber}&pageSize=${Environment.LIMIT_DEFAULT}&searchValue=${searchValue}`;
		const { data } = await Api.get(urlRelative);
		console.log("data",data);
		if (data) {
			const result = [{
				carro: data.carro,
				contAllCars: data.contAllCars,
			}];
			console.log("result",result);
			return result;
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
