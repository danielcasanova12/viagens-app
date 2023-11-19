import { IFlight } from "../../../Interfaces/Interfaces";
import { Environment } from "../../../environment/Environments";
import { Api } from "../axios-config";

type TFlightsCount = {
  flighti: IFlight[];
  contAllFlight: number;
}

const getAllFlights = async (searchValue: string, pageNumber = 1): Promise<TFlightsCount[] | Error> => {
	try {
		const urlRelative = `/Flights?pageNumber=${pageNumber}&pageSize=${Environment.LIMIT_DEFAULT}&searchValue=${searchValue}`;
		const { data } = await Api.get(urlRelative);
		console.log("data",data);
		if (data) {
			const result = [{
				flighti: data.flighti,
				contAllFlight: data.contAllFlight,
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

const getFlightById = async (id: number): Promise<IFlight | Error> => {
	try {
		const urlRelative = `/Flights/${id}`;
		const { data } = await Api.get(urlRelative);

		if (data) {
			return data;
		}

		return new Error("Erro ao buscar o voo.");
	} catch (error) {
		console.error(error);
		return new Error((error as { message: string }).message || "Erro ao buscar o voo.");
	}
};

export const FlightService = {
	getAllFlights,
	getFlightById,
};